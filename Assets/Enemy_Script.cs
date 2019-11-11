using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    private Rigidbody2D rbEnemy;
    public float Enemy_Speed = 5f;
    public int Enemy_Hit = 0;
    private int Enemy_MaxHit = 4;
    public enum EnemyState { Idle, Freeze, SnowBall, Death };
    public EnemyState E_State = EnemyState.Idle;
    private Animator A_Enemy;

    public float NextRecovering = 0f;
    public float Recovering = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
        A_Enemy = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        A_Enemy.SetInteger("FreezeLevel", Enemy_Hit);

        if (E_State == EnemyState.Freeze)
        {
            rbEnemy.velocity = Vector2.zero;
            //si esta congelado StartCoroutine(Recovering());
        }

        if (E_State == EnemyState.SnowBall)
        {
            Invoke("RecoveringON",2f);
        }
    }






    public void Bullet_Hit (int Hit)
    {
        E_State = EnemyState.Freeze;
        if (Enemy_Hit<Enemy_MaxHit)
        { Enemy_Hit += Hit;}

        if (Enemy_Hit == Enemy_MaxHit)
        {E_State = EnemyState.SnowBall;
            
        }
    }


     void RecoveringON(){
       
        if (Time.time > NextRecovering)
        { 
           if(Enemy_Hit > 0)
            {
                NextRecovering = Time.time + Recovering; 
                Enemy_Hit--;
            }
        }
     }
}
