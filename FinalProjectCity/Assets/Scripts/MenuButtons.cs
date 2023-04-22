using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public TMPro.TMP_Text introText;
    public GameMode gameMode;

    private void Start()
    {
        switch (gameMode.mode)
        {
            case GameMode.GameModes.StartMenu:
                introText.text = "Everyone fled the city in fear\nFind and kill all soildiers to win";
                break;
            case GameMode.GameModes.PlayerWin:
                introText.text = "You win!";
                break;
            case GameMode.GameModes.PlayerLoose:
                introText.text = "You loose!";
                break;
            default:
                break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
