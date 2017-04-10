using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

	// Use this for initialization
	public float lifeTime;
	void Start () {
		Destroy (gameObject, lifeTime);	
	}
}
