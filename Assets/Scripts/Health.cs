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
            if (gameObject.name == "Rover" && damage.type == DamageType.Player)
                return;
            if (gameObject.name == "Saucer" && damage.type == DamageType.Enemy)
                return;
            takeDamage(damage);
        }
    }

    public void takeDamage(Damage damage)
    {
        takeDamage(damage.damage);
    }

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            dead = true;
            health = 0;
        }

        onDamage.Invoke(amount, health);
    }
}

[System.Serializable]
public class DamageEvent : UnityEvent<int, int> { }