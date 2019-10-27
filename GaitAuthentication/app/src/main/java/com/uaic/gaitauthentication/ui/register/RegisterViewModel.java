package com.uaic.gaitauthentication.ui.register;

import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.uaic.gaitauthentication.R;
import com.uaic.gaitauthentication.data.RegisterRepository;
import com.uaic.gaitauthentication.data.model.RegisterModel;

import java.util.concurrent.Future;

import okhttp3.Response;

public class RegisterViewModel extends ViewModel {

    private RegisterRepository repository;
    private MutableLiveData<RegisterFormState> registerFormState;

    public RegisterViewModel(RegisterRepository repository){
        registerFormState = new MutableLiveData<>();
        this.repository = repository;
    }

    public Future<Response> register(String username, String password, String email){
        return repository.register(new RegisterModel(username, password, email));
    }

    public MutableLiveData<RegisterFormState> getFormState() {
        return registerFormState;
    }

    public void registerDataChanged(String email, String username, String password){
        RegisterFormState formState = new RegisterFormState();

        if(isEmailValid(email) == false){
            formState.setEmailError(R.string.invalid_email);
        }
        if(isUsernameValid(username) == false){
            formState.setUsernameError(R.string.invalid_username);
        }
        if(isPasswordValid(password) == false){
            formState.setPasswordError(R.string.invalid_password);
        }

        registerFormState.setValue(formState);
    }

    public boolean isEmailValid(String email){
        return email.contains("@") && email.length() > 4;
    }

    public boolean isPasswordValid(String email){
        return email.length() >= 8;
    }

    public boolean isUsernameValid(String username){
        return !username.isEmpty();
    }
}
