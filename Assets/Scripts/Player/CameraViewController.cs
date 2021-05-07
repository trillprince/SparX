using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraViewController : MonoBehaviour
{
    public float mouseXSensitivity = 50f;
    public float mouseYSensitivity = 50f;

    public Transform playerTransform;

    private float xRotation;

    private PhotonView photonViewRef;

    private Camera cameraRef;

    private void Awake()
    {
        cameraRef = GetComponentInParent<Camera>();
        cameraRef.enabled = false;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        photonViewRef = GetComponentInParent<PhotonView>();
        if (photonViewRef.IsMine)
        {
            cameraRef.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonViewRef.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X") * 8 * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 8 * mouseYSensitivity * Time.smoothDeltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerTransform.Rotate(Vector3.up * mouseX);
        }
    }
}