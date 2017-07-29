using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Support mouse if no joystick

public class TurretControls : MonoBehaviour
{
    public Transform laserSpawn;
    public GameObject laserPrefab;

    private bool usingGamePad = false;
    private Vector2 lastMouse = Vector2.zero;
    private Vector2 lastGamePad = Vector2.zero;
    private const float FULL_CHARGE = 0.25f;
    private float chargeTime = 0f;

    void Update()
    {
        chargeTime += Time.deltaTime;

        lastGamePad = new Vector2(-Input.GetAxis("Horizontal2"), -Input.GetAxis("Vertical2"));
        bool mouseButton = Input.GetMouseButton(0);

        if (lastGamePad.x != 0f || lastGamePad.y != 0f)
        {
            aimTurret(lastGamePad.x, lastGamePad.y);
            fire();
            usingGamePad = true;
        }

        else if (mouseButton)
        {
            usingGamePad = false;
            getMousePosition();
            aimTurret(lastMouse.x, lastMouse.y);
            fire();
        }

        else if (!usingGamePad)
        {
            getMousePosition();
            aimTurret(lastMouse.x, lastMouse.y);
        }
    }

    private void aimTurret(float x, float y)
    {
        float z = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, z);
    }

    private void fire()
    {
        if (chargeTime < FULL_CHARGE)
            return;

        chargeTime = 0f;
        GameObject go = (GameObject) Instantiate(laserPrefab, laserSpawn.position, transform.rotation);
        go.transform.parent = null;
    }

    private void getMousePosition()
    {
        Vector3 mouse3 = Input.mousePosition;
        mouse3 = Camera.main.ScreenToWorldPoint(mouse3);
        lastMouse.x = transform.position.x - mouse3.x;
        lastMouse.y = mouse3.y - transform.position.y;
    }
}