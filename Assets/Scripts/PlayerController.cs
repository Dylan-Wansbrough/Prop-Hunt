using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerNum;

    public bool isProp;
    public bool isFound;
    public bool lockMovement;

    public int points;

    public LayerMask mask;

    Mesh mesh;
    MeshCollider meshy;


    //player movement
    public float Speed = 12f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public CharacterController controller;
    Vector3 velocity;
    public float gravity = -20f;
    public float GroundDistance = 0.4f;
    bool isGrounded;
    public float JumpHight = 2f;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        meshy = GetComponent<MeshCollider>();
        Cursor.visible = false;
        Screen.lockCursor =  true;
    }

    // Update is called once per frame
    void Update()
    {
        //Player movement
        if (!lockMovement)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(JumpHight * -2 * gravity);
            }

            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

            isGrounded = controller.isGrounded;

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * Speed * Time.deltaTime + velocity * Time.deltaTime);
        }

        //Becoming a Prop
        if (isProp && !lockMovement)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Debug.DrawRay(gameObject.transform.position, transform.forward * 10, Color.green);
                if (Physics.Raycast(gameObject.transform.position, transform.forward * 10, out hit, 10, mask))
                {
                    Debug.Log(hit.transform.name);
                    points = hit.transform.gameObject.GetComponent<propFile>().points;
                    mesh = hit.transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
                    gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
                    gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
                }
                else
                {
                    Debug.Log("miss");
                }
            }
        }else if (!isProp && !lockMovement)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Debug.DrawRay(gameObject.transform.position, transform.forward * 10, Color.green);
                if (Physics.Raycast(gameObject.transform.position, transform.forward * 10, out hit, 10, mask))
                {
                    Debug.Log(hit.transform.name);
                    if(hit.transform.gameObject.tag == "Player")
                    {
                        hit.transform.gameObject.GetComponent<PlayerController>().isFound = true;
                    }
                    
                }
                else
                {
                    Debug.Log("miss");
                }
            }
        }
    }
}
