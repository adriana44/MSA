using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    public int goldOnDelete;
    public int hp = 3;
    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp < 0)
        {
            Tile tile = GamePlay.Instance.SelectGridTile((int)transform.position.x,
                                                      (int)transform.position.z);
            Destroy(tile.Unit.gameObject);
            tile.Occupied = false;
        }
    }
}
