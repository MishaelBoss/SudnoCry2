using UnityEngine;
using UnityEngine.Audio;

public class valumeInt : MonoBehaviour
{
    public string valumeParametr = "masterVol";
    public AudioMixer mixer;

    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(valumeParametr, valumeParametr == "effectVol" ? 0f : -80f);
        mixer.SetFloat(valumeParametr, volumeValue);
    }
}