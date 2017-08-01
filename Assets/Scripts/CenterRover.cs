using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRover : MonoBehaviour
{
    void Awake()
    {
        Init();
    }

    public void Init()
    {
        Transform rover = transform.Find("/Game/Rover");
        if (rover == null)
        {
            Invoke("Init", 0.1f);
            return;
        }

        rover.position = Vector3.zero;
        rover.gameObject.SetActive(true);
        Config.instance.gameOver = false;
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}