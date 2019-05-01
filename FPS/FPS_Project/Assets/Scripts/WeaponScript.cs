using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public int weaponSelected = 1;

	[SerializeField]
	GameObject gameController; 

	PlayerInventory inventory;

	[SerializeField]
	public GameObject primary, secondary, melee;

	public Animator anim;

	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		StartCoroutine(SwapWeapon(3,true));
		inventory = gameController.GetComponent<PlayerInventory>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			ChangeWeapon(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			ChangeWeapon(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			ChangeWeapon(3);
		}
	}

	public bool ChangeWeapon(int index)
	{
		if(weaponSelected != index && inventory.inventory[index - 1] != 0)
		{
			StartCoroutine(SwapWeapon(index));
			return true;
		}
		if (weaponSelected != index && index == 3)
		{
			StartCoroutine(SwapWeapon(index));
			return true;
		}
		return false;
	}

	IEnumerator SwapWeapon(int weaponType,bool start = false)
	{
		switch (weaponType)
		{
			case 1:
				anim.SetInteger("weaponType", weaponType);
				if (!start)
					anim.SetTrigger("weaponSwitchTrigger");

				yield return new WaitForSeconds(1f);

				primary.SetActive(true);
				secondary.SetActive(false);
				melee.SetActive(false);

				weaponSelected = weaponType;
				break;
			case 2:
				anim.SetInteger("weaponType", 2);
				if (!start)
					anim.SetTrigger("weaponSwitchTrigger");

				yield return new WaitForSeconds(1f);

				primary.SetActive(false);
				secondary.SetActive(true);
				melee.SetActive(false);

				weaponSelected = weaponType;
				break;
			case 3:
				anim.SetInteger("weaponType", weaponType);
				if (!start)
					anim.SetTrigger("weaponSwitchTrigger");

				
				yield return new WaitForSeconds(1f);


				primary.SetActive(false);
				secondary.SetActive(false);
				melee.SetActive(true);

				weaponSelected = weaponType;
				break;
		}
		// down animation time
	}
}
