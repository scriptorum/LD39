using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
    public StatusBar power;
    public StatusBar shields;
    private Transform rover;
    private const float fuelRate = 2.0f;
    private const float CHANCE_DROP = 0.1f;

    void Awake()
    {
        rover = transform.Find("/Rover");
    }

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
        if (currentHealth < 0)
            blowUp();
        else
        {
            for (int i = 0; i < amount; i++)
            {
                if (power.amount <= 0)
                    break;

                // TODO Sometimes destroy fuel instead of dropping it?
                else if (Random.Range(0.0f, 1.0f) < CHANCE_DROP)
                {
                    OreTosser.instance.toss(rover.position, 2f);
                    power.amount--;
                }
            }
        }
    }

    private void blowUp()
    {
        Debug.Log("TODO: You blew up!");
        // TOSS ALL CRYSTALS
    }
}