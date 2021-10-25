using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private IInteractable currentInteractable = null;

    private void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        if (currentInteractable == null) { return; }

        currentInteractable.Interact(transform.root.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable == null) { return; }

        currentInteractable = interactable;
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();

        if (interactable == null) { return; }

        if (interactable != currentInteractable) { return; }

        currentInteractable = null;
    }
}
