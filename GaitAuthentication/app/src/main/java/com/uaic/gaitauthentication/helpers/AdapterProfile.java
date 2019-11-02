package com.uaic.gaitauthentication.helpers;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Switch;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.uaic.gaitauthentication.R;

import java.util.List;

public class AdapterProfile extends ArrayAdapter<Profile> {
    private static LayoutInflater inflater = null;
    private final List<Profile> profiles;

    public AdapterProfile(@NonNull Context context, int resource, @NonNull List<Profile> objects) {
        super(context, resource, objects);

        profiles = objects;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
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
                holder.toggled =  view.findViewById(R.id.profile_switch);

                view.setTag(holder);

            } else {
                holder = (ViewHolder) view.getTag();
            }

            holder.profileName.setText(profiles.get(position).getProfileName());
            holder.toggled.setChecked(profiles.get(position).getToggled());


        } catch (Exception e) {


        }

        return view;
    }
}
