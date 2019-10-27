package com.uaic.gaitauthentication.ui.sensors;

import android.hardware.SensorManager;

import androidx.annotation.NonNull;
import androidx.lifecycle.ViewModel;
import androidx.lifecycle.ViewModelProvider;

import com.uaic.gaitauthentication.data.LoginDataSource;
import com.uaic.gaitauthentication.data.LoginRepository;
import com.uaic.gaitauthentication.ui.login.LoginViewModel;

public class SensorsViewModelFactory implements ViewModelProvider.Factory {

    private SensorManager sensorManager;

    public SensorsViewModelFactory(SensorManager sensorManager){
        this.sensorManager = sensorManager;
    }

    @NonNull
    @Override
    @SuppressWarnings("unchecked")
    public <T extends ViewModel> T create(@NonNull Class<T> modelClass) {
        if (modelClass.isAssignableFrom(SensorsViewModel.class)) {
            return (T) new SensorsViewModel(sensorManager);
        } else {
            throw new IllegalArgumentException("Unknown ViewModel class");
        }
    }
}
