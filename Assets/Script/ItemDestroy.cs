using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroy : MonoBehaviour {

    public GameObject unitychan;

	// Use this for initialization
	void Start () {
        this.unitychan = GameObject.Find("unitychan");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnBecameInvisible()
    {
        if (this.unitychan.transform.position.x > this.gameObject.transform.position.x)
        {
            Destroy(this.gameObject);
        }
    }
}
