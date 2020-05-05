using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// 敌人坦克类
/// </summary>
public class EnemyTank : MonoBehaviour {

    /// <summary>
    /// 子弹预设体
    /// </summary>
    public GameObject BulletPrefab;

    /// <summary>
    /// 子弹发射位置
    /// </summary>
    public GameObject ShotPos;


    /// <summary>
    /// 玩家坦克对象
    /// </summary>
    private PlayerTank playerTank;

    /// <summary>
    /// 刚体
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// 追击距离
    /// </summary>
    private float followDistance;

    /// <summary>
    /// 攻击距离
    /// </summary>
    private float attackDistance;

    /// <summary>
    /// 当前距离
    /// </summary>
    private float ctDis;

    /// <summary>
    /// 射击频率
    /// </summary>
    private float fireTime = 0.6f;

    /// <summary>
    /// 计时器
    /// </summary>
    private float nextFire;

    public float moveSpeed;//移动速度
    /// <summary>
    /// 敌人坦克初始生命值
    /// </summary>
    private int hp;

    private float totalHp;

    /// <summary>
    /// 爆炸特效
    /// </summary>
    public GameObject TankExp;

    /// <summary>
    /// 生命值进度条
    /// </summary>
    public Slider HPSlider;

    /// <summary>
    /// 敌人名字
    /// </summary>
    public Text NameText;


    //自动巡航方向
    public float waitSpeed;
    private float dirx, dirz;
    private float timer;


    void Awake()
    {
        moveSpeed = 5.0f;
        followDistance=12.0f;
        attackDistance = 5.0f;
        totalHp = hp = 100;
        timer = 0;
        dirx=dirz=0;
        waitSpeed = 1;
        //生命进度条
        HPSlider.value = hp / totalHp;

        NameText.text = "敌人坦克";

        EnemyTankTrigger.EnemyList.Add(this.gameObject);

    }

	void Start () {
        playerTank = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTank>();

        //Debug.Log(playerTank);
	}

    void Update()
    {
        //敌人坦克AI

        //1 敌人坦克追击玩家坦克
        ctDis = Vector3.Distance(this.transform.position, playerTank.transform.position);

        if(ctDis<=attackDistance)//攻击状态
        {
            attack();
        }
        else if (ctDis <= followDistance)//跟随状态
        {
            followTank();
        }
        else//待机
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                dirx = Random.Range(-1, 1f);
                dirz = Random.Range(-1, 1f);
                timer = 0;
            }
            Vector3 forward = new Vector3(dirx, 0, dirz);
            //旋转
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(forward), waitSpeed * Time.deltaTime);
            //移动
            this.transform.Translate(Vector3.forward * waitSpeed * Time.deltaTime); 
        }
    }

    private void followTank()//跟随函数
    {
        //旋转
        Vector3 forward = playerTank.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(forward), moveSpeed * Time.deltaTime);

        //移动
        this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime); 
    }

    private void attack()//攻击函数
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireTime;
            //实例化子弹
            GameObject bullet = GameObject.Instantiate(BulletPrefab, ShotPos.transform.position, ShotPos.transform.rotation);
            bullet.GetComponent<Bullet>().Owner = this.gameObject;
        }
    }

    /// <summary>
    /// 伤害更新函数
    /// </summary>
    /// <param name="num"></param>
    public void UpdateDamage(int num)
    {
        hp -= num;
        HPSlider.value = hp / totalHp;
        Debug.Log("Enemy Hp:" + hp);
        if (hp <= 0)
        {
            dead();//死亡
        }
    }

    /// <summary>
    /// 死亡处理函数
    /// </summary>
    private void dead()
    {
        //爆炸特效
        GameObject.Instantiate(TankExp, this.transform.position, Quaternion.identity);

        EnemyTankTrigger.EnemyList.Remove(this.gameObject);

        GameObject.Destroy(this.gameObject);
    }
}
