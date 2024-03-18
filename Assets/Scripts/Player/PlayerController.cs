using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Slope")]
    public float maxSlopeAngle;
    public RaycastHit slopeHit;
    public float stepHeight;
    public float stepSmooth;
}
