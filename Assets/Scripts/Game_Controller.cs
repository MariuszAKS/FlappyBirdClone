using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller instance;

    [SerializeField] float spawnDelaySeconds;
    public GameObject Wall;
    public GameObject Player;

    public bool gameRunning;

    void Start()
    {
        if (instance == null)
            instance = this;
        
        StartGame();
    }

    public void StartGame() {
        Time.timeScale = 1;
        Player.transform.position = new Vector3(0, 0, 0);
        gameRunning = true;

        StartCoroutine(SpawnWall_Timer());

        Debug.Log("Game started");
    }

    public void GameOver(string message) {
        Time.timeScale = 0;
        gameRunning = false;

        Debug.Log(message);
    }

    IEnumerator SpawnWall_Timer() {
        while (gameRunning) {
            yield return new WaitForSeconds(spawnDelaySeconds);
            Instantiate(Wall, new Vector3(10, Random.Range(-2, 2), 0), Quaternion.identity);
        }
    }
}
