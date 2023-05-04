using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderInfo : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField] public ColliderType colliderType;
                                                           //Thickness will determine whether or not a bullet goes through a collider. 
    [SerializeField] [Range(0, 1)] public float _thickness;// This is proportional to bullet penetration, and this
                                                           //value will determine how much force a bullet loses when hitting this collider*/

    [Header("References")]
    private PersonalColliderManager _parentColliderHandler;
    private Collider _collider;

    [Header("Runtime Variables")]

    int _colliderIndex;


    private void Awake()
    {
        if (colliderType == ColliderType.TerrainOrObstacle) return;
        _parentColliderHandler = GetComponentInParent<PersonalColliderManager>();
        _collider = GetComponent<Collider>();
        _parentColliderHandler.AddColliderRef(this);
    }

    public void GetIndex(int newID)
    {
        _colliderIndex = newID;
    }

    public void Hit(HitInfo info)
    {
        if (colliderType == ColliderType.TerrainOrObstacle) return;
        _parentColliderHandler.HitMessage(_colliderIndex,info);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

public enum ColliderType
{
    OneShotVital, SevereVital, ModerateVital, MinorVital, NonVital, TerrainOrObstacle
}
