package com.uaic.gaitauthentication.ui.profiles;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.helpers.Profile;

import java.util.List;

public class ProfilesFragment extends Fragment {

    private ProfilesViewModel profilesViewModel;
    private ListView profilesList;
    private EditText dialogInput;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        profilesViewModel =
                ViewModelProviders.of(this).get(ProfilesViewModel.class);
        View root = inflater.inflate(R.layout.fragment_profiles, container, false);

        profilesList = root.findViewById(R.id.profiles);
        TextView emptyListText = root.findViewById(R.id.empty_list_view);
        profilesList.setEmptyView(emptyListText);

        Button createProfile = root.findViewById(R.id.add_profile);

        createProfile.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showProfileDialog();
            }
        });

        profilesViewModel.getProfileList().observe(getActivity(), new Observer<List<Profile>>() {
            @Override
            public void onChanged(List<Profile> profiles) {
                ArrayAdapter profileAdapter = new AdapterProfile(getContext(), R.layout.profile_list_item, profiles);
                profilesList.setAdapter(profileAdapter);
                profileAdapter.notifyDataSetChanged();
            }
        });

        return root;
    }

    private void showProfileDialog() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        TextWatcher textWatcher = new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                List<Profile> profiles = profilesViewModel.getProfileList().getValue();
                if(!isUnique(s.toString(), profiles)){
                    dialogInput.setError("Duplicate profile name!");
                }
            }
        };

        builder.setTitle(getString(R.string.new_profile));

        dialogInput = new EditText(getContext());
        dialogInput.setInputType(InputType.TYPE_CLASS_TEXT);
        dialogInput.addTextChangedListener(textWatcher);

        builder.setView(dialogInput);

        builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                addProfileEntry(dialogInput.getText().toString());
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

    private void addProfileEntry(String profileName) {
        List<Profile> profiles = profilesViewModel.getProfileList().getValue();
        if (isUnique(profileName, profiles)) {
            profiles.add(new Profile(profileName));
            profilesViewModel.getProfileList().setValue(profiles);
        } else {
            dialogInput.setError("Duplicate profile name!");
        }
    }

    private boolean isUnique(String profileName, List<Profile> profiles) {
        for (int i = 0; i < profiles.size(); ++i) {
            if (profiles.get(i).profileName.equals(profileName)) {
                return false;
            }
        }

        return true;
    }
}