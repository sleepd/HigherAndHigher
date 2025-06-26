using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveHeight = 5f;
    public float moveSpeed = 2f;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isRaised = false;
    private bool shouldMove = false;
    private bool isCoolingDown = false;
    private Vector3 destination;
    public float cooldownAfterMove = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + Vector3.up * moveHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                transform.position = destination;
                shouldMove = false;
                isRaised = !isRaised;
            }
            StartCoroutine(Cooldown());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCoolingDown)
        {
            if (!shouldMove)
            {
                destination = isRaised ? originalPosition : targetPosition;
                shouldMove = true;
            }

        }
    }

    private System.Collections.IEnumerator Cooldown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownAfterMove);
        isCoolingDown = false;
    }
}
