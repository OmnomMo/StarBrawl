using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //temp solution to mirroring problem

        transform.localScale = new Vector3(-0.035f, 0.035f, 0.035f);

	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform, new Vector3(0,1,0));
	}
}
