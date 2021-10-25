using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GolemBehaviors { Idle, Chase, Combat, Guard, Die };
public class GolemAI : MonoBehaviour
{
	public GolemBehaviors aiBehaviors = GolemBehaviors.Idle;

	public bool isSuspicious = false;
	public bool isInRange = false;
	public bool FightsRanged = false;

	GameObject player;

	NavMeshAgent navAgent;
	Vector3 Destination;
	float Distance;
	public Transform[] Waypoints;
	public int curWaypoint = 0;
	bool ReversePath = false;
	Animator anim;
	public float health = 100f;
	float targetTime = 0.9f;
	float damg = 10f;

	public EnemyHealthSystem EHS;

	#region Behaviors
	void RunBehaviors()
	{
		switch (aiBehaviors)
		{
			case GolemBehaviors.Idle:
				RunIdleNode();
				break;
			case GolemBehaviors.Chase:
				RunChaseNode();
				break;
			case GolemBehaviors.Combat:
				RunCombatNode();
				break;
			case GolemBehaviors.Die:
				RunDieNode();
				break;
			case GolemBehaviors.Guard:
				RunGuardNode();
				break;
		}
	}

	void ChangeBehavior(GolemBehaviors newBehavior)
	{
		aiBehaviors = newBehavior;

		RunBehaviors();
	}

	void RunIdleNode()
	{
		Idle();
	}

	void RunChaseNode()
	{
		Chase();
	}

	void RunCombatNode()
	{
		Combat();
	}

	void RunDieNode()
	{
		Die();
	}
	void RunGuardNode()
    {
		Guard();
    }
	#endregion

	#region Actions
	void Idle()
	{
		anim.SetBool("isIdle", true);
		anim.SetBool("Attacking", false);
		anim.SetBool("isDead", false);
		anim.SetBool("Running", false);
		Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (Distance > 10f)
		{
			ChangeBehavior(GolemBehaviors.Guard);
		}
	}

	void Chase()
	{
		if (Distance <= 10f && Distance > 2f)
		{
			anim.SetBool("isIdle", false);
			anim.SetBool("Attacking", false);
			anim.SetBool("Running", true);
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
			Destination = player.transform.position;
			navAgent.SetDestination(Destination);
		}
		else if (Distance <= 2f)
		{
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
			navAgent.ResetPath();
			ChangeBehavior(GolemBehaviors.Combat);
		}
		else
		{
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
			navAgent.ResetPath();
			isSuspicious = false;
			ChangeBehavior(GolemBehaviors.Guard);
		}
	}

	void Combat()
	{
		if (Distance <= 2f)
		{
			MeleeAttack();
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
		}
		else
		{
			ChangeBehavior(GolemBehaviors.Chase);
		}
	}

	void Die()
	{
		navAgent.ResetPath();
		anim.SetBool("isDead", true);
		Destroy(this, 5f);
	}
	void MeleeAttack()
	{
		anim.SetBool("isIdle", false);
		anim.SetBool("Attacking", true);
		anim.SetBool("Running", false);
		if (targetTime <= 0f)
		{
			player.GetComponent<HealthSystem>().DealDamage(damg);
			targetTime += 1.8f;
		}
		targetTime -= Time.deltaTime;
	}

	void Guard()
    {
		Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (Distance <= 10f)
		{
			ChangeBehavior(GolemBehaviors.Chase);
		}
		else
        {
			Patrol();
        }
    }
	void Patrol()
	{
		anim.SetBool("isIdle", false);
		anim.SetBool("Attacking", false);
		anim.SetBool("Running", true);
		Distance = Vector3.Distance(gameObject.transform.position, Waypoints[curWaypoint].position);
		if (Distance > 2.00f)
		{
			Destination = Waypoints[curWaypoint].position;
			navAgent.SetDestination(Destination);
		}
		else
		{
			if (ReversePath)
			{
				if (curWaypoint <= 0)
				{
					ReversePath = false;
				}
				else
				{
					curWaypoint--;
					Destination = Waypoints[curWaypoint].position;
				}
			}
			else
			{
				if (curWaypoint >= Waypoints.Length - 1)
				{
					ReversePath = true;
				}
				else
				{
					curWaypoint++;
					Destination = Waypoints[curWaypoint].position;
				}
			}
		}
	}

	#endregion

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		anim = this.GetComponent<Animator>();
		navAgent = this.GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
		if (EHS.dead == true)
		{
			health = 0;
			ChangeBehavior(GolemBehaviors.Die);
		}
		RunBehaviors();
	}
}
