using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public float currentHealth;
	public float maxHealth;

	public float currentStamina;
	public float maxStamina;

	public bool isDead = false;

	void Start()
	{
		currentHealth = maxHealth;
		currentStamina = maxStamina;
	}

	public virtual void Die()
	{
		isDead = true;
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0) Die();
	}

}
