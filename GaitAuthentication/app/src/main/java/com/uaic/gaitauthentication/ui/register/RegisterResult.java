package com.uaic.gaitauthentication.ui.register;

import androidx.annotation.Nullable;

public class RegisterResult {

    private Boolean success;
    @Nullable
    private String error;

    RegisterResult(@Nullable String error) {
        this.error = error;
    }

    RegisterResult(@Nullable Boolean success) {
        this.success = success;
    }

    @Nullable
    boolean getSuccess() {
        return success;
    }

    @Nullable
    String getError() {
        return error;
    }
}
