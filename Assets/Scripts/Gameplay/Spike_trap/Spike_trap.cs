using UnityEngine;

public class Spike_trap : MonoBehaviour
{
    [SerializeField] bool moving = false;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (moving) animator.SetBool("Moving", moving);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Player.TakeDamege();
        }
    }
}
