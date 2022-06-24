using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicSettings : MonoBehaviour
{
    public static MusicSettings Instance;
    public Toggle checkbox;
    public AudioSource Music;
    public AudioMixer Mixer;

    private void Awake()
    {
        if (MusicSettings.Instance == null)
        {
            MusicSettings.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Mixer.SetFloat("MyExposedParam 1", Mathf.Log10(PlayerPrefs.GetFloat("VolumeLevel")) * 20);
        if (PlayerPrefs.GetString("Mute").Equals("true"))
        {
            checkbox.isOn = true;
        }else
        {
            checkbox.isOn = false;
        }
        MuteMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteMusic(bool boton)
    {
        if (boton)
        {
            Music.Pause();
            PlayerPrefs.SetString("Mute", "true");
        }
        else
        {
            Music.Play();
            PlayerPrefs.SetString("Mute", "false");
        }
    }

    public void MuteMusic()
    {
        if (checkbox.isOn)
        {
            Music.Pause();
            PlayerPrefs.SetString("Mute", "true");
        }
        else
        {
            Music.Play();
            PlayerPrefs.SetString("Mute", "false");
        }
    }

    public void MusicLevel(float Level)
    {
        Mixer.SetFloat("MyExposedParam 1", Mathf.Log10(Level)*20);
        PlayerPrefs.SetFloat("VolumeLevel", Level);

    }
}
