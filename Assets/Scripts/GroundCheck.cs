using System.ComponentModel.Design;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{   
    public bool IsGrounded { get; private set;}
    [SerializeField] Vector3 origin;
    [SerializeField] Vector3 boxSize = new Vector3(0.5f, 0.1f, 0.5f);
    [SerializeField] LayerMask groundMask;
    

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void Check()
    {

        IsGrounded = Physics.CheckBox(origin + transform.position, boxSize * 0.5f, Quaternion.identity, groundMask);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(origin+ transform.position, boxSize);
    }
}
