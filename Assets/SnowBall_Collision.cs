using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall_Collision : MonoBehaviour
{
    private Enemy_Script Enemy;
    public float Snow_Speed = 10f;
    public Transform Guide;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponentInParent<Enemy_Script>();
    }
    //verifica lo que esta chocando
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
           // float Side_Position = Mathf.Sign(Enemy.GetComponent<Rigidbody2D>().transform.position.x - collision.transform.position.x);
            Enemy.Addforce = true;
            //  Enemy.Ball_Direction = Side_Position * Snow_Speed;
            Enemy.Ball_Direction *=-1;
            Debug.Log(Enemy.Ball_Direction);
        }
        if (collision.gameObject.tag == "Wall_End")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player_Script>() != null)
            {
                Player_Script PlayerCollision = collision.gameObject.GetComponent<Player_Script>();

                Debug.Log("Te toque");
                PlayerCollision.gameObject.transform.SetParent(Guide);
                PlayerCollision.gameObject.transform.localRotation = transform.rotation;
                PlayerCollision.gameObject.transform.position = Guide.position;


            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player_Script>() != null)
            {
                Player_Script PlayerCollision = collision.gameObject.GetComponent<Player_Script>();

                Debug.Log("Te toque");
                PlayerCollision.Player_Surf = true;
                //falta mandar para que entre en modo surf , soluciona que se haga gigante.
                PlayerCollision.gameObject.transform.position = Guide.position;


            }
        }

    }
    /*  private void OnCollisionExit2D(Collision2D collision)
      {
          if (collision.gameObject.tag == "Ground")
          {

              player.grounded = false;
          }
      }*/
}
