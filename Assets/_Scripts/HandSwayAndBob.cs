using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwayAndBob : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement _playerMovement;
    public PlayerLook _playerLook;

    [Header("Sway")]
    public float step = 0.01f;
    public float maxStepDistance = 0.06f;
    Vector3 swayPos;

    [Header("Sway Rotation")]
    public float rotationStep = 4f;
    public float maxRotationStep = 5f;
    Vector3 swayEulerRot;

    public float smooth = 10f;
    float smoothRot = 12f;

    [Header("Bobbing")]
    public float speedCurve;
    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    Vector3 bobPosition;

    public float bobExaggeration;

    [Header("Bob Rotation")]
    public Vector3 walkingMultiplier;
    public Vector3 runningMultiplier;
    public Vector3 stalkingMultiplier;
    Vector3 bobEulerRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Sway();
        //SwayRotation();
        BobOffset();
        BobRotation();

        CompositePositionRotation();
    }


    Vector2 walkInput;
    Vector2 lookInput;

    void GetInput()
    {
        walkInput.x = _playerMovement._movementDirection.x;
        walkInput.y = _playerMovement._movementDirection.z;
        walkInput = walkInput.normalized;

        lookInput.x = _playerLook.viewX;
        lookInput.y = _playerLook.viewY;
    }


    void Sway()
    {
        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

        swayPos = invertLook;
    }

    void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = -Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);
        swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
    }

    void CompositePositionRotation()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition, Time.deltaTime * smooth);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation), Time.deltaTime * smoothRot);
    }

    void BobOffset()
    {
        speedCurve += Time.deltaTime * (_playerMovement._isGrounded ? (_playerMovement._movementDirection.x + _playerMovement._movementDirection.z)
            * bobExaggeration : 1f) + 0.01f;

        bobPosition.x = (curveCos * bobLimit.x * (_playerMovement._isGrounded ? 1 : 0)) - (walkInput.x * travelLimit.x);
        bobPosition.y = (curveSin * bobLimit.y) - (_playerMovement._movementDirection.z * travelLimit.y);
        bobPosition.z = -(walkInput.y * travelLimit.z);
    }

    void BobRotation()
    {
        if (_playerMovement._isRunning)
        {
            bobEulerRotation.x = (walkInput != Vector2.zero ? runningMultiplier.x * (Mathf.Sin(2 * speedCurve)) : runningMultiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
            bobEulerRotation.y = (walkInput != Vector2.zero ? runningMultiplier.y * curveCos : 0);
            bobEulerRotation.z = (walkInput != Vector2.zero ? runningMultiplier.z * curveCos * walkInput.x : 0);
        }
        else if (_playerMovement._isStalking)
        {
            bobEulerRotation.x = (walkInput != Vector2.zero ? stalkingMultiplier.x * (Mathf.Sin(2 * speedCurve)) : stalkingMultiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
            bobEulerRotation.y = (walkInput != Vector2.zero ? stalkingMultiplier.y * curveCos : 0);
            bobEulerRotation.z = (walkInput != Vector2.zero ? stalkingMultiplier.z * curveCos * walkInput.x : 0);
        }
        else
        {
            bobEulerRotation.x = (walkInput != Vector2.zero ? walkingMultiplier.x * (Mathf.Sin(2 * speedCurve)) : walkingMultiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
            bobEulerRotation.y = (walkInput != Vector2.zero ? walkingMultiplier.y * curveCos : 0);
            bobEulerRotation.z = (walkInput != Vector2.zero ? walkingMultiplier.z * curveCos * walkInput.x : 0);
        }
    }

}
