package com.uaic.gaitauthentication.ui.services;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Build;
import android.os.IBinder;
import android.util.Log;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.core.app.NotificationCompat;

import com.uaic.gaitauthentication.MainActivity;
import com.uaic.gaitauthentication.R;

import java.io.IOException;
import java.io.OutputStreamWriter;

public class SensorService extends Service implements SensorEventListener {

    private static final String CHANNEL_ID = "SensorsChannel";
    private SensorManager sensorManager;
    private Sensor accelerometer;
    private Sensor stepCounter;

    private final int stepsThreshold = 10;
    private final long stepPauseThreshold = 2000;

    private int stepConsecutiveCounter = 0;
    private long initialStepTimeStamp = 0;

    @Override
    public void onCreate() {
        super.onCreate();
        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        this.accelerometer = sensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);
        this.stepCounter = sensorManager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER);

        attachSensor();

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)
            createNotificationChannel();
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {

        Intent notificationIntent = new Intent(this, MainActivity.class);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 1, notificationIntent, 0);

        Notification notification = new NotificationCompat.Builder(this, CHANNEL_ID)
                .setContentTitle("Creating Profile")
                .setContentText("Please do not close the app")
                .setSmallIcon(R.drawable.ic_menu_camera)
                .setContentIntent(pendingIntent)
                .build();

        startForeground(101, notification);

        return START_STICKY;
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        detachSensor();
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    private void createNotificationChannel() {

        String channelName = "My Background Service";
        NotificationChannel channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationManager.IMPORTANCE_NONE);
        channel.setLightColor(Color.BLUE);
        channel.setLockscreenVisibility(Notification.VISIBILITY_PRIVATE);
        NotificationManager manager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        manager.createNotificationChannel(channel);
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        long currentTimeStamp = java.lang.System.currentTimeMillis();

        if (currentTimeStamp - initialStepTimeStamp > stepPauseThreshold) {
            stepConsecutiveCounter = 0;
        }

        if (event.sensor == accelerometer && stepConsecutiveCounter > stepsThreshold) {
            writeToFile(java.lang.System.currentTimeMillis() + "," + event.values[0] + "," + event.values[1] + "," + event.values[2]);
        }

        if (event.sensor == stepCounter) {
            stepConsecutiveCounter += 1;
            initialStepTimeStamp = java.lang.System.currentTimeMillis();
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    private void attachSensor() {
        sensorManager.registerListener(this, accelerometer, SensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(this, stepCounter, SensorManager.SENSOR_DELAY_FASTEST);
    }

    private void detachSensor() {
        sensorManager.unregisterListener(this);
    }

    private void writeToFile(String data) {
        try {
            OutputStreamWriter outputStreamWriter = new OutputStreamWriter(openFileOutput("data.txt", Context.MODE_APPEND));
            outputStreamWriter.write(data + "\n");
            outputStreamWriter.close();
        } catch (IOException e) {
            Log.e("Exception", "File write failed: " + e.toString());
        }
    }
}
