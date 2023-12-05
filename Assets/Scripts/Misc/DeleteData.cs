using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeleteData : MonoBehaviour
{
    public bool hasPrompted = false;
    public string originalText = "";
    public string promptedText = "";
    public int waitTime = 3;

    public void Start()
    {
        originalText = GetComponentInChildren<Text>().text;
    }
    public void DeleteGameData()
    {
        if (!hasPrompted)
        {
            hasPrompted = true;
            GetComponentInChildren<Text>().text = promptedText;
            StartCoroutine(WaitForXSeconds(waitTime));
        }
        else
        {
            var levels = Resources.LoadAll<GameObject>("Scenes/Forest Levels");

            for(int i = 0; i < levels.Length; i++)
            {
                var level = levels[i].GetComponent<Level>();
                LevelData data = new LevelData();
                GameManager.SaveLevel(data, level);
            }

            LevelProgression levelProgress = new LevelProgression();
            LevelSelectUI.SaveLevelProgression(levelProgress);

            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("MainMenu");

           
        }
    
    }

    public IEnumerator WaitForXSeconds(int time)
    {
        yield return new WaitForSeconds(time);
        hasPrompted = false;
        GetComponentInChildren<Text>().text = originalText;

    }

}
