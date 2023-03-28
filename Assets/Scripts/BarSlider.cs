using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSlider : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSlider(int fill)
    {
        slider.maxValue = fill;
        slider.value = fill;
    }

    public void SetFill(int fill)
    {
        slider.value = fill;
    }
}
