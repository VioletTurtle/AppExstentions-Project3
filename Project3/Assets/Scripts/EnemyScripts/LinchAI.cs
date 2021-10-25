using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Behaviors { Idle, Chase, Combat, Die};
public class LinchAI : MonoBehaviour
{
    public Behaviors aiBehaviors = Behaviors.Idle;

    public bool isSuspicious = false;
    public bool isInRange = false;
    public bool FightsRanged = false;

    public GameObject Projectile;
	GameObject player;
	public Transform target;

    NavMeshAgent navAgent;
    Vector3 Destination;
    float Distance;
	Animator anim;
	public float health;
	float targetTime = 0.9f;
	public EnemyHealthSystem EHS;

	#region Behaviors
	void RunBehaviors()
	{
		switch (aiBehaviors)
		{
			case Behaviors.Idle:
				RunIdleNode();
				break;
			case Behaviors.Chase:
				RunChaseNode();
				break;
			case Behaviors.Combat:
				RunCombatNode();
				break;
			case Behaviors.Die:
				RunDieNode();
				break;
		}
	}

	void ChangeBehavior(Behaviors newBehavior)
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
	#endregion

	#region Actions
	void Idle()
	{
		anim.SetBool("isIdle", true);
		anim.SetBool("Attacking", false);
		anim.SetBool("isDead", false);
		anim.SetBool("Running", false);
		Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (Distance <= 10f)
		{
			ChangeBehavior(Behaviors.Chase);
		}
	}

	void Chase()
	{
		if (Distance <= 10f && Distance > 5f)
		{
			anim.SetBool("isIdle", false);
			anim.SetBool("Attacking", false);
			anim.SetBool("Running", true);
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
			Destination = player.transform.position;
			navAgent.SetDestination(Destination);
		}
		else if (Distance < 5f)
		{
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
			navAgent.ResetPath();
			ChangeBehavior(Behaviors.Combat);
		}
		else
		{
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
			navAgent.ResetPath();
			isSuspicious = false;
			ChangeBehavior(Behaviors.Idle);
		}
	}

	void Combat()
	{
		if (Distance <= 5f)
		{
			RangedAttack();
			Distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
		}
		else
		{
			ChangeBehavior(Behaviors.Chase);
		}
	}

	void Die()
	{
		navAgent.ResetPath();
		anim.SetBool("isDead", true);
		Destroy(this, 5f);
	}
	void RangedAttack()
	{
		anim.SetBool("isIdle", false);
		anim.SetBool("Attacking", true);
		anim.SetBool("Running", false);
		if (targetTime <= 0f)
        {
			GameObject instantiatedProjectile = Instantiate(Projectile,
				new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z),
				target.transform.rotation) as GameObject;
			instantiatedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * (5 * 60));

			Destroy(instantiatedProjectile, 5f);

			targetTime += 1.8f;
		}
		targetTime -= Time.deltaTime;
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
			ChangeBehavior(Behaviors.Die);
		}
		RunBehaviors();
	}
}
