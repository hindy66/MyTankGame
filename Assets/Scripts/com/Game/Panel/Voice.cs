using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Voice : MonoBehaviour {
    //获取背景音乐
    public AudioSource backgroundmusic;

    //获取音量滑动条
    public Slider sd;

    //获取音效开关按钮
    public Toggle music_toggle;

	// Use this for initialization
	void Start () {
        backgroundmusic = GameObject.Find("GameController").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Music_PlayOrStop()
    {
        if (music_toggle.isOn != true)
        {
            backgroundmusic.Stop();
        }
        else
        {
            backgroundmusic.Play();
        }
    }

    public void ControlMusic()
    {
        backgroundmusic.volume = sd.value;
    }


    public void SettingPanelColse()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SettingOnClick()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void BackOnClick ()
    {
        SceneManager.LoadScene("Start");
    }

    public void Quit ()
    {
        Application.Quit();
    }
   
}
