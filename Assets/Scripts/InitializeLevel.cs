using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class InitializeLevel : MonoBehaviour
{
    public int restartPower = 20;
    public int restartShields = 50;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        GameObject game = GameObject.Find("/Game");
        Transform rover = null;
        if(game) rover = game.transform.Find("Rover");

        if (rover == null)
        {
            Invoke("Init", 0.1f);
            return;
        }

        rover.position = Vector3.zero;
        rover.gameObject.SetActive(true);

        PowerStation ps = rover.GetComponent<PowerStation>();
        if(ps.power.amount <= 0)
            ps.power.amount = restartPower;
        if(ps.shields.amount <= 0)
            ps.shields.amount = restartShields;

        Config.instance.gameOver = false;
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}