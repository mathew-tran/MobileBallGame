using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    public string placementId = "video";
    string gameId = "3157962";

    private bool testMode = false;

    public static int count = 0;
    public static int maxCount = 5;

    private static AdController _instance;

    public static AdController Instance { get { return _instance; } }

    Action adAction;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }

    public void ShowAd(Action action)
    {
        RatingPrompt prompt;
        prompt = LevelSelectUI.LoadRatingPromptProgression();
        prompt.timesBeforePrompt -= 1;

        Debug.Log(prompt.timesBeforePrompt + "test");

        if (prompt.timesBeforePrompt > -4)
        {
            LevelSelectUI.SaveRatingPromptProgression(prompt);
        }

        if (prompt.timesBeforePrompt == -3)
        {
            Application.OpenURL("market://details?id=" + Application.productName);
            action();
        }
        else
        {
            adAction = action;
            StartCoroutine(ShowAdWhenReady());
        }
        
    }

    private IEnumerator ShowAdWhenReady()
    {
        count++;
        if (count >= maxCount)
        {
            while (!Monetization.IsReady(placementId))
            {
                yield return new WaitForSeconds(0.25f);
            }

            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show(AdFinished);
            }
            else
            {
                Debug.LogError("ad could not be spawned");
            }
            count = 0;
        }
        else
        {
            adAction();
        }
    }

    void AdFinished(ShowResult result)
    {
        if(result == ShowResult.Finished)
        {

        }
        adAction();
    }
}
