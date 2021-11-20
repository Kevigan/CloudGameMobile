using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, GameManager.Main.endHeight);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            GameManager.Main.ChangeGameState(GameState.LevelFinished);
            SoundManager.Main.ChooseSound(SoundType.levelFinished);
        }
    }
}
