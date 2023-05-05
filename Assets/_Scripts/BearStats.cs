using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearStats : Stats
{
    public override void TakeDamage(float amount)
    {
        Debug.Log("TOOK DAMAGE : " + amount);
        _currentHp -= amount;
        if(_currentHp <= 0)
        {
            _currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
    }
}
