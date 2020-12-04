using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void UnlockedLevels()
    { 

    }
    public void ToGame()
    {
        SceneManager.LoadScene("Levels");
    }
    public void OnApplicationQuit()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
