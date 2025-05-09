using System.ComponentModel.Design;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{   
    public bool IsGrounded { get; private set;}
    [SerializeField] float radius = 0.3f;
    [SerializeField] float distance = 0.3f;
    [SerializeField] LayerMask groundMask;
    


    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void Check()
    {
        Vector3 origin = transform.position + Vector3.up * 0.4f;
        IsGrounded = Physics.SphereCast(origin, radius, Vector3.down, out RaycastHit hit, distance, groundMask, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmos()
{
    if (!Application.isPlaying) return;

    Vector3 origin = transform.position + Vector3.up * 0.1f;
    Vector3 direction = Vector3.down;

    // 起点
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(origin, radius);

    // 终点
    Vector3 end = origin + direction * distance;
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(end, radius);

    // 连接线
    Debug.DrawLine(origin, end, Color.red);
}
}
