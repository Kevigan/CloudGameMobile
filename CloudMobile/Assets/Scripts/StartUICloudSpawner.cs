using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUICloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Transform[] positions;

    float time = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(time >= 0)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                int a = Random.Range(0, 5);
                Instantiate(cloudPrefab, positions[a].position, Quaternion.identity);
                time = 2;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Cloud>() is Cloud cloud)
        {
            Destroy(cloud);
        }
    }
}
