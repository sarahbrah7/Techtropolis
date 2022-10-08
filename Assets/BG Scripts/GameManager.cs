using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]

    //includes the set foundations of each player
    public class Entity
    {
        //the name of each player
        public string playerName;
        //each player's sprite
        public Stone[] myStones;

        //checks the player's turn
        public bool hasTurn;
        //the type of player
        public enum PlayerTypes
        {
            //the human player
            HUMAN,
            //the ai player
            CPU,
            //if there is no player
            NO_PLAYER
        }
        //creates the varibale for each player type
        public PlayerTypes playerType;
        //checks to see if the player has won
        public bool hasWon;
        //holds the amoutn of moeny each player has
        public int money;


    }

    public List<Entity> playerList = new List<Entity>();

    //States Machine - holds the type of player state
    //for each player
    public enum States
    {
        //the player is at a halt
        WAITING,
        //the player should roll their dice
        ROLL_DICE,
        //the player needs to switch turns
        SWITCH_PLAYER,
        //player would be bale to playe with their sum of money
        MONEY
    }

    //holds the player's state as a variable
    public States state;
    //holds the current player
    public int activePlayer;
    //checks if players turns are switching
    bool switchingPlayer;

    //new to the code!!!
     
    //Human inputs
    //GameObject for our button
    public GameObject rollButton;
    //the output of the human dice output
    [HideInInspector] public int rolledHumanDice;

    //for the dice section
    public Dice dice;
    //the amount of money the player would have
    public int amount;
    
    //initalizes any variable 
    //before gameplay commences
    void Awake()
    {
        //this makes the script accessible anywhere
        instance = this;
    }

    //initiates as the game commences
    void Start()
    {
        //button is not visible 
        ActivateButton(false);
    }

    //Initiates with every gameplay update
    void Update()
    {
        //if it is the AI player's turn then it would check for their state
        if (playerList[activePlayer].playerType == Entity.PlayerTypes.CPU)
        {
            switch (state)
            {
                //if the player is to roll their dice
                case States.ROLL_DICE:
                    {
                        StartCoroutine(RollDiceDelay());
                        state = States.WAITING;
                    }
                    break;
                //if the player is swithcing their turn
                case States.SWITCH_PLAYER:
                    {
                        StartCoroutine(SwitchPlayer());
                        state = States.WAITING;
                    }
                    break;
                //if the player is waiting for their new state
                case States.WAITING:
                    {
                        //Idle
                    }
                    break;
                //when the player's total sum is affected throughout the gameplay
                case States.MONEY:
                    {
                        StartCoroutine(MoneyCheck());
                        state = States.WAITING;
                    }
                    break;
            }
        }

        //if it is the AI player's turn then it would check for their state
        if(playerList[activePlayer].playerType == Entity.PlayerTypes.HUMAN)
        {
            switch (state)
            {
                //if the player is to roll their dice
                case States.ROLL_DICE:
                    {
                        //Deactivate highlights
                        ActivateButton(true);
                        state = States.WAITING;
                    }
                    break;
                //if the player is swithcing their turn
                case States.SWITCH_PLAYER:
                    {
                        //Deactivate button
                        //Deactivate highlights 
                        StartCoroutine(SwitchPlayer());
                        state = States.WAITING;
                    }
                    break;
                //if the player is waiting for their new state
                case States.WAITING:
                    {
                        //idle
                    }
                    break;
                //when the player's total sum is affected throughout the gameplay
                case States.MONEY:
                    {
                        StartCoroutine(MoneyCheck());
                        state = States.WAITING;
                    }
                    break;
            }
        }
    }

    //where the physical dice is rolled by the AI
    void CPUDice()
    {
        dice.RollDice();
    }

    //this is the function wich handles the output from either player rolling their dice
    public void RollDice(int _diceNumber)//call this from dice
    {
        int diceNumber = _diceNumber;//random.range(1, 7);

        //checks if it is the AI player's turn
        if(playerList[activePlayer].playerType == Entity.PlayerTypes.CPU)
        {
            //this would add the output to the function ...
            //... that controls the sprite's moevement around the board
            MoveAStone(diceNumber);
            Debug.Log("Dice rolled number " + diceNumber);
           
        }

        //checks if it is the human player's turn
        if(playerList[activePlayer].playerType == Entity.PlayerTypes.HUMAN)
        {
            //this would add the output to the function ...
            //... that controls the sprite's moevement around the board
            rolledHumanDice = _diceNumber;
            HumanRollDice(); 
        }
    }

    //creates a delay before trhe dice is to roll 
    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(1);
        //then woudl proceed to roll the physical dice
        dice.RollDice();
    }

    //this function would iterate the player's moevemnt arond the board
    //based on output of the dice    
    void MoveAStone(int diceNumber)
    {
        List<Stone> movableStones = new List<Stone>();

        //checks for the active player
        for (int i = 0; i < playerList[activePlayer].myStones.Length; i++)
        {
            movableStones.Add(playerList[activePlayer].myStones[i]);
        }

        //would then create an array which stores the amount
        //of steps the player is to take around the board
        if (movableStones.Count > 0)
        {
            int num = Random.Range(0, movableStones.Count);
            movableStones[num].StartTheMove(diceNumber);
            state = States.WAITING;
         
            return;
        }

        //once done then the player's shou;d switch turns
        state = States.SWITCH_PLAYER;
    }

    // this function would then switch the player's turn
    IEnumerator SwitchPlayer()
    {
        //woudl check if the players have just recently switched
        if (switchingPlayer)
        {
            yield break;
        }

        switchingPlayer = true;

        yield return new WaitForSeconds(1);
        //woud then switch the role of the active player
        SetNextActivePlayer();
        //then the players would be switched
        switchingPlayer = false;
    }
    //the active player role is switched
    void SetNextActivePlayer()
    {
        //would literally switch their  players
        activePlayer++;
        //literally swiches the players rolls
        activePlayer %= playerList.Count;

        Info.instance.ShowMessage(playerList[activePlayer].playerName + "'s turn!");
        //then the new active player would now have to roll the dice
        state = States.ROLL_DICE;
    }

    //----------------------------------HUMAN INPUT-------------------------------------//
    //would display the utton for the human player to roll their dice
    void ActivateButton(bool on)
    {
        //literally displays the button
        rollButton.SetActive(on);
    }

    //this sits on the roll dice button
    //actiated once the player rolls the dice
    public void HumanRoll()
    {
        //would literally roll the dice
        dice.RollDice();
        //the button is no longer on display
        ActivateButton(false);

    }

    //translates the human dice output into steps 
    public void HumanRollDice()
    {
        //rolledHumanDice = Random.Range(1, 7);

        List<Stone> movableStones = new List<Stone>();
        //checks for the curretn active player
        for (int i = 0; i < playerList[activePlayer].myStones.Length; i++)
        {
            movableStones.Add(playerList[activePlayer].myStones[i]);
        }
        //then the dice output is translated into steps the sprite
        //would take
        if (movableStones.Count > 0)
        {
            int num = Random.Range(0, movableStones.Count);
            movableStones[num].StartTheMove(rolledHumanDice);
            state = States.WAITING;
            return;
        }
        //then the player's state woudl switch to switch turns
        state = States.SWITCH_PLAYER;



    }

    //---------------------------------------MONEY----------------------------------------//
    //commences the player's ability to ...
    //...gain or lose money
    IEnumerator MoneyCheck()
    {
        yield return new WaitForSeconds(1);

        EarnMoney();
    }

    //considers all situations the player could earn money
    public void EarnMoney()
    {
        bool startNodeFull = false;
        //checks for the active player
        for (int i = 0; i < playerList[activePlayer].myStones.Length; i++)
        {
            //checks if the player is on the start node
            if (Stone.exactNode == playerList[activePlayer].myStones[i].startNode)
            {
                startNodeFull = true;
                //if so then the player would have £200 added onto their sum
                if (startNodeFull)
                {
                    amount += 200;
                    Money.instance.AddMoney(amount);
                    break;
                }

                //if not they remain in the waiting state
                else
                {
                    state = States.WAITING;
                }
            }


            //checks if the player is on the jackpot node
            if(Stone.exactNode == playerList[activePlayer].myStones[i].jackpotNode)
            {
                startNodeFull = true;
                //if so then the player would have £1000 added onto their sum
                if (startNodeFull)
                {
                    amount += 1000;
                    Money.instance.AddMoney(amount);
                    break;
                }

                //if not they remain in the waiting state
                else
                {
                    state = States.WAITING;
                }
            }

            //checks if the player is on the jail node
            if(Stone.exactNode == playerList[activePlayer].myStones[i].jailNode)
            {
                Debug.Log("it went here");
                startNodeFull = true;
                //if so then the player would have £300 taken away from their sum
                if (startNodeFull)
                {
                    amount += 300;
                    Money.instance.SubMoney(amount);
                    break;
                }
                //if not then they would remain in the waiting state
                else
                {
                    state = States.WAITING;
                }
            }
            //checks if the player is on the warning node
            if (Stone.exactNode == playerList[activePlayer].myStones[i].warningNode)
            {
                Debug.Log("it went here");
                startNodeFull = true;
                //if so then the player would have £100 taken away from their sum
                if (startNodeFull)
                {
                    amount += 100;
                    Money.instance.SubMoney(amount);
                    break;
                }
                //if not then they would remain in the waiting state
                else
                {
                    state = States.WAITING;
                }
            }
        } 
        //once positions are checked then the player
        // ... would change states to switch turns  
        state = States.SWITCH_PLAYER;
    }

}
