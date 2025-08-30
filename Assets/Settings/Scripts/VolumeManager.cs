using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer Master;
    [SerializeField] private Slider Masterslider;
    [SerializeField] private Slider Musicslider;
    [SerializeField] private Slider SFXslider;

    private void Start()
    {
        LoadVolume();
    }
    public void SetMasterVolume()
    {
        float volume = Masterslider.value;
        Master.SetFloat("MasterVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXslider.value;
        Master.SetFloat("SFXVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = Musicslider.value;
        Master.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void LoadVolume()
    {
        Masterslider.value = PlayerPrefs.GetFloat("MasterVolume", 100);
        Musicslider.value = PlayerPrefs.GetFloat("MusicVolume", 100);
        SFXslider.value = PlayerPrefs.GetFloat("SFXVolume", 100);
        SetMasterVolume();
        SetSFXVolume();
        SetMusicVolume();

    }

}
