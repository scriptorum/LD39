using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class Container : MonoBehaviour 
{
	public ParticleSystem hitFX;
	public GameObject deathPrefab;
	public string hitSound;
	public string deathSound;
	public int hitFXAmount = 30;
	public int contentsCount = 6;
	public PickupType type;

	public void OnDamage(int damage, int healthRemaining)
	{
		hitFX.Emit(hitFXAmount);

		if(healthRemaining <= 0)
		{
			SoundManager.instance.Play(deathSound);			

			while(contentsCount-- > 0)
				OreTosser.instance.toss(transform.position, type);

			transform.DetachChildren();
			Destroy(gameObject);
			
			if(deathPrefab != null)
			{
				GameObject go = Instantiate(deathPrefab, transform.position, Quaternion.identity);
				go.transform.parent = transform.Find("/Game");
			}
		}
		else SoundManager.instance.Play(hitSound);
	}
}
