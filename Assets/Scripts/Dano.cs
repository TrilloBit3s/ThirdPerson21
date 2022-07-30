using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dano : MonoBehaviour
{
    public Slider vidaPlayer;//deixe variaveis iniciais com minusculo se nao o programa reconhece como classe
   // public GameObject player; 

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            vidaPlayer.value--;
        }     
    }
}
