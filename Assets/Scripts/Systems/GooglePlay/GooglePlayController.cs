using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GooglePlayController : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        AuthenticateUser();
    }

    void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        text.text = "Initializing";
        PlayGamesPlatform.InitializeInstance(config);
        text.text = "Activating";
        PlayGamesPlatform.Activate();

        text.text = "Attempting to Log in";

        Social.localUser.Authenticate((bool success) =>
        {
            if(success == true)
            {
                text.text = "Logged in";
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                //SceneManager.LoadScene("MainMenu");
                text.text = "Unable to sign into Google Play! Please install Google Play Games and sign in via Google Play Games! Please reopen this application after these steps.";
            }
        });
    }

    public static bool IsAuthenticated()
    {
        return Social.localUser.authenticated;
    }
    /// <summary>
    /// newTime should be in milleseconds 66032 would be interpreted as 1:06.03.
    /// </summary>
    /// <param name="newTime"></param>
    public static void PostToLeaderboard(long newTime, string board)
    {
        if (!IsAuthenticated())
        {
            return;
        }
        Social.ReportScore(newTime, board, (bool success) =>
        {
            if(success)
            {
                Debug.Log("Posted new time to leaderboard");
            }
            else
            {
                Debug.LogError("Unable to post to new leaderboard");
            }
        });
    }

    public static void ShowLeaderboardUI(string board)
    {
        if (!IsAuthenticated())
        {
            return;
        }
        //GPGSIds.leaderboard_best_time
        PlayGamesPlatform.Instance.ShowLeaderboardUI(board);
    }
}
