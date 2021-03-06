using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = FindObjectOfType<Player>()?.transform;
        if (transform != null)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

    }
}
