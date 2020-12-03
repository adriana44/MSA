using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile
{
    public bool Occupied { set; get; }
    public Vector2 Position { set; get; }
    public BaseUnit Unit { set; get; }
}

public class GamePlay : MonoBehaviour
{
    public static GamePlay Instance { set; get; }
    
    private const int X_TILES = 12;
    private const int Y_TILES = 6;
    private const int X_ENEMY = 13;

    public Tile[,] Grid { set; get; }
    public GameObject[] UnitPrefab;
    public GameObject[] EnemyPrefab;
    public List<BaseEnemy> activeEnemies = new List<BaseEnemy>();

    private int selectedUnitIndex;
    private bool isSelectingUnit;
    private bool isDeletingUnit;

    private void Start()
    {
        Instance = this;

        Grid = new Tile[X_TILES, Y_TILES];

        for (int i = 0; i < X_TILES; i++)
        {
            for (int j = 0; j < Y_TILES; j++)
            {
                Grid[i, j] = new Tile() { Occupied = false, Position = new Vector2(i, j), Unit = null };
            }
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 30.0f, LayerMask.GetMask("GameGrid")))
            {
                int x = (int)hit.point.x;
                int y = (int)hit.point.z;
                Debug.Log(hit.point);

                if (isDeletingUnit)
                {
                    Tile t = SelectGridTile(x, y);
                    if (t.Occupied)
                    {
                        GameManager.Instance.Gold += t.Unit.goldOnDelete;
                        GameManager.Instance.UpdateGoldText();
                        Destroy(t.Unit.gameObject);
                        t.Occupied = false;
                    }

                    isDeletingUnit = false;
                }
                if (isSelectingUnit)
                {
                    Tile t = SelectGridTile(x, y);
                    if (!t.Occupied)
                    {
                        GameObject go = Instantiate(UnitPrefab[selectedUnitIndex]);
                        go.transform.position = (Vector3.right * x) + (Vector3.forward * y)+
                            (Vector3.right * 0.5f) + (Vector3.forward * 0.5f);
                        t.Occupied = true;
                        t.Unit = go.GetComponent<BaseUnit>();

                        isSelectingUnit = false;
                        selectedUnitIndex = -1;
                    }
                    else
                    {
                        isSelectingUnit = false;
                        selectedUnitIndex = -1;
                    }
                }

            }
        }
        //if (Input.GetKeyDown(KeyCode.T)) //asta ii doar ca sa testam enemies
        //{
        //    SpawnEnemy(0, 2);
        //}
        for (int i = 0; i <= X_TILES; i++)
        {
            Debug.DrawLine(Vector3.right * i, Vector3.right * i+ Vector3.forward * Y_TILES);
        }

        for (int i = 0; i <= Y_TILES; i++)
        {
            Debug.DrawLine(Vector3.forward * i, Vector3.forward * i + Vector3.right *X_TILES);
        }
    }

    public Tile SelectGridTile(int x, int y)
    {
        return Grid[x, y];
    }

    public void SelectUnit(int index)
    { 
        //Debug.Log("index" + index);
        int cost = UnitPrefab[index].GetComponent<BaseUnit>().cost;
        //Debug.Log("cost" + cost);

        if(cost <= GameManager.Instance.Gold)
        {
            isSelectingUnit = true;
            selectedUnitIndex = index;

            GameManager.Instance.Gold -= cost;
            GameManager.Instance.UpdateGoldText();
        }
        else
        {
            Debug.Log("not enough gold");
        }

        
    }
    public void SelectDelete()
    {
        isDeletingUnit = true;
    }

    // Enemy stuff:
    public void SpawnEnemy(int prefabIndex, int lane)
    {
        GameObject go = Instantiate(EnemyPrefab[prefabIndex]);
        go.transform.position = new Vector3(X_ENEMY, 0.5f, 1 * lane + 0.5f);

        BaseEnemy e = go.GetComponent<BaseEnemy>();
        e.Position = new Vector2(X_ENEMY, lane);
        activeEnemies.Add(e);
    }

    public void DeleteEnemy(BaseEnemy e)
    {
        activeEnemies.Remove(e);
        Destroy(e.gameObject);

        // Create some sort of effect?
        // Play sound effect?
    }

}
