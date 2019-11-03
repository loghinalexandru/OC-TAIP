package com.uaic.gaitauthentication.helpers;

public class Constants {
    public static final String authorityBaseUrl = "http://192.168.0.10:45456";
    public static final String registerEndpoint = authorityBaseUrl + "/Authority/register";
    public static final String loginEndpoint = authorityBaseUrl + "/Authority/token";

    public static final String storageServerBaseUrl = "http://192.168.0.10:45458";
    public static final String uploadEndpoint = storageServerBaseUrl + "/api/storage/upload";
    public static final String getDataEndpoint = storageServerBaseUrl + "/api/storage/user";
}
