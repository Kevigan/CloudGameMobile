using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    [SerializeField] private float jumpForce = 4.5f;
    [SerializeField] private int collectableValue = 100;
    private int _collectableValue = 0;
    [SerializeField] private GameObject floatingScoreTextPrefab;

    private void Start()
    {
        _collectableValue = collectableValue;
    }

    public void PlayFloatingText()
    {
        GameManager.Main.floatingScoreText = collectableValue;
        Instantiate(floatingScoreTextPrefab, new Vector3(transform.position.x - 1, transform.position.y), Quaternion.identity);
        GameManager.Main.ActualHighScore += _collectableValue;
        GameManager.Main.UpdateScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (player.Velocity.y <= 0)
            {
                GameManager.Main.floatingScoreText = collectableValue;
                Instantiate(floatingScoreTextPrefab, new Vector3(transform.position.x - 1, transform.position.y), Quaternion.identity);
                player.SetYForce(jumpForce);
                GameManager.Main.ActualHighScore += _collectableValue;
                GameManager.Main.UpdateScore();


                Destroy(transform.parent.gameObject);
            }
        }
        

    }
}
