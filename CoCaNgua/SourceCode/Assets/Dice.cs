using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    static Rigidbody rb;
    private bool da_tung = false;
    public static Vector3 diceVelocity;
    private bool first_turn = true;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame

    public void Throw() {
        float dirX = Random.Range(5000, 10000);
        float dirY = Random.Range(5000, 10000);
        float dirZ = Random.Range(5000, 10000);
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 1500);
        rb.AddTorque(dirX, dirY, dirZ);
    }

    private void Update() {

    }

    /*
    public bool checkDice() {
       // Debug.Log("Check dice duoc ket qua" + da_tung.ToString());
        return this.da_tung;
    }*/
    public void resetDice()
    {
        Debug.Log("Tien hanh reset dice");
        this.da_tung = false;
    }
   
}
