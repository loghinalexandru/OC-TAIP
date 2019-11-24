package com.uaic.gaitauthentication.ui.profiles;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CompoundButton;
import android.widget.Switch;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.helpers.Constants;
import com.uaic.gaitauthentication.helpers.Profile;
import com.uaic.gaitauthentication.ui.services.SensorService;

import java.util.List;

public class AdapterProfile extends ArrayAdapter<Profile> {
    private static LayoutInflater inflater = null;
    private final List<Profile> profiles;
    private final Intent sensorService;

    public AdapterProfile(@NonNull Context context, int resource, @NonNull List<Profile> objects) {
        super(context, resource, objects);

        profiles = objects;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        sensorService = new Intent(getContext(), SensorService.class);
    }

    public static class ViewHolder {
        public TextView profileName;
        public Switch toggled;
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        View view = convertView;

        final ViewHolder holder;
        try {
            if (convertView == null) {

                view = inflater.inflate(R.layout.profile_list_item, null);
                holder = new ViewHolder();

                holder.profileName = view.findViewById(R.id.profile_name);
                holder.toggled = view.findViewById(R.id.profile_switch);

                view.setTag(holder);

            } else {
                holder = (ViewHolder) view.getTag();
            }

            holder.profileName.setText(profiles.get(position).getProfileName());
            holder.toggled.setChecked(profiles.get(position).getToggled());

            holder.toggled.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                @Override
                public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                    buttonView.setChecked(isChecked);

                    if (isChecked) {
                        Bundle serviceData = new Bundle();
                        serviceData.putString("profileName", holder.profileName.getText().toString());
                        sensorService.setAction(Constants.START_SERVICE);
                        sensorService.putExtras(serviceData);
                        getContext().startService(sensorService);
                    } else {
                        sensorService.setAction(Constants.STOP_SERVICE);
                        getContext().startService(sensorService);
                    }
                }
            });

        } catch (Exception e) {
            Log.d("Exception", e.getMessage());

        }
        return view;
    }
}
