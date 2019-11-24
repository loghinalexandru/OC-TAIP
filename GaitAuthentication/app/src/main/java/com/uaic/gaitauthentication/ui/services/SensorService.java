package com.uaic.gaitauthentication.ui.services;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Build;
import android.os.Bundle;
import android.os.IBinder;
import android.os.PowerManager;
import android.util.Log;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.core.app.NotificationCompat;
import androidx.lifecycle.Observer;

import com.uaic.gaitauthentication.ui.MainActivity;
import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.data.UploadRepository;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.helpers.Result;

import java.io.File;
import java.io.IOException;
import java.io.OutputStreamWriter;

import static android.preference.PreferenceManager.getDefaultSharedPreferences;

public class SensorService extends Service implements SensorEventListener {

    private static final String CHANNEL_ID = "SensorsChannel";
    private SensorManager sensorManager;
    private Sensor accelerometer;
    private Sensor stepDetector;
    private UploadRepository uploadRepository;
    private PowerManager.WakeLock wakeLock;

    private final int stepsThreshold = 10;
    private final long stepPauseThreshold = 2000;

    private int stepConsecutiveCounter = 0;
    private long initialStepTimeStamp = 0;
    private long lastStepTimeStamp = 0;
    private boolean isMoving = false;
    private String profileName;
    private String currentFilePath;

    @Override
    public void onCreate() {
        super.onCreate();

        PowerManager powerManager = (PowerManager) getSystemService(POWER_SERVICE);
        wakeLock = powerManager.newWakeLock(PowerManager.PARTIAL_WAKE_LOCK, "SensorService::WakeLock");

        try {
            sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
            uploadRepository = UploadRepository.getInstance(getTokenFromPreferences());
            this.accelerometer = sensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);
            this.stepDetector = sensorManager.getDefaultSensor(Sensor.TYPE_STEP_DETECTOR);
        } catch (Exception e) {
            Toast.makeText(getApplicationContext(), e.getMessage(), Toast.LENGTH_SHORT).show();
        }

        attachSensor();

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)
            createNotificationChannel();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {

        if (intent.getAction().equals(Constants.STOP_SERVICE)) {
            stopForeground(true);
            stopSelf();
        }

        Bundle serviceData = intent.getExtras();
        profileName = serviceData.getString("profileName");

        wakeLock.acquire();

        uploadRepository.getResult().observeForever(new Observer<Result>() {
            @Override
            public void onChanged(Result result) {
                Toast.makeText(getApplicationContext(), result.toString(), Toast.LENGTH_LONG);
            }
        });

        Intent notificationIntent = new Intent(this, MainActivity.class);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 1, notificationIntent, 0);

        Notification notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("Creating Profile")
                .setContentText("Please do not close this notification")
                .setSmallIcon(R.drawable.ic_menu_gallery)
                .setContentIntent(pendingIntent)
                .build();

        startForeground(101, notification);

        return START_REDELIVER_INTENT;
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        detachSensor();
        wakeLock.release();
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    private void createNotificationChannel() {

        String channelName = "SensorService";
        NotificationChannel channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationManager.IMPORTANCE_NONE);
        channel.setLightColor(Color.BLUE);
        channel.setLockscreenVisibility(Notification.VISIBILITY_PRIVATE);
        NotificationManager manager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        manager.createNotificationChannel(channel);
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        long currentTimeStamp = java.lang.System.currentTimeMillis();

        if (isMoving && currentTimeStamp - lastStepTimeStamp > stepPauseThreshold) {
            isMoving = false;
            stepConsecutiveCounter = 0;
            uploadRepository.upload(new File(getApplicationContext().getFilesDir() + "/" + currentFilePath));
        }

        if (event.sensor == accelerometer && isMoving) {
            writeToFile(currentFilePath, java.lang.System.currentTimeMillis() + "," + event.values[0] + "," + event.values[1] + "," + event.values[2]);
        }

        if (event.sensor == stepDetector) {
            stepConsecutiveCounter += 1;
            lastStepTimeStamp = java.lang.System.currentTimeMillis();
            if (!isMoving && stepConsecutiveCounter > stepsThreshold) {
                isMoving = true;
                initialStepTimeStamp = java.lang.System.currentTimeMillis();
                currentFilePath = profileName + "_" + initialStepTimeStamp + ".csv";
            }
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    private void attachSensor() {
        sensorManager.registerListener(this, accelerometer, SensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(this, stepDetector, SensorManager.SENSOR_DELAY_FASTEST);
    }

    private void detachSensor() {
        sensorManager.unregisterListener(this);
    }

    private void writeToFile(String filePath, String data) {
        try {
            OutputStreamWriter outputStreamWriter = new OutputStreamWriter(openFileOutput(filePath, Context.MODE_APPEND));
            outputStreamWriter.write(data + "\n");
            outputStreamWriter.close();
        } catch (IOException e) {
            Log.e("Exception", "File write failed: " + e.toString());
        }
    }

    private String getTokenFromPreferences() throws Exception {
        SharedPreferences preferences = getDefaultSharedPreferences(getApplicationContext());
        String token = preferences.getString("token", null);

        if (token == null) {
            throw new Exception("Token not set!");
        }

        return token;
    }
}
