using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Stats
{
    public override void TakeDamage(float amount)
    {
        _currentHp -= amount;   
    }
}
