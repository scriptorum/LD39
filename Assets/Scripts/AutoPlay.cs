using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    private float elapsed = 0f;
    private float x = -10f;

    void Start()
    {
        SoundManager.instance.Play("song-play");
    }

	// PlayAt Test
    // void Update()
    // {
    //     elapsed += Time.deltaTime;
    //     if (elapsed > 1.0f)
    //     {
    //         elapsed = 0f;
    //         SoundManager.instance.PlayAt("checkpoint", new Vector3(x, 0f, 0f));
    //         x += 1.0f;

    //         if (x > 10f)
    //             x = -10f;
    //     }
    // }
}