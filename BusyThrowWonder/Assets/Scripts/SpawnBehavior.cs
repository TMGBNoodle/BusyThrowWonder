using UnityEngine;

public class SpawnBehavior : MonoBehaviour
{
    public GameObject zombie;
    public GameObject spawnpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("CreateZombie", 4.0f, Random.Range(4.0f, 8.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateZombie() {
        Instantiate(zombie, spawnpoint.transform.position, Quaternion.identity);
    }
}
