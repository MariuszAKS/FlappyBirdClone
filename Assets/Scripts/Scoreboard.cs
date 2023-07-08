using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scoreboard
{
    [SerializeField] List<ScoreboardEntry> Entries;

    public Scoreboard() {
        Entries = new List<ScoreboardEntry>();
    }

    public void AddEntry(string name, int points)
    {
        Entries.Add(new ScoreboardEntry(name, points));
        Entries.Sort((e1, e2) => e2.Points.CompareTo(e1.Points));
    }

    public List<ScoreboardEntry> GetFirstEntries(int amount) {
        if (amount > Entries.Count) {
            return Entries;
        } else {
            return Entries.GetRange(0, amount);
        }
    }
}

[System.Serializable]
public struct ScoreboardEntry
{
    [SerializeField] private string name;
    [SerializeField] private int points;
    [SerializeField] private string dateString;

    public string Name { get { return name; } }
    public int Points { get { return points; } }
    public string DateString { get { return dateString; } }

    public ScoreboardEntry(string name, int points) {
        this.name = name;
        this.points = points;
        dateString = DateTime.Now.ToString("yyyy-MM-dd");
    }

    public override string ToString()
    {
        return name + $" {points} " + dateString;
    }
}