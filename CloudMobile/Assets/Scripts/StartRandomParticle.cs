using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandomParticle : MonoBehaviour
{
    [SerializeField] private List<GameObject> particlePrefabs;
    [SerializeField] private Color blueBackground;

    void Start()
    {
        int number = Random.Range(0, particlePrefabs.Count);
        particlePrefabs[number].SetActive(true);
        if (number == 0) Camera.main.backgroundColor = new Color(0, 0, 0);
        else Camera.main.backgroundColor = blueBackground;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
