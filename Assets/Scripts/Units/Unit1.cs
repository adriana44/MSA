using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// produces gold; does not attack
public class Unit1 : BaseUnit
{
    public int goldGain = 10;
    public override void Attack()
    {
        GameManager.Instance.Gold += goldGain;
        GameManager.Instance.UpdateGoldText();
        lastAttack = Time.time;
    }
}
