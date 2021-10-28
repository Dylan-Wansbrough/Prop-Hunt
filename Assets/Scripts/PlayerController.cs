using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject camera;

    public bool isProp;
    public bool isFound;
    public bool noGuesses;
    public bool lockMovement;

    public int points;
    public int guesses;

    public LayerMask mask;
    public LayerMask mask2;

    Mesh mesh;
    Material m_Material;


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
            if(pitch > 40f)
            {
                pitch = 40f;
            }else if(pitch < -30f)
            {
                pitch = -30f;
            }
            camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

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
                Debug.DrawRay(gameObject.transform.position, camera.transform.forward * 20, Color.green);
                if (Physics.SphereCast(gameObject.transform.position, 2, camera.transform.forward * 20, out hit, 10, mask))
                {
                    Debug.Log(hit.transform.name);
                    points = hit.transform.gameObject.GetComponent<propFile>().points;
                    controller.center = new Vector3(0,hit.transform.gameObject.GetComponent<propFile>().groundBoost, 0);
                    controller.radius = hit.transform.gameObject.GetComponent<propFile>().groundRadius;
                    controller.height = hit.transform.gameObject.GetComponent<propFile>().groundHeight;
                    mesh = hit.transform.gameObject.GetComponent<MeshFilter>().sharedMesh;
                    m_Material = hit.transform.gameObject.GetComponent<Renderer>().sharedMaterial;
                    gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
                    gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
                    gameObject.GetComponent<Renderer>().sharedMaterial = m_Material;
                }
                else
                {
                    Debug.Log("miss");
                }
            }
        }else if (!isProp && !lockMovement)
        {

            if(guesses <= 0)
            {
                noGuesses = true;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                Debug.DrawRay(gameObject.transform.position, camera.transform.forward * 20, Color.green);
                if (Physics.SphereCast(gameObject.transform.position, 2, camera.transform.forward * 20, out hit, 10, mask))
                {
                    Debug.Log(hit.transform.name);
                    if(hit.transform.gameObject.tag == "Player")
                    {
                        hit.transform.gameObject.GetComponent<PlayerController>().isFound = true;
                    }
                    else
                    {
                        guesses--;
                    }
                    
                }
                else
                {
                    guesses--;
                    Debug.Log("miss");
                }
            }
        }
    }
}
