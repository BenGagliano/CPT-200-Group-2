using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public static class GoogleAuthenticator
{
    private static string ClientId = "139043050252-8v3oqtq65p7qptuqigctk3lt9p53s514.apps.googleusercontent.com";
    private static string ClientSecret = "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";

    private const int Port = 1234;
    private static readonly string RedirectUri = $"http://localhost:{Port}";

    private static readonly HttpCodeListener codeListener = new HttpCodeListener(Port);

    public static void GetAuthCode()
    {
        Application.OpenURL($"https://accounts.google.com/o/oauth2/v2/auth?client_id={ClientId}&redirect_uri={RedirectUri}&response_type=code&scope=email");

        codeListener.StartListening(code =>
        {
            ExchangeAuthCodeWithIdToken(code, idToken =>
            {
                FirebaseAuthHandler.SignInWithToken(idToken, "google.com");
            });

            codeListener.StopListening();
        });
    }


    public static void ExchangeAuthCodeWithIdToken(string code, Action<string> callback)
    {
        try
        {
            RestClient.Request(new RequestHelper
            {
                Method = "POST",
                Uri = "https://oauth2.googleapis.com/token",
                Params = new Dictionary<string, string>
                {
                    {"code", code},
                    {"client_id", ClientId},
                    {"client_secret", ClientSecret},
                    {"redirect_uri", RedirectUri},
                    {"grant_type", "authorization_code"}
                }
            }).Then(
                response =>
                {
                    var data =
                        StringSerializationAPI.Deserialize(typeof(GoogleIdTokenResponse), response.Text) as
                            GoogleIdTokenResponse;
                    callback(data.id_token);
                }).Catch(Debug.Log);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
