using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class BarValueDisplay : MonoBehaviour {
    public Text label;
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChange);
        OnValueChange(slider.value);
    }

    public void OnValueChange(float val)
    {
        val = Mathf.FloorToInt(val);
        label.text = val + "/" + slider.maxValue;
    }
}
