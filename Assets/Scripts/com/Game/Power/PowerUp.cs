using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class PowerUp : MonoBehaviour{

    private AudioSource pickMusic;


	// Use this for initialization
	void Start () {
        pickMusic = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter(Collider collider) {

        if(collider.gameObject.tag=="Player")
        {
            pickMusic.Play();
            Debug.Log("开始接触");
            dead();

        }         
     }

    public void dead()
    {
        PowerupTrigger.PowerList.Remove(this.gameObject);
    }
}
