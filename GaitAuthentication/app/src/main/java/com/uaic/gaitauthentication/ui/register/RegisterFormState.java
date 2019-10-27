package com.uaic.gaitauthentication.ui.register;

import androidx.annotation.Nullable;

public class RegisterFormState {
    @Nullable
    private Integer emailError;
    @Nullable
    private Integer usernameError;
    @Nullable
    private Integer passwordError;
    private boolean isDataValid;

    RegisterFormState(){
        this.emailError = null;
        this.usernameError = null;
        this.passwordError = null;
        this.isDataValid = true;
    }

    @Nullable
    Integer getUsernameError() {
        return usernameError;
    }

    @Nullable
    Integer getPasswordError() {
        return passwordError;
    }

    @Nullable
    Integer getEmailError(){
        return emailError;
    }

    public void setEmailError(@Nullable Integer emailError) {
        this.emailError = emailError;
        this.isDataValid = false;
    }

    public void setUsernameError(@Nullable Integer usernameError) {
        this.usernameError = usernameError;
        this.isDataValid = false;
    }

    public void setPasswordError(@Nullable Integer passwordError) {
        this.passwordError = passwordError;
        this.isDataValid = false;
    }

    boolean isDataValid() {
        return isDataValid;
    }
}
