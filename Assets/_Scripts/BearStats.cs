using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearStats : Stats
{
    public bool _ragdoll = false;

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
        if (!_ragdoll) GetComponent<Animator>().SetTrigger("Die");
        else RagdollBehaviour();
    }

    private void RagdollBehaviour()
    {
        GetComponent<Animator>().enabled = false;
    }
}
