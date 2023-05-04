using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalColliderManager : MonoBehaviour
{
    private List<ColliderInfo> _myColliders = new List<ColliderInfo>();

    public void AddColliderRef(ColliderInfo newColliderInfo)
    {
        if (!_myColliders.Contains(newColliderInfo))
        {
            _myColliders.Add(newColliderInfo);
            newColliderInfo.GetIndex(_myColliders.Count);
        } 

    }

    public void HitMessage(int colliderIndex, Collision collision)
    {

    }
}
