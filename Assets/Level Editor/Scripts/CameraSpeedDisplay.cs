using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSpeedDisplay : MonoBehaviour
{
    public Slider slider;
    public Text text;

    void Update()
    {
        // Converts value to a percentage
        // This is TOP LEVEL programming here
        int percentage = (int)(slider.value * 100.0f);
        text.text = percentage.ToString() + "%";
    }
}
