using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    Vector2 moveInput;

    //cam ref
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Stats to edit")]
    [SerializeField] float zoomAmount = 5f;
    [SerializeField] float zoomMinValue = 50f;
    [SerializeField] float zoomMaxValue = 120f;
    [SerializeField] float zoomSpeed = 10f;
    [SerializeField] float moveSpeed = 30f;

    private float curFovSize;
    private float targetFovSize;

    private void Start()
    {
        curFovSize = virtualCamera.m_Lens.FieldOfView;
        targetFovSize = curFovSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        targetFovSize += Mouse.current.scroll.ReadValue().normalized.y * zoomAmount;
        targetFovSize = Mathf.Clamp(targetFovSize, zoomMinValue, zoomMaxValue); //targetSize value is between min and max
        curFovSize = Mathf.Lerp(curFovSize, targetFovSize, zoomSpeed * Time.deltaTime); //update the curPos
        virtualCamera.m_Lens.FieldOfView = curFovSize; //assign that updated pos to the FOV
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
