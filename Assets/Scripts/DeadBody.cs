using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public bool fadeOutWhenRemoved = true;
    public bool removeAfterLifeTime = true;
    public bool keepUnderMaxCount = true;
    public float fadeTime = 1.0f;
    public float lifeTime = 30f;
    public int maxCount = 100;

    private static int nextId = 0;
    private static int minId = 0;
    private int id;
    private float birthTime;
	private bool fading = false;
	private float fadeCurrent;
	private SpriteRenderer sr;

    void Awake()
    {
		if(removeAfterLifeTime == false && keepUnderMaxCount == false)
			Debug.Log("Either removeAfterLifeTime or keepUnderMaxCount must be true, or DeadBody does nothing");

		id = nextId++;
        if (nextId - minId > maxCount)
            minId++;
			
        birthTime = Time.realtimeSinceStartup;
		sr = GetComponent<SpriteRenderer>();
		fadeCurrent = fadeTime;
    }

    void Update()
    {
		if(fading)
		{
			fadeCurrent -= Time.deltaTime;
			if(fadeCurrent <= 0.0)
				Destroy(gameObject);
			sr.color = new Color(1, 1, 1, fadeCurrent / fadeTime);
		}

        else if (keepUnderMaxCount && minId > id)
            remove();

        else if (removeAfterLifeTime && Time.realtimeSinceStartup - birthTime > lifeTime)
            remove();		
    }

    private void remove()
    {
		if(fadeOutWhenRemoved)
			fading = true;
	    else Destroy(gameObject);
    }
}