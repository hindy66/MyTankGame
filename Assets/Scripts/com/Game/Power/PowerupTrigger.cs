using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour {

    /// <summary>
    /// 道具位置数组
    /// </summary>
    public GameObject[] PowerupArr;

    /// <summary>
    /// 道具预制体数组
    /// </summary>
    public GameObject[] PowerupPrefabArr;

    /// <summary>
    /// 当前在线道具列表
    /// </summary>
    public static List<GameObject> PowerList = new List<GameObject>();

	void Start () {
        PowerList.Clear();
        createPower();
	}
	
	void Update () {
        if (PowerList.Count == 0)
        {
            createPower();
        }
        Debug.Log(PowerList.Count);
		
	}

    /// <summary>
    /// 创建道具函数
    /// </summary>
    private void createPower()
    {
        for (int i = 0; i < PowerupArr.Length; i++)
        {
            GameObject pos = PowerupArr[i];
            //实例化
            GameObject power=GameObject.Instantiate(PowerupPrefabArr[Random.Range(0, 4)], pos.transform.position, pos.transform.rotation);
            PowerupTrigger.PowerList.Add(power);
        }
    }

    
}
