using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class InitializeLevel : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("InitializeLevel is awake!");
        Init();
    }

    public void Init()
    {
        GameObject game = GameObject.Find("/Game");
        Transform rover = null;
        if(game) rover = game.transform.Find("Rover");

        if (rover == null)
        {
            Debug.Log("Can't find rover! Game:" + game);
            Invoke("Init", 0.1f);
            return;
        }

        Debug.Log("Found rover!");
        // If power or shields == 0, reset either to reasonable setting for this level
        rover.position = Vector3.zero;
        rover.gameObject.SetActive(true);
        Config.instance.gameOver = false;
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}