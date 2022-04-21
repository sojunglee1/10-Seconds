using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float time;
    public string objective;

    public bool FoundKey = false;
    public bool FoundDoor = false;

    public bool GameWon = false;
    public bool GameLost = false;

    private void Awake()
    {
        instance = this;
        objective = "Find the Key";
    }
    private void Start()
    {
        ResumeGame();
    }
    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

        }
        else if (time < 0)
        {
            if (!FoundKey || !FoundDoor)
            {
                GameLost = true;
            }
            else if (FoundKey & FoundDoor)
            {
                GameWon = true;
            }    
        }

        if (!FoundKey & !FoundDoor)
        {
            objective = "Find the Gold Key";
        }
        else if (FoundKey & !FoundDoor)
        {
            objective = "Find the Gold Door";
        }
    }

    public void PauseGame()
    {
        UIManager.instance.PauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager.instance.PauseScreen.SetActive(false);
    }
}
