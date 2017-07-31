using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class Container : MonoBehaviour
{
    public ParticleSystem hitFX;
    public GameObject deathPrefab;
    public string hitSound;
    public string deathSound;
    public int hitFXAmount = 30;
    public int contentsCount = 6;
    public bool detachChildrenWhenDying = true;
    public PickupType type;

    public void OnDamage(int damage, int healthRemaining)
    {
        hitFX.Emit(hitFXAmount);

        if (healthRemaining <= 0)
        {
            if (deathSound != "" && deathSound != null)
                SoundManager.instance.Play(deathSound);

            while (contentsCount-- > 0)
                OreTosser.instance.toss(transform.position, type);

            if (detachChildrenWhenDying)
                transform.DetachChildren();
            Destroy(gameObject);

            if (deathPrefab != null)
                Instantiate(deathPrefab, transform.position, Quaternion.identity);
        }
        else SoundManager.instance.Play(hitSound);
    }
}