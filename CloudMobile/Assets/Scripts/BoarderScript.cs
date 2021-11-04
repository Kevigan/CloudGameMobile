using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarderScript : MonoBehaviour
{
    [SerializeField] private GameObject oppositeBoarder;
    [SerializeField] private float distance = 0f;
    [SerializeField] private bool leftBoarder;
    [SerializeField] private bool rightBoarder;

    Vector3 cameraPos = Vector3.zero;
    private void Start()
    {
        float cameraWidth = Screen.width;
        //if (rightBoarder) cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(cameraWidth + 100, transform.position.y));
        //if (leftBoarder) cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(-100, transform.position.y));
        if (rightBoarder) cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(cameraWidth + (cameraWidth / 12), transform.position.y));
        if (leftBoarder) cameraPos = Camera.main.ScreenToWorldPoint(new Vector3(-(cameraWidth / 12), transform.position.y));

        transform.position = cameraPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            player.transform.position = new Vector3(oppositeBoarder.transform.position.x + distance, player.transform.position.y);
        }
        if (collision.GetComponent<Cloud>() is Cloud cloud && leftBoarder)
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
