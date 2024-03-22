using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float camCurXRot;
    [Header("Look")]
    public float minXLook;
    public float maxXLook;
    public float lookSensitivity;

    [Header("Jump")]
    public bool isJump;
    public float gravity;
    public Vector3 velocity;
}
