using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoSkeleto : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;

    public GameObject arma;
    public bool stunado;
    public RangeInimigo rango;

    public float speed;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            if (Vector3.Distance(transform.position, target.transform.position) > 1f && !atacando)
            {

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ani.SetBool("walk", false);
                ani.SetBool("run", true);

                if (transform.rotation == rotation)
                {
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }

                ani.SetBool("attack", false);
            }
            else
            {
                if (!stunado && !atacando)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                    ani.SetBool("walk", false);
                    ani.SetBool("run", false);
                }
            }
        } 
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
        stunado = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
    }
    public void ColliderWeaponTrue()
    {
        arma.GetComponent<BoxCollider>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        arma.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }
}