using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config instance;

	public bool gamePaused = false;
	public bool gameOver = false;

    void Awake()
    {
		instance = this;
    }
}