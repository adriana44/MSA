using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string LevelName{set;get;}
    //public int LevelIndex{set;get;};
    public int UnlockedUnits;
    public int StartingGold;

    public Level(string levelString)
    {
        string[] allLines = levelString.Split('\n');
        string[] firstLineValues = allLines[0].Split(',');

        LevelName = firstLineValues[0];
        StartingGold = int.Parse(firstLineValues[1]);
    }
}
