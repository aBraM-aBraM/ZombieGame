using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	void Start()
	{
		maxHealth = 100;
		currentHealth = maxHealth;

		maxStamina = 100;
		currentStamina = maxStamina;
	}

	public override void Die()
	{
		Debug.Log("dead af");
	}
}
