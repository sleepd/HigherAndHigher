using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public int MaxHealth;
    public float JumpForce;
    public float Gravity;
    public float RotationSpeed;
    public float MovementDeadZone = 0.1f;
    public float WalkingToRunningThreshold = 0.5f;
}
