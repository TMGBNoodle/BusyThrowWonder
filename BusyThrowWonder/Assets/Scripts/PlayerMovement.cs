using System;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {get; private set;}

    public Animator animator;

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
    public int health = 100;

    private bool deathComplete = false;

    public Slider slider;

    [SerializeField] private string nextScene;

    
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
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }


    // Update is called once per frame
    async void Update()
    {
        if (health <= 0) {
            if (!deathComplete) {
                animator.SetBool("Alive", false);
                PlayerCamera.transform.position += new Vector3(0, 5, 0);
                PlayerCamera.transform.LookAt(transform.position);
                deathComplete = true;
                await  Task.Delay(4000);
                SceneManager.LoadScene(nextScene);
            }
        } else {
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");
        camYRot += yAxis * camMoveSpeedX;
        camXRot += xAxis * camMoveSpeedY;
        PlayerCamera.transform.localRotation = Quaternion.Euler(-camYRot, 0, 0);
        transform.localRotation = Quaternion.Euler(0, camXRot, 0);
        //int oldState = animator.GetInteger("State");
        //int newState = 0;
        int throttle = 0;
        int strafe = 0;
        // bool idleFlag1 = false;
        // bool idleFlag2 = false;
        if (Input.GetKey(KeyCode.Space)) {
            shootCharge = Math.Min(shootCharge + chargeRate * Time.deltaTime, maxCharge);
            sliderUpdate();
        }
        if (Input.GetKey(KeyCode.W)) {
            throttle = 1;
            //newState = 1;
        } else if(Input.GetKey(KeyCode.S)) {
            throttle = -1;
            //newState = 2;
        }

        if (Input.GetKey(KeyCode.A)) {
            strafe = -1;
        } else if(Input.GetKey(KeyCode.D)) {
            strafe = 1;
        }

        if (Input.GetKeyUp(KeyCode.Space) && shootCharge > 0) {
            //newState = 5;
            shoot();
        }
        // if (newState != oldState) {
        //     animator.SetInteger("State", newState);
        //     animator.SetBool("SwitchState", true);
        // } else  if (idleFlag1 && idleFlag2) {
        //     animator.SetInteger("State", 0);
        //     animator.SetBool("SwitchState", true);
        // } else {
        //     animator.SetBool("SwitchState", false);
        // }
        Vector3 moveInfo = (transform.forward * throttle * moveSpeed) + (transform.right * strafe * moveSpeed);
        body.linearVelocity = new Vector3(moveInfo.x, body.linearVelocity.y, moveInfo.z);
        }
    }

    public void sliderUpdate(){
        slider.value = shootCharge;
    }



    void shoot() {
        GameObject newRock = Instantiate(rockPrefab);
        newRock.transform.position = launchPoint.transform.position;
        newRock.GetComponent<Rigidbody>().linearVelocity = transform.forward * (shootCharge+5);
        shootCharge = 0;
        slider.value = shootCharge;
    }
    // void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.gameObject.tag == "Zombie") {
    //         playerHealth = playerHealth - 10.0f;
    //     }
    // }
}
