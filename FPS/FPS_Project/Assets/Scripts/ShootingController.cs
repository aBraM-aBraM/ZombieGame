using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour {

	WeaponScript wp;
	Camera cam;
	ItemDatabase database;
	Animator anim;

	bool isReloading = false;
	float nextTimeToShoot = 0;

	void Start()
	{
		wp = GetComponent<WeaponScript>();
		cam = GetComponentInChildren<Camera>();
		database = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemDatabase>();
		anim = GetComponentInChildren<Animator>();
		
	}

	void Update()
	{
		HandleInput();
	}

	void Shoot()
	{
		Weapon currentWeapon = GetCurrentWeapon();
		if (currentWeapon == null) return;
		

		Ray ray = new Ray(cam.transform.position, cam.transform.forward);
		RaycastHit hit;

		if (currentWeapon.currentMag > 0 && !isReloading && Time.time > nextTimeToShoot && currentWeapon.isGun)
		{
			anim.SetTrigger("attackTrigger");
			currentWeapon.currentMag--;
			nextTimeToShoot = Time.time + (60 / currentWeapon.fireRate);

			print("currentAmmo: " + currentWeapon.currentMag);

			if (Physics.Raycast(ray, out hit, currentWeapon.range))
			{

				if (hit.transform.gameObject.GetComponent<CharacterStats>() != null)
				{
					hit.transform.gameObject.GetComponent<CharacterStats>().TakeDamage(currentWeapon.damage);
				}
			}
			else
			{
			}
		}
		else if (!currentWeapon.isGun && Time.time > nextTimeToShoot)
		{
			anim.SetTrigger("attackTrigger");
			nextTimeToShoot = Time.time + (1 / currentWeapon.fireRate);
		}
	}
	
	IEnumerator Reload()
	{

		bool canReload = true;
		Weapon currentWeapon = GetCurrentWeapon();
		if (currentWeapon == null) canReload = false;
		else if (!currentWeapon.isGun) canReload = false;
		else if (isReloading) canReload = false;


		if (canReload && currentWeapon.currentMag != currentWeapon.magSize)
		{
			print("Reloading");
			print(currentWeapon.currentMag);

			isReloading = true;

			anim.SetTrigger("reloadTrigger");
			GetCurrentWeaponAnimator().SetTrigger("reloadTrigger");
			
			yield return new WaitForSeconds(currentWeapon.reloadSpeed);

			int magSize = currentWeapon.magSize;
			currentWeapon.ammo -= magSize - currentWeapon.currentMag;
			currentWeapon.currentMag = magSize;

			isReloading = false;

			print(currentWeapon.currentMag);
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
			print("weaponID");
			print("you aren't holding a weapon");
			return null;
		}

		return database.weapons[weaponID];
	}

	Animator GetCurrentWeaponAnimator()
	{
		switch (wp.weaponSelected)
		{
			case 1:
				return wp.primary.GetComponentInChildren<Animator>();
			case 2:
				return wp.secondary.GetComponentInChildren<Animator>();
			case 3:
				return wp.melee.GetComponentInChildren<Animator>();
			default:
				return null;
		}
	}

	void HandleInput()
	{
		if(GetCurrentWeapon() != null)
		{
			if (!GetCurrentWeapon().isAutomatic)
			{
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					Shoot();
				}
				else if (Input.GetKeyDown(KeyCode.R))
				{
					StartCoroutine(Reload());
				}
			}
			if (GetCurrentWeapon().isAutomatic)
			{
				if (Input.GetKey(KeyCode.Mouse0))
				{
					Shoot();
				}
				else if (Input.GetKeyDown(KeyCode.R))
				{
					StartCoroutine(Reload());
				}
			}
		}

	}
}
