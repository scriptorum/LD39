using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    public StatusBar power;
    public StatusBar shields;

    public float fuelRate = 2.0f;

    public void onMove(float amount)
    {
        // inventory.onDrop(PickupType.Ore, amount * Time.deltaTime * fuelRate);
        float fuel = amount * Time.deltaTime * fuelRate;
        if (fuel > power.amount)
        {
            power.amount = 0;
            Debug.Log("TODO: Out of power!");
        }

        else
        {
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

        power.amount += amount;
        if (power.amount > power.max)
            power.amount = power.max;

    }

    public void onTakeDamage(int amount, int currentHealth)
    {
		shields.amount = currentHealth;
		if(currentHealth < 0)
            Debug.Log("TODO: You blew up!");
    }
}