using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public enum GameModes
    {
        StartMenu,
        PlayerWin,
        PlayerLoose
    }
    
    public GameModes mode;
    private int deadSoldiers = 0;
    public int DeadSoldiers
    {
        get { return deadSoldiers; }
        set
        {
            deadSoldiers = value;
            if (deadSoldiers == 8)
            {
                mode = GameModes.PlayerWin;
                SceneManager.LoadScene(0);
            }
        }
    }

    //singeltion
    public static GameMode instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
