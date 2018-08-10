using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {
    private int squareNumber;               
    private bool hasHorse = false;          //co ngua
    private int playerofHorse; // ngua cua player nao
    private bool HorseOnThisCanRun = true;  // ngua co the chay?
    private Vector3 squarePosition;
    //public Square() { }
    private void Start()            
    {
        squarePosition = transform.position;
    }

    public void resetAll() {
        hasHorse = false;          //co ngua
        playerofHorse = 0;              // ngua cua player nao
        HorseOnThisCanRun = true;
}

    public void UpdateSquare(int number) {
        squareNumber = number;
        Debug.Log("vao con me may di");
    }

    public int GetsquareNumber() {
        return squareNumber;
    }

    public void SetHasHorse() {
        hasHorse = true;
    }

    public void SetNotHorse() {
        hasHorse = false;
    }

    public bool HasHorse() {
        return hasHorse;
    }

    public void SetPlayerOnThisSquare(int playerNumber) {
        playerofHorse = playerNumber;
    }

    public int GetPlayerOnThisSquare() {
        return playerofHorse;
    }

    public void SetHorseOnThisCanRun(bool canRun)
    {
        HorseOnThisCanRun = canRun;
    }

    public bool GetHorseOnThisCanRun()
    {
        return HorseOnThisCanRun;
    }

    public Vector3 getPosition() {
        return squarePosition;
    }
}
