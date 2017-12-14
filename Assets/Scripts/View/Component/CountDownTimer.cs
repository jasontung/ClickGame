using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Animator))]
public class CountDownTimer : MonoBehaviour {
    [SerializeField]
    private Text timerLabel;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public IEnumerator Show(float startTime)
    {
        gameObject.SetActive(true);
        float remainTime = startTime;
        while(remainTime > 0)
        {
            if (gameObject.activeInHierarchy == false)
                yield break;
            timerLabel.text = remainTime.ToString("0.00");
            animator.SetFloat("remainTime", remainTime);
            yield return null;
            remainTime -= Time.deltaTime;
        }
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
