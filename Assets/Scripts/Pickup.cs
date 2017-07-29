using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType type;
    private Transform rover;
    private const float MAGNET_DISTANCE = 2.0f;
    private const float MAGNET_SPEED = 5.0f;

    void Update()
    {
        if (rover == null)
            rover = transform.Find("/Rover");

		Vector3 diff = transform.position - rover.transform.position;
        if (diff.magnitude < MAGNET_DISTANCE)
        { 
			diff.Normalize();
			transform.Translate(-diff * Time.deltaTime * MAGNET_SPEED);
		}
    }
}

public enum PickupType
{
    Ore
}