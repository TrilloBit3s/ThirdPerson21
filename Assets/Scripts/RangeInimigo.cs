using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeInimigo : MonoBehaviour
{
    public Animator anim;
    public InimigoSkeleto inimigo;

    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            anim.SetBool("attack", true);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            if(!inimigo.stunado)
            {
                print("golpe");
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                anim.SetBool("attack", true);
                inimigo.atacando = true;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }    
    }
}