using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
	public static Messages instance;
	public Text text;
	public GameObject gamePrefab;

	private SpriteRenderer sr;
	private System.Action followupAction;

    void Awake()
    {
		instance = this;
		sr = GetComponent<SpriteRenderer>();
		sr.enabled = false;
		text.enabled = false;

    }

	void Start()
	{
		msg1();
	}

	public void msg1()
	{
		show("<Incoming Message>", msg2);		
	}
	public void msg2()
	{
		show("Oof! You're a in real pickle there. Look at your fuel gauge below! Not enough to get back to your ship. At least you got shields.", msg3);		
	}

	public void msg3()
	{
		show("I guess I'll help since I'm on coffee break. See those rocks? Shoot them with your mining laser. Your rover can process them as fuel.", msg4); 		
	}

	public void msg4()
	{
		show("Move with arrows or AWSD. Your rover is equipped with a mouse and a left button to aim and fire. Or maybe you have a gamepad and the developer got the button assignments correct? Not likely!");
	}

	void Update()
	{
		#if DEBUG
		if(Input.GetKeyDown(KeyCode.R))
		{
			restart();
			return;
		}

		#endif

		if(Config.instance.gamePaused)
		{
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button16))
			{
				if(followupAction == null)
					hide();
				else followupAction();
				// TODO SFX
			}
		}
	}

	public void show(string msg, System.Action nextAction = null)
	{
		text.text = msg;
		sr.enabled = true;
		text.enabled = true;
		Config.instance.gamePaused = true;
		followupAction = nextAction;
	}

	public void hide()
	{
		sr.enabled = false;
		text.enabled = false;
		Config.instance.gamePaused = false;
	}

	public void outOfFuel()
	{
		show("Yup. You're out of fuel.  Well I tried to help.\nSo ah ... I gotta run. I got this thing. In space. This is weird talking to a dead guy.", msg10);
	}

	public void msg10()
	{
		show("<Transmission Ended>", msg11);
	}

	public void msg11()
	{
		show("... CLONE ACTIVATED ...\n ... DROPPING ANOTHER CRAPPY ROVER ...", restart);
	}

	public void blowUp()
	{
		show("Well, there you go. Dead. They don't make rovers like they used to. I love you! Don't leave! I'm just kidding, you're way dead. See ya!", msg10);
	}

	public void restart()
	{
		CoroutineManager.instance.StopAllCoroutines();
		GameObject.Destroy(transform.Find("/Game").gameObject);
		Invoke("startUp", 0.2f);
	}
	public void startUp()
	{
		GameObject game = Instantiate(gamePrefab, Vector3.zero, Quaternion.identity);
		game.name = "Game";
		hide();
		Invoke("msg1", 0.2f);
	}
}