using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class TireControls : MonoBehaviour
{
	private Anim anim;
	private const float MAX_TURN = 30f;

	void Awake()
	{
		anim = GetComponent<Anim>();
	}

	public void onRoverRotate(float angleDeg)
	{
		Debug.Log("Angle:" + angleDeg);

		// Restrict tire angle
		angleDeg = Mathf.Min(MAX_TURN, Mathf.Abs(angleDeg)) * Mathf.Sign(angleDeg);

		// Rotate tire
		transform.localEulerAngles = new Vector3(0f, 0f, -angleDeg);
	}

	public void onRoverMove(float speed)
	{
		if(speed <= 0f)
		{
			if(!anim.paused)
				anim.Pause();
		}
		else
		{
			if(anim.paused)
				anim.Resume();
		}		
	}
}