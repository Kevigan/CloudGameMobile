using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool followNegative = false;

    // Start is called before the first frame update
    void Start()
    {
        DamageColider.onPlayerFalling += ChangeBool;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (target.position.y > transform.position.y && GameManager.Main.GameState == GameState.Playing)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
        else if(followNegative)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }

    public void ChangeBool()
    {
        Debug.Log("invoke");
        followNegative = !followNegative;
    }
}
