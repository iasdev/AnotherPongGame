using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject panelGameModeButtons;
    public GameObject panelMainOptionsButtons;
    public TextMeshProUGUI pauseGameButtonText;
    
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

    public void OnPauseGameClick()
    {
        pauseGameButtonText.text = pauseGameButtonText.text == "Continuar partida" ? "Pausar partida" : "Continuar partida";
        gameManager.OnPause();
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
