using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;
    private static bool spawned = false;
    public int index;

    public List<Level> levelDatas;
    private void Awake()
    {
        if (!spawned)
        {
            spawned = true;
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);

            //DontDestroyOnLoad(_instance.transform.GetChild(0));
            //DontDestroyOnLoad(_instance.transform.GetChild(1));
        }
    }
    public void Start()
    {
        var levels = Resources.LoadAll<GameObject>($"Scenes");

        levels = levels.OrderBy(x => x.GetComponent<Level>().level).ToArray();

        foreach (var levelName in levels)
        {
            levelDatas.Add(levelName.GetComponent<Level>());
        }
    }
}
