using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;

    private float _minValue = 0f;
    private float _maxValue = 1f;

    public void SetRadialValue(float value)
    {
        value = Mathf.Clamp(value, _minValue, _maxValue);

        if (foregroundImage != null)
        {
            foregroundImage.fillAmount = value;
        }
    }

    public void SetMinMaxValues(float minValue, float maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
    }
}
