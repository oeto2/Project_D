using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider loadingBar;
    private void Awake()
    {
        instance = this;
    }
}
