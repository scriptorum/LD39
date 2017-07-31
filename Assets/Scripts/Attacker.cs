using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Health roverHealth;
    private Damage attackerDamage;
    private const float ATTACKING_RANGE = 1.25f;
    private const float FORGET_RANGE = 8f;
    private const float SPEED = 2f;
    private const float JITTER = 0.05f;
    private const float CHARGE_MAX = .3f;
    private float attackCharge = 0f;
    private Anim anim;

    void Awake()
    {
        attackerDamage = GetComponent<Damage>();
        anim = GetComponent<Anim>();
    }

    void FixedUpdate()
    {
        if (Config.instance.gamePaused)
            return;

        if (roverHealth == null || roverHealth.transform == null)
            roverHealth = GameObject.Find("/Game/Rover").GetComponent<Health>();

        if(roverHealth == null)
            return;

        Vector3 diff = transform.position - roverHealth.transform.position;
        float magnitude = diff.magnitude;

        if (magnitude > FORGET_RANGE)
            return;

        if (magnitude < ATTACKING_RANGE)
            attack();
        else chase(diff);
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
        anim.Play("idle");
        diff.Normalize();
        float speed = SPEED * Time.fixedDeltaTime;
        transform.Translate((speed + Random.Range(-JITTER, JITTER)) * -diff.x, (speed + Random.Range(-JITTER, JITTER)) * -diff.y, 0f);
    }
}