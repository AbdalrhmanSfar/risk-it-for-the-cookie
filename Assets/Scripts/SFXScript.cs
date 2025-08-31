using UnityEngine;
using UnityEngine.UI;

public class SFXScript : MonoBehaviour
{
    public static SFXScript instance;
    public LogicScript logic;
    [SerializeField] private AudioSource SFXObject;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip boopSound;
    [SerializeField] private AudioClip chocoSound;
    [SerializeField] private AudioClip redbullSound;
    [SerializeField] private AudioClip awakeSound;
    [SerializeField] private AudioClip hit1Sound;
    [SerializeField] private AudioClip hit2Sound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private Slider Masterslider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;


    void Start()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        musicSource.pitch = 1 + 0.001f * logic.score;
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
    public void awakeSFX()
    {

        SFXScript.instance.PlaySFX(awakeSound, transform, 1, 1);
    }
    public void boopSFX()
    {

        SFXScript.instance.PlaySFX(boopSound, transform, 0.9f, Random.Range(0.8f, 1.2f));
    }
    public void hit1SFX()
    {

        SFXScript.instance.PlaySFX(hit1Sound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void hit2SFX()
    {

        SFXScript.instance.PlaySFX(hit2Sound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void redbullSFX()
    {

        SFXScript.instance.PlaySFX(redbullSound, transform, 0.9f, Random.Range(0.8f, 1.2f));
    }
    public void chocoSFX()
    {

        SFXScript.instance.PlaySFX(redbullSound, transform, 0.9f, Random.Range(0.8f, 1.2f));
    }
    public void dashSFX()
    {

        SFXScript.instance.PlaySFX(dashSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void deathSFX()
    {

        SFXScript.instance.PlaySFX(deathSound, transform, 1, Random.Range(0.8f, 1.2f));
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
