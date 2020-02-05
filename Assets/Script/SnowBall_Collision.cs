using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall_Collision : MonoBehaviour
{
    private Enemy_Script Enemy;
    private Player_Script Player;
    public float Snow_Speed = 10f;
    public Transform Guide;
    public GameObject Sw_Particle;
    public bool Carry = false;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponentInParent<Enemy_Script>();
        Player = GetComponent<Player_Script>();

        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            Player = PlayerObject.GetComponent<Player_Script>();

        }
    }


    private void Update()
    { if (Carry == true)
        {
            Player.gameObject.transform.position = new Vector3(Guide.position.x, Guide.position.y, Guide.position.z);
        }
    }


    //verifica lo que esta chocando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            float Side_Position = Mathf.Sign(Enemy.GetComponent<Rigidbody2D>().transform.position.x - collision.transform.position.x);
            Enemy.Addforce = true;
            Enemy.Ball_Direction = Side_Position * Snow_Speed ;
            if ( Enemy.Enemy_Speed > -0.01f )
            {
                Enemy.Ball_Direction *=-1;
            }
        }
        if (collision.gameObject.tag == "Wall_End" && Carry == true)
        {
            Player.GetComponent<Rigidbody2D>().simulated = true;
            Player.SendMessage("Surf_Snowball", false);
            Player.Player_State = Player_Script.PlayerState.Jump;
            Instantiate(Sw_Particle, transform.position, Quaternion.identity);
        } else if (collision.gameObject.tag == "Wall_End" && Carry == false)
        {
            Instantiate(Sw_Particle, transform.position, Quaternion.identity);
        }
    }


    private void OnCollisionStay2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Enemy.Addforce == true)
        {
            if (collision.gameObject.GetComponent<Player_Script>() != null)
            {
                // Me aseguro que mantenga la escala.
                var originalScale = Player.transform.localScale;
                Player.transform.localScale = originalScale;

                if (Player.Player_State == Player_Script.PlayerState.Idle && Carry == false) 
                {
                    Player.SendMessage("Surf_Snowball", true);
                    Carry = true;
                }
            }
        }

    }
    
}


