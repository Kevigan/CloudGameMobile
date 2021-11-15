using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColider : MonoBehaviour
{
    [SerializeField] private float timer = 5;
    [SerializeField] private float katapultForce = 15f;

    public delegate void OnPlayerFalling();
    public static event OnPlayerFalling onPlayerFalling;

    private bool timerStarted = false;
    private bool justExited = false;

    private void Start()
    {
    }

    private void Update()
    {

    }
    
    IEnumerator PauseTimer(PlayerCharacter2D player)
    {
        yield return new WaitForSeconds(3);
        GameManager.Main.ChangeGameState(GameState.Playing);
        player.transform.position = new Vector3(player.transform.position.x, GameManager.Main._highestHeight);
        player.SetYForce(katapultForce);
        player.SetInvincibleTimer(1);
        timerStarted = true;
        justExited = false;
    }
    IEnumerator timer2(PlayerCharacter2D player)
    {
        yield return new WaitForSeconds(.5f);
        onPlayerFalling.Invoke();
        GameManager.Main.Life--;
        justExited = true;
        if (GameManager.Main.GameState != GameState.Death)
        {
            GameManager.Main.ChangeGameState(GameState.LostLive);
            timerStarted = false;
            StartCoroutine(PauseTimer(player));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player && !justExited)
        {
            onPlayerFalling.Invoke();
            StartCoroutine(timer2(player));
        }else if (collision.GetComponent<Obsticle>() is Obsticle ob)
        {
            Destroy(ob.gameObject);
        }

    }
}
