using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrivingControls : MonoBehaviour
{
	public RoverRotateEvent roverRotate;
	public RoverMoveEvent roverMove;
	public PickupEvent roverPickup;

	private const float TURN_SPEED = 5f;
	private const float KNOCKBACK = 0.5f;
	private float speed = 8f;
	private float curAngleDeg = 0f;
	private Vector2 lastMove;
	private float stunTimer = 0f;

    void FixedUpdate()
    {

		lastMove = new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));

		if(lastMove.x == 0f && lastMove.y == 0f)
		{
			roverMove.Invoke(0f);
			return;
		}

		// Determine desired angle of rover
		float z = Mathf.Atan2(lastMove.x, -lastMove.y);
		float zDeg = z * Mathf.Rad2Deg;

		float tireAngle = Mathf.DeltaAngle(zDeg, curAngleDeg);
		roverRotate.Invoke(tireAngle);

		// Restrict angle by turning speed
		zDeg = Mathf.LerpAngle(curAngleDeg, zDeg, Time.deltaTime * TURN_SPEED);
		z = zDeg * Mathf.Deg2Rad;

		// Recalculate input
		float magnitude = lastMove.magnitude;
		lastMove.x = Mathf.Sin(z) * -magnitude;
		lastMove.y = Mathf.Cos(z) * magnitude;

		// Aim rover
		transform.eulerAngles = new Vector3(0f, 0f, zDeg);
		curAngleDeg = zDeg;		

		// Move rover
		if(stunTimer <= 0f)
		{
			transform.Translate(lastMove.x * Time.deltaTime * speed, lastMove.y * Time.deltaTime * speed, 0, Space.World);
			roverMove.Invoke(magnitude);		
		}
		else stunTimer -= Time.deltaTime;
    }


	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "solid")
		{
			lastMove.Normalize();
			transform.Translate(lastMove.x * -KNOCKBACK, lastMove.y * -KNOCKBACK, 0, Space.World);
			stunTimer = 0.5f;
		}

		else if(other.tag == "cloud")
		{
			Debug.Log("Cloud!");
		}

		Pickup pickup = other.GetComponent<Pickup>();
		if(pickup != null)
		{
			roverPickup.Invoke(pickup);
			GameObject.Destroy(pickup.gameObject);
		}
	}
	
}

[System.Serializable]
public class RoverRotateEvent : UnityEvent<float> // angleDeg
{
}

[System.Serializable]
public class RoverMoveEvent : UnityEvent<float> // speed
{
}

[System.Serializable]
public class PickupEvent: UnityEvent<Pickup>
{
}