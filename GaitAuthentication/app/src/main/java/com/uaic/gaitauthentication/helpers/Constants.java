package com.uaic.gaitauthentication.helpers;

public class Constants {
    public static final String authorityBaseUrl = "http://192.168.43.152:45456";
    public static final String registerEndpoint = authorityBaseUrl + "/Authority/register";
    public static final String loginEndpoint = authorityBaseUrl + "/Authority/token";

    public static final String storageServerBaseUrl = "http://267e5e0f.ngrok.io";
    public static final String uploadEndpoint = storageServerBaseUrl + "/api/storage/upload";
    public static final String getDataEndpoint = storageServerBaseUrl + "/api/storage/user";

    public static final String START_SERVICE = "START";
    public static final String STOP_SERVICE = "STOP";
}
