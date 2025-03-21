using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    private bool Alive = true;

    private float debounce = 2.4f;

    private float lastHitTime = 0;
    public float range = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Alive) {
            transform.LookAt(PlayerMovement.Instance.transform);
        }
        float dist = (transform.position - PlayerMovement.Instance.transform.position).magnitude;
        if(dist <= range) {
            if (Time.time - lastHitTime > debounce) {
                lastHitTime = (int)Time.time;
                animator.SetBool("In Range", true);
                PlayerMovement.Instance.health -= 10;
                print("Attack!");
            }
        } else {
            animator.SetBool("In Range", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rock") {
            collision.gameObject.GetComponent<RockBehavior>().Die();
            animator.SetBool("Alive", false);
            Invoke("d", 7);
            Alive = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = false;
        }
    }
    
    void d() {
        Destroy(gameObject);
    }

}
