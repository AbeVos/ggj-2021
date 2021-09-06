using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Clock : MonoBehaviour
{
    private TextMeshProUGUI clockText;
    
    void Start()
    {
        clockText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        clockText.text = DateTime.Now.ToString("hh:mm tt");
    }
}
