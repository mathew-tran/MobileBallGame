using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelProgression
{
    public int levelProgression = 1;
}

[Serializable]
public class RatingPrompt
{
    public int timesBeforePrompt = -3;
}


public class LevelSelectUI : MonoBehaviour
{
    public LevelProgression currentLevelProgression;
    public string world = "Forest Levels";
    public AudioSource levelSelectMusic;

    public static RatingPrompt LoadRatingPromptProgression()
    {
        RatingPrompt savedData = new RatingPrompt();
        savedData.timesBeforePrompt = PlayerPrefs.GetInt("LevelPrompt");
        return savedData;
    }

    public static void SaveRatingPromptProgression(RatingPrompt data)
    {
        PlayerPrefs.SetInt($"LevelPrompt", data.timesBeforePrompt);
        PlayerPrefs.Save();
    }

    public static LevelProgression LoadLevelProgression()
    {
        LevelProgression savedData = new LevelProgression();
        savedData.levelProgression = PlayerPrefs.GetInt("LevelProgression2");
        if(savedData.levelProgression == 0)
        {
            savedData.levelProgression = 1;
        }
        return savedData;
    }

    public static void SaveLevelProgression(LevelProgression data)
    {
        PlayerPrefs.SetInt($"LevelProgression2", data.levelProgression);
        PlayerPrefs.Save();
    }

    public GameObject LevelThumbnail;
    void Start()
    {
        currentLevelProgression = LoadLevelProgression();
        StartCoroutine(loadLevels());
    }

    public IEnumerator loadLevels()
    {
        AudioManager._instance.PlaySong(levelSelectMusic.clip);
        var levels = LevelManager._instance.levelDatas;

        if (levels == null)
        {
            var newLevel = new Level();
            newLevel.levelName = "Could not load asset";
            UpdateThumbnail(LevelThumbnail.GetComponentInChildren<LevelButton>(), newLevel);
        }

        var obj = LevelThumbnail;
        var level = levels[0].GetComponentInChildren<Level>();
        for (int i = 0; i < levels.Count; i++)
        {
            
            if (i != 0)
            {
                obj = Instantiate(LevelThumbnail, LevelThumbnail.transform.parent);
                level = levels[i].GetComponentInChildren<Level>();
            }

            UpdateThumbnail(obj.GetComponentInChildren<LevelButton>(), level);
            if (currentLevelProgression.levelProgression > level.level)
            {
                obj.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (currentLevelProgression.levelProgression == level.level)
            {
                obj.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                obj.transform.GetChild(3).gameObject.SetActive(true);
                obj.GetComponent<Button>().interactable = false;
                obj.GetComponentInChildren<Text>().text = $"Complete level {level.level - 1} to unlock";
            }
            obj.GetComponentsInChildren<Text>()[1].text = (i + 1).ToString();
        }
        yield return new WaitForSeconds(2.0f);
    }

    public void UpdateThumbnail(LevelButton button, Level level)
    {
        button.levelName = level.name;        
        button.GetComponentInChildren<Text>().text = level.levelName;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
