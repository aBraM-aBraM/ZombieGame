using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

	NavMeshAgent agent;

	Transform target;

	Animator anim;


	[SerializeField]
	private float lookDistance;
	[SerializeField]
	private float stoppingDistance;
	[SerializeField]
	private float rotSpeed;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		target = GetTarget();
	}

	void Update()
	{
		Work();
	}

	void Work()
	{

		target = GetTarget();
		anim.SetFloat("velocity", agent.velocity.magnitude);

		if (target == null) return;


		float dist = Vector3.Distance(transform.position, target.position);

		if (dist <= lookDistance)
		{
			if (dist < stoppingDistance)
			{
				StopMoving();
				FaceTarget();
			}
			else
			{
				GoToTarget();
			}
		}
	}


	Transform GetTarget()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, lookDistance);
		for (int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].GetComponent<WeaponScript>() != null)
			{
				return colliders[i].transform;
			}
		}
		return null;
	}

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRot , Time.deltaTime * rotSpeed);
	}

	void GoToTarget()
	{
		agent.isStopped = false;
		agent.SetDestination(target.position);
		anim.SetBool("attackRange", false);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookDistance);
	}

	void StopMoving()
	{
		agent.isStopped = true;
		anim.SetBool("attackRange", true);
	}
}
