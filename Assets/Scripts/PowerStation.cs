using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStation : MonoBehaviour
{
	public Inventory inventory;
	public float fuelRate = 2.0f;

	public void onMove(float amount)
	{
		inventory.onDrop(PickupType.Ore, amount * Time.deltaTime * fuelRate);
	}
}