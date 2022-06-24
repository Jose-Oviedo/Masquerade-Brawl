using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMusic : MonoBehaviour
{

    public GameObject Music;
    public MusicSettings musicsettings;
    public Toggle checkbox;


    // Start is called before the first frame update
    void Start()
    {
        Music = GameObject.FindGameObjectWithTag("Music");
        musicsettings = Music.GetComponent<MusicSettings>();
        if (PlayerPrefs.GetString("Mute").Equals("true"))
        {
            checkbox.isOn = true;
        }
        else
        {
            checkbox.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void music(bool boton)
    {
        musicsettings.MuteMusic(boton);
    }
}
