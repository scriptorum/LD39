using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{
	// If you have only one CameraShake instance, you can access it this way
	public static CameraShake instance;
	public float strength = 0.1f;
	public float duration = 0.25f;
	public bool fadeStrength = true;

	private float curStrength;
	private float curDuration;
	private bool fading;
	private float timeRemaining = 0;
	private Vector3 startPosition;

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		#if DEBUG
		if(Input.GetKey(KeyCode.C))
			Run();
		#endif

		if(timeRemaining > 0)
		{
			timeRemaining -= Time.deltaTime;
			if(timeRemaining < 0)
				transform.position = startPosition;
			else 
			{
				float str = fading ? curStrength * timeRemaining / curDuration  : curStrength;
				transform.position = new Vector3(Random.Range(-str, str), Random.Range(-str, str), 0f);
			}
		}	
	}

	public void Run()
	{
		Run(strength, duration, fadeStrength);
	}

	public void Run(float shakeStrength, float shakeDuration, bool fadeStrength = true)
	{
		curStrength = shakeStrength;
		curDuration = timeRemaining = shakeDuration;
		fading = fadeStrength;
		startPosition = transform.position;
	}
}
