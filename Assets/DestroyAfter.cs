using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float _time;

    private void Awake()
    {
        Destroy(gameObject, _time);
    }
}
