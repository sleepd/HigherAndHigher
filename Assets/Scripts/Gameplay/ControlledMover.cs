using UnityEngine;

public class ControlledMover : ControlledObject
{
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float speed;
    private bool _shouldMove = false;

    public override void Active()
    {
        base.Active();
        _shouldMove = true;
    }

    void Update()
    {
        if (_shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                _shouldMove = false;
            }
        }
    }
}
