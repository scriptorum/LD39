using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour 
{
	public float amount;
	public float max;
	public SpriteRenderer fillSprite;

	private const float SPEED = 10.0f;
	private float timer = 0f;

	void Awake()
	{
		fillSprite.transform.localScale = new Vector3(0, 1, 1);
	}

	void Update()
	{
		float curScale = fillSprite.transform.localScale.x;
		float newScale = amount / max;

		if(newScale != curScale)
		{
			timer += Time.deltaTime;
			fillSprite.transform.localScale = new Vector3(Mathf.Lerp(curScale, newScale, timer / SPEED), 1, 1);
			if(timer > SPEED)
				timer = 0f;
		}
	}
}
