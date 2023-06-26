using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _force;
    Rigidbody rb;
    public GameObject _impact;

    [Header("Detection Check")]
    [SerializeField] float _forwardRaycastDistance;
    [SerializeField] float _backwardsRaycastDistance;
    List<Collider> _alreadyHitColliders = new List<Collider>();

    private float _totalThicknessHit = 0;
    public bool _spawnImpact = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * _force);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHit();
        if (_totalThicknessHit >= 1) Destroy(gameObject);
    }

    private void CheckHit()
    {
        ColliderInfo colliderInfo1 = null;
        ColliderInfo colliderInfo2 = null;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _forwardRaycastDistance))
        {
            if (_alreadyHitColliders.Contains(hit.collider)) return;

            colliderInfo1 = hit.collider.transform.GetComponent<ColliderInfo>();
            if (_spawnImpact) Instantiate(_impact, transform.position, Quaternion.LookRotation(hit.normal));
            if (rb.useGravity) rb.useGravity = false;
            _alreadyHitColliders.Add(hit.collider);
            //Debug.Log("Hit " + hit.collider.name);
        }

        if (colliderInfo1)
        {
            //Debug.Log("ENTREI 1");
            colliderInfo1.Hit(new HitInfo(10, hit));
            _totalThicknessHit += colliderInfo1._thickness;
            if (_totalThicknessHit >= 1) Destroy(gameObject);
        }

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit1, _backwardsRaycastDistance))
        {
            if (_alreadyHitColliders.Contains(hit1.collider)) return;

            colliderInfo2 = hit1.collider.transform.GetComponent<ColliderInfo>();
            if(_spawnImpact) Instantiate(_impact, transform.position, Quaternion.LookRotation(hit1.normal));
            if (rb.useGravity) rb.useGravity = false;
            _alreadyHitColliders.Add(hit1.collider);
            //Debug.Log("Hit " + hit1.collider.name);
        }

        if (colliderInfo2)
        {
            //Debug.Log("ENTREI 2");
            colliderInfo2.Hit(new HitInfo(1, hit1));
            _totalThicknessHit += colliderInfo2._thickness;
            if (_totalThicknessHit >= 1) Destroy(gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        //Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * _forwardRaycastDistance);
        Gizmos.DrawRay(transform.position, -transform.forward * _backwardsRaycastDistance);
    }

    private void OnDestroy()
    {
        
    }
}
