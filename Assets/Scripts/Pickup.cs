using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public PickupType type;
}

public enum PickupType
{
	Ore
}