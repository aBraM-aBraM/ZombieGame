using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour {

	float pickupRange = 2f;

	int pickupLayerMask;

	GameObject gameController;

	GameObject primaryWeapon, secondaryWeapon, meleeWeapon;

	Camera cam;

	[SerializeField]
	ItemDatabase database;
	[SerializeField]
	PlayerInventory inventory;

	void Start()
	{
		cam = GetComponent<Camera>();
		gameController = GameObject.FindGameObjectWithTag("GameController");
		database = gameController.GetComponent<ItemDatabase>();
		inventory = gameController.GetComponent<PlayerInventory>();

		pickupLayerMask = LayerMask.GetMask("Pickup");
	}

	void Update()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;

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

						inventory.inventory[0] = id;
						primaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[0].transform.position, inventory.weaponSlot[0].transform.rotation,inventory.weaponSlot[0].transform);
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

						inventory.inventory[1] = id;
						secondaryWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[1].transform.position, inventory.weaponSlot[1].transform.rotation, inventory.weaponSlot[1].transform);
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
							
						inventory.inventory[2] = id;
						meleeWeapon = Instantiate(database.weapons[id].weaponObject, inventory.weaponSlot[2].transform.position, inventory.weaponSlot[2].transform.rotation, inventory.weaponSlot[2].transform);
					}
				}
			}
		}
	}
}
