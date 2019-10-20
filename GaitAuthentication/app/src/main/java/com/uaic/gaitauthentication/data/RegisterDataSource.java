package com.uaic.gaitauthentication.data;

import com.uaic.gaitauthentication.data.model.LoggedInUser;
import com.uaic.gaitauthentication.data.model.RegisterModel;

import java.io.IOException;

public class RegisterDataSource {

    public Result register(RegisterModel model) {

        try {
            // TODO: handle loggedInUser authentication
            LoggedInUser fakeUser =
                    new LoggedInUser(
                            java.util.UUID.randomUUID().toString(),
                            "Jane Doe");
            return new Result.Success();
        } catch (Exception e) {
            return new Result.Error(new IOException("Error when signing up", e));
        }
    }
}
