using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {
    private GameObject StartPanel;

    //获取背景音乐
    public  AudioSource backgroundmusic;

    //获取音量滑动条
    public Slider sd;

    //获取音效开关按钮
    public Toggle music_toggle;

    //获取设置界面的输入框
    public InputField namefield;

    //玩家名字
    public string playername;

    //获得游戏数据保存脚本
    public GameObject data;

	// Use this for initialization
	void Start () {
        StartPanel =GameObject.FindGameObjectWithTag("StartPanel");
        if( StartPanel==null)
        {
            Debug.Log("未能获取StartPanel！");
        }
        else
        {
           backgroundmusic=StartPanel.GetComponent<AudioSource>();
            if(backgroundmusic==null)
            {
                Debug.Log("未能获取背景音乐！");
            }
        }
        data = GameObject.FindGameObjectWithTag("Data");
       
       
	}


	
	// Update is called once per frame
	void Update () {

	}



    public void ControlMusic()
    {
        backgroundmusic.volume = sd.value;
    }

    public void Music_PlayOrStop ()
    {
        if(music_toggle.isOn!=true)
        {
            backgroundmusic.Stop();        
        }
        else
        {
            backgroundmusic.Play();
        }
    }



    public void GetName()
    {
        playername = namefield.text;
        data.GetComponent<SaveStartSceneData>().playername = playername;

    }





}
