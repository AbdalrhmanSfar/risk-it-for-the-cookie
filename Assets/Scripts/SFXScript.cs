using UnityEngine;
using UnityEngine.UI;

public class SFXScript : MonoBehaviour
{
    public static SFXScript instance;
    [SerializeField] private AudioSource SFXObject;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip boopSound;
    [SerializeField] private Slider Masterslider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;


    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void PlaySFX(AudioClip clip, Transform spawnTrans, float volume = 1, float pitch = 1)
    {
        AudioSource audioSource = Instantiate(SFXObject, spawnTrans.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void clickSFX()
    {

        SFXScript.instance.PlaySFX(clickSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void slideSFX1()
    {
        float pitch;
        pitch = 1f + Masterslider.value;
        SFXScript.instance.PlaySFX(boopSound, transform, 1, pitch);
    }
    public void slideSFX2()
    {
        float pitch;
        pitch = 1f + MusicSlider.value;
        SFXScript.instance.PlaySFX(boopSound, transform, 1, pitch);
    }
    public void slideSFX3()
    {
        float pitch;
        pitch = 1f + SFXSlider.value;
        SFXScript.instance.PlaySFX(boopSound, transform, 1, pitch);
    }
}
