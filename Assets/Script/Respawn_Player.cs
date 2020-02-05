using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Player : MonoBehaviour
{
    public GameObject Player;
    private Animator Respawn_Animation;
    public Transform Respawn_Positon;
    private Player_Script player;
    public bool summon = false;

    private void Start()
    {
        player = GetComponent<Player_Script>();
        Respawn_Animation = GetComponent<Animator>();

        GameObject PlayerObject = GameObject.FindWithTag("Player");

        if (PlayerObject != null)
        {
           player = PlayerObject.GetComponent<Player_Script>();

        }
    }


    public void Respawn()
    {
        if (player.Player_Die == true && summon == false)
        {
            Respawn_Animation.SetTrigger("On");
            Instantiate(Player, new Vector3(Respawn_Positon.transform.position.x,Respawn_Positon.transform.position.y,0), Quaternion.identity);
            summon = true;
        }
    }
}

