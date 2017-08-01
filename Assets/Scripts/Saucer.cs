using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    public GameObject laserPrefab;
    private Transform shadow;
    private Transform[] mounts = new Transform[2];
    private int currentMount = 1;
    private const float FULL_CHARGE = 0.35f;
    private const float SIGHT = 10f;
    private const float SPEED = 2.0f;
    private float chargeTime = 0f;
    private Transform rover;

    void Awake()
    {
        shadow = transform.Find("saucer-shadow");
        mounts[0] = transform.Find("mount1");
        mounts[1] = transform.Find("mount2");
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

        if (chargeTime >= FULL_CHARGE)
        {
            chargeTime = 0f;
            fire(mounts[currentMount]);
            currentMount = 1 - currentMount;
        }

        shadow.transform.position = transform.position + Vector3.down * 0.2f;
    }

    private void fire(Transform mount)
    {
        GameObject go = (GameObject) Instantiate(laserPrefab, mount.position, transform.rotation);
        Damage damage = go.GetComponent<Damage>();
        damage.type = DamageType.Enemy;
        damage.damage = 10;
    }
}