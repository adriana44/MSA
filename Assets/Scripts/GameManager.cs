using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// this is trash and we don't use it yet
public class GameManager : MonoBehaviour
{
    public Text currentLevelIndex;
    public Text goldAmountText;
    public GameObject unitContainer;

    private Level currentLevel;
    private int gold;

    private void Start()
    {
        //currentLevel = DataHelper.Instance.Levels[DataHelper.Instance.CurrentLevel];
        gold = currentLevel.StartingGold;

        // UI
        currentLevelIndex.text = "Current Level: " + DataHelper.Instance.CurrentLevel.ToString();
        UpdateGoldText();
    }

    private void UnlockUnits()
    {
        int i = 0;
        foreach(Transform u in unitContainer.transform)
        {
            bool activeButton = ((currentLevel.UnlockedUnits) & (1<<i)) != 0;

            //u.GetComponent<Button>().interactable = false;
            i++;
        }
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void UpdateGoldText()
    {
        goldAmountText.text = gold.ToString();

    }
}
