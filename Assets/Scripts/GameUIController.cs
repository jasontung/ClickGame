using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIController : MonoBehaviour {
    public Text lvLabel;
    public Text coinLabel;
    public Text attackLabel;
    public Slider expSlider;
    public UIComponent levelUpEffect;
    public UIComponent bossMsgEffect;
    public UIComponent stageClearEffect;
    public UIComponent stageFailEffect;
    public CountDownTimer countDownTimerEffect;

    public void UpdateLv(int lv)
    {
        lvLabel.text = lv.ToString();
    }

    public void UpdateCoin(int coin)
    {
        coinLabel.text = coin.ToString();
    }

    public void UpdateAttack(int attack)
    {
        attackLabel.text = attack.ToString();
    }

    public void UpdateExpSlider(int exp)
    {
        expSlider.value = exp;
    }

    public void UpdateExpSlider(int exp, int minValue, int maxValue)
    {
        expSlider.minValue = minValue;
        expSlider.maxValue = maxValue;
        expSlider.value = exp;
    }
}
