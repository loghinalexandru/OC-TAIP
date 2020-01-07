package com.uaic.gaitauthentication.ui.profiles;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.Switch;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.ui.services.SensorService;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;

import static android.preference.PreferenceManager.getDefaultSharedPreferences;

public class ProfilesFragment extends Fragment {

    private Switch toggleProfile;
    private TextView profileTime;
    private Intent sensorService;
    private Button profileNameButton;
    private SharedPreferences preferences;
    private SharedPreferences.OnSharedPreferenceChangeListener listner;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_profiles, container, false);
        toggleProfile = root.findViewById(R.id.enable_profile);
        profileTime = root.findViewById(R.id.profile_time);
        profileNameButton = root.findViewById(R.id.profile_name);

        sensorService = new Intent(getContext(), SensorService.class);
        preferences = getDefaultSharedPreferences(getContext().getApplicationContext());

        setStorageRemoteServer();

        profileNameButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
                builder.setTitle("Profile Name");

                final EditText input = new EditText(getContext());
                builder.setView(input);
                builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        setProfileName(input.getText().toString());
                    }
                });
                builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.cancel();
                    }
                });

                builder.show();
            }
        });

        toggleProfile.setChecked(preferences.getBoolean("isEnabled", false));
        toggleProfile.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                SharedPreferences.Editor preferenceEditor = preferences.edit();
                preferenceEditor.putBoolean("isEnabled", isChecked);
                preferenceEditor.commit();

                Bundle serviceData = new Bundle();
                serviceData.putString("profileName", preferences.getString("username", null));
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

    private void setProfileName(String profileName) {
        SharedPreferences.Editor preferences = getDefaultSharedPreferences(getContext().getApplicationContext()).edit();
        preferences.putString("username", profileName);
        preferences.commit();
    }

    private void setStorageRemoteServer(){
        new Thread(new Runnable(){
            @Override
            public void run() {
                try{
                    String urlString = "https://raw.githubusercontent.com/loghinalexandru/OC-TAIP/master/remoteServers.txt";
                    URL url = new URL(urlString);
                    URLConnection conn = url.openConnection();
                    InputStream is = conn.getInputStream();
                    String result = new BufferedReader(new InputStreamReader(is)).readLine();
                    is.close();
                    Constants.setStorageServerBaseUrl(result);
                }
                catch (Exception ex)
                {
                    Log.d("HTTP CALL FAILURE", ex.toString());
                }
            }
        }).start();
    }
}