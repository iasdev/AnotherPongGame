using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject panelGameModeButtons;
    public GameObject panelMainOptionsButtons;
    
    public void OnPlayClick()
    {
        panelGameModeButtons.SetActive(true);
        panelMainOptionsButtons.SetActive(false);
    }

    public void OnReturnMenuClick()
    {
        panelGameModeButtons.SetActive(false);
        panelMainOptionsButtons.SetActive(true);
        gameManager.OnMenu();
    }
    
    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnSelectGameMode(string maxScore)
    {
        gameManager.OnGameStart(maxScore);
    }
}
