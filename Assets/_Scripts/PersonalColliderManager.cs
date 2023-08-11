using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalColliderManager : MonoBehaviour
{
    private List<ColliderInfo> _myColliders = new List<ColliderInfo>();
    private Stats _stats;

    AudioSource _audioSource;

    private void Start()
    {
        _stats = GetComponent<Stats>();
        _audioSource = GetComponent<AudioSource>();
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

        if(hitInfo.collision == null)
        {
            //VFX
            var impactVFX = Instantiate(_myColliders[colliderIndex].GetImpactVFX(), hitInfo.hit.point, Quaternion.LookRotation(hitInfo.hit.normal));

            //AUDIO
            if (_audioSource) _audioSource.PlayOneShot(_myColliders[colliderIndex].GetImpactSFX());
            else AudioSource.PlayClipAtPoint(_myColliders[colliderIndex].GetImpactSFX(), hitInfo.hit.point);

        }
        else
        {
            var impactVFX = Instantiate(_myColliders[colliderIndex].GetImpactVFX(), hitInfo.collision.GetContact(0).point, Quaternion.LookRotation(hitInfo.collision.GetContact(0).normal));

            if (_audioSource) _audioSource.PlayOneShot(_myColliders[colliderIndex].GetImpactSFX());
            else AudioSource.PlayClipAtPoint(_myColliders[colliderIndex].GetImpactSFX(), hitInfo.collision.GetContact(0).point);

        }


        //var impactSFX = 
    }
}
