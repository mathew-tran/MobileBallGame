using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string levelName;
    public GameObject levelPopup;
    public void Click()
    {
        levelPopup.SetActive(true);
        GameObject.Find("Level Name").GetComponent<Text>().text = this.levelName;
        GameObject.Find("Play Button").GetComponent<LevelPlayButton>().levelResource = $"Scenes/Forest Levels/{this.levelName}";

        var level = Resources.Load<GameObject>($"Scenes/Forest Levels/{this.levelName}").GetComponentInChildren<Level>();
        var levelName = level.levelName;
        if(levelName != null)
        {            
            GameObject.Find("Your Time").GetComponentInChildren<Text>().text = "Your Best Time: " + String.Format(GameTimeUI.format, GameManager.LoadData(level).bestTime) + "s";

            if(GameObject.Find("See Leaderboards") != null)
            {
                GameObject.Find("See Leaderboards").GetComponent<SeeLeaderboards>().boardId = level.boardId;
            }
        }
        else
        {
            GameObject.Find("Your Time").GetComponentInChildren<Text>().text = "Error occurred";
        }
        
        GameObject.Find("World Time");
    }
}
