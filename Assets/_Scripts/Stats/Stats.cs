using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour, IDamageable
{
    [field: Header("General Stats")]
    [field : SerializeField] public float _maxHp { get; protected set; }
    [field : SerializeField] public float _maxStamina { get; protected set; }
    [field : SerializeField] public float _staminaRegen { get; protected set; }
    [field : SerializeField] public float _vitalProtection { get; protected set; }
    [field : SerializeField] public float _nonVitalProtection { get; protected set; }
    [field : SerializeField] public float _currentHp { get; protected set; }
    [field : SerializeField] public float _currentStamina { get; protected set; }

    public Transform GetTransform()
    {
        return transform;
    }

    public abstract void TakeDamage(float amount);
}
