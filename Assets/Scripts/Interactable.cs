using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Canvas _hagCanvas;

    private void Start()
    {
        _hagCanvas = GetComponentInChildren<Canvas>();
        _hagCanvas.enabled = false;
    }

    public void Interact()
    {
        _hagCanvas.enabled = true;
        Debug.Log("Interacted with item");
    }

    public void EndInteract()
    {
        _hagCanvas.enabled = false;
    }
    
}
