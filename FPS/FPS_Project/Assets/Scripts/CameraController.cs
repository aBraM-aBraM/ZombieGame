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
	}

	void PerformRotation()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		float rotAmountX = mouseX * mouseSensitivity;
		float rotAmountY = mouseY * mouseSensitivity;

		xAxisClamp -= rotAmountY;

		Vector3 rotPlayerArms = playerArms.rotation.eulerAngles;
		Vector3 rotPlayer = player.transform.rotation.eulerAngles;

		rotPlayerArms.x -= rotAmountY; 
		rotPlayerArms.z = 0;
		rotPlayer.y += rotAmountX;

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

		playerArms.rotation = Quaternion.Euler(rotPlayerArms);
		player.rotation = Quaternion.Euler(rotPlayer);
	}
}
