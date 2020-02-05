using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSnowBall : MonoBehaviour
{

    public float pushPower = 2.0F;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            Rigidbody2D body = collision.collider.attachedRigidbody;
        

        if (body == null || body.isKinematic)
            return;

      
        if (collision.transform.position.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(collision.transform.position.x, 0, collision.transform.position.z);

        body.velocity = pushDir * pushPower;
        }
    }


}
