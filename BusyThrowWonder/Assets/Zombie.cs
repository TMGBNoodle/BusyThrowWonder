using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerMovement.Instance.transform);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Rock") {
            Destroy(collision.gameObject); 
            animator.SetBool("Alive", false);
            Invoke(Destroy(gameObject), animator.get)
        }
    }
}
