using UnityEngine;

public class Coin : Collectible
{
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnCollect()
    {
        animator.SetTrigger("Collected");
    }

    public void OnCollectAnimationEnd()
    {
        Destroy(gameObject);
    }
}
