using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {

	public int itemID;
	public int weaponType;

	public string name;

	public GameObject weaponObject;

	public float mass;

	public bool isGun = false;
	public bool isAutomatic = false;
	public int currentMag;
	public int magSize;
	public int ammo;


	public float reloadSpeed;


	public float damage;
	public float range;

	public float fireRate;
}
