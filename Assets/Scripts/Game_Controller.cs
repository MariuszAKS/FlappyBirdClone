using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] float spawnDelaySeconds;
    public GameObject Wall;

    void Start()
    {
        StartCoroutine(SpawnWall_Timer());
    }

    IEnumerator SpawnWall_Timer() {
        while (true) {
            yield return new WaitForSeconds(spawnDelaySeconds);
            Instantiate(Wall, new Vector3(10, Random.Range(-2, 2), 0), Quaternion.identity);
        }
    }
}
