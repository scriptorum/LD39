using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Support mouse if no joystick

public class TurretControls : MonoBehaviour
{

    void Update()
    {
		float h = Input.GetAxis("Horizontal2");
		float v = Input.GetAxis("Vertical2");


		// Aim turret
		if(h != 0f || v != 0f)
		{
		Debug.Log("Joy2 input:" + h +"," + v);
			float z = Mathf.Atan2(-h, -v) * Mathf.Rad2Deg;
			transform.eulerAngles = new Vector3(0f, 0f, z);
		}

		// Check for fire
    }
}