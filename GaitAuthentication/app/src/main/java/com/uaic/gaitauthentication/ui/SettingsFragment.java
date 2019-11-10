package com.uaic.gaitauthentication.ui;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.preference.Preference;
import androidx.preference.PreferenceFragmentCompat;
import androidx.preference.PreferenceManager;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.ui.login.LoginActivity;

public class SettingsFragment extends PreferenceFragmentCompat {

    private final Context context;

    public SettingsFragment(Context context){
        this.context = context;
    }

    @Override
    public void onCreatePreferences(Bundle savedInstanceState, String rootKey) {
        setPreferencesFromResource(R.xml.root_preferences, rootKey);
    }

    @Override
    public boolean onPreferenceTreeClick(Preference preference) {
        switch (preference.getKey()){
            case "logout":
                logoutUser(PreferenceManager.getDefaultSharedPreferences(context));
                redirectToLogin();
                return true;
        }
        return super.onPreferenceTreeClick(preference);
    }

    public void logoutUser(SharedPreferences preferences){
        SharedPreferences.Editor editor = preferences.edit();
        editor.clear();
        editor.commit();
    }

    public void redirectToLogin(){
        Intent loginActivity = new Intent(context, LoginActivity.class);
        startActivity(loginActivity);
    }
}
