package com.uaic.gaitauthentication.ui.register;

import androidx.annotation.Nullable;

public class RegisterFormState {
    @Nullable
    private String emailError;
    @Nullable
    private String usernameError;
    @Nullable
    private String passwordError;
    private boolean isDataValid;

    RegisterFormState(){
        this.emailError = null;
        this.usernameError = null;
        this.passwordError = null;
        this.isDataValid = true;
    }

    @Nullable
    String getUsernameError() {
        return usernameError;
    }

    @Nullable
    String getPasswordError() {
        return passwordError;
    }

    @Nullable
    String getEmailError(){
        return emailError;
    }

    public void setEmailError(@Nullable String emailError) {
        this.emailError = emailError;
        this.isDataValid = false;
    }

    public void setUsernameError(@Nullable String usernameError) {
        this.usernameError = usernameError;
        this.isDataValid = false;
    }

    public void setPasswordError(@Nullable String passwordError) {
        this.passwordError = passwordError;
        this.isDataValid = false;
    }

    boolean isDataValid() {
        return isDataValid;
    }
}
