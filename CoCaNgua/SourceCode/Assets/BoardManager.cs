using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class BoardManager : MonoBehaviour {

    // Use this for initialization
    private Horse horsePicking = null;//con ngua duoc pick
    private int currentPlayer = 1;
    private bool END = false;
    private Player[] player = new Player[5];
    private Dice dice = new Dice();
    private Square[] square = new Square[55];
    private int[] SquareofHorse = new int[17];
    private Vector3[] Chuong = new Vector3[17];
    private Camera[] camera = new Camera[5];
    private Chuong[,] chuongDich = new Chuong[5, 7];
    private int diceNumber = 0;
    private bool can_run = false;
    private bool option1 = false;// chi di binh thuong
    private bool option2 = false;// chi co the xuat chuong
    private bool option3 = false;// co the di chuyen tren chuong dich
    Thread DiceThrowThread;
    Thread MoveHorseThread;
    
    Text text, textpoint;
    private bool[] CheckBot = new bool[5];
    private MenuManager menu = null;
    


    public void SetBot(int cnt) {
        for (int i = 4; i > 0; i--)
        {
            if (i > 4 - cnt) CheckBot[i] = true;
            else CheckBot[i] = false;
        }
    }

    private void Start() {
        {
            //    SceneManager.LoadScene("a");
            UpdateMenu();
            UpdateCamera();
            UpdateSquare();     //       
            UpdatePlayer();
            UpdateDice();
            UpdateChuongDich();
            UpdateChuong();
            UpdateText();

        }
    }

    private void UpdateMenu() {
        menu = GameObject.Find("/MenuGame").GetComponent<MenuManager>();
    }

    private void UpdateText() {
        text = GameObject.Find("/GameObject/Canvas/Text").GetComponent<Text>();
        textpoint = GameObject.Find("/GameObject/Canvas/TextPoint").GetComponent<Text>();
        ChangeTextAndColor(1);
    }

    private void UpdateCamera()
    {
        for (int i = 1; i <= 4; i++) {
            camera[i] = GameObject.Find("/GameObject/camera/camera" + i.ToString()).GetComponent<Camera>();
            camera[i].enabled = false;
        }
        camera[1].enabled = true;
        Debug.Log("UpdateCamera Success");
    }
    private void UpdateChuongDich()
    {
        for (int i = 1; i <= 4; i++) {
            for (int j = 1; j <= 6; j++) {
                chuongDich[i, j] = GameObject.Find("/GameObject/Chuong/chuong" + i.ToString() + "/chuong" + j.ToString()).GetComponent<Chuong>();
            }
        }
        Debug.Log("UpdateChuongDich Success");
    }
    private void UpdateDice() {
        dice = GameObject.Find("GameObject/DiceFile/dice").GetComponent<Dice>();
        Debug.Log("UpdateDice Success");
    }

    private void BotUpdate() {
        if (menu != null && menu.getNOB() != 0) SetBot(menu.getNOB());
    }

    private void Update()
    {
        Debug.Log("current player " + currentPlayer.ToString());
        BotUpdate();

        MouseUpdate();

        ChangeCamera(currentPlayer);

        if (DiceThrowThread == null) ChangeTextAndColor(currentPlayer);

        if (DiceThrowThread == null && (Input.GetKeyDown(KeyCode.Space) || CheckBot[currentPlayer]))
        {
            text.text = "";
            textpoint.text = "";
            dice.Throw();//nem xuc sac 
            DiceThrowThread = new Thread(Delay);
            DiceThrowThread.Start();// doi 8 giay cho suc sac roi xuong  
            
        }

        if (DiceThrowThread != null && !DiceThrowThread.IsAlive)
        {
            
            diceNumber = DiceNumberTextScript.diceNumber;
            //text.text = "Dice Number is " + diceNumber.ToString();
            textpoint.text = "Dice Number is : " + diceNumber.ToString();
            MoveHorseThread = new Thread(Playing);
            MoveHorseThread.Start();
            
            }

        

    }

    private void ChangeCamera(int playerNumber) {
        for (int i = 1; i <= 4; i++) {
            camera[i].enabled = false;
        }
        camera[playerNumber].enabled = true;
        textpoint.text = "";

    }

    private void ChangeTextAndColor(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                text.text = "Red Player Turn. \n Press space throw dice!";
                text.color = new Color(255, 0, 0);
                break;
            case 2:
                text.text = "Yellow Player Turn. \n Press space throw dice!";
                text.color = new Color(255, 255, 0);
                break;
            case 3:
                text.text = "Blue Player Turn. \n Press space throw dice!";
                text.color = new Color(0, 0, 255);
                break;
            case 4:
                text.text = "Green Player Turn. \n Press space throw dice!";
                text.color = new Color(0,255,0);
                break;
        }
        
    }
    private void Playing() {

        Debug.Log("currentPlayer " + currentPlayer);
        option1 = Co_the_xuat_chuong(currentPlayer, diceNumber);// chi co the xuat chuong
        option2 = CheckAllHorseofPlayer(currentPlayer, diceNumber);// chi di binh thuong
        option3 = CheckDiChuyenTrongChuongDich(currentPlayer, diceNumber);// di kieu gi cung duoc        

        Debug.Log("kiem tra co the di hay k " + diceNumber.ToString());
        can_run = (option1 || option2 || option3);
        Debug.Log(option1.ToString() + option2.ToString() + option3.ToString());
        if (can_run == false)
        {
            // ChangeCamera((++currentPlayer - 1) % 4 + 1);

            if (diceNumber != 6)
            {
                currentPlayer = currentPlayer % 4 + 1;
            }
            
            DiceThrowThread = null;

        }
        if (CheckBot[currentPlayer])
        {
            for (int i = 1; i <= 4; i++)
            {
                if (player[currentPlayer].getHorse(i).GetCanRun() > 0)
                {
                    horsePicking = player[currentPlayer].getHorse(i);
                    break;
                }
            }
        }

        // Debug.Log("player can run " + currentPlayer.ToString());

        if (horsePicking != null && horsePicking.getplayerNumber() == currentPlayer && horsePicking.GetCanRun() > 0)
        {
            Debug.Log(currentPlayer.ToString() + "Co the di chuyen");
            Run_horse(currentPlayer);
            ResetAfterOneTurn(currentPlayer);
            if (diceNumber != 6) {
                currentPlayer = currentPlayer % 4 + 1;
            }
            else
            {
                horsePicking = null;
            }
            DiceThrowThread = null;

        }
    
    }

  
    private void Delay() {
        Thread.Sleep(TimeSpan.FromMilliseconds(6000));
    }

   

    private void UpdatePlayer() {
        for (int i = 1; i <= 4; i++) {
            player[i] = new Player();
            player[i].FourHorseUpdate(i);
        }
        Debug.Log("UpdatePlayer Success");
    }


    private void UpdateSquareOfHorse(int playerNumber, int horseNumber, int value) {
        player[playerNumber].getHorse(horseNumber).SetSquareUnder(value);
        square[value].SetPlayerOnThisSquare(playerNumber);
        square[value].SetHasHorse();
    }

    private void UpdateChuong()
    {
        for (int i = 1; i <= 4; i++)
            for (int j = 1; j <= 4; j++) {
                Chuong[(i - 1) * 4 + j] = player[i].getHorse(j).transform.position;
            }
        Debug.Log("UpdateChuong Success");

    }

    private void MoveToChuong(Horse horse, int playerNumber, int horseNumber)
    {
        horse.MoveTo(Chuong[(playerNumber - 1) * 4 + horseNumber]);
    }
    private void UpdateSquare()
    {
        for (int i = 1; i <= 48; i++) {
            if (square[i] == null) Debug.Log("null con mem may");
            square[i] = GameObject.Find("GameObject/Board/square" + i.ToString()).GetComponent<Square>();
            square[i].UpdateSquare(i);
        }
        Debug.Log("UpdateSquare Success");
    }


    
    // Update is called once per frame
    private void MouseUpdate()
    {
        Ray rayOrgin = camera[currentPlayer].ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(rayOrgin, out hit, 250.0f))
            {
                string ObjName = hit.collider.tag;

                if (ObjName == "horse")
                {
                    horsePicking = hit.collider.gameObject.GetComponent(typeof(Horse)) as Horse;
                    Debug.Log("pick" + ObjName);
                    Debug.Log(hit.collider.transform.position);
                    return;
                }

            }
        }
    }

 

    private void ResetSquare() 
    {
        for (int i = 1; i <= 48; i++) square[i].SetHorseOnThisCanRun(true);
    }

    private void ResetAfterOneTurn(int playerNumber) {
        for (int i = 1; i <= 4; i++) {
            player[playerNumber].getHorse(i).SetCanRun(0);
            horsePicking = null;
        }
    }    

    private bool Co_the_xuat_chuong(int playerNumber, int num) {
       // Debug.Log("" + num.ToString() + "xxx" + playerNumber.ToString());
        if (num == 6) {
          //  Debug.Log("duoc 6 " + num.ToString() + "xxx" + playerNumber.ToString());
            if (playerNumber == 1 && square[1].HasHorse()) return false;
            if (playerNumber == 2 && square[13].HasHorse()) return false;
            if (playerNumber == 3 && square[25].HasHorse()) return false;
            if (playerNumber == 4 && square[37].HasHorse()) return false;
            for (int i = 1; i <= 4; i++) {
                if (player[playerNumber].getHorse(i).Check_trong_chuong())
                    player[playerNumber].getHorse(i).SetCanRun(1);
            }
            return true;
        }
        return false;
    }

    private Horse HorseOfSquare(int squareNumber) { // tim vi tri cua con ngua trong group tu so hieu cua o
        for (int i = 1; i <= 4; i++) {
            for (int j = 1; j <= 4; j++) {
                if (player[i].getHorse(j).GetSquareUnder() == squareNumber) return player[i].getHorse(j);
            }
        }
        return null;
    }

    private void Run_horse(int playerNumber) {
        int currentsquare = horsePicking.GetSquareUnder(); //o hien tai dang dung
        int tmp = (currentsquare + DiceNumberTextScript.diceNumber - 1) % 48 + 1; // o tiep theo se di den
        Debug.Log("currentsquare " + currentsquare);
        Debug.Log("tmp " + tmp);
        if (horsePicking.GetCanRun() == 1)
        {
            //xuat chuong
            if (playerNumber == 1)
            {
                tmp = 1;
                horsePicking.MoveTo(square[1].getPosition());
                square[1].SetHasHorse();
            }
            if (playerNumber == 2)
            {
                tmp = 13;
                horsePicking.MoveTo(square[13].getPosition());
                square[13].SetHasHorse();
            }
            if (playerNumber == 3)
            {
                tmp = 25;
                square[25].SetHasHorse();
                horsePicking.MoveTo(square[25].getPosition());
            }
            if (playerNumber == 4)
            {
                tmp = 37;
                square[37].SetHasHorse();
                horsePicking.MoveTo(square[37].getPosition());
            }
            horsePicking.Xuat_chuong();

        }
        if(horsePicking.GetCanRun() == 2)
        {            
            // di binh thuong 
            Debug.Log("di binh thuong");
            if (square[tmp].HasHorse())
            { // neu co quan cua thang khac thi da 
                Debug.Log("da chet cmm di");
                Horse dieHorse = HorseOfSquare(tmp);
                Vector3 tmpVec = Chuong[(dieHorse.getplayerNumber() - 1) * 4 + dieHorse.gethorseNumber()];
                dieHorse.MoveTo(new Vector3(tmpVec.x, tmpVec.y - 3.5f, tmpVec.z));
                dieHorse.Nhap_chuong();
            }// da
            
            square[tmp].SetHasHorse();
            horsePicking.MoveTo(square[tmp].getPosition()); // mac dinh la di binh thuong
            
            
            square[currentsquare].resetAll();

        }

        if (horsePicking.GetCanRun() == 3) {
            //di trong chuong dich
            Chuong chuong = null;
            if (horsePicking.GetSquareUnder() > 0)
            {
                chuong = chuongDich[playerNumber, DiceNumberTextScript.diceNumber];
                horsePicking.SetChuongUnder(DiceNumberTextScript.diceNumber);
                chuongDich[currentPlayer, DiceNumberTextScript.diceNumber].SetHasHorse();
            }
            if (horsePicking.GetSquareUnder() == 0)
            {
                chuong = chuongDich[playerNumber, horsePicking.GetChuongUnder() + 1];
                horsePicking.SetChuongUnder(horsePicking.GetChuongUnder() + 1);
                chuongDich[currentPlayer, horsePicking.GetChuongUnder() + 1].SetHasHorse();
            }

            if(chuong != null ) horsePicking.MoveTo(new Vector3(chuong.getPosition().x, chuong.getPosition().y - 1.8f, chuong.getPosition().z));
            
            
        }

        if (horsePicking.GetCanRun() != 3) UpdateSquareOfHorse(playerNumber, horsePicking.gethorseNumber(), tmp);
        return;

    }

    private bool CheckAllHorseofPlayer(int playerNumber, int diceNumber) {
        int cnt = 0;
        int squareMaxPlayer = (playerNumber-1)*12+48;       // o toi da co the den cua playerNumber
        

        for (int i = 1; i <= 4; i++) { 
            Horse horse = player[playerNumber].getHorse(i);
            if (horse.Check_trong_chuong()) continue;
            if (horse.GetChuongUnder() != 0) continue;
            horse.SetCanRun(2);
            int squareUnder = horse.GetSquareUnder();
            if (playerNumber == 2 && 1 <= squareUnder && squareUnder <= 12) squareUnder += 48;
            if (playerNumber == 3 && 1 <= squareUnder && squareUnder <= 24) squareUnder += 48;
            if (playerNumber == 4 && 1 <= squareUnder && squareUnder <= 36) squareUnder += 48;

            Debug.Log(squareUnder.ToString());
            if (squareUnder + diceNumber > squareMaxPlayer && squareUnder < squareMaxPlayer)
            {
                horse.SetCanRun(0);
            }
            else
            {
                for (int j = squareUnder + 1; j <= diceNumber + squareUnder - 1; j++)
                {
                    //int k = (j - 1) % 48 + 1;// fix vuot qua gioi han 48 o
                    if (square[(j - 1) % 48 + 1].GetPlayerOnThisSquare() != 0) // neu tren o nay co con ngua dang dung
                    {
                        Debug.Log(i.ToString() + "bi chan");
                        horse.SetCanRun(0);
                        break;
                    }
                }
            }

            if (square[(squareUnder + diceNumber-1)%48+1].GetPlayerOnThisSquare() == playerNumber)
            {
                horse.SetCanRun(0);
            } 

            if (squareUnder == squareMaxPlayer)     // kiem tra di chuyen o o toi da cua player
            {
                horse.SetCanRun(3);
                for (int j = 1; j <= diceNumber; j++)
                    if (chuongDich[playerNumber, j].HasHorse()) horse.SetCanRun(0);
             
            }

            
        }

        Debug.Log(cnt);
        for (int i = 1; i <= 4; i++) {
            if (player[playerNumber].getHorse(i).GetCanRun() > 0) return true;
        }
        return false; 
    }

    private bool CheckDiChuyenTrongChuongDich(int playerNumber, int diceNumber) {
        for (int i = 1; i <= 5; i++)
            if (chuongDich[playerNumber, i].HasHorse() && !chuongDich[playerNumber, i + 1].HasHorse()) return true;
        return false;

    }

    private bool CheckEnd(int playerNumber)     // kiem tra player win
    {
        for (int i = 3; i <= 6; i++)
            if (!chuongDich[playerNumber, i].HasHorse()) return false;
        return true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
