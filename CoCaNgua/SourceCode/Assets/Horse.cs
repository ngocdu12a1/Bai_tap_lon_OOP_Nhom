using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour {

    private int horseNumber;
    private int playerNumber;
    private bool trong_chuong = true;
    private int square_under = 0;
    private int chuong_under = 0;
    private int can_run = 0;
    private Vector3 horsePosition;
    //1 xuat chuong
    //2 di binh thuong 
    //3 di trong chuong dich

    private void Start() {
        horsePosition = transform.position;
    }

    private void Update()
    {
        transform.position = horsePosition;
    }
    public int GetSquareUnder() {
        return square_under;
    }


    public void SetSquareUnder(int num) {
        square_under = num;
    }

    public int GetChuongUnder()
    {
        return chuong_under;
    }

    public void SetChuongUnder(int num)
    {
        chuong_under = num;
    }

    public void MoveTo(Vector3 position) {
        horsePosition = new Vector3(position.x, position.y + 3.5f, position.z);
        Debug.Log("Move horse");

    }


    public void UpdateHorse(int horseNumber, int playerNumber)
    {
        this.horseNumber = horseNumber;
        this.playerNumber = playerNumber;
        
    }

    public int gethorseNumber()
    {
        return horseNumber;
    }

    public bool Check_trong_chuong() {
        return trong_chuong;
    }
    
    public void Xuat_chuong()
    {
        trong_chuong = false;
    }

    public void Nhap_chuong() {
        trong_chuong = true;
    }
    public int getplayerNumber()
    {
        return playerNumber;
    }

    public void SetCanRun(int option) {
        can_run = option;
    }

    public int GetCanRun() {
        return can_run;
    }


}
