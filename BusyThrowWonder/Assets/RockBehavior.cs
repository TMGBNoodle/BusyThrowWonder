using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    public GameObject particlePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        GameObject newParticleSys = Instantiate(particlePrefab);
        newParticleSys.transform.position = transform.position;
        
    }
}
