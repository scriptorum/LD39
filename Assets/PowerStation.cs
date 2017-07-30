using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
	public Inventory inventory;
	private const float FUEL_RATE = 1.0f;

	public void onMove(float amount)
	{
		inventory.onDrop(PickupType.Ore, amount * Time.deltaTime * FUEL_RATE);
	}
}