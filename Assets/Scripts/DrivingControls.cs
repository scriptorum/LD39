using System.Collections;
using System.Collections.Generic;
using Spewnity;
using UnityEngine;
using UnityEngine.Events;

public class DrivingControls : MonoBehaviour
{
    public ParticleSystem stunFx;
    public RoverRotateEvent roverRotate;
    public RoverMoveEvent roverMove;
    public PickupEvent roverPickup;

    private const float TURN_SPEED = 5f;
    private const int STUN_DAMAGE = 2;
    private float speed = 8f;
    private float curAngleDeg = 0f;
    private Vector2 lastMove;
    private float stunTimer = 0f;
    private PowerStation powerStation;
    private Health health;
    private Vector3 oldPos;

    void Awake()
    {
        powerStation = GetComponent<PowerStation>();
        health = GetComponent<Health>();
        oldPos = transform.position;
    }

    void Start()
    {
        CameraManager.instance.SetFollowTarget(transform);
    }

    void FixedUpdate()
    {
        if (Config.instance.gamePaused)
            return;

        if (powerStation.outOfFuel())
            return;

        lastMove = new Vector2(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));

        if (lastMove.x == 0f && lastMove.y == 0f)
        {
            roverMove.Invoke(0f);
            return;
        }

        // Determine desired angle of rover
        float z = Mathf.Atan2(lastMove.x, -lastMove.y);
        float zDeg = z * Mathf.Rad2Deg;

        float tireAngle = Mathf.DeltaAngle(zDeg, curAngleDeg);
        roverRotate.Invoke(tireAngle);

        // Restrict angle by turning speed
        zDeg = Mathf.LerpAngle(curAngleDeg, zDeg, Time.deltaTime * TURN_SPEED);
        z = zDeg * Mathf.Deg2Rad;

        // Recalculate input
        float magnitude = lastMove.magnitude;
        lastMove.x = Mathf.Sin(z) * -magnitude;
        lastMove.y = Mathf.Cos(z) * magnitude;

        // Aim rover
        transform.eulerAngles = new Vector3(0f, 0f, zDeg);
        curAngleDeg = zDeg;

        // Move rover
        if (stunTimer <= 0f)
        {
            oldPos = transform.position;
            transform.Translate(lastMove.x * Time.deltaTime * speed, lastMove.y * Time.deltaTime * speed, 0, Space.World);
            roverMove.Invoke(magnitude);
        }
        else stunTimer -= Time.deltaTime;

        if (powerStation.outOfFuel())
        {
            roverRotate.Invoke(0);
            roverMove.Invoke(0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "solid")
        {
            lastMove.Normalize();
            transform.position = oldPos;
            stunTimer = 0.5f;
            stunFx.Play();
            SoundManager.instance.Play("rover-stun");
            // This is dorky how I've got to sync health and shields
            // In this case, I don't want the damage fx, so I can't call health.takeDamage()
            int damage = health.health > STUN_DAMAGE ? STUN_DAMAGE : health.health - 1;
            health.health -= damage;
            powerStation.shields.amount = health.health;
            CameraManager.instance.Shake(0.045f, 0.18f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup != null && pickup.available)
        {
            roverPickup.Invoke(pickup);
            GameObject.Destroy(pickup.gameObject);
        }
    }
}

[System.Serializable]
public class RoverRotateEvent : UnityEvent<float> // angleDeg
    { }

[System.Serializable]
public class RoverMoveEvent : UnityEvent<float> // speed
    { }

[System.Serializable]
public class PickupEvent : UnityEvent<Pickup> { }