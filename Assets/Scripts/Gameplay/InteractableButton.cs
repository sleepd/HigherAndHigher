using UnityEngine;

public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] ControlledObject[] controlledObjects;
    public void Interact()
    {
        foreach (ControlledObject go in controlledObjects)
        {
            go.Active();
        }
    }
}