using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlayButton : MonoBehaviour
{
    public string levelResource;
    public bool useAdgate;

    public void Click()
    {
        GameData data = new GameData();
        data.selectedLevel = levelResource;
        GameManager.SetLevel(data);
        if (!useAdgate)
        {           
            SceneManager.LoadScene("Game");
        }
        AdController.Instance.ShowAd(() => { SceneManager.LoadScene("Game"); });


    }
}
