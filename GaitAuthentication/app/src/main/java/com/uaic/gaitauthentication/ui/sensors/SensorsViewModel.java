package com.uaic.gaitauthentication.ui.sensors;

import android.app.Application;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.util.Log;

import androidx.lifecycle.LifecycleObserver;
import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

public class SensorsViewModel extends ViewModel implements SensorEventListener {

    private MutableLiveData<String> mText;
    private MutableLiveData<String> xAxis;
    private MutableLiveData<String> yAxis;
    private MutableLiveData<String> zAxis;
    private MutableLiveData<String> counter;

    private final SensorManager sensorManager;
    private final Sensor accelerometer;
    private final Sensor stepCounter;

    private final int stepsThreshold = 10;
    private final long stepPauseThreshold = 2000;

    private int stepConsecutiveCounter = 0;
    private long initialStepTimeStamp = 0;

    public SensorsViewModel(SensorManager sensorManager) {
        Init();

        this.sensorManager = sensorManager;
        this.accelerometer = sensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);
        this.stepCounter = sensorManager.getDefaultSensor(Sensor.TYPE_STEP_COUNTER);
    }

    private void Init() {
        mText = new MutableLiveData<>();
        xAxis = new MutableLiveData<>();
        yAxis = new MutableLiveData<>();
        zAxis = new MutableLiveData<>();
        counter = new MutableLiveData<>();

        mText.setValue("Accelerometer");
        xAxis.setValue("Empty");
        yAxis.setValue("Empty");
        zAxis.setValue("Empty");
        counter.setValue("Empty");
    }

    public void attachSensor() {
        sensorManager.registerListener(this, accelerometer, SensorManager.SENSOR_DELAY_FASTEST);
        sensorManager.registerListener(this, stepCounter, SensorManager.SENSOR_DELAY_FASTEST);
    }

    public void detachSensor() {
        sensorManager.unregisterListener(this);

    }

    public LiveData<String> getText() {
        return mText;
    }

    public LiveData<String> getxAxis() {
        return xAxis;
    }

    public LiveData<String> getyAxis() {
        return yAxis;
    }

    public LiveData<String> getzAxis() {
        return zAxis;
    }

    public LiveData<String> getCounter() {
        return counter;
    }

    @Override
    public void onSensorChanged(SensorEvent event) {

        long currentTimeStamp = java.lang.System.currentTimeMillis();

        if(currentTimeStamp - initialStepTimeStamp > stepPauseThreshold){
            stepConsecutiveCounter = 0;
        }

        if (event.sensor == accelerometer) {
            xAxis.postValue(Float.toString(event.values[0]));
            yAxis.postValue(Float.toString(event.values[1]));
            zAxis.postValue(Float.toString(event.values[2]));
        }

        if (event.sensor == stepCounter) {
            counter.postValue(Float.toString(event.values[0]));
            stepConsecutiveCounter += 1;
            initialStepTimeStamp = java.lang.System.currentTimeMillis();
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }
}