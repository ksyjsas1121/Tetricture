using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource backGroundSound;

    public AudioClip titleSounds;
    public AudioClip mainSounds;
    public AudioClip gameSounds;
    public AudioClip touchSounds;

    public CanvasGroup tapText;
    public AudioSource tapSoundSource;
    public AudioClip tapSound;

    public float backVolume;
    // Start is called before the first frame update
    void Start()
    {
        tapSoundSource.volume = 0.5f;
        backGroundSound.clip = titleSounds;
        backGroundSound.Play();
        backGroundSound.volume = 0;
        DOTween.To(() => backGroundSound.volume, x => backGroundSound.volume = x, 1, 2);
        Debug.Log("Ω√¿€");
        StartCoroutine(FadeOut());
        //tapSound = Resources.Load<AudioClip>("GameStart");
    }

    public void StartTap()
    {
        tapSoundSource.PlayOneShot(tapSound);
        GoMain();
        
        
    }

    public void GoMain()
    {
        backGroundSound.clip = mainSounds;
        backGroundSound.Play();
        backGroundSound.volume = 0;
        DOTween.To(() => backGroundSound.volume, x => backGroundSound.volume = x, 1,8 );
    }
    public void GoGame()
    {
        backGroundSound.clip = gameSounds;
        backGroundSound.Play();
        backGroundSound.volume = 0;
        DOTween.To(() => backGroundSound.volume, x => backGroundSound.volume = x, 1, 8);
    }

    public void Taps()
    {
        tapSoundSource.PlayOneShot(tapSound);
    }

    IEnumerator FadeOut()
    {
        tapText.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        tapText.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
