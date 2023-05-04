using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecoilScript : MonoBehaviour
{
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;


    [SerializeField] float _upwardsRecoil;
    [SerializeField] float _sidewaysXRecoil;
    [SerializeField] float _sidewaysZRecoil;

    [SerializeField] float _snapiness;
    [SerializeField] float _returnSpeed;


    private void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snapiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(_currentRotation);

    }

    public void AddRecoil()
    {
        _targetRotation += new Vector3(_upwardsRecoil,
            Random.Range(_sidewaysXRecoil, -_sidewaysXRecoil),
            Random.Range(_sidewaysZRecoil, -_sidewaysZRecoil));
    }

}
