using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public static ItemDescription instance;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    void Awake()
    {
        instance = this;
    }
}
