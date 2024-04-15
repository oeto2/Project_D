using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public static ItemDescription instance;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    void Awake()
    {
        instance = this;
        this.gameObject.SetActive(false);
    }
}
