using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Horse[] horse = new Horse[5];
    private int playerNumber;
   
    // Use this for initialization

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public Horse getHorse(int cnt) {
        return horse[cnt];
    }

    public void FourHorseUpdate(int playerNumber)
    {
        string name = null;
        if (playerNumber == 1) name = "RedHorse";
        if (playerNumber == 2) name = "YellowHorse";
        if (playerNumber == 3) name = "BlueHorse";
        if (playerNumber == 4) name = "GreenHorse";
        for (int i = 1; i <= 4; i++)
        {
            horse[i] = GameObject.Find("/GameObject/" + name + "/horse" + i.ToString()).GetComponent<Horse>();
            Debug.Log("Cap nhap doi tuong ngua thanh cong");
            horse[i].UpdateHorse(i, playerNumber);
        }




    }
    

}
