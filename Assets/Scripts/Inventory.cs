using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float ore = 5;
    private Text oreText;

    void Awake()
    {
        oreText = transform.Find("ore").GetComponent<Text>();
        oreText.text = ore.ToString();
    }

    public void onPickup(Pickup pickup)
    {
        switch (pickup.type)
        {
            case PickupType.Ore:
                ore++;
                oreText.text = ore.ToString();
                break;

            default:
                break;
        }
    }

    public void onDrop(PickupType type, float amount)
    {
        switch (type)
        {
            case PickupType.Ore:
                ore -= amount;
                oreText.text = ore.ToString();
                break;

            default:
                break;
        }
    }
}