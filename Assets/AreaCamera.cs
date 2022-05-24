using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCamera : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null) virtualCamera.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null) virtualCamera.enabled = false;
    }
}
