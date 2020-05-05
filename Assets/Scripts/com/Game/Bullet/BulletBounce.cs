using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour {
    private EnemyTank enemyTank;

    /// <summary>
    /// 爆炸特效
    /// </summary>
    public GameObject FXExp;

    /// <summary>
    /// 拥有者对象
    /// </summary>
    public GameObject Owner;

    /// <summary>
    /// 刚体
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// 飞行速度
    /// </summary>
    private float moveSpeed = 10;

    //玩家受伤害值
    public int playerDamage;

    //敌人受伤害值
    public int enermyDamage;

    //减速敌人
    public int loseSpeed = 0;
    void Start()
    {

        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = this.transform.forward * moveSpeed;//前进方向添加力
        playerDamage = 5;
        enermyDamage = 10;
    }

    /// <summary>
    /// 碰撞检测函数
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (Owner.gameObject == other.gameObject)
        {
            return;
        }

        //伤害处理
        if (Owner != null && Owner.gameObject.tag == "Enemy" && other.gameObject.tag == "Player")//玩家坦克伤害
        {
            other.gameObject.GetComponent<PlayerTank>().UpdateDamage(playerDamage);//执行伤害函数
        }
        if (Owner != null && Owner.gameObject.tag == "Player" && other.gameObject.tag == "Enemy")//敌人坦克伤害
        {
            enemyTank = other.gameObject.GetComponent<EnemyTank>();
            enemyTank.moveSpeed = 0;
            other.gameObject.GetComponent<EnemyTank>().UpdateDamage(enermyDamage);
        }
        //爆炸特效
        GameObject.Instantiate(FXExp, this.transform.position, Quaternion.identity);

        //销毁子弹
        GameObject.Destroy(this.gameObject);
    }
}
