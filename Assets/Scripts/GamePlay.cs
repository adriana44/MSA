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
    private const int X = 12;
    private const int Y = 6;

    public Tile[,] Grid { set; get; }
    public GameObject[] UnitPrefab;
    private int selectedUnitIndex;
    private bool isSelectingUnit;
    // Start is called before the first frame update
    private void Start()
    {
        Grid = new Tile[X, Y];       
    }

   
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 0f, LayerMask.GetMask("GameGrid")))
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
                        go.transform.position = (Vector3.right * x) + (Vector3.forward * y);
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
}
