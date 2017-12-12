using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    [SerializeField]
    private AudioClip battleSound;
    [SerializeField]
    private AudioClip bossSound;
    [SerializeField]
    private AudioSource audioSource;
	public void PlayBattleSound()
    {
        audioSource.clip = battleSound;
        audioSource.Play();
    }

    public void PlayBossSound()
    {
        audioSource.clip = bossSound;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
