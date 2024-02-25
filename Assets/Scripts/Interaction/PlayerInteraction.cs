using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public float playerReach = 1f;
    private Interactable currentInteractable;

    private void Update()
    {
        CheckInteraction();
    }

    public void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void CheckInteraction()
    {
        var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        EnableInteractionText(newInteractable.message);
    }

    private void DisableCurrentInteractable()
    {
        DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable = null;
        }
    }

    private void EnableInteractionText(string text)
    {
        interactionText.text = $"{text} (E)";
        interactionText.gameObject.SetActive(true);
    }

    private void DisableInteractionText()
    {
        interactionText.text = "";
        interactionText.gameObject.SetActive(false);
    }
}