using UnityEngine;
using PrimeTween;

public class Target : ShootableObject
{
    [SerializeField] private float _distance;
    [SerializeField] private float _duration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartAnimation()
    {
        Tween.PositionX(transform, endValue: _distance, duration: _duration, cycles: -1, cycleMode: CycleMode.Yoyo);

    }
}
