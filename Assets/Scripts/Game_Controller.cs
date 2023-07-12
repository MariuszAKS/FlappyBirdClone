using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller instance;

    [SerializeField] float spawnDelaySeconds;
    public GameObject WallPrefab;
    public GameObject Player;
    int Player_Score;

    [SerializeField] GameObject Background;
    [SerializeField] TextMeshProUGUI Text_Score;
    [SerializeField] GameObject GameOverScreen;
    TextMeshProUGUI GameOver_Message;
    TextMeshProUGUI GameOver_Score;
    TMP_InputField Input_PlayerName;
    bool scoreboardEntrySaved;



    void Start()
    {
        if (instance == null)
            instance = this;
        
        GameOver_Message = GameOverScreen.transform.Find("Text_Message").GetComponent<TextMeshProUGUI>();
        GameOver_Score = GameOverScreen.transform.Find("Text_Score").GetComponent<TextMeshProUGUI>();
        Input_PlayerName = GameOverScreen.transform.Find("Input_PlayerName").GetComponent<TMP_InputField>();
        GameOverScreen.SetActive(false);

        scoreboardEntrySaved = false;
        Time.timeScale = 1;
        
        StartCoroutine(SpawnWall_Timer());
    }

    IEnumerator SpawnWall_Timer() {
        while (true) {
            GameObject WallObject = Instantiate(WallPrefab, new Vector3(10, Random.Range(-2, 2), 0), Quaternion.identity);
            WallObject.name = "Wall";

            yield return new WaitForSeconds(spawnDelaySeconds);
        }
    }



    public void IncreaseScore() {
        Player_Score++;
        Text_Score.text = Player_Score.ToString();
    }

    public void GameOver(string message) {
        Time.timeScale = 0;

        GameOverScreen.SetActive(true);
        GameOver_Message.text = message;
        GameOver_Score.text = Player_Score.ToString();
    }



    public void SaveScoreboardEntry() {
        if (scoreboardEntrySaved) {
            return;
        }

        string Player_Name = string.IsNullOrEmpty(Input_PlayerName.text) ? "Bezimienny" : Input_PlayerName.text;
        Scoreboard_Controller.instance.UpdateScoreboard(Player_Name, Player_Score);
        scoreboardEntrySaved = true;
    }

    public void RestartGame() {
        Player_Score = 0;
        Text_Score.text = Player_Score.ToString();

        GameOver_Message.text = "Placeholder message";
        GameOver_Score.text = Player_Score.ToString();
        GameOverScreen.SetActive(false);

        DeleteAllWalls();

        Player.GetComponent<Player_Controller>().StartingPosition();
        Background.GetComponent<Background_Controller>().ResetBackground();
        
        scoreboardEntrySaved = false;
        Time.timeScale = 1;
    }

    private void DeleteAllWalls() {
        GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach(GameObject obj in Walls) Destroy(obj.gameObject);
    }

    public void MainMenu() {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(0);
    }
}
