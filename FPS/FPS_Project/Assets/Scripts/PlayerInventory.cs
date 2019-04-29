using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public int[] inventory;
	public GameObject[] weaponSlot;

	void Start()
	{
		inventory = new int[3];
		weaponSlot = new GameObject[3];

		weaponSlot[0] = GameObject.FindGameObjectWithTag("Primary");
		weaponSlot[1] = GameObject.FindGameObjectWithTag("Secondary");
		weaponSlot[2] = GameObject.FindGameObjectWithTag("Melee");
	}
}
