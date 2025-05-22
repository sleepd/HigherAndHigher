using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 5f;
    private Vector3 targetPosition;
    private Vector3 targetDir;
    private bool hasTarget = false;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnEnable()
    {
        Vector3 newDir = RandomDirectionWithinAngle(transform.up, 90);
        transform.rotation = Quaternion.LookRotation(newDir);
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed * 10000f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Die();
    }

    void FixedUpdate()
    {
        Vector3 direction;

        if (hasTarget) direction = (targetPosition - transform.position).normalized;
        else direction = targetDir;

        rb.AddForce(direction * speed * Time.deltaTime);
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
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    Vector3 RandomDirectionWithinAngle(Vector3 forward, float maxAngleDegrees)
    {
        // 随机一个角度范围内的旋转轴（避免只绕Y轴）
        Vector3 randomAxis = Random.onUnitSphere;

        // 生成一个随机角度（±范围内）
        float randomAngle = Random.Range(-maxAngleDegrees, maxAngleDegrees);

        // 绕这个轴旋转 forward 向量
        return Quaternion.AngleAxis(randomAngle, randomAxis) * forward;
    }
}
