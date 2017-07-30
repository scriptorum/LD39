using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class Container : MonoBehaviour 
{
	public ParticleSystem hitFX;
	public int hitFXAmount = 30;
	public GameObject contentsPrefab;
	public int contentsCount = 6;
	private const float TOSS = 2f;
	private const float TOSS_SPEED = 0.3f;
	public void OnDamage(int damage, int healthRemaining)
	{
		hitFX.Emit(hitFXAmount);

		if(healthRemaining <= 0)
		{
			while(contentsCount-- > 0)
			{
				Vector3 pos = new Vector3(transform.position.x + Random.Range(-TOSS, TOSS), transform.position.y + Random.Range(-TOSS, TOSS), 0f);
				GameObject go = (GameObject) Instantiate(contentsPrefab, transform.position, Quaternion.identity);
				go.transform.parent = null;
				CoroutineManager.instance.Run(go.transform.LerpPosition(pos, TOSS_SPEED));
			}

			transform.DetachChildren();
			Destroy(gameObject);
		}

		else
		{
			Debug.Log("Ouch");
		}
	}
}
