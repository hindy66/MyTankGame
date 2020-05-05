using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerTank : MonoBehaviour {
    //护罩
    GameObject sheildFxp;

    //获取敌人坦克挂载的脚本
    public EnemyTank enemyTank;

    /*获取游戏控制脚本*/
    private GameControl gameC;


    //获取计分板的生命条
    public Slider scoreBoardHealthSlider;

    public Text scoreBoardHealthText;


    //游戏结束面板;
    public GameObject gameOverPanel;


    //获取摇杆脚本
    private ScrollCircel s;

    //获取射击摇杆面板
    private FireScrollCircel f;


    public string playername;

    //所保存的玩家数据
    public SaveStartSceneData data;

    //当前发射的子弹类型
    public bool normalShoot;

    public Transform TankTurret;//坦克炮台
    public GameObject BulletPrefab;//子弹预制体
    public GameObject BulletPowerPrefab;//增强子弹预制体
    public GameObject BulletBouncePrefab;//减速子弹预制体
    public GameObject ShotPos;//发射位置
    private Rigidbody rb;
    private float moveSpeed = 7;//米/秒

    //发射特效预制体
    public GameObject ExShot;

    private float nextFire=0.0f;
    private float fireRate = 0.5f;

    int mask = -1;

    /// <summary>
    /// 生命值进度条
    /// </summary>
    public Slider HPSlider;
    /// <summary>
    /// 生命值
    /// </summary>
    public float hp;

    private float totalHp;

    /// <summary>
    /// 爆炸特效
    /// </summary>
    public GameObject TankExp;


   /// <summary>
   /// 姓名
   /// </summary>
    public Text nameText;

    void Awake()
    {
        totalHp = hp= 100;
        //HPSlider.value = hp / totalHp;
        HPSlider.value = hp;
    }

    void Start()
    {

        gameC = GameObject.Find("GameController").GetComponent<GameControl>();
        s = GameObject.Find("DirectionButton").GetComponent<ScrollCircel>();
        f = GameObject.Find("FireButton").GetComponent<FireScrollCircel>();
        sheildFxp = transform.Find("sheild").gameObject;
        rb = this.transform.GetComponent<Rigidbody>();

        normalShoot = true;

        //坦克跟随
        Camera.main.GetComponent<FollowTarget>().Target = this.transform;


        data = GameObject.FindGameObjectWithTag("Data").GetComponent<SaveStartSceneData>();
        playername = data.playername;
        SetName();     
    }


    
    void Update()
    {

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitInfo;

        //if (Physics.Raycast(ray, out hitInfo, 200, mask))
        //{
        //    Vector3 target = hitInfo.point;
        //    target.y = TankTurret.position.y;
        //    TankTurret.LookAt(target);
        //}


        //1 旋转移动
        tankMove();

        //2 炮口旋转
       // TankTurret.rotation = Quaternion.Euler(10,0,20);
        tankRate();


        //3 子弹攻击
        //tankshot();

        
       
    }
    void OnDestroy()
    {
        Debug.Log("游戏结束");
    }


    private void tankMove()
    {
        Vector3 moveDir;
        //旋转、移动判断
        if (s.moveDirection.x != 0 || s.moveDirection.z!=0)
        {
            moveDir.x = s.moveDirection.x;
            moveDir.z = s.moveDirection.z;
            moveDir.y = 0;
            move(moveDir);
        }

    }
    private void tankRate()
    {
        if (f.moveDirection.x != 0 || f.moveDirection.z != 0)
        {
            float dirx = f.moveDirection.x;
            float dirz = f.moveDirection.z;
            TankTurret.rotation = Quaternion.LookRotation(new Vector3(dirx, 0, dirz));
            tankshot();
            
        }
     
    }

    private void tankshot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shoot();
        }
            
        
    }
    private void move(Vector3 dir)
    {
        //旋转
        this.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
        //移动
        Vector3 targetPos = this.transform.position + this.transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(targetPos);
        //rb.velocity = dir*moveSpeed;
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    private void shoot()
    {
        if(normalShoot==true)
        {
            //实例化子弹
            GameObject bullet = GameObject.Instantiate(BulletPrefab, ShotPos.transform.position, ShotPos.transform.rotation);
            bullet.GetComponent<Bullet>().Owner = this.gameObject;
        }
        else
        {
            if(gameC.bulletPowerNum>0&&gameC.bulletBounceNum==0)
            {
                GameObject bullet = GameObject.Instantiate(BulletPowerPrefab, ShotPos.transform.position, ShotPos.transform.rotation);
                bullet.GetComponent<BulletPower>().Owner = this.gameObject;
                gameC.BulletPowerShoot();
            }
            
            if(gameC.bulletPowerNum==0&&gameC.bulletBounceNum>0)
            {
                GameObject bullet = GameObject.Instantiate(BulletBouncePrefab, ShotPos.transform.position, ShotPos.transform.rotation);
                bullet.GetComponent<BulletBounce>().Owner = this.gameObject;
                gameC.BulletBounceShoot();
            }

            if(gameC.bulletPowerNum>0&&gameC.bulletBounceNum>0)
            {
                GameObject bullet = GameObject.Instantiate(BulletPowerPrefab, ShotPos.transform.position, ShotPos.transform.rotation);
                bullet.GetComponent<BulletPower>().Owner = this.gameObject;
                gameC.BulletPowerShoot();
                gameC.BulletBounceShoot();
            }


            if (gameC.bulletPowerNum==0 && gameC.bulletBounceNum==0)
            {
                normalShoot = true;
            }                          
        }
       

        //发射特效
        GameObject.Instantiate(ExShot, ShotPos.transform.position, ShotPos.transform.rotation);
    }

    /// <summary>
    /// 伤害更新函数
    /// </summary>
    /// <param name="num"></param>
    public void UpdateDamage(int num)
    {
        
        if(gameC.sheildTime>0)
        {
            hp--;
            HPSlider.value = hp;
            scoreBoardHealthSlider.value = hp;
            scoreBoardHealthText.text = "" + hp;
        }
        else
        {
            if(hp>=90&&num<0)
            {
                hp =100;
                HPSlider.value = hp;
                scoreBoardHealthSlider.value = hp;
                scoreBoardHealthText.text = "" + hp;
            }
            hp -= num;
            HPSlider.value = hp;
            scoreBoardHealthSlider.value = hp;
            scoreBoardHealthText.text = "" + hp;
            Debug.Log("Player Hp:" + hp);
        }



        if (hp<= 0)
        {
            dead();//死亡
        }
    }

    /// <summary>
    /// 死亡处理函数
    /// </summary>
    private void dead()
    {
        //游戏结束
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        //爆炸特效
        GameObject.Instantiate(TankExp, this.transform.position, Quaternion.identity);
        //销毁
        GameObject.Destroy(this.gameObject);
        

    }

     private void SetName()
    {
       nameText.text = playername;
    }

     void OnTriggerExit(Collider other)
     {
         if (other.gameObject.tag == "BulletBounce")
         {
             gameC.BulletBounce();
             Destroy(other.gameObject);
         }

         if (other.gameObject.tag == "BulletPower")
         {
             gameC.BulletPower();
             Destroy(other.gameObject);
         }

         if (other.gameObject.tag == "Health")
         {
             if(hp<80)
             {
                 hp += 20;                
             }
             else
             {
                 hp = 100;
             }
             HPSlider.value = hp;
             scoreBoardHealthSlider.value = hp;
             scoreBoardHealthText.text = "" + hp;
             Destroy(other.gameObject);
         }

         if (other.gameObject.tag == "Shield")
         {
             gameC.SheildPick();
             sheildFxp.SetActive(true);
             //PowerupTrigger.PowerList.Remove(other.gameObject);
             Destroy(other.gameObject);
             gameC.SheildCountdown();
         }

     }
    
}
