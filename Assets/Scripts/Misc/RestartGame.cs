using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{    
    public void Click()
    {
        AdController.Instance.ShowAd(Restart);
    }
    public void Restart()
    {
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
