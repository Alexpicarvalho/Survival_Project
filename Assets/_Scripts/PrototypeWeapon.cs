using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrototypeWeapon : MonoBehaviour
{
    public GameObject _bullet;
    public GameObject _muzzleFlash;
    public Transform _firePoint;
    public float _bulletsPerMinute;
    private float _timeBetweenShots;
    private float _timeSinceLastShot = 0f;
    private RecoilScript _recoil;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _timeBetweenShots = 60 / _bulletsPerMinute;
        _recoil = Camera.main.GetComponentInParent<RecoilScript>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
        if (Mouse.current.leftButton.IsPressed() && _timeSinceLastShot >= _timeBetweenShots) Fire();


    }

    private void Fire()
    {
        _recoil.AddRecoil();
        _anim.SetTrigger("Fire");
        _timeSinceLastShot = 0;
        Instantiate(_bullet, _firePoint.position, Quaternion.LookRotation(GetFireRotation()));
        var mf = Instantiate(_muzzleFlash, _firePoint.position, Quaternion.LookRotation(Camera.main.transform.forward),_firePoint);
        Destroy(mf, 2.0f);
    }

    Vector3 GetFireRotation()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,
            out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point - _firePoint.position;
        }
        else return Camera.main.transform.forward;
    }
}
