using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject Bullet;
    public Transform Bullet_position;
    public float Player_Speed = 10f;
    public float Player_Direction = 1f;
    public float Player_JumpForce = 10f;
    private bool Player_jump = false;
    public bool grounded = false;
    public float Bullet_Cooldown = 0.5f;
    public float Cooldown_Timer;
    private Animator Player_Animation;
    
    // Start is called before the first frame update
    void Start()
    {
        Player_Animation = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Player_Animation.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)); //Valor absoluto = siempre positivo.
        Player_Animation.SetBool("Ground",grounded);
       


        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        { Player_jump = true;
          Player_Animation.SetTrigger("Jump");
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
        if (Input.GetButton("Fire1")&& Cooldown_Timer ==0)
        {
            Player_Animation.SetTrigger("Shoot");
            Spawn_Bullet(true);
            Cooldown_Timer = Bullet_Cooldown;
        }

    }

    private void FixedUpdate()
    {
        /*Vector3 fixedvelocity = rb2d.velocity;
        fixedvelocity.x *= 0.75f;
        if (grounded)
        { rb2d.velocity = fixedvelocity; }
        */
        float PlayerSpeed_x = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * Player_Speed * PlayerSpeed_x);

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
        if (Player_jump == true)
        {
            rb2d.AddForce(Vector2.up * Player_JumpForce, ForceMode2D.Impulse);
            //forcemode2d.impulse es para no manejar numeros grandes.
            Player_jump = false;
        }
        
    }

    void Spawn_Bullet (bool is_Shooting)
    {
      if (is_Shooting == true){
       
            GameObject CloneBullet = Instantiate( Bullet,Bullet_position.transform.position, Quaternion.identity) as GameObject;
         
        }
    }
}
