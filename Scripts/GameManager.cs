using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameOver = true;
    public GameObject player;
    //if game over is true
    //is pace key pressed 
    //spawn the player
    //gameOver is false
    //hide title screen
    private Ui_Manager _ui_Manager;
    void Start()
    {
        _ui_Manager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
    }
    void Update()
    {
        if (gameOver==true) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _ui_Manager.HideTitleScreen();
            }
        }
    }
}
