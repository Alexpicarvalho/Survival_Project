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
    private PlayerLook _playerLook;
    private HandSwayAndBob _handSwayAndBob;

    public Vector3 _aimPosition;
    public Vector3 _aimRotation;
    public float _aimLerpDuration;

    [Header("Audio")]
    private AudioSource _audioSource;
    public AudioClip _shotSound;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _timeBetweenShots = 60 / _bulletsPerMinute;
        _recoil = Camera.main.GetComponentInParent<RecoilScript>();
        _anim = GetComponent<Animator>();
        _playerLook = GetComponentInParent<PlayerLook>();
        _handSwayAndBob = GetComponentInParent<HandSwayAndBob>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
        if (Mouse.current.leftButton.IsPressed() && _timeSinceLastShot >= 60/_bulletsPerMinute /*_timeBetweenShots*/) Fire();

        if (Mouse.current.rightButton.wasPressedThisFrame) Aim(true);
        else if(Mouse.current.rightButton.wasReleasedThisFrame) Aim(false);


    }

    private void Aim(bool toogle)
    {
        if (toogle)
        {
            _handSwayAndBob.enabled = false;
            StartCoroutine(LerpAim());
            _playerLook.Aim(true, _aimLerpDuration);

        }
        else
        {
            StartCoroutine(LerpResetAim());
            _handSwayAndBob.enabled = true;
            _playerLook.Aim(false, _aimLerpDuration);

        }
    }

    IEnumerator LerpAim()
    {
        float timeElapsed = 0;
        while (timeElapsed < _aimLerpDuration)
        {
            transform.parent.localPosition = Vector3.Lerp(Vector3.zero, _aimPosition, timeElapsed / _aimLerpDuration);
            transform.parent.localRotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(_aimRotation), timeElapsed / _aimLerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator LerpResetAim()
    {
        float timeElapsed = 0;
        while (timeElapsed < _aimLerpDuration)
        {
            transform.parent.localPosition = Vector3.Lerp(_aimPosition, Vector3.zero, timeElapsed / _aimLerpDuration);
            transform.parent.localRotation = Quaternion.Lerp(Quaternion.Euler(_aimRotation),Quaternion.identity, timeElapsed / _aimLerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }



    private void Fire()
    {
        _recoil.AddRecoil();
        _anim.SetTrigger("Fire");
        _timeSinceLastShot = 0;
        Instantiate(_bullet, _firePoint.position, Quaternion.LookRotation(GetFireRotation()));
        var mf = Instantiate(_muzzleFlash, _firePoint.position, Quaternion.LookRotation(Camera.main.transform.forward), _firePoint);
        _audioSource.PlayOneShot(_shotSound);
        Destroy(mf, 1.0f);
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
