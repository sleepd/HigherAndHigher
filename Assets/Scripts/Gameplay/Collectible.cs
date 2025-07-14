using UnityEngine;

public class Collectible : MonoBehaviour
{
    protected virtual void OnCollect()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnCollect();
        }
    }
}
