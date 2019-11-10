package com.uaic.gaitauthentication.ui.profiles;

import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.uaic.gaitauthentication.helpers.Profile;
import com.uaic.gaitauthentication.ui.services.SensorService;

import java.util.ArrayList;
import java.util.List;

public class ProfilesViewModel extends ViewModel {

    private final MutableLiveData<List<Profile>> profilesList;

    public ProfilesViewModel() {
        this.profilesList = new MutableLiveData<>();
        this.profilesList.setValue(new ArrayList<Profile>());
    }

    public MutableLiveData<List<Profile>> getProfileList() {
        return profilesList;
    }
}