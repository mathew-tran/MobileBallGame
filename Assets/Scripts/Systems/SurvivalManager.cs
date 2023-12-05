using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurvivalManager : MonoBehaviour
{
    public int levelAmount = 3;
    public int[] indexes;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
      
        DontDestroyOnLoad(this);
        var loadedlevels = Resources.LoadAll<GameObject>("Scenes/Forest Levels");

        indexes = new int[levelAmount];

        List<int> levelIndexes = new List<int>();
        for (int i = 0; i < loadedlevels.Length; ++i)
        {
            levelIndexes.Add(i);
        }

        levelIndexes = levelIndexes.OrderBy(x => UnityEngine.Random.value).ToList();
        for (int i = 0; i < levelAmount; ++i)
        {
            indexes[i] = levelIndexes[i];
        }
    }
    
    public void PlayLevel()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
