using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Requirements")]
    private Camera _camera;

    [Header("Properties")]
    public float _cameraSensitivity = 30f;
    [SerializeField] float _lookClamp = 80f;

    [Header("Values")]
    private float xRotation = 0f;

    private void Awake()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        float viewX = input.x;
        float viewY = input.y;

        xRotation -= viewY * Time.deltaTime * _cameraSensitivity;
        xRotation = Mathf.Clamp(xRotation, -_lookClamp, _lookClamp);

        _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * Time.deltaTime * viewX * _cameraSensitivity);
    }
}
