using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public Vector2 Position{set;get;}
    public float attackCooldown = 1.5f;
    public int damage = 1;
    public int hp = 3;

    private float lastAttack;
    private Vector2 desiredPosition;
    private bool isBlocked;

    private void Update()
    {
        if(!isBlocked)
        {
            transform.position += -Vector3.right * Time.deltaTime;

            if(Position.x - transform.position.x > 0.5f)
            {
                UpdateGridPosition();
            }
        }
        else
        {
            Attack();
            isBlocked = GamePlay.Instance.Grid[(int)desiredPosition.x-1, (int)desiredPosition.y].Occupied;
        }
    }

    private void UpdateGridPosition()
    {
        desiredPosition = new Vector2((int)transform.position.x, (int)transform.position.z);
        
        if(desiredPosition.x < 1)
        {
            GamePlay.Instance.DeleteEnemy(this);
            GameManager.Instance.RemoveLife();
            return;
        }

        if(GamePlay.Instance.Grid[(int)desiredPosition.x-1, (int)desiredPosition.y].Occupied)
        {
            isBlocked = true;
            lastAttack = Time.time;
        }
        else
        {
            Position = desiredPosition;
        }
    }

    private void Attack()
    {
        if(Time.time - lastAttack > attackCooldown)
        {
            GamePlay.Instance.SelectGridTile((int)desiredPosition.x-1, (int)desiredPosition.y).Unit.TakeDamage(damage);
            lastAttack = Time.time;
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if(hp <= 0)
        {
            GamePlay.Instance.DeleteEnemy(this);
        }

    }
}
