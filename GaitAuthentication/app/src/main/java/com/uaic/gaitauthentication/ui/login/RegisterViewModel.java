package com.uaic.gaitauthentication.ui.login;

import android.util.Log;
import android.widget.Toast;

import androidx.lifecycle.ViewModel;

import com.uaic.gaitauthentication.data.RegisterRepository;
import com.uaic.gaitauthentication.data.Result;
import com.uaic.gaitauthentication.data.model.RegisterModel;

public class RegisterViewModel extends ViewModel {

    private RegisterRepository repository;

    public RegisterViewModel(RegisterRepository repository){
        this.repository = repository;
    }

    public void register(String username, String password, String email){
        
        Result result = repository.register(new RegisterModel(username, password, email));
    }
}
