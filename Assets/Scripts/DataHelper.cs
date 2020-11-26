using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataHelper : MonoBehaviour
{
    public static DataHelper Instance { set; get; }
    public static BitArray UnlockedLevels { set; get; }
    public int CurrentLevel { set; get; }
    public TextAsset LevelData;

    public List<Level> Levels { set; get; } // leave this here ms pwp

    void Start()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        Load();
        ReadLevelData();
        SceneManager.LoadScene(1); // hardcoded; to be changed
        //CurrentLevel = 1; // hardcoded; to be changed
    }

    public void Save()
    {
        string save = "";

        for (int i = 0; i < UnlockedLevels.Count; ++i)
            save += UnlockedLevels.Get(i).ToString();

        PlayerPrefs.SetString("save", save);
    }

    public void Load()
    {
        string load = PlayerPrefs.GetString("save");

        int i = 0;
        foreach (char c in load)
        {
            if (c == '0')
                UnlockedLevels.Set(i, false);
            else
                UnlockedLevels.Set(i, true);
            i++;
        }


    }

    private void ReadLevelData()
    {
        Levels = new List<Level>();

        string[] allLevels = LevelData.text.Split('%');

        Debug.Log(allLevels[0]);
    }
}
