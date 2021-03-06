﻿using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    public StatusBar power;
    public StatusBar shields;
    public ParticleSystem explosionPrefab;

    private Health health;
    private const float fuelRate = 2.0f;
    private const float CHANCE_DROP = 0.1f;
    private bool moving = false;

    void Awake()
    {
        power = GameObject.FindGameObjectWithTag("power-bar").GetComponent<StatusBar>();
        shields = GameObject.FindGameObjectWithTag("shields-bar").GetComponent<StatusBar>();
        health = GetComponent<Health>();
    }

    void Update()
    {

#if DEBUG
        if (Input.GetKeyDown(KeyCode.F))
        {
            power.amount = power.max;
            shields.amount = shields.max;
            GetComponent<Health>().health = Mathf.FloorToInt(shields.max);
        }
#endif
    }

    public bool outOfFuel()
    {
        return power.amount <= 0;
    }

    public void onMove(float amount)
    {
        if (power.amount <= 0)
        {
            moving = false;
            return;
        }

        float fuel = amount * Time.deltaTime * fuelRate;
        if (fuel > power.amount)
        {
            moving = false;
            power.amount = 0;
            SoundManager.instance.Play("motor-empty");
            Messages.instance.Invoke("outOfFuel", 0.2f);
        }

        else
        {
            if (amount == 0)
            {
                moving = false;
                return;
            }

            if (!moving)
            {
                moving = true;
                // SoundManager.instance.Play("motor-on");
            }

            power.amount -= fuel;
        }
    }

    public void onCollect(Pickup pickup)
    {
        SoundManager.instance.Play("ore-pickup");

        switch (pickup.type)
        {
            case PickupType.Ore:
                if (power.amount == power.max)
                    return;
                power.amount += 1;
                if (power.amount > power.max)
                    power.amount = power.max;
                health.health = Mathf.FloorToInt(power.amount);
                break;

            case PickupType.Shields:
                if (shields.amount == shields.max)
                    return;
                shields.amount += 1;
                if (shields.amount > power.max)
                    shields.amount = power.max;
                break;

            default:
                Debug.Log("WTF");
                break;
        }
    }

    // This is invoked by the Health component, health should be in sync with shields
    public void onTakeDamage(int amount, int currentHealth)
    {
        shields.amount = currentHealth;
        if (currentHealth <= 0)
            blowUp();
        else
        {
            CameraManager.instance.Shake(0.25f, 0.5f);
            SoundManager.instance.Play("rover-damage");

            // Spit out fuel when hit, sometimes
            // TODO Sometimes destroy fuel instead of dropping it?
            for (int i = 0; i < amount; i++)
            {
                if (power.amount <= 0)
                    break;

                else if (Random.Range(0.0f, 1.0f) < CHANCE_DROP)
                {
                    OreTosser.instance.toss(transform.position, PickupType.Ore, 2f);
                    power.amount--;
                }
            }
        }
    }

    private void blowUp()
    {
        CameraManager.instance.Shake(0.75f, 1.5f);
        SoundManager.instance.Play("rover-death");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Config.instance.gameOver = true;
        Messages.instance.Invoke("blowUp", 0.1f);
        gameObject.SetActive(false);
    }
}