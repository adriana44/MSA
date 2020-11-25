using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DataHelper : MonoBehaviour
{
    public static DataHelper Instance { set; get; }
    public static BitArray Levels { set; get; }
    public int CurrentLevel { set; get; }

    //public List<Level> Levels; // leave this here ms pwp

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Update();

        SceneManager.LoadScene("Game"); // Load Main instead??? we don't have a "Game" scene lol
    }

    public void Save()
    {
        string save = "";

        for (int i = 0; i < Levels.Count; ++i)
            save += Levels.Get(i).ToString();
    }

    void Update()
    {

    }
}
