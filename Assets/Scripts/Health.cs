using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public DamageEvent onDamage;
    public int health = 100;
    public bool dead = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "damage")
        {
            Damage damage = other.gameObject.GetComponent<Damage>();
            if(gameObject.name == "Rover" && damage.originatedFromPlayer)
                return;
            takeDamage(damage);
        }
    }

    public void takeDamage(Damage damage)
    {
        health -= damage.damage;

        onDamage.Invoke(damage.damage, health);

        if (health < 0)
            dead = true;
    }
}

[System.Serializable]
public class DamageEvent : UnityEvent<int, int>
{ }