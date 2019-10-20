package com.uaic.gaitauthentication.ui.login;

import androidx.lifecycle.ViewModel;

import com.uaic.gaitauthentication.data.RegisterRepository;

public class RegisterViewModel extends ViewModel {

    private RegisterRepository repository;

    public RegisterViewModel(RegisterRepository repository){
        this.repository = repository;
    }
}
