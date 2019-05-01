using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour {

	NavMeshAgent agent;

	GameObject target;

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
		target = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		anim.SetFloat("velocity", agent.velocity.magnitude);
		float dist = Vector3.Distance(transform.position, target.transform.position);
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

	void FaceTarget()
	{
		Vector3 direction = (target.transform.position - transform.position).normalized;
		Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRot , Time.deltaTime * rotSpeed);
	}

	void GoToTarget()
	{
		agent.isStopped = false;
		agent.SetDestination(target.transform.position);
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
