using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataHelper : MonoBehaviour
{
    public static DataHelper Instance { set; get; }
    public BitArray UnlockedLevels { set; get; }
    public int CurrentLevel { set; get; }
    
    public TextAsset LevelData;

    public List<Level> Levels { set; get; }

    void Start()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        ReadLevelData();
        UnlockedLevels = new BitArray(Levels.Count);

        Load();
        Save();

        SceneManager.LoadScene("Menu"); 
        //CurrentLevel = 1; // hardcoded; to be changed
    }

   
    public void Save()
    {
        string save = "";

        for (int i = 0; i < UnlockedLevels.Count; ++i)
            save += (UnlockedLevels.Get(i)) ? '1': '0' ; // if level is accessible

        PlayerPrefs.SetString("save", save);
    }

    public void Load()
    {
        string load = PlayerPrefs.GetString("save");

        if (load == "")
            UnlockedLevels.Set(0, true);// for first load

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

        foreach(string s in allLevels)
        {
            Levels.Add(new Level(s));
        }
    }
}
