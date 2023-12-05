using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData
{
    public float bestTime = 999.99f;
    public string levelName = "";    
}


public class Level : MonoBehaviour
{
    public string levelName;
    public string boardId = GPGSIds.leaderboard_green_forest__best_time;
    public int level = 0;
    public string world = "Forest Levels";
    public AudioSource music;
}
