using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeGather : MonoBehaviour
{
    [Header("Animation Values")]
    [SerializeField] float _rightOffsetPoint = .2f;
    [SerializeField] float _forwardOffsetPoint = .2f;
    [SerializeField] float _forwardRaycastDistance = 2f;
    [SerializeField] float _backToTreeRayDistance = 2f;
    [SerializeField] float _penetrationOffset = .2f;
    [SerializeField] Vector3 _noHitPoint;
    [SerializeField] float _timeToTarget = .2f;
    [SerializeField] AnimationCurve _animationCurve;
    [SerializeField] LayerMask _layerMask;
    Vector3 _targetPosition;
    Vector3 _startPosition;
    bool _readyHit = true;

    [Header("Bezier Curve")]


    [Header("Debug")]
    [SerializeField] bool _hitTree = false;
    [SerializeField] GameObject _rightSidePointerRef;
    [SerializeField] GameObject _backToTreePointerRef;
    [SerializeField] GameObject _rightSidePointer;
    [SerializeField] GameObject _backToTreePointer;
    [SerializeField] Transform _axe;
    [SerializeField] Vector3 _axeTipOffset;

    

    [Header("Visuals")]
    [SerializeField] GameObject _hitParticle;
    Vector3 _hitNormal;
    Vector3 _hitPosition;

    Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _startPosition = _axe.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _readyHit)
        {
            //_readyHit = false;
            //TryHit();
            //StartCoroutine(LerpAxeToPosition());
            _axe.GetComponent<Animator>().SetTrigger("Hit");
        }
    }


    public void CheckHit()
    {
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _forwardRaycastDistance))
        {
            _hitPosition = hit.point;
            _hitNormal = hit.normal;
            SpawnParticle();
            var gatheable = hit.collider.GetComponent<IGatherable>();
            if (gatheable != null) gatheable.Gather(new HitInfo(0, hit));
        }
    }






    // PROCEDURAL ANIMATION

    private void TryHit()
    {
        Vector3 rightSidePoint = Vector3.zero;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _forwardRaycastDistance))
        {
            Vector3 rightSideVector = _camera.transform.right;
            rightSidePoint = hit.point + rightSideVector * _rightOffsetPoint - hit.normal * _forwardOffsetPoint;
            _rightSidePointer = Instantiate(_rightSidePointerRef, rightSidePoint, Quaternion.identity);
            RaycastBackToTree(rightSidePoint);
        }
        else
        {
            _targetPosition = _camera.transform.position + _noHitPoint;
            _rightSidePointer = Instantiate(_rightSidePointerRef, _targetPosition, Quaternion.identity);
        }
    }

    private void RaycastBackToTree(Vector3 rightSidePoint)
    {
        if (Physics.Raycast(rightSidePoint, -_camera.transform.right, out RaycastHit hit, _backToTreeRayDistance, _layerMask))
        {
            _targetPosition = hit.point - (_penetrationOffset * hit.normal) /*+ _axeTipOffset*/;
            _backToTreePointer = Instantiate(_backToTreePointerRef, hit.point - (_penetrationOffset * hit.normal), Quaternion.identity);
            _hitNormal = hit.normal;
            _hitPosition = hit.point;
            _hitTree = true;
        }
        else
        {
            _targetPosition = _camera.transform.position + _noHitPoint;
            _rightSidePointer = Instantiate(_rightSidePointerRef, _targetPosition, Quaternion.identity);
        }
    }

    IEnumerator LerpAxeToPosition()
    {
        float elapsedTime = 0;
        Vector3 startPos = _axe.position;
        while (elapsedTime < _timeToTarget)
        {
            float t = elapsedTime / _timeToTarget;
            float curvePoint = _animationCurve.Evaluate(t);
            _axe.position = Vector3.Lerp(startPos, _targetPosition, curvePoint * t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SpawnParticle();

        yield return new WaitForSeconds(.2f);

        float elapsedTime1 = 0;
        Vector3 endPos = _axe.position;
        while (elapsedTime1 < .2f)
        {
            float t = elapsedTime1 / .2f;
            _axe.position = Vector3.Lerp(endPos, startPos, t);
            elapsedTime1 += Time.deltaTime;
            yield return null;
        }
        _readyHit = true;
    }

    void SpawnParticle()
    {
        Instantiate(_hitParticle, _hitPosition, Quaternion.LookRotation(_hitNormal));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_camera.transform.position, _camera.transform.forward * _forwardRaycastDistance);

        if (_hitTree) Gizmos.DrawRay(_rightSidePointer.transform.position, -_camera.transform.right * _backToTreeRayDistance);
    }
}
