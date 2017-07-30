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
		sr.enabled = false;
		text.enabled = false;

    }

	void Start()
	{
		show("Hi there");
	}

	void Update()
	{
		if(Config.instance.gamePaused)
		{
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button16))
			{
				hide();
				// TODO SFX
			}
		}
	}

	public void show(string msg)
	{
		text.text = msg;
		sr.enabled = true;
		text.enabled = true;
		Config.instance.gamePaused = true;
		// TODO Wait for LMB or (A)
	}

	public void hide()
	{
		sr.enabled = false;
		text.enabled = false;
		Config.instance.gamePaused = false;
	}
}