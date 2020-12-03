using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelContainer : MonoBehaviour
{
    public GameObject levelContainer;
    public GameObject levelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (Level l in DataHelper.Instance.Levels)//creates new level component fo every level in LevelData.txt
        {
            GameObject go = Instantiate(levelPrefab);
            go.transform.SetParent(levelContainer.transform);

            int j = i;
            go.GetComponent<Button>().onClick.AddListener(() => ToGame(j));
            go.GetComponent<Button>().interactable = (DataHelper.Instance.UnlockedLevels.Get(i));
            go.GetComponentInChildren<Text>().text = (i + 1).ToString();
            i++;
        }
    }
    public void ToGame(int levelIndex)
    {
        DataHelper.Instance.CurrentLevel = levelIndex;
        SceneManager.LoadScene("GamePlay");
    }
}
