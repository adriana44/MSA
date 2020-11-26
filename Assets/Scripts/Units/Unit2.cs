using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit2 : BaseUnit
{
    public void Start()
    {
        cost = 20;
        damage = 1;
    }

    public override void Attack()
    {
        BaseEnemy enemy = null;

        for(int i = 0; i < GamePlay.Instance.activeEnemies.Count; i++)
        {
            if(GamePlay.Instance.activeEnemies[i].Position.y == (int)transform.transform.position.z)
            {
                enemy = GamePlay.Instance.activeEnemies[i];      
            }
        }

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            lastAttack = Time.time;
        }
    }
}
