﻿using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;

public class TireControls : MonoBehaviour
{
    public ParticleSystem dust;
    public bool canRotate = true;

    private Anim anim;
    private const float MAX_TURN = 38f;
    private static int counter = 0;
    private float partAccum = 0f;

    void Awake()
    {
        anim = GetComponent<Anim>();
        dust.randomSeed = (uint)(System.Environment.TickCount + counter++);
        anim.Pause();
    }

    public void onRoverRotate(float angleDeg)
    {
        if (Config.instance.gamePaused)
            return;

        // Restrict tire angle
        float absAngleDeg = Mathf.Abs(angleDeg);
        float clampedAngle = Mathf.Min(MAX_TURN, absAngleDeg) * Mathf.Sign(angleDeg);

        // Rotate tire
        if (canRotate)
            transform.localEulerAngles = new Vector3(0f, 0f, -clampedAngle);
    }

    public void onRoverMove(float speed)
    {
        if (Config.instance.gamePaused)
            return;

        if (speed <= 0.01f)
        {
            if (!anim.paused)
            {
                anim.Pause();
                // dust.Emit(20);
            }
        }
        else
        {
            if (anim.paused)
                anim.Resume();

            partAccum += speed * 2;
            if (partAccum > 1.0f)
            {
                int particles = Mathf.FloorToInt(partAccum);
                dust.Emit(particles);
                partAccum -= particles;
            }
        }
    }
}