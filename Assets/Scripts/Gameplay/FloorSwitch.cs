using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    [SerializeField] ControlledObject controlledObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            controlledObject.Active();
        }
    }
}
