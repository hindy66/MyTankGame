using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 敌人坦克管理器类
/// 
/// </summary>
public class EnemyTankTrigger : MonoBehaviour {

    /// <summary>
    /// 敌人随机数组
    /// </summary>
    public GameObject[] EnemyPrefabArr;

    /// <summary>
    /// 当前在线敌人坦克列表
    /// </summary>
    public static List<GameObject> EnemyList = new List<GameObject>();

    /// <summary>
    /// 敌人位置数组
    /// </summary>
    public GameObject[] EnemyPosArr;


	
	void Start () {
        EnemyList.Clear();
        createEnemy();
        
	}

    void Update()
    {

        if (EnemyList.Count == 0)
        {
            
            createEnemy();
            //StopCoroutine(RoundShow());
        }

    }

    /// <summary>
    /// 创建敌人坦克函数
    /// </summary>
    private void createEnemy()
    {
        for (int i = 0; i < EnemyPosArr.Length;i++ )
        {
            GameObject pos = EnemyPosArr[i];
            //实例化
            GameObject.Instantiate(EnemyPrefabArr[Random.Range(0,4)],pos.transform.position,pos.transform.rotation);
        }
    }

  

}
