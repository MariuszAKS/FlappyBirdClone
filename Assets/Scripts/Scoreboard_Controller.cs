using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Scoreboard_Controller : MonoBehaviour
{
    public static Scoreboard_Controller instance;
    [SerializeField] Scoreboard scoreboard;

    GameObject ScoreboardCanvas;



    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

            scoreboard = new Scoreboard();

            if (!ScoreboardFileExists()) {
                SaveScoreboard();
            } else {
                LoadScoreboard();
            }
        } else {
            Destroy(gameObject);
        }
    }



    public void DisplayScoreboard() {
        LoadScoreboard();

        List<ScoreboardEntry> entries = GetScoreboardEntries(10);
        int entriesShown = Mathf.Min(entries.Count, 10);

        ScoreboardCanvas = GameObject.Find("Scoreboard View");

        for (int i = 0; i < entriesShown; i++) {
            Transform entry = ScoreboardCanvas.transform.Find($"Entry ({i})");
            entry.Find("Name").GetComponent<TextMeshProUGUI>().text = entries[i].Name;
            entry.Find("Points").GetComponent<TextMeshProUGUI>().text = entries[i].Points.ToString();
            entry.Find("Date").GetComponent<TextMeshProUGUI>().text = entries[i].DateString;
        }
    }

    public void UpdateScoreboard(string name, int points) {
        scoreboard.AddEntry(name, points);
        SaveScoreboard();
    }




    void LoadScoreboard() {
        string data = "";
        using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/scoreboard.json")) {
            data = sr.ReadToEnd();
        }
        scoreboard = JsonUtility.FromJson<Scoreboard>(data);
    }

    void SaveScoreboard() {
        string data = JsonUtility.ToJson(scoreboard, true);
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/scoreboard.json")) {
            sw.Write(data);
        }
    }

    List<ScoreboardEntry> GetScoreboardEntries(int amount) {
        return scoreboard.GetFirstEntries(amount);
    }

    bool ScoreboardFileExists() {
        return File.Exists(Application.persistentDataPath + "/scoreboard.json");
    }
}
