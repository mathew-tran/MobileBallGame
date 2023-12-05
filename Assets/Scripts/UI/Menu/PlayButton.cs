using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string sceneName;
    public string originalText = "";
    public string doublePromptText = "";
    public int doublePromptTime = 3;
    public bool doublePrompt = false;
    private bool hasPrompted = false;
    public void Start()
    {
        originalText = GetComponentInChildren<Text>().text;
    }

    public void PlayAdGate()
    {
        if (doublePrompt && !hasPrompted)
        {
            Play();
        }
        else
        {
            AdController.Instance.ShowAd(Play);
        }
    }
    public void Play()
    {
        if (doublePrompt)
        {
            if (!hasPrompted)
            {
                hasPrompted = true;
                GetComponentInChildren<Text>().text = doublePromptText;
                StartCoroutine(WaitForXSeconds(doublePromptTime));
            }
            else
            {
                SceneManager.LoadScene(sceneName);
                AudioManager._instance.musicSource.pitch = 1.0f;
            }
        }
        else
        {
            SceneManager.LoadScene(sceneName);
            AudioManager._instance.musicSource.pitch = 1.0f;
        }
    }
    public IEnumerator WaitForXSeconds(int time)
    {
        yield return new WaitForSeconds(time);
        hasPrompted = false;
        GetComponentInChildren<Text>().text = originalText;

    }
}
