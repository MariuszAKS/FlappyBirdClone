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

    [SerializeField] TextMeshProUGUI Text_Score;
    [SerializeField] GameObject GameOverScreen;
    TextMeshProUGUI GameOver_Message;
    TextMeshProUGUI GameOver_Score;



    void Start()
    {
        if (instance == null)
            instance = this;
        
        GameOver_Message = GameOverScreen.transform.Find("Text_Message").GetComponent<TextMeshProUGUI>();
        GameOver_Score = GameOverScreen.transform.Find("Text_Score").GetComponent<TextMeshProUGUI>();
        GameOverScreen.SetActive(false);

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



    public void RestartGame() {
        Player_Score = 0;
        Text_Score.text = Player_Score.ToString();

        GameOver_Message.text = "Placeholder message";
        GameOver_Score.text = Player_Score.ToString();
        GameOverScreen.SetActive(false);

        DeleteAllWalls();

        Player.GetComponent<Player_Controller>().StartingPosition();
        
        Time.timeScale = 1;
    }

    private void DeleteAllWalls() {
        GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach(GameObject obj in Walls) Destroy(obj.gameObject);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
