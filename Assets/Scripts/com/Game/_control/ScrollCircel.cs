using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollCircel :ScrollRect {
    //摇杆中心可移动的范围半径
     float radius=0;

    //用于坦克移动的方向向量
     public Vector3 moveDirection;
	// Use this for initialization
	void Start () {
        radius = (transform as RectTransform).sizeDelta.x * 0.5f;
        
	}

    public override void OnDrag(PointerEventData eventData)
    {

        moveDirection.x = content.localPosition.x / radius;
        moveDirection.y = 0;
        moveDirection.z = content.localPosition.y / radius;
        Debug.Log(moveDirection.x + "," + moveDirection.y);
        base.OnDrag(eventData);
        Vector2 pos = content.anchoredPosition;

        //pos.magnitude摇杆中心按钮距离中心所偏移的距离向量
        if(pos.magnitude>radius)
        {
            //pos.normalized摇杆中心按钮偏移方向上的单位向量
            pos=pos.normalized* radius;
            SetContentAnchoredPosition(pos);
        }

    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        moveDirection.x = 0;
        moveDirection.y = 0;
        moveDirection.z = 0;
    }

	// Update is called once per frame
	void Update () {
        ////1.让偏移方向上的距离向量作为坦克的位移参数
        //Debug.Log(content.anchoredPosition.magnitude / radius);

        //2.利用摇杆中心相对于父对象（摇杆底盘）的坐标作为坦克位移的参数
        
        
	}
}
