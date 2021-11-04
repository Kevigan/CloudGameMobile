using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandomParticle : MonoBehaviour
{
    [SerializeField] private List<GameObject> particlePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        int number = Random.Range(0, particlePrefabs.Count);
        particlePrefabs[number].SetActive(true);
        if (number == 0) Camera.main.backgroundColor = new Color(0, 0, 0);
        else Camera.main.backgroundColor = new Color(0.4481132f, 0.6627358f, 255);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
