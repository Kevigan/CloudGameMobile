using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    [SerializeField] private float timer = 5;
    [SerializeField] private float katapultForce = 15f;

    

    private bool timerStarted = false;
    private bool justExited = false;

    private Vector2 moveDelta = Vector2.zero;
    private float speed = 1f;

    [SerializeField] private GameObject floatingScoreTextPrefab;

    IEnumerator PauseTimer(PlayerCharacter2D player)
    {
        yield return new WaitForSeconds(3);
        //player.SetRigid(false);
        GameManager.Main.ChangeGameState(GameState.Playing);
        player.transform.position = new Vector3(player.transform.position.x, GameManager.Main._highestHeight);
        player.SetYForce(katapultForce);
        player.SetInvincibleTimer(1);
        timerStarted = true;
        Destroy(gameObject);
    }
    IEnumerator timer2(PlayerCharacter2D player)
    {
        yield return new WaitForSeconds(0);
        //player.SetRigid(true);
        GameManager.Main.Life--;
        justExited = true;
        if (GameManager.Main.GameState != GameState.Death)
        {
            GameManager.Main.ChangeGameState(GameState.LostLive);
            StartCoroutine(PauseTimer(player));
        }
    }
    private void Start()
    {
        TurnLeft();
        
    }
    private void FixedUpdate()
    {
        if(GameManager.Main.GameState == GameState.Playing)
            MoveObstacle();
    }

    private void MoveObstacle()
    {
        transform.position += (Vector3)moveDelta * Time.fixedDeltaTime * speed;
    }

    public void TurnRight()
    {
        moveDelta = Vector2.right;
    }

    public void TurnLeft()
    {
        moveDelta = Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {

            if(!player.IsInvincible)
            StartCoroutine(timer2(player));
        }
    }
}
