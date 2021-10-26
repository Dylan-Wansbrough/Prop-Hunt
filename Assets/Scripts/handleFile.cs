using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handleFile : MonoBehaviour
{

    public GameObject playerPrefab;

    public GameObject[] player = new GameObject[2];

    public float[] points = new float[2];

    public int round;

    public float timer;

    bool spawnOnce;

    public Text[] textFields;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        rounds();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            round++;
            timer = 0;
            spawnOnce = false;
        }
        textThings();
    }

    void textThings()
    {
        textFields[0].text = "Time left: " + timer;
        textFields[1].text = "Player 1: " + points[0];
        textFields[2].text = "player 2: " + points[1];
    }

    void rounds()
    {
        switch (round)
        {
            case 1:
                if (!spawnOnce)
                {
                    player[0] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
                    player[0].GetComponent<PlayerController>().isProp = true;
                    player[0].GetComponent<PlayerController>().isFound = false;
                    timer = 30;
                    spawnOnce = true;
                    textFields[3].text = "Player 1 hide!";
                }
                break;
            case 2:
                if (!spawnOnce)
                {
                    player[0].transform.GetChild(0).gameObject.SetActive(false);
                    player[0].GetComponent<PlayerController>().lockMovement = true;
                    player[1] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
                    player[1].GetComponent<PlayerController>().isProp = false;
                    timer = 90;
                    spawnOnce = true;
                    textFields[3].text = "Player 2 is hunting!";
                }
                if(player[0].GetComponent<PlayerController>().isFound == true)
                {
                    player[0].GetComponent<PlayerController>().points = 0;
                    round++;
                    spawnOnce = false;
                }
                break;
            case 3:
                if (!spawnOnce)
                {
                    //points
                    if (player[0] != null)
                    {
                        points[0] += player[0].GetComponent<PlayerController>().points;
                    }

                    Destroy(player[0]);
                    Destroy(player[1]);

                    timer = 10;
                    spawnOnce = true;
                }
                break;
            case 4:
                if (!spawnOnce)
                {
                    player[1] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
                    player[1].GetComponent<PlayerController>().isProp = true;
                    player[1].GetComponent<PlayerController>().isFound = false;
                    timer = 30;
                    spawnOnce = true;
                    textFields[3].text = "Player 2 hide!";
                }
                break;
            case 5:
                if (!spawnOnce)
                {
                    player[1].transform.GetChild(0).gameObject.SetActive(false);
                    player[1].GetComponent<PlayerController>().lockMovement = true;
                    player[0] = Instantiate(playerPrefab, new Vector3(-1.149769f, -2.158f, 0.2496133f), Quaternion.identity);
                    player[0].GetComponent<PlayerController>().isProp = false;
                    timer = 90;
                    spawnOnce = true;
                    textFields[3].text = "Player 1 is hunting!";
                }
                if (player[1].GetComponent<PlayerController>().isFound == true)
                {
                    player[1].GetComponent<PlayerController>().points = 0;
                    round++;
                    spawnOnce = false;
                }
                break;
            case 6:
                if (!spawnOnce)
                {
                    //points
                    if (player[1] != null)
                    {
                        points[1] += player[1].GetComponent<PlayerController>().points;
                    }

                    Destroy(player[0]);
                    Destroy(player[1]);

                    timer = 10;
                    spawnOnce = true;
                }
                break;
            default:
                Debug.Log("Invalid round");
                break;
        }
    }
}
