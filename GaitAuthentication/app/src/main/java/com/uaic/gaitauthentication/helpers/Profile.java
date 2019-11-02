package com.uaic.gaitauthentication.helpers;

public class Profile {
    public String profileName;
    public Boolean toggled;

    public Profile(String profileName){
        this.profileName = profileName;
        toggled = false;
    }

    public String getProfileName() {
        return profileName;
    }

    public void setProfileName(String profileName) {
        this.profileName = profileName;
    }

    public Boolean getToggled() {
        return toggled;
    }

    public void setToggled(Boolean toggled) {
        this.toggled = toggled;
    }
}
