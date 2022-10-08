using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    //creates the money variable
    public static Money instance;

    public int money;
    //Stores in the start node variable ...
    //...from the node script
    public Node startNode;

    //initalizes any varible
    //before gameplay commences
    private void Awake()
    {
        instance = this;
    }
    //commences when the gameplay starts
    void Start()
    {
        //each player is set a starting amount
        money = 500;
        //these are displayed to both of the player's money box
        RedPlayerInfo.instance.ShowMessage("£" + money);

        BluePlayerInfo.instance.ShowMessage("£" + money);

    }
    //function adds on money to the player's sum and info box
    public void AddMoney(int _amount)
    {
        //here the player's sum is updated
        money += _amount;
        Debug.Log(money);
        //BluePlayerInfo.instance.ShowMessage("£" + money);
        //PlayerInfo.instance.ShowMessage("£" + money);
        //if it is the AI player's turn then their info box is updated
        if(GameManager.instance.playerList[GameManager.instance.activePlayer].playerType == GameManager.Entity.PlayerTypes.CPU)
        {
            RedPlayerInfo.instance.ShowMessage("£" + money);
        }
        //if it is the human player's turn then their info box is updated
        if (GameManager.instance.playerList[GameManager.instance.activePlayer].playerType == GameManager.Entity.PlayerTypes.HUMAN)
        {
            BluePlayerInfo.instance.ShowMessage("£" + money);
        }

        //then the player will switch turns
        GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
    }
    //functions subtracts money to the player's sum and info box
    public void SubMoney(int _amount)
    {
        //here the player's sum is updated
        money -= _amount;
        Debug.Log(money);
        //if it is the AI player's turn then their info box is updated
        if (GameManager.instance.playerList[GameManager.instance.activePlayer].playerType == GameManager.Entity.PlayerTypes.CPU)
        {
            RedPlayerInfo.instance.ShowMessage("£" + money);
        }
        //if it is the human player's turn then their info box is updated
        if (GameManager.instance.playerList[GameManager.instance.activePlayer].playerType == GameManager.Entity.PlayerTypes.HUMAN)
        {
            BluePlayerInfo.instance.ShowMessage("£" + money);
        }

        //then the player will switch turns
        GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
    }
}
