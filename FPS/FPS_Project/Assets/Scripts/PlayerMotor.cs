using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	CharacterController controller;
	WeaponScript weaponScript;
	ItemDatabase database;

	Animator anim;

	Vector3 moveDirection;

	float moveSpeed;
	float walkSpeed = 4f;
	float sprintSpeed = 6f;
	float jumpSpeed = 30f;
	float gravity = 4f;


	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();
		weaponScript = GetComponent<WeaponScript>();
		database = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemDatabase>();
	}

	void Update()
	{
		PerformMovement();
	}

	void PerformMovement()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveZ = Input.GetAxis("Vertical");


		if (controller.isGrounded)
		{
			moveDirection = new Vector3(moveX, 0, moveZ);
			moveDirection = transform.TransformDirection(moveDirection);
			if (Input.GetKey(KeyCode.LeftShift) && moveZ == 1)
			{
				moveSpeed = sprintSpeed;
				anim.SetInteger("condition", 2);
			}
			else
				moveSpeed = walkSpeed;

			moveDirection *= moveSpeed;

			if(moveZ != 0 && moveSpeed == walkSpeed)
			{
				anim.SetInteger("condition", 1);
			}
			if(moveZ == 0)
			{
				anim.SetInteger("condition", 0);
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				moveDirection.y += jumpSpeed;
				anim.SetTrigger("jumpTrigger");
			}
		}

		moveDirection.y -= gravity;


		controller.Move(moveDirection * Time.deltaTime);
	}

}
