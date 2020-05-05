using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// 摄像机跟随类
/// describe: 摄像机跟随目标管理
/// 
/// company:广州粤嵌通信科技股份有限公司
/// author:Jatn
/// e-mail:jatn@163.com
/// createdDate:2019-2-14
/// modifiedDate:2019-2-14
/// </summary>
public class FollowTarget : MonoBehaviour
{
    /// <summary>
    /// 跟随目标对象
    /// </summary>
    [HideInInspector]
    public Transform Target;

    /// <summary>
    /// 复位图层
    /// </summary>
    public LayerMask respawnMask;

    /// <summary>
    /// 距离
    /// </summary>
    public float distance = 10.0f;

    /// <summary>
    /// 高度
    /// </summary>
    public float height = 5.0f;

    /// <summary>
    /// 摄像机
    /// </summary>
    [HideInInspector]
    public Camera cam;

    /// <summary>
    /// 摄像机对象
    /// </summary>
    [HideInInspector]
    public Transform camTransform;

    void Start()
    {
        cam = GetComponent<Camera>();
        camTransform = transform;
        //Transform listener = GetComponentInChildren<AudioListener>().transform;
        //listener.position = transform.position + transform.forward * distance;
    }

    void LateUpdate()
    {
        if (!Target)
            return;

        Quaternion currentRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        Vector3 pos = Target.position;
        pos -= currentRotation * Vector3.forward * Mathf.Abs(distance);
        pos.y = Target.position.y + Mathf.Abs(height);
        transform.position = pos;

        transform.LookAt(Target);

        transform.position = Target.position - (transform.forward * Mathf.Abs(distance));
    }

    public void HideMask(bool shouldHide)
    {
        if (shouldHide) cam.cullingMask &= ~respawnMask;
        else cam.cullingMask |= respawnMask;
    }
}