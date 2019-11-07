using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player_Script player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player_Script>();

        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null)
        {
            player = PlayerObject.GetComponent<Player_Script>();

        }


       rb.AddForce(new Vector2(6f* player.Player_Direction, 4f), ForceMode2D.Impulse);
        if (player.Player_Direction == -1f)
        {
            rb.transform.Rotate(0f, -180f, 0);

        }
        Destroy(this.gameObject, 1f);
    }

 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Cuando toque con el enemigo.
    }
}
