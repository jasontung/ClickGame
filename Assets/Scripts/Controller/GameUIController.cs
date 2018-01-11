using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIController : MonoBehaviour {
    [SerializeField]
    private Text lvLabel;
    [SerializeField]
    private Text attackLabel;
    [SerializeField]
    private Slider expSlider;
	
    public void UpdateLv(int lv)
    {
        lvLabel.text = lv.ToString();
    }

    public void UpdateAttack(int attack)
    {
        attackLabel.text = attack.ToString();
    }

    public void UpdateExpSlider(int exp, int minValue, int maxValue)
    {
        expSlider.minValue = minValue;
        expSlider.maxValue = maxValue;
        expSlider.value = exp;
    }
}
