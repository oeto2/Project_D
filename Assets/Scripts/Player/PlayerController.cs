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

    public LayerMask groundLayerMask;
}
