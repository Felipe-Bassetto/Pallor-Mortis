using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider SoundSlider;
    public Toggle fullScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundSlider.value = 1f;
        SetSoundVolume(SoundSlider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSoundVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("SoundMixer", -80f); // Mudo
        }
        else
        {
            audioMixer.SetFloat("SoundMixer", Mathf.Log10(volume) * 20f);
        }
    }

    public void GetSlider()
    {
        SetSoundVolume(SoundSlider.value);
    }

    /*public void SetFullScreen()
    {
        if (fullScreen.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
            Screen.SetResolution(1280, 720, false);
        }
    }*/
}
