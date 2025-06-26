using UnityEngine;

public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] TargetManager targetManager;
    public void Interact()
    {
        targetManager.ShowTargets();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
