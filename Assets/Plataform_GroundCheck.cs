using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform_GroundCheck : MonoBehaviour
{
    private Player_Script player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player_Script>();
    }
    //verifica lo que esta chocando
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Hola");
            player.grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            player.grounded = false;
        }
    }
}
