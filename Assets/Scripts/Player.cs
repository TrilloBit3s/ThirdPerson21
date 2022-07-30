using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb; 
	public float speed;
	public Animator anim; 
	public Transform Axis; 
	
	public bool inground; 
	private RaycastHit hit; 
	public float distance; 
	public Vector3 v3; 

	public bool EstaNoChao = true; 

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;  
		Cursor.visible = false;  
	}
			
	void Update()
	{
		if(Physics.Raycast(transform.position + v3, transform.up * -1, out hit, distance)) 
		{
			if(hit.collider.tag == "piso")
			{
				inground = true;
			}
			else
			{
				inground = false;
			}
		}
	}
	
	void FixedUpdate()
	{
		Move(); 
		RunFaster();  
		Jump(); 
	}
	
	void Move() 
	{
		Vector3 RotaTargetZ = Axis.transform.forward; 
		RotaTargetZ.y = 0; 

		if(Input.GetKey(KeyCode.W)) 
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetZ), 0.3f);
			var dir = transform.forward * speed * Time.deltaTime;
			dir.y = rb.velocity.y;
			rb.velocity = dir;
			anim.SetBool("walk", true);
		}
		else
		{
			if(inground)
			{
				rb.velocity = Vector3.zero;	
			}
			anim.SetBool("walk", false);
		}
		
		if(Input.GetKey(KeyCode.S))
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetZ* -1), 0.3f);
			var dir = transform.forward * speed * Time.deltaTime;
			dir.y = rb.velocity.y;
			rb.velocity = dir;
			anim.SetBool("walk", true);
		}
			
			Vector3 RotaTargetX =  Axis.transform.right;
			RotaTargetX.y = 0;
		
		if(Input.GetKey(KeyCode.D))
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetX), 0.3f);
			var dir = transform.forward * speed * Time.deltaTime;
			dir.y = rb.velocity.y;
			rb.velocity = dir;
			anim.SetBool("walk", true);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RotaTargetX* -1), 0.3f);
			var dir = transform.forward * speed * Time.deltaTime;
			dir.y = rb.velocity.y;
			rb.velocity = dir;
			anim.SetBool("walk", true);
		}
	}

	void Jump() 
	{
		if(Input.GetButtonDown("Jump") && EstaNoChao)
			{
				rb.AddForce(new Vector3(0,4,0), ForceMode.Impulse);
				EstaNoChao = false;
				anim.SetBool("in_air", true);
			}
			else
			{
				anim.SetBool("in_air", false);
			}
	}    

    private void OnCollisionEnter(Collision other) 
    {
        EstaNoChao = true;
    }

	void RunFaster()  
	{
		if(Input.GetKey(KeyCode.LeftShift)  && EstaNoChao)
		{
			anim.SetBool("FastRun", true);
		}
		else
		{
			anim.SetBool("FastRun", false);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.CompareTag("Arma"))
		{
			print("Dano");
		}
	}
	
	void OnDrawGizmos() 
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position + v3, Vector3.up * -1 * distance);
	}
}