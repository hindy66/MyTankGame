using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInformation : MonoBehaviour {
    //姓名信息框
    public Text nameInfo;

    //等级信息框
    public Text levelInfo;

    //所保存的玩家数据
    public SaveStartSceneData data;


	// Use this for initialization
	void Start () {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent <SaveStartSceneData>();
        nameInfo.text = data.playername;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetName(string name)
    {
        nameInfo.text=name;
    }


    public void SetLevel(string level)
    {
        levelInfo.text=level;
    }
}
