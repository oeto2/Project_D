using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI promptText;
    public Slider loadingBar;
    private void Awake()
    {
        instance = this;
    }
}
