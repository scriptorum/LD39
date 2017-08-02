using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    public GameObject laserPrefab;

    private const float CHARGE_SUBTRACT = 0.70f;
    private const float SIGHT = 10f;
    private const float SPEED = 2.0f;
    private Transform rover;
    private Transform shadow;
    private Transform[] mounts = new Transform[2];
    private ParticleSystem[] fx = new ParticleSystem[2];
    private int currentMount = 1;
    private float chargeTime = 0f;
    private float curMaxCharge = 0.2f;

    void Awake()
    {
        shadow = transform.Find("saucer-shadow");
        mounts[0] = transform.Find("mount1");
        mounts[1] = transform.Find("mount2");
        fx[0] = mounts[0].GetComponent<ParticleSystem>();
        fx[1] = mounts[1].GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Config.instance.gamePaused)
            return;

       if (rover == null)
        {
            GameObject game = GameObject.Find("/Game");
            if (game != null)
            {
                rover = game.transform.Find("Rover");
                if (rover == null)
                    return;
            }
        }

        Vector3 diff = rover.position - transform.position;
		if(diff.magnitude > SIGHT)
			return;

        float z = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(transform.eulerAngles.z, -z, 0.08f));
        shadow.transform.rotation = transform.rotation;
        transform.Translate(0, SPEED * Time.deltaTime, 0, Space.Self);

        chargeTime += Time.deltaTime;

        if (chargeTime >= curMaxCharge)
        {
            curMaxCharge = CHARGE_SUBTRACT - curMaxCharge;
            chargeTime = 0f;
            fire(currentMount);
            currentMount = 1 - currentMount;
        }

        shadow.transform.position = transform.position + Vector3.down * 0.2f;
    }

    private void fire(int mount)
    {
        fx[mount].Play();
        GameObject go = (GameObject) Instantiate(laserPrefab, mounts[mount].position, transform.rotation);
        Damage damage = go.GetComponent<Damage>();
        damage.type = DamageType.Enemy;
        damage.damage = 10;
    }
}