using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Units()
    {
        SceneManager.LoadScene("Units");
    }
    public void OnApplicationQuit()
    {

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

   
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void Click(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    public void Reset()
    {
        for (int j = 1; j < DataHelper.Instance.UnlockedLevels.Count; ++j)
            DataHelper.Instance.UnlockedLevels.Set(j, false);
        string load = PlayerPrefs.GetString("");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
