using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI promptText;
    public Slider loadingBar;

    [SerializeField] private PlayerInputaction playerInputAction;
    private void Awake()
    {
        instance = this;
    }
}
