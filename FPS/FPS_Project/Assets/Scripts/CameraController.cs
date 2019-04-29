using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	float mouseSensitivity;

	float xAxisClamp;

	[SerializeField]
	Transform player, playerArms;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;

	}

	void Update()
	{
		PerformRotation();
		if (Input.GetKey(KeyCode.Escape))
		{
			Cursor.lockState = 1 - Cursor.lockState;
		}
	}

	void PerformRotation()
	{
		// Input of mouse
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		// Amount of rotation applied
		float rotAmountX = mouseX * mouseSensitivity;
		float rotAmountY = mouseY * mouseSensitivity;

		// Borders for rotation
		xAxisClamp -= rotAmountY;

		// Rotating the camera for y axis and body for z/x
		Vector3 rotPlayerArms = playerArms.rotation.eulerAngles;
		Vector3 rotPlayer = player.transform.rotation.eulerAngles;

		rotPlayerArms.x -= rotAmountY; 
		rotPlayerArms.z = 0;
		rotPlayer.y += rotAmountX;


		// Clamping 
		if(xAxisClamp > 90)
		{
			xAxisClamp = 90;
			rotPlayerArms.x = 90;
		}
		else if(xAxisClamp < -90)
		{
			xAxisClamp = -90;
			rotPlayerArms.x = 270;
		}


		// Performing Rotation
		playerArms.rotation = Quaternion.Euler(rotPlayerArms);
		player.rotation = Quaternion.Euler(rotPlayer);
	}
}
