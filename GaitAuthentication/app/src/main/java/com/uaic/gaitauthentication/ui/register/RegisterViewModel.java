package com.uaic.gaitauthentication.ui.register;

import androidx.lifecycle.ViewModel;

import com.uaic.gaitauthentication.data.RegisterRepository;
import com.uaic.gaitauthentication.helpers.Result;
import com.uaic.gaitauthentication.data.model.RegisterModel;

import java.util.concurrent.Future;

import okhttp3.Response;

public class RegisterViewModel extends ViewModel {

    private RegisterRepository repository;
    private Result result;

    public RegisterViewModel(RegisterRepository repository){
        this.repository = repository;
    }

    public Future<Response> register(String username, String password, String email){
        return repository.register(new RegisterModel(username, password, email));
    }
}
