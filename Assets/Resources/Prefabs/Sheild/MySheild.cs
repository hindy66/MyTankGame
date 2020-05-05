using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySheild : MonoBehaviour {
   public Material sheildMat;

    //由上往下扫光的时间间隔
    float scaleTime=1;
    float curTime = 0;
    bool isScaleLight=false;
    float offsetY = -1;
    
	void Start () {
        sheildMat = GetComponent<MeshRenderer>().sharedMaterial;
        if( sheildMat==null)
        {
            Debug.Log("Kong");
        }

	}
	
	// Update is called once per frame
	void Update () {
		if(isScaleLight)
        {
            offsetY=Mathf.Lerp(offsetY,1,0.02f);
            sheildMat.SetFloat("_offsety", offsetY);
            if(offsetY+1>1.9f)
            {
                offsetY =1;
                isScaleLight = false;
                curTime = 0;
            }
        }
        else
        {
            curTime += Time.deltaTime;
            if(curTime>=scaleTime)
            {
                offsetY = -1;
                isScaleLight = true;
            }
        }
	}

}
