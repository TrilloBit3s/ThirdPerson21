using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    public Transform currentTarget;
    public string tagName = "enemy";

    GameObject[] AllEnemys; 

    void Start()
    {
        AllEnemys = GameObject.FindGameObjectsWithTag(tagName);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.T))
        {
            GetTarget();
            Vector3 dir = currentTarget.position - transform.position;
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }

    void GetTarget()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;

        //Compara a distancia entre os enemys mais proximos ao player
        foreach (var enemy in  AllEnemys)
        {
            float dist = (enemy.transform.position - transform.position).magnitude;
            if(dist < minDist)
            {
                tMin = enemy.transform;
                minDist = dist;
            }
        }
        currentTarget = tMin;
    }
}
