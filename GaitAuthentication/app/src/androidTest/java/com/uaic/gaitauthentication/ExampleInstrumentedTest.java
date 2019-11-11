package com.uaic.gaitauthentication;

import androidx.test.ext.junit.runners.AndroidJUnit4;

import com.uaic.gaitauthentication.ui.login.LoginActivity;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;

/**
 * Instrumented test, which will execute on an Android device.
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
@RunWith(AndroidJUnit4.class)
public class ExampleInstrumentedTest {

    @Test
    public void isExpired_whenTokenIsNotExpired_returnsFalse() {
        //Arrange
        LoginActivity loginActivity = new LoginActivity();
        String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImEubG9naGluIiwianRpIjoiYTA4ZjFjN2MtNzNkNy00MzcxLTk0ZjYtYjEzZDlhN2ZhMDYxIiwic3ViIjoibG9naGluYWxleGFuZHJ1NjFAZ21haWwuY29tIiwiZXhwIjoxNjA1MDM0MjMwLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAifQ.Ks80SY5otkJR9uNgpolLJJSZk5TGZoxDJ8pDRKy0pUQ";

        //Act
        boolean result = loginActivity.isExpired(token);

        //Assert
        Assert.assertEquals(false , result);
    }

    @Test
    public void isExpired_whenTokenIsExpired_returnsTrue() {
        //Arrange
        LoginActivity loginActivity = new LoginActivity();
        String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImEubG9naGluIiwianRpIjoiNDY1NzRhMjctNzEyYS00NjRkLWFjNDctYzBmMjQyNmJlZGM3Iiwic3ViIjoibG9naGluYWxleGFuZHJ1NjFAZ21haWwuY29tIiwiZXhwIjoxNTQxOTYyNjY3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAifQ.zzhvPmXsw-W7mtWFZV4bqmwNejwFsy8Wg6hJ_9qtn-U";

        //Act
        boolean result = loginActivity.isExpired(token);

        //Assert
        Assert.assertEquals(true , result);
    }
}
