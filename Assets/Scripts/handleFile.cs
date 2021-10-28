using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handleFile : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    public GameObject[] player = new GameObject[3];

    public float[] points = new float[2];

    public int turn;
    public int round;

    public float timer;
    public float hintTimer;

    bool spawnOnce;

    public Text[] textFields;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (round > 3)
        {
            gameEnd();
        }
        else
        {
            rounds();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                turn++;
                timer = 0;
                spawnOnce = false;
                if (player[2] != null)
                {
                    Destroy(player[2]);
                }
            }
            textThings();
        }
        
       
    }

    void textThings()
    {
        textFields[0].text = "Time left: " + Mathf.Round(timer);
        textFields[1].text = "Player 1: " + points[0];
        textFields[2].text = "player 2: " + points[1];
        textFields[4].text = "Round: " + round;
    }

    void rounds()
    {
        switch (turn)
        {
            case 1:
                if (!spawnOnce)
                {
                    player1Prop();
                }
                if (Input.GetKeyDown("e"))
                {
                    timer = 0.1f;
                }
                break;
            case 2:
                if (!spawnOnce)
                {
                    player2Hunt();
                }
                hotAndColdSystem(0, 1);
                textFields[6].text = "Guesses: " + player[1].GetComponent<PlayerController>().guesses;
                if (player[0].GetComponent<PlayerController>().isFound == true)
                {
                    player[0].GetComponent<PlayerController>().points = 0;
                    turn++;
                    spawnOnce = false;
                    textFields[5].text = "Player 2 Caught player 1!";
                }else if (player[1].GetComponent<PlayerController>().noGuesses == true)
                {
                    turn++;
                    spawnOnce = false;
                    textFields[5].text = "Player 2 ran out of guesses!";
                }
                break;
            case 3:
                if (!spawnOnce)
                {
                    player1betweenRounds();
                }
                break;
            case 4:
                if (!spawnOnce)
                {
                    player2Prop();
                }
                if (Input.GetKeyDown("e"))
                {
                    timer = 0.1f;
                }
                break;
            case 5:
                if (!spawnOnce)
                {
                    player1Hunt();
                }
                hotAndColdSystem(1, 0);
                textFields[6].text = "Guesses: " + player[0].GetComponent<PlayerController>().guesses;
                if (player[1].GetComponent<PlayerController>().isFound == true)
                {
                    player[1].GetComponent<PlayerController>().points = 0;
                    turn++;
                    spawnOnce = false;
                }
                break;
            case 6:
                if (!spawnOnce)
                {
                    player2betweenRounds();
                }
                break;
            default:
                Debug.Log("Invalid round");
                break;
        }
    }

    void player1Prop()
    {
        textFields[5].text = " ";
        textFields[6].text = "";
        player[0] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
        player[0].GetComponent<PlayerController>().isProp = true;
        player[0].GetComponent<PlayerController>().isFound = false;
        timer = 30;
        spawnOnce = true;
        textFields[3].text = "Player 1 hide!";
        textFields[7].text = "";
        textFields[8].text = "";
    }

    void player1Hunt()
    {
        player[1].transform.GetChild(0).gameObject.SetActive(false);
        player[1].GetComponent<PlayerController>().lockMovement = true;
        player[0] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
        player[0].GetComponent<PlayerController>().isProp = false;
        timer = 90;
        spawnOnce = true;
        textFields[3].text = "Player 1 is hunting!";
        hintTimer = 10;
    }

    void player2Prop()
    {
        textFields[5].text = " ";
        textFields[6].text = "";
        player[1] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
        player[1].GetComponent<PlayerController>().isProp = true;
        player[1].GetComponent<PlayerController>().isFound = false;
        timer = 30;
        spawnOnce = true;
        textFields[3].text = "Player 2 hide!";
        textFields[7].text = "";
        textFields[8].text = "";
    }

    void player2Hunt()
    {
        player[0].transform.GetChild(0).gameObject.SetActive(false);
        player[0].GetComponent<PlayerController>().lockMovement = true;
        player[1] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
        player[1].GetComponent<PlayerController>().isProp = false;
        timer = 90;
        spawnOnce = true;
        textFields[3].text = "Player 2 is hunting!";
        textFields[6].text = "Guesses" + player[1].GetComponent<PlayerController>().guesses;
        hintTimer = 10;
    }


    void player1betweenRounds()
    {
        player[2] = Instantiate(cameraPrefab, new Vector3(0.05620193f, 17.62f, -0.9289341f), Quaternion.identity);
        //points
        if (player[0] != null)
        {
            points[0] += player[0].GetComponent<PlayerController>().points;
        }

        Destroy(player[0]);
        Destroy(player[1]);

        timer = 10;
        spawnOnce = true;
        textFields[3].text = "Get ready for the next round";
        textFields[7].text = "";
        textFields[8].text = "";
    }

    void player2betweenRounds()
    {
        player[2] = Instantiate(cameraPrefab, new Vector3(0.05620193f, 17.62f, -0.9289341f), Quaternion.identity);
        //points
        if (player[1] != null)
        {
            points[1] += player[1].GetComponent<PlayerController>().points;
        }

        Destroy(player[0]);
        Destroy(player[1]);

        timer = 10;
        spawnOnce = true;
        textFields[3].text = "Get ready for the next round";
        textFields[7].text = "";
        textFields[8].text = "";
        turn = 0;
        round++;
    }

    void gameEnd()
    {
        if(round != 5)
        {
            player[2] = Instantiate(cameraPrefab, new Vector3(0.05620193f, 17.62f, -0.9289341f), Quaternion.identity);
            textFields[3].text = "And thats the game!";
            textFields[4].text = "Round: " + 3;
            textFields[0].text = "Time left: " + 0;
        } 
        round = 5;
    }

    void hotAndColdSystem(int prop, int hunter)
    {
        hintTimer -= Time.deltaTime;
        Vector3 propPos = player[prop].transform.position;
        Vector3 hunterPos = player[hunter].transform.position;
        
        float dist = Vector3.Distance(propPos, hunterPos);
        textFields[7].text = "next hint in: " + Mathf.Round(hintTimer) + " sec";
        if (hintTimer < 0)
        {
            if (dist < 15)
            {
                textFields[8].text = "Very Hot";
            }
            else if (dist < 25)
            {
                textFields[8].text = "Hot";
            }
            else if (dist < 40)
            {
                textFields[8].text = "Warm";
            }
            else if (dist < 60)
            {
                textFields[8].text = "Cold";
            }
            else if (dist < 80)
            {
                textFields[8].text = "Very Cold";
            }
            else
            {
                textFields[8].text = "Freezing";
            }
            hintTimer = 10;
        }
        
    }
}
