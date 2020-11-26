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

    public List<Level> Levels; // leave this here ms pwp


    void Start()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        Load();

        SceneManager.LoadScene(1);
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
}
