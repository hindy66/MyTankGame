using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStartSceneData : MonoBehaviour {

    public GameObject settingPanel;

    //保存玩家姓名
    public string playername;


	// Use this for initialization
	void Start () {
        GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
