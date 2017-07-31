using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRover : MonoBehaviour
{
    void Awake()
    {
		Invoke("Init", 0.01f);
	}

	public void Init()
	{
        Transform rover = transform.Find("/Game/Rover");
        rover.position = Vector3.zero;
		rover.gameObject.SetActive(true);
        Config.instance.gameOver = false;		
        Config.instance.gamePaused = false;
    }
}