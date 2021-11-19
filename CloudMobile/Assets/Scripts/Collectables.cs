using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private CollectableType type;
    [SerializeField] private int collectableValue = 100;
    private int _collectableValue = 0;
    [SerializeField]
    private Sprite sprite;


    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject floatingScoreTextPrefab;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    private void Start()
    {
        _collectableValue = collectableValue;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>())
        {
            GameManager.Main.floatingScoreText = collectableValue;
            Instantiate(floatingScoreTextPrefab, new Vector3(transform.position.x -1, transform.position.y) , Quaternion.identity);
            Destroy(gameObject);
            GameManager.Main.ActualHighScore += _collectableValue;
            GameManager.Main.UpdateScore();
        }

    }
}
public enum CollectableType
{
    Coin,
    Diamond,
    Gold
}

