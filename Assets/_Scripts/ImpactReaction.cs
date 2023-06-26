using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ImpactReaction", menuName = "Impact Reactions")]
public class ImpactReaction : ScriptableObject
{
    [SerializeField] private string _materialType;
    [SerializeField] private List<GameObject> _impactVFXs;
    [SerializeField] private List<AudioClip> _impactSFXs;

    public GameObject GetRandomVFX()
    {
        return _impactVFXs[Random.Range(0, _impactVFXs.Count)];
    }
    public AudioClip GetRandomSFX()
    {
        return _impactSFXs[Random.Range(0, _impactSFXs.Count)];
    }
} 
