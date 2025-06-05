using UnityEngine;
using UnityEngine.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] VisualEffect bulletVFX;
    private Vector3 targetPosition;
    private Vector3 targetDir;
    private bool hasTarget = false;
    private Rigidbody rb;
    private float _age;
    private bool _isAlive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnEnable()
    {
        Vector3 newDir = RandomDirectionWithinAngle(transform.up, 90);
        transform.rotation = Quaternion.LookRotation(newDir);
        rb = GetComponent<Rigidbody>();
        bulletVFX.SetFloat("LifeTime", lifeTime);
        bulletVFX.SendEvent("OnPlay");
    }

    // Update is called once per frame
    void Update()
    {
        _age += Time.deltaTime;
        bulletVFX.SetFloat("Age", _age);
        if (_age >= lifeTime) Die();

        if (_isAlive)
        {
            Vector3 direction;
            if (hasTarget) direction = (targetPosition - transform.position).normalized;
            else direction = targetDir;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            // turnSpeed *= turnSpeed;
        }
    }

    void FixedUpdate()
    {
        if(_isAlive) rb.linearVelocity = transform.forward * speed;
    }


    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
        hasTarget = true;
    }
    public void SetTargetDir(Vector3 dir)
    {
        targetDir = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        rb.linearVelocity = Vector3.zero;
        _isAlive = false;
        // reduce the age
        if (lifeTime - _age > 1f) _age = lifeTime - 1f;
    }

    void Die()
    {
        // check the vfx age, if 
        Destroy(gameObject);
    }

    Vector3 RandomDirectionWithinAngle(Vector3 forward, float maxAngleDegrees)
    {
        Vector3 randomAxis = Random.onUnitSphere;
        float randomAngle = Random.Range(-maxAngleDegrees, maxAngleDegrees);
        return Quaternion.AngleAxis(randomAngle, randomAxis) * forward;
    }
}
