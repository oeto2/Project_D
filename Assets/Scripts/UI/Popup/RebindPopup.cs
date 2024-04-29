using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindPopup : MonoBehaviour
{
    public InputActionReference moveRef, interactionRef, jumpRef, runRef, attackRef,inventoryRef,equipRef,defenseRef;

    private void OnEnable()
    {
        moveRef.action.Disable();
        interactionRef.action.Disable();
        jumpRef.action.Disable();
        runRef.action.Disable();
        attackRef.action.Disable();
        inventoryRef.action.Disable();
        equipRef.action.Disable();
        defenseRef.action.Disable();
    }

    private void OnDisable()
    {
        moveRef.action.Enable();
        interactionRef.action.Enable();
        jumpRef.action.Enable();
        runRef.action.Enable();
        attackRef.action.Enable();
        inventoryRef.action.Enable();
        equipRef.action.Enable();
        defenseRef.action.Enable();
    }
}
