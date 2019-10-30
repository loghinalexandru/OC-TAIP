package com.uaic.gaitauthentication.ui.register;


import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModel;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.uaic.gaitauthentication.data.RegisterRepository;
import com.uaic.gaitauthentication.data.model.RegisterModel;
import com.uaic.gaitauthentication.helpers.Result;
import com.uaic.gaitauthentication.helpers.ValidationErrors;

public class RegisterViewModel extends ViewModel {

    private RegisterRepository repository;
    private MutableLiveData<RegisterFormState> registerFormState;
    private LiveData<Result> result;

    public RegisterViewModel(RegisterRepository repository) {
        this.registerFormState = new MutableLiveData<>();
        this.repository = repository;
        this.result = repository.getResult();

        result.observeForever(new Observer<Result>() {
            @Override
            public void onChanged(Result result) {
                RegisterFormState formState = new RegisterFormState();
                Gson jsonHelper = new Gson();

                if (result instanceof Result.Error) {
                    JsonObject errorMessages = jsonHelper.fromJson(((Result.Error) result).getError().getMessage(), JsonObject.class).getAsJsonObject("ErrorMessages");

                    if (errorMessages.has("Email")) {
                        formState.setEmailError(errorMessages.get("Email").toString());
                    }

                    if (errorMessages.has("Username")) {
                        formState.setUsernameError(errorMessages.get("Username").toString());
                    }

                    if (errorMessages.has("Password")) {
                        formState.setPasswordError(errorMessages.get("Password").toString());
                    }

                    registerFormState.setValue(formState);
                }
            }
        });
    }

    public void register(String username, String password, String email) {
        repository.register(new RegisterModel(username, password, email));
    }

    public MutableLiveData<RegisterFormState> getFormState() {
        return registerFormState;
    }

    public void registerDataChanged(String email, String username, String password) {
        RegisterFormState formState = new RegisterFormState();

        if (isEmailValid(email) == false) {
            formState.setEmailError(ValidationErrors.invalidEmail);
        }
        if (isUsernameValid(username) == false) {
            formState.setUsernameError(ValidationErrors.invalidUsername);
        }
        if (isPasswordValid(password) == false) {
            formState.setPasswordError(ValidationErrors.invalidPassowrd);
        }

        registerFormState.setValue(formState);
    }

    public boolean isEmailValid(String email) {
        return email.contains("@") && email.length() > 4;
    }

    public boolean isPasswordValid(String email) {
        return email.length() >= 8;
    }

    public boolean isUsernameValid(String username) {
        return !username.isEmpty();
    }

    public LiveData<Result> getResult() {
        return result;
    }
}
