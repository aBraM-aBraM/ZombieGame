using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public int weaponSelected = 1;

	[SerializeField]
	public GameObject primary, secondary, melee;

	public Animator anim;

	void Start()
	{
		anim = GetComponentInChildren<Animator>();
		StartCoroutine(SwapWeapon(1,true));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			if (weaponSelected != 1)
			{
				StartCoroutine(SwapWeapon(1));
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			if (weaponSelected != 2)
			{
				StartCoroutine(SwapWeapon(2));
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			if (weaponSelected != 3)
			{
				StartCoroutine(SwapWeapon(3));
			}
		}
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
