using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
	public static Messages instance;
	public Text text;
	private SpriteRenderer sr;

    void Awake()
    {
		instance = this;
		sr = GetComponent<SpriteRenderer>();
		hide();
    }

	public void show(string msg)
	{
		text.text = msg;
		sr.enabled = true;
		text.enabled = true;
		// TODO Pause whole game -- put everything in Game object, so you can just deactivate it
		// TODO Wait for LMB or (A)
	}

	public void hide()
	{
		sr.enabled = false;
		text.enabled = false;
	}
}