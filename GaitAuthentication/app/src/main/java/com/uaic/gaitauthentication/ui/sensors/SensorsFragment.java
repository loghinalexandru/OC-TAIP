package com.uaic.gaitauthentication.ui.sensors;

import android.content.Context;
import android.content.SharedPreferences;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.uaic.gaitauthentication.R;

import static android.preference.PreferenceManager.getDefaultSharedPreferences;

public class SensorsFragment extends Fragment {

    private SensorsViewModel sensorsViewModel;
    private SensorManager sensorManager;
    private String token;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        sensorManager = (SensorManager) getActivity().getSystemService(Context.SENSOR_SERVICE);
        sensorsViewModel =
                ViewModelProviders.of(this, new SensorsViewModelFactory(sensorManager)).get(SensorsViewModel.class);
        View root = inflater.inflate(R.layout.fragment_tools, container, false);

        final TextView textView = root.findViewById(R.id.text_tools);
        final TextView xAxisAccelerometer = root.findViewById(R.id.accelerometer_x);
        final TextView yAxisAccelerometer = root.findViewById(R.id.accelerometer_y);
        final TextView zAxisAccelerometer = root.findViewById(R.id.accelerometer_z);
        final TextView stepCounter = root.findViewById(R.id.step_counter_value);

        try {
            setTokenFromPreferences();
        } catch (Exception e) {
            e.printStackTrace();
        }

        sensorsViewModel.getText().observe(this, new Observer<String>() {
            @Override
            public void onChanged(@Nullable String s) {
                textView.setText(s);
            }
        });

        sensorsViewModel.getxAxis().observe(this, new Observer<String>() {
            @Override
            public void onChanged(String s) {
                xAxisAccelerometer.setText(s);
            }
        });

        sensorsViewModel.getyAxis().observe(this, new Observer<String>() {
            @Override
            public void onChanged(String s) {
                yAxisAccelerometer.setText(s);
            }
        });

        sensorsViewModel.getzAxis().observe(this, new Observer<String>() {
            @Override
            public void onChanged(String s) {
                zAxisAccelerometer.setText(s);
            }
        });

        sensorsViewModel.getCounter().observe(this, new Observer<String>() {
            @Override
            public void onChanged(String s) {
                stepCounter.setText(s);
            }
        });

        return root;
    }

    @Override
    public void onResume() {
        super.onResume();
        sensorsViewModel.attachSensor();
    }

    @Override
    public void onPause() {
        super.onPause();
        sensorsViewModel.detachSensor();
    }

    private void setTokenFromPreferences() throws Exception {
        SharedPreferences preferences = getDefaultSharedPreferences(getContext());
        token = preferences.getString("token", null);

        Toast.makeText(getContext(), token, Toast.LENGTH_LONG).show();

        if (token == null) {
            throw new Exception("Token not set!");
        }
    }
}