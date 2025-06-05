using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public int MaxHealth;
    public float JumpForce;
    public float JumpInputThreshold;
    public float JumpBufferingThreshold;
    public float JumpForwardForceWalking;
    public float JumpForwardForceRunning;
    public float Gravity;
    public float RotationSpeed;
    public float MovementDeadZone = 0.1f;
    public float WalkingToRunningThreshold = 0.5f;
    public float fireRate = 1f;
    public GameObject BulletPrefab;
}
