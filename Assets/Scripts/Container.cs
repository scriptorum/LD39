using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class Container : MonoBehaviour 
{
	public ParticleSystem hitFX;
	public string hitSound;
	public string deathSound;
	public int hitFXAmount = 30;
	public int contentsCount = 6;
	public void OnDamage(int damage, int healthRemaining)
	{
		hitFX.Emit(hitFXAmount);

		if(healthRemaining <= 0)
		{
			SoundManager.instance.Play(deathSound);			

			while(contentsCount-- > 0)
				OreTosser.instance.toss(transform.position);

			transform.DetachChildren();
			Destroy(gameObject);
		}
		else SoundManager.instance.Play(hitSound);
	}
}
