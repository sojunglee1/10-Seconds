using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI ObjectiveText;

    [SerializeField] public GameObject PauseScreen;

    [SerializeField] GameObject GameResultsScreen;
    [SerializeField] TextMeshProUGUI GameResultsText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (GameManager.instance.GameWon)
        {
            GameResultsScreen.gameObject.SetActive(true);
            GameResultsText.text = "You win!";
            GameManager.instance.PauseGame();
        }
        else if (GameManager.instance.GameLost)
        {
            GameResultsScreen.gameObject.SetActive(true);
            GameResultsText.text = "You Lost!";
            GameManager.instance.PauseGame();
        }
        else
        {
            TimerText.text = $"Timer: {(int)GameManager.instance.time}";
            ObjectiveText.text = GameManager.instance.objective;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
