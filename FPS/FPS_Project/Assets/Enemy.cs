using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

	NavMeshAgent agent;

	GameObject target;

	Animator anim;

	[SerializeField]
	private float stoppingDistance;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		anim.SetFloat("velocity", agent.velocity.magnitude);
		float dist = Vector3.Distance(transform.position, target.transform.position);
		if (dist < stoppingDistance)
		{
			StopMoving();
		}
		else
		{
			GoToTarget();
		}
	}

	void GoToTarget()
	{
		agent.isStopped = false;
		agent.SetDestination(target.transform.position);
		anim.SetBool("attackRange", false);
	}

	void StopMoving()
	{
		agent.isStopped = true;
		anim.SetBool("attackRange", true);
	}
}
