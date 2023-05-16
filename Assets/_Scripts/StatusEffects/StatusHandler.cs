using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusHandler : MonoBehaviour
{
    [SerializeField] List<StatusResistance> resistances;

    [SerializeField]
    protected Dictionary<StatusEffect, float /*current intesity of the status*/> _statusEffects;


    private void SetUpStatusEffects()
    {
        foreach (StatusEffect status in StatusManager.Instance.Status)
        {
            _statusEffects.Add(status, 0.0f);
        }
    }
    

}

[Serializable]
public struct StatusResistance
{
    public StatusType StatusType;
    public float ResistancePercentage;
}

