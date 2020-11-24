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
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Update();

        SceneManager.LoadScene("Game");
    }

    public void Save()
    {
        string save = "";

        for (int i = 0; i < Levels.Count; ++i)
            save += Levels.Get(i).ToString();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
