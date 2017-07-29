using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class OreDeposit : MonoBehaviour 
{
	public ParticleSystem hit;
	public GameObject orePrefab;
	public int crystals = 6;
	private const float TOSS = 2f;
	private const float TOSS_SPEED = 0.3f;
	public void OnDamage(int damage, int healthRemaining)
	{
		hit.Emit(30);

		if(healthRemaining <= 0)
		{
			while(crystals-- > 0)
			{
				Vector3 pos = new Vector3(transform.position.x + Random.Range(-TOSS, TOSS), transform.position.y + Random.Range(-TOSS, TOSS), 0f);
				GameObject go = (GameObject) Instantiate(orePrefab, transform.position, Quaternion.identity);
				go.transform.parent = null;
				StartCoroutine(go.transform.LerpPosition(pos, TOSS_SPEED));
			}

			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<CircleCollider2D>().enabled = false;
			Destroy(gameObject, 3.0f); // wait for dust to settle
		}

		else
		{
			Debug.Log("Ouch");
		}
	}
}
