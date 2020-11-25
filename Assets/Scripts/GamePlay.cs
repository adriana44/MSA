using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile
{
    public bool Occupied { set; get; }
    public Vector2 Position { set; get; }
    public Units Unit { set; get; }
}

public class GamePlay : MonoBehaviour
{
    public static GamePlay Instance { set; get; } //ADRIANA
    private const int X = 12;
    private const int Y = 6;
    private const int X_ENEMY = 11; //ADRIANA

    public Tile[,] Grid { set; get; }
    public GameObject[] UnitPrefab;
    public GameObject[] EnemyPrefab; //ADRIANA
    public List<BaseEnemy> activeEnemies = new List<BaseEnemy>(); //ADRIANA
    private int selectedUnitIndex;
    private bool isSelectingUnit;
    // Start is called before the first frame update
    private void Start()
    {
        Grid = new Tile[X, Y];
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Grid[i, j] = new Tile() { Occupied = false, Position = new Vector2(i, j), Unit = null }; 
            }
        }

    }

   
    // Update is called once per frame
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

                if (isSelectingUnit)
                {
                    Tile t = SelectGridTile(x, y);
                    if (!t.Occupied)
                    {
                        GameObject go = Instantiate(UnitPrefab[selectedUnitIndex]) as GameObject;
                        go.transform.position = (Vector3.right * x) + (Vector3.forward * y)+
                            (Vector3.right * 0.5f) + (Vector3.forward * 0.5f);
                        t.Occupied = true;
                        t.Unit = go.GetComponent<Units>();

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
        if (Input.GetKeyDown(KeyCode.T)) //ADRIANA - asta ii doar ca sa testam enemies
        {
            SpawnEnemy(0, 2);
        }
        for (int i = 0; i <= X; i++)
        {
            Debug.DrawLine(Vector3.right * i, Vector3.right * i+ Vector3.forward * Y);
        }

        for (int i = 0; i <= Y; i++)
        {
            Debug.DrawLine(Vector3.forward * i, Vector3.forward * i + Vector3.right *X);
        }
    }

    public Tile SelectGridTile(int x, int y)
    {
        return Grid[x, y];
    }

    public void SelectUnit(int index)
    {
        isSelectingUnit = true;
        selectedUnitIndex = index;
    }
    public void SpawnEnemy(int prefabIndex, int lane) //ADRIANA
    {
        GameObject go = Instantiate(EnemyPrefab[prefabIndex]) as GameObject;
        go.transform.position = new Vector3(X_ENEMY, 0.5f, 1 * lane + 0.5f);

        BaseEnemy e = go.GetComponent<BaseEnemy>();
        e.Position = new Vector2(X_ENEMY, lane);
        activeEnemies.Add(e);
    }

    public void DeleteEnemy(BaseEnemy e) //ADRIANA
    {
        activeEnemies.Remove(e);
        Destroy(e.gameObject);

        // Create Particle effect ??
        // Play sound effect ?? 
    }

}
