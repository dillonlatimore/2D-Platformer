using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float moveSpeed;
    public Transform[] patrolPoints;
    public Transform leftLimit;
    public Transform rightLimit;
    private Transform currentPatrolPoint;
    public Transform target;
    public float chaseRange;
    private float distanceToTarget;

    public GameObject bloodSpatter;
    private Animator anim;

    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;

    public int startHealth;
    public int currentHealth;

    private Rigidbody2D myRigidbody;

    private LevelManager theLevelManager;

    public Collider2D attackCollider;

	// Use this for initialization
	void Start () {
        
        myRigidbody = GetComponent<Rigidbody2D>();
        currentPatrolPoint = patrolPoints[0];
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        theLevelManager = GetComponent<LevelManager>();
        damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
        //****Patrolling AI****//

        // If current patrol point is patrol point 1 then face and move towards it, if it is patrol point 2 turn and face it
        // then move towards it.
        if(currentPatrolPoint == patrolPoints[0])
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        

        // Check if the patrol point has been reached, once it has swap to the other patrol point.
        if(Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.3f)
        {
            // Patrol point reached
            if(currentPatrolPoint == patrolPoints[0])
            {
                currentPatrolPoint = patrolPoints[1];
            }
            else
            {
                currentPatrolPoint = patrolPoints[0];
            }
        }

        //****Chasing AI****//

        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if(distanceToTarget < chaseRange && leftLimit.position.x < transform.position.x && rightLimit.position.x > transform.position.x)
        {
            
            // Start chasing player
            Vector3 targetDir = target.position - transform.position;
            Debug.Log(targetDir);
            if(targetDir.x < 0)
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            } else if(targetDir.x > 0)
            {
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        //****Attacking AI****//

        //if(distanceToTarget < attackRange)
        //{
        //    if(Time.time > lastAttackTime + attackDelay)
        //    {
        //        theLevelManager.HurtPlayer(damage);
        //        lastAttackTime = Time.time;
        //    }

            
        //}

        if (currentHealth <= 0)
        {
            Instantiate(bloodSpatter, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            
        }

        anim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "KillPlane")
        {
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            theLevelManager.HurtPlayer(damage);
        }

        
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;
    }

    
}
