using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camm : MonoBehaviour
{
   public GameObject target; //1
   public Vector3 v3; //2
   public float speed; //3
   public float maxLook; //4 visao de cima, não passar da cabeça
   public float minLook; //5 visao de baixo, não passar do chão
   public Quaternion camRotation; //6

    void Start() //7
    {
        camRotation = transform.localRotation;
    }

    void Update() //9
    {
        Cam();
    }

    public void Cam() //8
    {
        if(target)
        {
            transform.position = target.transform.position + v3;
        }

        camRotation.y += Input.GetAxis("Mouse X") * speed;
        camRotation.x += Input.GetAxis("Mouse Y") * speed * -1;
        
        camRotation.x = Mathf.Clamp(camRotation.x, minLook, maxLook);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
    }
}