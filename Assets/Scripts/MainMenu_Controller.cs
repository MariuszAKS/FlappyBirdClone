using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Scoreboard;



    void Start() {
        Menu.SetActive(true);
        Scoreboard.SetActive(false);
    }



    public void StartNewGame() {
        SceneManager.LoadScene(1);
    }

    public void ShowScoreboard() {
        Menu.SetActive(false);
        Scoreboard.SetActive(true);
        Scoreboard_Controller.instance.DisplayScoreboard();
    }

    public void GoBackToMenu() {
        Scoreboard.SetActive(false);
        Menu.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
