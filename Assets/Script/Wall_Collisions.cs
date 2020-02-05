using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Collisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

    }
}
