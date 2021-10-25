using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderScript : MonoBehaviour
{
    [SerializeField] private GameObject oppositeBoarder;
    [SerializeField] private float distance = .5f;
    [SerializeField] private bool leftBoarder;
    [SerializeField] private bool rightBoarder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            player.transform.position = new Vector3(oppositeBoarder.transform.position.x + distance, player.transform.position.y);
        }
        if(collision.GetComponent<Cloud>() is Cloud cloud && leftBoarder)
        {
            //cloud.transform.position = new Vector3(oppositeBoarder.transform.position.x + distance, cloud.transform.position.y);
            cloud.TurnRight();
        }
        if (collision.GetComponent<Cloud>() is Cloud cloud2 && rightBoarder)
        {
            cloud2.TurnLeft();
        }
    }
}
