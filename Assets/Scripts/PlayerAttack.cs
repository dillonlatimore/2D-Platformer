using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool attacking;
    private float attackTimer;
    private float attackCooldown;

    public Collider2D attackTrigger;

    private Animator anim;

	// Use this for initialization
	void Start () {
        attacking = false;
        attackTimer = 0;
        attackCooldown = 0.3f;

        anim = GetComponent<Animator>();
        attackTrigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown("z") && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            attackTrigger.enabled = true;
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            } 
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
	}
}
