using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct EnemySpawn
{
    public int index;
    public int laneIndex;
    public float time;
}

public class Level 
{
   
    public string LevelName{set;get;}
    //public int LevelIndex{set;get;};
    public int UnlockedUnits;
    public int StartingGold;
    public List<EnemySpawn> enemies;

    public Level(string levelString)
    {
        enemies = new List<EnemySpawn>();
        string[] allLines = levelString.Split('\n');
        string[] firstLineValues = allLines[0].Split(',');
       

        LevelName = firstLineValues[0];
        StartingGold = int.Parse(firstLineValues[1]);

        for(int i=1;i<allLines.Length;i++)
        {
            if(allLines[i]=="")
                    break;

            string[] enemyInfo = allLines[i].Split(',');

            EnemySpawn e = new EnemySpawn
            {
                index = int.Parse(enemyInfo[0]),
                laneIndex = int.Parse(enemyInfo[1]),
                time = float.Parse(enemyInfo[2])
            };


            enemies.Add(e);

        }
    }

}
