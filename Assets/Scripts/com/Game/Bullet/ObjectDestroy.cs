using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 销毁类
/// </summary>
public class ObjectDestroy : MonoBehaviour {

    /// <summary>
    /// 延迟销毁时间
    /// </summary>
    public float delayTime = 1.5f;

	void Start () {
        GameObject.Destroy(this.gameObject, delayTime);
	}

}
