using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour {
  
    //用于获取playerInformation脚本的变量
    private playerInformation playerI;

    //玩家姓名
    public string playername;

    //等级
    private string level;


    // Use this for initialization
    void Start()
    {
       playerI=GameObject.FindGameObjectWithTag("playerInfo").GetComponent<playerInformation>();
       playername = playerI.nameInfo.text;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void SetName()
    //{
    //    nameInfo.GetComponent<Text>().text = playername;
    //}

    //public void SetLevel()
    //{
    //    levelInfo.GetComponent<Text>().text = "LV " + level;
    //}
}
