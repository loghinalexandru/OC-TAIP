package com.uaic.gaitauthentication.ui.profiles;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.Switch;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.ui.services.SensorService;

import static android.preference.PreferenceManager.getDefaultSharedPreferences;

public class ProfilesFragment extends Fragment {

    private Switch toggleProfile;
    private TextView profileTime;
    private Intent sensorService;
    private String username;
    private SharedPreferences preferences;
    private SharedPreferences.OnSharedPreferenceChangeListener listner;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_profiles, container, false);
        toggleProfile = root.findViewById(R.id.enable_profile);
        profileTime = root.findViewById(R.id.profile_time);

        sensorService = new Intent(getContext(), SensorService.class);
        preferences = getDefaultSharedPreferences(getContext().getApplicationContext());

        username = preferences.getString("username", null);

        toggleProfile.setChecked(preferences.getBoolean("isEnabled", false));
        toggleProfile.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                SharedPreferences.Editor preferenceEditor = preferences.edit();
                preferenceEditor.putBoolean("isEnabled", isChecked);
                preferenceEditor.commit();

                Bundle serviceData = new Bundle();
                serviceData.putString("profileName", username);
                sensorService.putExtras(serviceData);

                if (isChecked) {
                    sensorService.setAction(Constants.START_SERVICE);
                    getContext().startService(sensorService);
                } else {
                    sensorService.setAction(Constants.STOP_SERVICE);
                    getContext().startService(sensorService);
                }
            }
        });

        listner = new SharedPreferences.OnSharedPreferenceChangeListener() {
            @Override
            public void onSharedPreferenceChanged(SharedPreferences sharedPreferences, String key) {
                setProfileTime();
            }
        };

        preferences.registerOnSharedPreferenceChangeListener(listner);

        setProfileTime();

        return root;
    }

    private void setProfileTime() {
        long elapsedTime = preferences.getLong("profileTime", 0);
        long days = (elapsedTime / (60 * 60 * 24 * 1000));
        long hours = ((elapsedTime / (1000 * 60 * 60)) % 24);
        long minutes = ((elapsedTime / (1000 * 60)) % 60);

        profileTime.setText(String.format("%dD %dH %dM", days, hours, minutes));
    }
}