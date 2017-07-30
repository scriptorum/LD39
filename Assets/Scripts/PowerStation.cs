﻿using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    public StatusBar power;
    public StatusBar shields;
    private Transform rover;
    private const float fuelRate = 2.0f;
    private const float CHANCE_DROP = 0.1f;
    private bool moving = false;

    void Awake()
    {
        rover = transform.Find("/Game/Rover");
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
            Debug.Log("TODO: Out of power!");
            SoundManager.instance.Play("motor-empty");
        }

        else
        {
            if(amount == 0)
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

    public void onCollectFuel(float amount)
    {
        if (power.amount == power.max)
        {
            Debug.Log("TODO: Respawn fuel");
            return;
        }

        SoundManager.instance.Play("ore-pickup");

        power.amount += amount;
        if (power.amount > power.max)
            power.amount = power.max;

    }

    public void onTakeDamage(int amount, int currentHealth)
    {
        shields.amount = currentHealth;
        if (currentHealth < 0)
            blowUp();
        else
        {
            SoundManager.instance.Play("rover-damage");

            for (int i = 0; i < amount; i++)
            {
                if (power.amount <= 0)
                    break;

                // TODO Sometimes destroy fuel instead of dropping it?
                else if (Random.Range(0.0f, 1.0f) < CHANCE_DROP)
                {
                    if(rover == null || rover.transform == null)
                        rover = transform.Find("/Game/Rover");
                    OreTosser.instance.toss(rover.position, 2f);
                    power.amount--;
                }
            }
        }
    }

    private void blowUp()
    {
        SoundManager.instance.Play("rover-death");
        Debug.Log("TODO: You blew up!");
        // TOSS ALL CRYSTALS
    }
}