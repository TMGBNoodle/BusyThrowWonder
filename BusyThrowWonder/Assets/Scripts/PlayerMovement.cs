using System;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}

    public GameObject PlayerCamera;

    public GameObject rockPrefab;

    public GameObject launchPoint;

    public float moveSpeed = 10;

    private Rigidbody body;
    
    public float xAxis = 0;
    public float yAxis = 0;

    public float camYRot = 0;

    public float camXRot = 0;
    public float chargeRate = 10;
    public float shootCharge = 0;

    private float maxCharge = 25;

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
        launchPoint = transform.GetChild(1).gameObject;
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
        PlayerCamera.transform.localRotation = Quaternion.Euler(-camYRot, 0, 0);
        transform.localRotation = Quaternion.Euler(0, camXRot, 0);
        int throttle = 0;
        int strafe = 0;
        if (Input.GetKey(KeyCode.Space)) {
            shootCharge = Math.Min(shootCharge + chargeRate * Time.deltaTime, maxCharge);
        }
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

        if (Input.GetKeyUp(KeyCode.Space) && shootCharge > 0) {
            shoot();
        }
        Vector3 moveInfo = (transform.forward * throttle) + (transform.right * strafe);
        body.linearVelocity = new Vector3(moveInfo.x, body.linearVelocity.y, moveInfo.z);
    }

    void shoot() {
        GameObject newRock = Instantiate(rockPrefab);
        newRock.transform.position = launchPoint.transform.position;
        newRock.GetComponent<Rigidbody>().linearVelocity = transform.forward * (shootCharge+5);
        shootCharge = 0;
    }
}
