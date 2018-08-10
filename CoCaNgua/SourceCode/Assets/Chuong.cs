using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chuong : MonoBehaviour {

    private bool hasHorse = false;
    private Vector3 chuongPosition;

    public void SetHasHorse()
    {
        hasHorse = true;
    }

    public void SetNotHorse()
    {
        hasHorse = false;
    }

    public bool HasHorse()
    {
        return hasHorse;
    }

    public Vector3 getPosition()
    {
        return chuongPosition;
    }

    // Use this for initialization
    void Start () {
        chuongPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
