package com.uaic.gaitauthentication;

import android.content.Context;
import android.content.SharedPreferences;

import com.uaic.gaitauthentication.ui.SettingsFragment;

import org.junit.Before;
import org.junit.Test;
import org.mockito.Mockito;

import static org.mockito.Matchers.anyString;


/**
 * Example local unit test, which will execute on the development machine (host).
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ExampleUnitTest {

    private Context context = Mockito.mock(Context.class);
    private SharedPreferences prefs = Mockito.mock(SharedPreferences.class);
    private SharedPreferences.Editor editor = Mockito.mock(SharedPreferences.Editor.class);

    @Before
    public void setup() {
        Mockito.when(prefs.getString(anyString(), anyString())).thenReturn("test-string");
        Mockito.when(prefs.edit()).thenReturn(editor);
        Mockito.when(editor.commit()).thenReturn(true);
    }

    @Test
    public void logoutUser_whenLogoutIsClicked_userIsLoggedOut() {
        //Arrange
        SettingsFragment settingsFragment = new SettingsFragment(context);

        //Act
        settingsFragment.logoutUser(prefs);

        //Assert
        Mockito.verify(editor).clear();
        Mockito.verify(editor).commit();
    }
}