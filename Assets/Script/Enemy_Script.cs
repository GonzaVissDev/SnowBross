using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    private Rigidbody2D rbEnemy;
    public float Enemy_Speed = 1f;
    public float Enemy_MaxSpeed = 1f;
    public int Enemy_Hit = 0;
    private int Enemy_MaxHit = 4;
    public bool Addforce = true;
    public bool MoveBall = false;
    public float Ball_Direction;
    private float Snowball_Side;
    public GameObject Die_Particle;
    private Player_Script pscript;
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
        pscript = GetComponent<Player_Script>();
    }

   
    void Update()
    {
        A_Enemy.SetInteger("FreezeLevel", Enemy_Hit);

        if (E_State == EnemyState.Freeze)
        {
            switch (Enemy_Hit)
            {
               
                case 1:
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    break;
                case 2:
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    break;
                case 3:
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezePositionX;
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    break;

                case 4:
                    rbEnemy.constraints = RigidbodyConstraints2D.None;
                    rbEnemy.constraints = RigidbodyConstraints2D.FreezeRotation;
                    break;
           
            }
          
        }
        if (E_State == EnemyState.SnowBall)
        {
            Invoke("RecoveringON",5f);
            if (MoveBall == true)
            {
                rbEnemy.transform.position += ((Vector3.right*Snowball_Side * Ball_Direction * Time.deltaTime));
            }
        }
    }

    void FixedUpdate()
    {
        if (E_State == EnemyState.Idle)
        {
          rbEnemy.AddForce(Vector2.right * Enemy_Speed);
          float limitedSpeed = Mathf.Clamp(rbEnemy.velocity.x, -Enemy_MaxSpeed,Enemy_MaxSpeed);
          rbEnemy.velocity = new Vector2(limitedSpeed, rbEnemy.velocity.y);

        if (rbEnemy.velocity.x > -0.01f && rbEnemy.velocity.x < 0.01f)
        {
            
            Enemy_Speed = -Enemy_Speed;
            rbEnemy.velocity = new Vector2(Enemy_Speed, rbEnemy.velocity.y);
            
            if (Enemy_Speed >0)
            {
                transform.localScale = new Vector3(-4f, 4f, 1f);
            }else if (Enemy_Speed<0){
                transform.localScale = new Vector3(4f, 4f, 1f);
            }
        }
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
           if(Enemy_Hit > 0 && Addforce ==false && MoveBall != true)
            {
                NextRecovering = Time.time + Recovering; 
                Enemy_Hit--;
                if (Enemy_Hit <= 0)
                {
                    E_State = EnemyState.Idle;
                    NextRecovering = 0;
                }
            }
        }
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && E_State == EnemyState.Idle)
        {
            pscript = collision.gameObject.GetComponent<Player_Script>();
            pscript.Player_State = Player_Script.PlayerState.Death;

        } else if (collision.gameObject.tag == "Enemy" && E_State == EnemyState.SnowBall)
        {
            Instantiate(Die_Particle,transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

    public void Impulse (float side)
    {
        if ( E_State == EnemyState.SnowBall)
        {
            float Side_Position = Mathf.Sign(transform.position.x - side);
            Snowball_Side = Side_Position;
            MoveBall = true;
        }
    }
}
