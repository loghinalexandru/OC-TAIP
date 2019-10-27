package com.uaic.gaitauthentication.data;

import android.os.Build;

import androidx.annotation.RequiresApi;

import com.uaic.gaitauthentication.data.model.LoggedInUser;
import com.uaic.gaitauthentication.data.model.LoginModel;

import java.util.concurrent.Future;

import okhttp3.Response;

/**
 * Class that requests authentication and user information from the remote data source and
 * maintains an in-memory cache of login status and user credentials information.
 */
public class LoginRepository {

    private static volatile LoginRepository instance;

    private LoginDataSource dataSource;

    // private constructor : singleton access
    private LoginRepository(LoginDataSource dataSource) {
        this.dataSource = dataSource;
    }

    public static LoginRepository getInstance(LoginDataSource dataSource) {
        if (instance == null) {
            instance = new LoginRepository(dataSource);
        }
        return instance;
    }

    @RequiresApi(api = Build.VERSION_CODES.N)
    public Future<Response> login(String username, String password) {
        return dataSource.login(new LoginModel(username, password));
    }
}
