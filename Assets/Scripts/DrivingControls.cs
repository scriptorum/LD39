using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrivingControls : MonoBehaviour
{
	public RoverRotateEvent roverRotate;
	public RoverMoveEvent roverMove;

	private const float TURN_SPEED = 5f;
	private float speed = 8f;
	private float curAngleDeg = 0f;

    void FixedUpdate()
    {
		Vector2 input = new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));

		if(input.x == 0f && input.y == 0f)
		{
			roverMove.Invoke(0f);
			return;
		}

		// Determine desired angle of rover
		float z = Mathf.Atan2(input.x, -input.y);
		float zDeg = z * Mathf.Rad2Deg;

		float tireAngle = Mathf.DeltaAngle(zDeg, curAngleDeg);
		roverRotate.Invoke(tireAngle);

		// Restrict angle by turning speed
		zDeg = Mathf.LerpAngle(curAngleDeg, zDeg, Time.deltaTime * TURN_SPEED);
		z = zDeg * Mathf.Deg2Rad;

		// Recalculate input
		float magnitude = input.magnitude;
		input.x = Mathf.Sin(z) * -magnitude;
		input.y = Mathf.Cos(z) * magnitude;

		// Aim rover
		transform.eulerAngles = new Vector3(0f, 0f, zDeg);
		curAngleDeg = zDeg;		

		// Move rover
		transform.Translate(input.x * Time.deltaTime * speed, input.y * Time.deltaTime * speed, 0, Space.World);
		roverMove.Invoke(magnitude);		
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
