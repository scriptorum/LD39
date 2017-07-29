using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingControls : MonoBehaviour
{
	private float speed = 8f;

    void Update()
    {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if(h == 0f && v == 0f)
			return;

		// Aim rover
		float z = Mathf.Atan2(h, -v) * Mathf.Rad2Deg - 180f;
		transform.eulerAngles = new Vector3(0f, 0f, z);

		// Move rover
		transform.Translate(h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0, Space.World);
    }
}