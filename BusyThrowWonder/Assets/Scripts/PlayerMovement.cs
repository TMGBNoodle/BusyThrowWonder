using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}

    public GameObject PlayerCamera;

    public float moveSpeed = 10;

    private Rigidbody body;
    
    public float xAxis = 0;
    public float yAxis = 0;

    public float camYRot = 0;

    public float camXRot = 0;

    public float camMoveSpeedX = 0.5f;
    
    public float camMoveSpeedY = 0.5f;
    
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        PlayerCamera = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");
        camYRot += yAxis * camMoveSpeedX;
        camXRot += xAxis * camMoveSpeedY;
        PlayerCamera.transform.localRotation = Quaternion.Euler(-camYRot, camXRot, 0);
        int throttle = 0;
        int strafe = 0;
        if (Input.GetKey(KeyCode.W)) {
            throttle = 1;
        } else if(Input.GetKey(KeyCode.S)) {
            throttle = -1;
        }

        if (Input.GetKey(KeyCode.A)) {
            strafe = -1;
        } else if(Input.GetKey(KeyCode.D)) {
            strafe = 1;
        }
        body.linearVelocity = transform.forward * throttle;
    }
}
