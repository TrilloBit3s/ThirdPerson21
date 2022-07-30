//Movimentação terceira pessoa com controle ps4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	public float moveMult;	//Multiplie for movement
	public float turnMult;	//Multiplie for turning
	public float jumpMult;	//Jump force multiplier
	Rigidbody rb;
	
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;	
		Cursor.visible = false;
		
		rb = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		transform.Translate(Input.GetAxis("Horizontal") * moveMult, 0, Input.GetAxis("Vertical") * moveMult);
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * turnMult);

		if(Input.GetButton("Jump"))
		{
			rb.AddForce(Vector2.up * jumpMult);	
		}	
	}
}