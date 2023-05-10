using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public TMPro.TMP_Text introText;
    public GameMode.GameModes gameMode;
    public float audioVolume;
    public AudioSource audio;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UpdateIntroText();
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnAudioClick()
    {
        if(audio.volume != 0)
        {
            audioVolume = audio.volume;
            audio.volume = 0;
        }
        else
        {
            audio.volume = audioVolume;
        }
    }

    public void UpdateIntroText()
    {
        var singelton = GameObject.FindObjectOfType<GameMode>();
        if (singelton)
        {
            gameMode = singelton.mode;
            switch (gameMode)
            {
                case GameMode.GameModes.StartMenu:
                    introText.text = "Everyone fled the city in fear\nFind and kill all soildiers to win";
                    break;
                case GameMode.GameModes.PlayerWin:
                    introText.text = "You win!";
                    Debug.Log("you win in UpdateIntroText");
                    break;
                case GameMode.GameModes.PlayerLoose:
                    introText.text = "You loose!";
                    break;
                default:
                    break;
            }
        }
    }
}
