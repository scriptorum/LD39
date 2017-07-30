using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config instance;

	public bool gamePaused = true;
	public bool gameOver = false;

    void Awake()
    {
		instance = this;
    }
}