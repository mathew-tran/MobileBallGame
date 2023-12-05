using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeLeaderboards : MonoBehaviour
{
    public string boardId = "";
    public void ClickUsingGameManager()
    {        
        GooglePlayController.ShowLeaderboardUI(FindObjectOfType<GameManager>().level.boardId);
    }
    public void ClickUsingId()
    {
        GooglePlayController.ShowLeaderboardUI(boardId);
    }

}
