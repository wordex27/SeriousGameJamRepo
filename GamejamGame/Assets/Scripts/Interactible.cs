using UnityEngine;

public abstract class Interactible : MonoBehaviour
{

    public string promptMessage;
    
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        
    }
}
