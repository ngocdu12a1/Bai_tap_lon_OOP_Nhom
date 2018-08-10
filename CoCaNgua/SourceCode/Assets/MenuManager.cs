using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public int NOB = 0;
    // Use this for initialization

    public void QuitGame()
    {
        Application.Quit();
    }

    public void setNOB1()
    {
        this.NOB = 1;
        Debug.Log(NOB);
    }

    public void setNOB2()
    {
        this.NOB = 2;
        Debug.Log(NOB);
    }

    public void setNOB3()
    {
        this.NOB = 3;
        Debug.Log(NOB);
    }

    public void setNOB4()
    {
        this.NOB = 4;
        Debug.Log(NOB);
    }

    public int getNOB()
    {
        return NOB;
    }

    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
