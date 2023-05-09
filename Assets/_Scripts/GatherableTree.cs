using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableTree : MonoBehaviour, IGatherable
{
    private Stats _stats;
    private Rigidbody _rb;
    [SerializeField] private int _hitsToFall;
    private int _currentHits;

    bool _readyToBreak;
    public GameObject _breakParts;
    public GameObject _breakVFX;
    public int _breakPartsToSpawn;
    public LayerMask _breakCollisionLayers;

    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<Stats>();
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_readyToBreak)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + transform.up * 10, 4.0f, _breakCollisionLayers);

            if (colliders.Length >= 1) BreakMe();
        }
    }

    private void BreakMe()
    {
        for (int i = 0; i < _breakPartsToSpawn; i++)
        {
            Instantiate(_breakParts, transform.position + transform.up * i * 6, Quaternion.identity);
            _breakVFX.SetActive(true);
            _breakVFX.transform.parent = null;
        }
        Destroy(gameObject);
    }

    public void Gather(HitInfo info)
    {
        _currentHits++;
        if (_currentHits == _hitsToFall) Fall(info);
    }

    private void Fall(HitInfo info)
    {
        _rb.isKinematic = false;
        _readyToBreak = true;
        _rb.AddForce(info.hit.normal * -2000);
        _rb.AddTorque(info.hit.normal * -100);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + transform.up * 10, 4.0f);
    }

}
