using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Cloud>() is Cloud cloud)
        {
            // cloud.gameObject.SetActive(false);
            cloud.transform.parent.gameObject.SetActive(false);
        }
    }
}
