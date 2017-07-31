using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Spewnity;

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

    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart();
            return;
        }

#endif

        if (Config.instance.gamePaused)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button16) || Input.GetKeyDown(KeyCode.Space))
            {
				SoundManager.instance.Play("beep");
                if (followupAction == null)
                    hide();
                else followupAction();                
            }
        }
    }

    public void show(string msg, System.Action nextAction = null)
    {
		if(!Config.instance.gamePaused)
			SoundManager.instance.Play("beep3");
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

    public void outOfFuel()
    {
        show("Yup. You're out of fuel. That's bad. Well I tried to help. How much oxygen do you have left? So ah ... I gotta run. I got this thing. In space.", msg10);
    }

    public void msg10()
    {
        show("<Transmission Ended>", restart);
    }

    public void blowUp()
    {
        show("So I was talking to this guy over in ore processing and he said there's no way you're going to make it. And I'm like wanna bet?", msg11);
    }

    public void msg11()
    {
        show("Uh hello? Hello??\n You're not dead, are you?\nDamn.", msg10);
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}