using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum FriendBehaviors { Idle, Chase, Combat, Guard };
public class FriendAI : MonoBehaviour
{
    GameObject player;
    Animator anim;

	public FriendBehaviors aiBehaviors = FriendBehaviors.Idle;

	NavMeshAgent navAgent;
	Vector3 Destination;
	float Distance;
	float EnemyDistance;
	float DistanceToEnemy;
	GameObject enemyCurrent;
	float targetTime = 0.9f;
	float damg = 5;

	#region Behaviors
	void RunBehaviors()
	{
		switch (aiBehaviors)
		{
			case FriendBehaviors.Idle:
				RunIdleNode();
				break;
			case FriendBehaviors.Chase:
				RunChaseNode();
				break;
			case FriendBehaviors.Combat:
				RunCombatNode();
				break;
			case FriendBehaviors.Guard:
				RunGuardNode();
				break;
		}
	}

	void ChangeBehavior(FriendBehaviors newBehavior)
	{
		aiBehaviors = newBehavior;

		checkDistance();
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
		if (Distance > 3f && EnemyDistance > 5f)
		{
			ChangeBehavior(FriendBehaviors.Guard);
		}
		else if (EnemyDistance <= 5f && enemyCurrent.GetComponent<EnemyHealthSystem>().dead == false)
        {
			ChangeBehavior(FriendBehaviors.Chase);
        }
	}

	void Chase()
	{
		if (enemyCurrent.GetComponent<EnemyHealthSystem>().dead == false)
		{
			if (DistanceToEnemy <= 5f && DistanceToEnemy > 2f && Distance <= 5f && EnemyDistance <= 5f)
			{
				anim.SetBool("isIdle", false);
				anim.SetBool("Attacking", false);
				anim.SetBool("Running", true);
				navAgent.SetDestination(enemyCurrent.transform.position);
			}
			else if (DistanceToEnemy <= 2f && Distance <= 5f && EnemyDistance <= 5f)
			{
				navAgent.ResetPath();
				ChangeBehavior(FriendBehaviors.Combat);
			}
			else if (EnemyDistance > 5f || Distance > 5f)
			{
				navAgent.ResetPath();
				ChangeBehavior(FriendBehaviors.Guard);
			}
		}
        else
        {
			ChangeBehavior(FriendBehaviors.Guard);
        }
	}

	void Combat()
	{
		checkDistance();
		if (DistanceToEnemy <= 2f && enemyCurrent.GetComponent<EnemyHealthSystem>().dead == false)
		{
			MeleeAttack();
			//Debug.Log("ATTACK!!!!");
		}
		else
		{
			ChangeBehavior(FriendBehaviors.Chase);
		}
	}

	void Guard()
    {
		if(Distance > 3f && EnemyDistance > 5f)
		{
			Destination = player.transform.position;
			navAgent.SetDestination(Destination);
			anim.SetBool("isIdle", false);
			anim.SetBool("Running", true);
			anim.SetBool("Attacking", false);
		}
		else if (Distance <= 3f && EnemyDistance > 5f)
		{
			navAgent.ResetPath();
			ChangeBehavior(FriendBehaviors.Idle);
		}
		else if(EnemyDistance <= 5f && Distance <= 5f && enemyCurrent.GetComponent<EnemyHealthSystem>().dead == false)
        {
			navAgent.ResetPath();
			ChangeBehavior(FriendBehaviors.Chase);
		}
	}
	void MeleeAttack()
	{
		if (enemyCurrent.GetComponent<EnemyHealthSystem>().dead == false)
		{
			anim.SetBool("Running", false);
			anim.SetBool("Attacking", true);
			anim.SetBool("isIdle", false);
			if (targetTime <= 0f)
			{
				Debug.Log("Friend is Helping :)");
				enemyCurrent.GetComponent<EnemyHealthSystem>().DealDamage(damg);
				targetTime += 1.8f;
			}
			targetTime -= Time.deltaTime;
		}
        else
        {
			ChangeBehavior(FriendBehaviors.Guard);
        }
	}
	#endregion
	// Start is called before the first frame update
	void Start()
    {
        navAgent = this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		checkDistance();
		RunBehaviors();
	}

	public void SetCurrentEnemy(GameObject enemy)
    {
		if(enemy == null) { return; }
		enemyCurrent = enemy;
    }

	void checkDistance()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		Distance = 0;
		EnemyDistance = 0;
		DistanceToEnemy = 0;
		//enemyCurrent = GameObject.FindGameObjectWithTag("Enemy");
		Distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        EnemyDistance = Vector3.Distance(player.transform.position, enemyCurrent.transform.position);
		DistanceToEnemy = Vector3.Distance(this.gameObject.transform.position, enemyCurrent.transform.position);
	}
}
