﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScript : MonoBehaviour {

	Vector3 diceVelocity;
    private bool first_turn = true;
	// Update is called once per frame
	void FixedUpdate () {
		diceVelocity = Dice.diceVelocity;
	}

    private void Delay(long time)
    {
        for (int i = 1; i <= time; i++) { }
    }


    void OnTriggerEnter(Collider col)
	{
        
        if (DiceNumberTextScript.next_turn && !first_turn)
        {
            
            DiceNumberTextScript.dice_of_player++;
            if (DiceNumberTextScript.dice_of_player == 5) DiceNumberTextScript.dice_of_player = 1;
        }
        if (first_turn) {
            first_turn = false;  
        }

        //Delay(350000000);
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
		{
            
            switch (col.gameObject.name) {               
			case "Side1":
				DiceNumberTextScript.diceNumber = 6;
				break;
			case "Side2":
				DiceNumberTextScript.diceNumber = 5;
				break;
			case "Side3":
				DiceNumberTextScript.diceNumber = 4;
				break;
			case "Side4":
				DiceNumberTextScript.diceNumber = 3;
				break;
			case "Side5":
				DiceNumberTextScript.diceNumber = 2;
				break;
			case "Side6":
				DiceNumberTextScript.diceNumber = 1;
				break;
			}
        }
	}

}
