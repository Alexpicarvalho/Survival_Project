using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Non-Rigidbody Movement")]
    public bool _nonRigidbodyMovement = false;
    public float _bulletSpeed = 100;
    public float _gravity = 10;

    [Header("Rigidbody Movement")]
    public float _force;
    Rigidbody rb;

    [Header("Detection Check")]
    [SerializeField] bool _triggerChecking = false;
    [SerializeField] float _forwardRaycastDistance;
    [SerializeField] float _backwardsRaycastDistance;
    [SerializeField] float _longRangeCheckDistance;
    List<Collider> _alreadyHitColliders = new List<Collider>();
    List<Collider> _longRangeCheckColliders = new List<Collider>();

    private float _totalThicknessHit = 0;
    public bool _spawnImpact = true;
    public GameObject _impact;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (_nonRigidbodyMovement)
        {
            //rb.isKinematic = true;
            rb.mass = 0f;
        }
        else
        {
            rb.isKinematic = false;
            rb.mass = 0.1f;
            rb.AddForce(transform.forward * _force);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
        CheckHit();
        LongRangeCheck();
        if (_totalThicknessHit >= 1) Destroy(gameObject);
    }

    private void MoveBullet()
    {
        if (!_nonRigidbodyMovement) return;

        transform.position += (transform.forward * _bulletSpeed + Vector3.down * _gravity) * Time.deltaTime ;
    }

    private void LongRangeCheck()
    {

    }

    private void CheckHit()
    {
        if (_triggerChecking) return;

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
            if (_spawnImpact) Instantiate(_impact, transform.position, Quaternion.LookRotation(hit1.normal));
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!_triggerChecking || _alreadyHitColliders.Contains(other)) return;

    //    _alreadyHitColliders.Add(other);

    //    //Normal Calculation
    //    T_CalculateNormal(other);

    //    var colliderHit = other.GetComponent<ColliderInfo>();

    //    if (!colliderHit) { Debug.LogWarning("No Collider Info on " + other.name); return; }

    //   // colliderHit.Hit(new HitInfo(1, new Collision()))
    //    _totalThicknessHit += colliderHit._thickness;


    //    if (_totalThicknessHit >= 1) Destroy(gameObject);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (!_triggerChecking || _alreadyHitColliders.Contains(collision.collider)) return;

        Debug.Log("Hit " + collision.collider.name);

        _alreadyHitColliders.Add(collision.collider);

        var colliderHit = collision.collider.GetComponent<ColliderInfo>();

        if (!colliderHit) { Debug.LogWarning("No Collider Info on " + collision.collider.name); return; }

        colliderHit.Hit(new HitInfo(1, collision));
        _totalThicknessHit += colliderHit._thickness;


        if (_totalThicknessHit >= 1) Destroy(gameObject);
    }

    private Vector3 T_CalculateNormal(Collider other)
    {
        return (transform.position - other.transform.position).normalized;
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
