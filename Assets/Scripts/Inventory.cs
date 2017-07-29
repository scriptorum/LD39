using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int ore = 0;
    private Text oreText;

    void Awake()
    {
        oreText = transform.Find("ore").GetComponent<Text>();
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

    public void onDrop(PickupType type, int count)
    {
        switch (type)
        {
            case PickupType.Ore:
                ore -= count;
                oreText.text = ore.ToString();
                break;

            default:
                break;
        }
    }
}