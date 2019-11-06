using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float Player_Speed = 10f;
    public float Player_JumpForce = 10f;
    private bool Player_jump = false;
    public bool grounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        { Player_jump = true; }

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
            transform.localScale = new Vector3(5f, 5f, 5f);
        }
        if (PlayerSpeed_x < -0.1f)
        {
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
}
