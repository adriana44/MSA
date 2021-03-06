﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public int goldOnDelete;
    public int cost;
    public int hp = 3;
    public int damage = 1; // to be changed
    public float attackCooldown = 1.5f; // to be changed
    public float lastAttack;

    public virtual void Attack(){} // will be different for each Unit

    public void Update()
    {
        if(Time.time - lastAttack > attackCooldown)
        {
            Attack();
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp < 0)
        {
            Tile tile = GamePlay.Instance.SelectGridTile((int)transform.position.x,
                                                      (int)transform.position.z);

            //returns half of the cost of the unit when it dies
            int cost = tile.Unit.gameObject.GetComponent<BaseUnit>().cost/2;
            GameManager.Instance.Gold += cost;
            GameManager.Instance.UpdateGoldText();

            Destroy(tile.Unit.gameObject);
            tile.Occupied = false;
        }
    }
}
