using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    void Start()
    {
        rb.AddRelativeForce(new Vector2(0, 1f));
		Destroy(this.gameObject, 1.0f);
    }
}