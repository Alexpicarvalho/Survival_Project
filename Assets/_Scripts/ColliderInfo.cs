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

    [Header("Impact Handeling")]
    [SerializeField] private ImpactReaction _impactReaction;
    [SerializeField] private bool _customImpact = false;
    [SerializeField] private GameObject _customImpactVFX;
    [SerializeField] private AudioClip _customImpactSFX;
    
    [Header("References")]
    private PersonalColliderManager _parentColliderHandler;
    private Collider _collider;

    [Header("Runtime Variables")]

    int _colliderIndex;


    private void Awake()
    {
        //If terrain 
        if (colliderType == ColliderType.TerrainOrObstacle)
        {
            _parentColliderHandler = GetComponent<PersonalColliderManager>();
            _collider = GetComponent<TerrainCollider>();
        }
        //If not terrain
        else
        {
            _parentColliderHandler = GetComponentInParent<PersonalColliderManager>();
            _collider = GetComponent<Collider>();
        }

        if (!_parentColliderHandler) Debug.Log("COULDN'T FIND COLLIDER HANDLER");
        
    }
    private void Start()
    {
        _parentColliderHandler.AddColliderRef(this);
    }
    public void GetIndex(int newID)
    {
        _colliderIndex = newID;
    }

    public void Hit(HitInfo info)
    {
        //if (colliderType == ColliderType.TerrainOrObstacle) return;
        _parentColliderHandler.HitMessage(_colliderIndex,info);
    }

    public GameObject GetImpactVFX()
    {
        if (_customImpact) return _customImpactVFX;
        else return _impactReaction.GetRandomVFX();
    }
    public AudioClip GetImpactSFX()
    {
        if (_customImpact) return _customImpactSFX;
        else return _impactReaction.GetRandomSFX();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

public enum ColliderType
{
    OneShotVital, SevereVital, ModerateVital, MinorVital, NonVital, TerrainOrObstacle
}
