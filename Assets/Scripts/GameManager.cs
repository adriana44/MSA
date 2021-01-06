using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    // UI stuff
    public Text currentLevelIndex;
    public Text goldAmountText;
    public Text enemies;
    public int nrEnemiesInLevel;
    public int nrEnemiesInLevelLeft;
    public GameObject UnitContainer;
    public GameObject LifeContainer;
    public Button victory;

    public Level currentLevel;
    public int Gold{set; get;}
    public int Life{set; get;}
    private int startTime;
    private void Start()
    {
        Instance = this;
        Life = 5;

        currentLevel = DataHelper.Instance.Levels[DataHelper.Instance.CurrentLevel];

        Gold = currentLevel.StartingGold;
        // Gold = 40; // hardcoded; needs to be an attribute of Level


        nrEnemiesInLevel = currentLevel.enemies.Count; //needs to be modified or taken out
        nrEnemiesInLevelLeft = -1;                       //


        // UI
        currentLevelIndex.text = currentLevel.LevelName;
        
        //UnlockUnits();
        UpdateGoldText();
        UpdateLivesUI();
        UpdateEnemiesText();

        startTime = (int)Time.time;
        Invoke("Hide", 2f);//deletes current level text

        InvokeRepeating("Timer", 1f, 1f);//does something every second
    }
    private void Update()
    {
        
    }
    private void UnlockUnits()
    {
        /*int i = 0;
        foreach(Transform u in UnitContainer.transform)
        {
            bool activeButton = ((currentLevel.UnlockedUnits) & (1<<i)) != 0;

            //u.GetComponent<Button>().interactable = false;
            i++;
        }*/ //nu ii necesar
    }
    public void ToMenu()
    {        
        SceneManager.LoadScene("Main");
    }

    public void UpdateGoldText()
    {
        goldAmountText.text = Gold.ToString();
    }

    public void Timer()
    {
        int gameDuration = (int)Time.time - startTime;

        for (int i = 0; i < currentLevel.enemies.Count; i++)// spanws units at given time
        {

            if (currentLevel.enemies[i].time == gameDuration)
            {
                GamePlay.Instance.SpawnEnemy(currentLevel.enemies[i].index, currentLevel.enemies[i].laneIndex);
                //currentLevel.enemies.Remove(currentLevel.enemies[i]);               
            }
        }
        Gold++;
         goldAmountText.text = Gold.ToString();       
    }
    
    public void Hide()
    {
        Destroy(currentLevelIndex);
    }

    public void UpdateLivesUI()
    {
        int i = 0;
        foreach(Transform t in LifeContainer.transform)
        {
            if(Life-1 < i)
            {
                t.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
            }
            
            if(Life <= 0)
            {
                Death();
                return;
            }

            i++;
        }
    }
    public void UpdateEnemiesText()
    {
        nrEnemiesInLevelLeft++;
        enemies.text= nrEnemiesInLevelLeft.ToString()+"/" + nrEnemiesInLevel.ToString();
    }

    
    public void RemoveLife()
    {
        Life--;
        UpdateLivesUI();
        // Add sound effect ?
    }

    public void Death()
    {
        SceneManager.LoadScene("Levels"); // to be replaced with Lose Scene
    }

    public void Victory()
    {
        int nextLevel = DataHelper.Instance.CurrentLevel + 1;
        if(nextLevel >= DataHelper.Instance.Levels.Count)
        {
            //done with the game
            return;
        }
        DataHelper.Instance.UnlockedLevels.Set(nextLevel, true);
        DataHelper.Instance.Save();
        if(nrEnemiesInLevelLeft== nrEnemiesInLevel)
            victory.gameObject.SetActive(true);
    }
}
