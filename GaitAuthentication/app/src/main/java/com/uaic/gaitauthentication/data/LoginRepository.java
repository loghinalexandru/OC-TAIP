package com.uaic.gaitauthentication.data;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.uaic.gaitauthentication.data.model.LoginModel;
import com.uaic.gaitauthentication.helpers.Result;

/**
 * Class that requests authentication and user information from the remote data source and
 * maintains an in-memory cache of login status and user credentials information.
 */
public class LoginRepository {

    private static volatile LoginRepository instance;

    private LoginDataSource dataSource;
    private MutableLiveData<Result> result;

    // private constructor : singleton access
    private LoginRepository() {
        this.result = new MutableLiveData<>();
        this.dataSource = new LoginDataSource(result);
    }

    public static LoginRepository getInstance() {
        if (instance == null) {
            instance = new LoginRepository();
        }
        return instance;
    }

    public void login(String username, String password) {
        dataSource.login(new LoginModel(username, password));
    }

    public LiveData<Result> getResult() {
        return result;
    }
}
