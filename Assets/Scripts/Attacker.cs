﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Health roverHealth;
	private Damage attackerDamage;
    private bool attacking = false;
    private const float ATTACKING_RANGE = 1.25f;
	private const float FORGET_RANGE = 10f;
    private const float SPEED = 2f;
	private const float JITTER = 0.05f;
	private const float CHARGE_MAX = .3f;
	private float attackCharge = 0f;
    
	void Awake()
    {
		roverHealth = GameObject.Find("/Rover").GetComponent<Health>();
		attackerDamage = GetComponent<Damage>();
    }

    void FixedUpdate()
    {
        Vector3 diff = transform.position - roverHealth.transform.position;
		float magnitude = diff.magnitude;

		if(magnitude > FORGET_RANGE)
			return;

        if (magnitude < ATTACKING_RANGE)
            attack();
        else chase(diff);
    }

    private void attack()
    {
		attackCharge += Time.fixedDeltaTime;
		if(attackCharge < CHARGE_MAX)
			return;
		attackCharge = 0f;
        roverHealth.takeDamage(attackerDamage);
    }

    private void chase(Vector3 diff)
    {
        diff.Normalize();
        float speed = SPEED * Time.fixedDeltaTime;
        transform.Translate((speed + Random.Range(-JITTER, JITTER)) * -diff.x, (speed + Random.Range(-JITTER, JITTER)) * -diff.y, 0f);
    }
}