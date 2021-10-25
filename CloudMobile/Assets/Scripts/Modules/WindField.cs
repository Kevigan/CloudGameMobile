using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    public delegate void OnChangeWindDirection(int direction);
    public static event OnChangeWindDirection changeDirection;

    public Vector2 windDirection = Vector2.zero;
    public float windSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (changeDirection != null && windDirection.x > 0)
            {
                changeDirection(1);
            }
            else if (changeDirection != null && windDirection.x < 0)
            {
                changeDirection(-1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (changeDirection != null)
            {
                changeDirection(0);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (GameManager.Main.GameState != GameState.LevelFinished)
            {
                Vector3 bla = new Vector3(windDirection.x, player.Velocity.y);
                player.transform.position += new Vector3(bla.x * windSpeed, player.Velocity.y) * Time.fixedDeltaTime;
            }
        }
    }
}
