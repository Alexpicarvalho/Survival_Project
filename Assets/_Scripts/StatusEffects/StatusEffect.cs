using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "StatusEffects")]
public class StatusEffect : ScriptableObject
{
    // Protected Fields

    [Header("General Properties")]
    [SerializeField] protected StatusType _statusType;
    [SerializeField] protected StatusPolarity _statusPolarity;
    [SerializeField] [Range(0, 1)] protected float _statusIntensity;
    [SerializeField] protected bool _isActive;

    //Public Properties 

    public StatusType StatusType => _statusType;
    public StatusPolarity StatusPolarity => _statusPolarity;
    public float StatusIntesity => _statusIntensity;
    public bool IsActive => _isActive;

    // Methods
    protected void SetActivationState(bool isActive, float intensity = 0, float ownerResistance = 0)
    {
        _isActive = isActive;

        if(_isActive == false) return;

        // Set Intensity based on received intensity - owner resistance

        _statusIntensity = intensity - ownerResistance;
        _statusIntensity = Mathf.Clamp01(intensity);


    }

    protected virtual void SetUpStatus(Transform owner)
    {

    }

    protected virtual void ApplyEffect()
    {
        // Logic for whatever effect this status does, in case of tick based effect call it with tickTime
    }

    protected virtual void RemoveEffect()
    {
        // Cancel effect
    }

}

public enum StatusType
{
    Bleeding, Poisoned, Ablaze
}

public enum StatusPolarity
{
    Positive, Negative, Neutral
}
