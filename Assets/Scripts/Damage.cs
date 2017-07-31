using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    public DamageType type = DamageType.Player;
}

public enum DamageType
{
    Player, Enemy
}