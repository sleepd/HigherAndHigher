using Unity.Cinemachine;
using UnityEngine;

public class CameraBinder : MonoBehaviour
{
    [SerializeField] CinemachineCamera FreeLookCamera;
    [SerializeField] CinemachineCamera AimCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FreeLookCamera.Follow = GameManager.Instance.Player.transform;
        FreeLookCamera.LookAt = GameManager.Instance.Player.AimTarget;
        AimCamera.Follow = GameManager.Instance.Player.AimTarget;
    }
}
