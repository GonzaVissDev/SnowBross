using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject Bullet;
    public Transform Bullet_position;
    public enum PlayerState { Idle, Walk,Jump, SurfSnow, Death };
    public PlayerState Player_State = PlayerState.Idle;


    public float Player_Speed = 10f;
    public float Player_MaxSpeed = 5f;
    public float Player_Direction = 1f;
    public float Player_JumpForce = 10f;
    public int Player_Life = 2;
    public bool Player_Die = false;
    public bool Player_Surf = false;
    public bool Player_MoveSnowBall = false;
    public bool grounded = false;
    public bool Stop = false;
    public float Bullet_Cooldown = 0.5f;
    public float Cooldown_Timer;
    private Animator Player_Animation;
    Vector3 Snowball_Position;
    Vector3 Player_Snowball;
    public Respawn_Player Respawn_Script;

    // Start is called before the first frame update
    void Start()
    {
        Player_Animation = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        Respawn_Script = GetComponent<Respawn_Player>();

        GameObject RespawnObject = GameObject.FindWithTag("RespawnObject");

        if (RespawnObject != null)
        {
            Respawn_Script = RespawnObject.GetComponent<Respawn_Player>();

        }

    }

        // Update is called once per frame
        void Update()
    {
        Player_Animation.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        Player_Animation.SetBool("Ground",grounded);
        Player_Animation.SetBool("MoveObj", Player_MoveSnowBall);
        Player_Animation.SetBool("Surf",Player_Surf);
        Player_Animation.SetBool("Die", Player_Die);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            Player_State = PlayerState.Jump;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Player_Animation.SetTrigger("Shoot");
        }

        if (Cooldown_Timer>0)
        {
            Cooldown_Timer -= Time.deltaTime;
        }

        if (Cooldown_Timer < 0)
        {
            Cooldown_Timer =0;
        }
        if (Input.GetKeyDown(KeyCode.X) && Cooldown_Timer ==0)
        {
            Player_Animation.SetTrigger("Shoot");
            Spawn_Bullet(true);
            Cooldown_Timer = Bullet_Cooldown;
        }

    }

    private void FixedUpdate()
    {
        float PlayerSpeed_x = Input.GetAxis("Horizontal");
        if (Stop== true){ PlayerSpeed_x = 0; }

        rb2d.AddForce(Vector2.right * Player_Speed * PlayerSpeed_x);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -Player_MaxSpeed, Player_MaxSpeed);

        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
       

        if (PlayerSpeed_x> 0.1f)
        {
            Player_Direction = 1f;
            transform.localScale = new Vector3(5f, 5f, 5f);
        }
        if (PlayerSpeed_x < -0.1f)
        {
            Player_Direction = -1f;
            transform.localScale = new Vector3(-5f, 5f, 5f);
            
        }


        //Movimientos 
        if (Player_State == PlayerState.Jump)
        {
            Player_Animation.SetTrigger("Jump");
            rb2d.AddForce(Vector2.up * Player_JumpForce, ForceMode2D.Impulse);
            Player_State = PlayerState.Idle;
            Stop = false;
        }

        if (Player_State == PlayerState.SurfSnow)
        {
            Stop = true;
            rb2d.GetComponent<Rigidbody2D>().simulated = false;
            
        }

        if (Player_State == PlayerState.Death)
        {
            Player_Die = true;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            rb2d.MovePosition(transform.position + transform.up * Time.fixedDeltaTime);
            Respawn_Script.SendMessage("Respawn");
            Destroy(this.gameObject, 1f);

        }



    }

    void Spawn_Bullet (bool is_Shooting)
    {
      if (is_Shooting == true){
       
            GameObject CloneBullet = Instantiate( Bullet,Bullet_position.transform.position, Quaternion.identity) as GameObject;
         
        }
    }

    public void Surf_Snowball(bool Surfing)
    {
        Player_State = PlayerState.SurfSnow;
        Player_Surf = Surfing;
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Enemy_Script>() != null)
            {
                Enemy_Script EnemyCollision = collision.gameObject.GetComponent<Enemy_Script>();
                if (EnemyCollision.E_State == Enemy_Script.EnemyState.SnowBall)
                {
                    Player_MoveSnowBall = true;

                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        EnemyCollision.SendMessage("Impulse",transform.position.x);
                    }

                }
            }
        }
    }
    
private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<Enemy_Script>() != null)
            {
                Enemy_Script EnemyCollision = collision.gameObject.GetComponent<Enemy_Script>();
                if (EnemyCollision.E_State == Enemy_Script.EnemyState.SnowBall)
                {
                    Player_MoveSnowBall = false;
                }
            }
        }
    }
}
