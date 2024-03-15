using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenuManager : MonoBehaviour
{
    public InputActionReference moveRef, interactionRef, jumpRef, runRef, attackRef;

    public void OnOk()
    {
        this.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        moveRef.action.Disable();
        interactionRef.action.Disable();
        jumpRef.action.Disable();
        runRef.action.Disable();
        attackRef.action.Disable();
    }

    private void OnDisable()
    {
        moveRef.action.Enable();
        interactionRef.action.Enable();
        jumpRef.action.Enable();
        runRef.action.Enable();
        attackRef.action.Enable();
    }
}
