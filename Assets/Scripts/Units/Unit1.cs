using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// produces gold; does not attack
public class Unit1 : BaseUnit
{
    public override void Attack()
    {
        Debug.Log("unit1 attack (it doesn't deal damage");
        //lastAttack = Time.time;
    }
}
