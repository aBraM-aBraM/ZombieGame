using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	float pickupRange = 2f;

	int pickupLayerMask;

	GameObject gameController;

	GameObject primaryWeapon, secondaryWeapon, meleeWeapon;

	WeaponScript wp;

	Camera cam;

	[SerializeField]
	ItemDatabase database;
	[SerializeField]
	PlayerInventory inventory;

	void Start()
	{
		cam = GetComponent<Camera>();
		gameController = GameObject.FindGameObjectWithTag("GameController");
		wp = GetComponentInParent<WeaponScript>();
		database = gameController.GetComponent<ItemDatabase>();
		inventory = gameController.GetComponent<PlayerInventory>();

		pickupLayerMask = LayerMask.GetMask("Pickup");


	}

	void Update()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;

		if (Input.GetKeyDown(KeyCode.G))
		{
			ThrowCurrentWeapon();
		}


		if (Physics.Raycast(ray, out hit, pickupRange, pickupLayerMask))
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				int id = hit.transform.GetComponent<ItemID>().itemID;
				if(database.weapons[id].weaponType == 1)
				{
					if(inventory.inventory[0] == id)
					{
						print("You already have that weapon");
					}
					else 
					{
						if(inventory.inventory[0] != 0)
							Destroy(primaryWeapon);

						Destroy(hit.transform.gameObject);

						inventory.inventory[0] = id;
						primaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[0].transform.position, inventory.weaponSlot[0].transform.rotation,inventory.weaponSlot[0].transform);
						Destroy(primaryWeapon.GetComponent<Rigidbody>());
					}
				}
				if (database.weapons[id].weaponType == 2)
				{
					if (inventory.inventory[1] == id)
					{
						print("You already have that weapon");
					}
					else
					{
						if(inventory.inventory[1] != 0)
							Destroy(secondaryWeapon);

						Destroy(hit.transform.gameObject);

						inventory.inventory[1] = id;
						secondaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[1].transform.position, inventory.weaponSlot[1].transform.rotation, inventory.weaponSlot[1].transform);
						Destroy(secondaryWeapon.GetComponent<Rigidbody>());
					}
				}
				if (database.weapons[id].weaponType == 3)
				{
					if (inventory.inventory[2] == id)
					{
						print("You already have that weapon");
					} 
					else
					{
						if(inventory.inventory[2] != 0)
							Destroy(primaryWeapon);

						Destroy(hit.transform.gameObject);

						inventory.inventory[2] = id;
						meleeWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[2].transform.position, inventory.weaponSlot[2].transform.rotation, inventory.weaponSlot[2].transform);
						Destroy(meleeWeapon.GetComponent<Rigidbody>());
					}
				}
			}
		}
	}

	GameObject GetCurrentWeaponGO()
	{
			switch (wp.weaponSelected)
			{
				case 1:
					return wp.primary.transform.GetChild(0).gameObject;
				case 2:
					return wp.secondary.transform.GetChild(0).gameObject;
				case 3:
					return wp.melee.transform.GetChild(0).gameObject;
				default:
					return null;
			}
	}

	Weapon GetCurrentWeapon()
	{
		int weaponID = -1;
		if (wp.weaponSelected == 1 && wp.primary.GetComponentInChildren<ItemID>() != null)
			weaponID = wp.primary.GetComponentInChildren<ItemID>().itemID;
		if (wp.weaponSelected == 2 && wp.secondary.GetComponentInChildren<ItemID>() != null)
			weaponID = wp.secondary.GetComponentInChildren<ItemID>().itemID;
		if (wp.weaponSelected == 3 && wp.melee.GetComponentInChildren<ItemID>() != null)
			weaponID = wp.melee.GetComponentInChildren<ItemID>().itemID;
		if (weaponID < 0)
		{
			return null;
		}

		return database.weapons[weaponID];
	}

	void ThrowCurrentWeapon()
	{
		if (GetCurrentWeapon() != null)
		{
			print("throwing weapon");

			float throwForceAmp = 300f;
			GetCurrentWeaponGO().AddComponent<Rigidbody>();
			GetCurrentWeaponGO().GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForceAmp / GetCurrentWeapon().mass);
			GetCurrentWeaponGO().transform.parent = null;
			inventory.inventory[wp.weaponSelected - 1] = 0;

			print("weapon thrown");

			if (wp.ChangeWeapon(1)) ;
			else if (wp.ChangeWeapon(2)) ;
			else wp.ChangeWeapon(3);
		}
	}

	

}
