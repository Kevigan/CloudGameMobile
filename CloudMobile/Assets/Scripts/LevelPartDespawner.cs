using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartDespawner : MonoBehaviour
{
   // [SerializeField] private List<GameObject> cloudPositions;

    private void Start()
    {
        //foreach (GameObject obj in cloudPositions)
        //{
        //    float x = Random.Range(0f, 8f + 1f);
        //    obj.transform.position = new Vector3(obj.transform.position.x - x, obj.transform.position.y);
        //}
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (player.Velocity.y > 0)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
