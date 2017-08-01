using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class BruteAttack : MonoBehaviour
{
    private Health roverHealth;
    private Damage attackerDamage;
    private const float ATTACKING_RANGE = 1.25f;
    private const float FORGET_RANGE = 8f;
    private const float SPEED = 2f;
    private const float JITTER = 0.05f;
    private const float CHARGE_MAX = .3f;
    private const float STUN_MAX = 0.1f;
    private float attackCharge = 0f;
    private Anim anim;
    private Vector3 oldPos;
    private float stunTimer = 0f;

    void Awake()
    {
        attackerDamage = GetComponent<Damage>();
        anim = GetComponent<Anim>();
        oldPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Config.instance.gamePaused)
            return;

        if(roverHealth == null)
        {
            GameObject game = GameObject.Find("/Game");
            if(game != null)
            {
                Transform rover = game.transform.Find("Rover");
                if(rover == null)
                    return;
                roverHealth = rover.GetComponent<Health>();
            }
        }

        stunTimer -= Time.fixedDeltaTime;
        if(stunTimer > 0)
            return;

        Vector3 diff = transform.position - roverHealth.transform.position;
        float magnitude = diff.magnitude;

        if (magnitude > FORGET_RANGE)
            idle();    
        else if (magnitude < ATTACKING_RANGE)
            attack();
        else chase(diff);
    }

    void idle()
    {
        anim.Play("idle");
    }

    private void attack()
    {
        attackCharge += Time.fixedDeltaTime;
        if (attackCharge < CHARGE_MAX)
            return;

        anim.Play("attack");
        SoundManager.instance.Play("brute-attack");
        attackCharge = 0f;
        roverHealth.takeDamage(attackerDamage);
    }

    private void chase(Vector3 diff)
    {
        anim.Play("walk");
        diff.Normalize();
        float speed = SPEED * Time.fixedDeltaTime;
        oldPos = transform.position;
        transform.Translate((speed + Random.Range(-JITTER, JITTER)) * -diff.x, (speed + Random.Range(-JITTER, JITTER)) * -diff.y, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "solid")
        {
            idle();
            transform.position = oldPos;
            stunTimer = Random.Range(0.0f, STUN_MAX);
        }        
    }
}