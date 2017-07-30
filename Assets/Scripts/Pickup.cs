using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public bool available = false;
    private Transform rover;
    private const float MAGNET_DISTANCE = 3.0f;
    private const float MAGNET_SPEED = 5.0f;
    private const float MAGNET_CHARGE = 0.6f;
    private float lifetime = 0f;

    void FixedUpdate()
    {
        lifetime += Time.deltaTime;
        if(lifetime < MAGNET_CHARGE)
            return;
        available = true;

        if (rover == null || rover.transform == null)
            rover = transform.Find("/Game/Rover");

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
    Shields, Ore
}