using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalColliderManager : MonoBehaviour
{
    private List<ColliderInfo> _myColliders = new List<ColliderInfo>();
    private Stats _stats;

    private void Start()
    {
        _stats = GetComponent<Stats>();
    }

    public void AddColliderRef(ColliderInfo newColliderInfo)
    {
        if (!_myColliders.Contains(newColliderInfo))
        {
            _myColliders.Add(newColliderInfo);
            newColliderInfo.GetIndex(_myColliders.Count - 1);
        } 

    }

    public void HitMessage(int colliderIndex, HitInfo hitInfo)
    {
        if (_myColliders[colliderIndex].colliderType == ColliderType.OneShotVital) _stats.TakeDamage(9999);
        else if(_stats) _stats.TakeDamage(hitInfo.amount);

        Debug.Log("Spawning impact VFX");
        var impactVFX = Instantiate(_myColliders[colliderIndex].GetImpactVFX(),hitInfo.hit.point, Quaternion.LookRotation(hitInfo.hit.normal));
        //var impactSFX = 
    }
}
