using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class Laser : MonoBehaviour
{
	private Rigidbody2D rb;	
	private Damage damage;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		damage = GetComponent<Damage>();
	}

    void Start()
    {
        rb.AddRelativeForce(new Vector2(0, 1000f));
		Destroy(this.gameObject, 0.5f);
		if(damage.type == DamageType.Enemy)
			SoundManager.instance.Play("laser-fire2");
		else SoundManager.instance.Play("laser-fire");
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.name == "Rover" && damage.type == DamageType.Player)
			return;
		if(other.name == "Saucer" && damage.type == DamageType.Enemy)
			return;
			
		Health health = other.gameObject.GetComponent<Health>();
		if(health != null)
			Destroy(this.gameObject);
	}
}