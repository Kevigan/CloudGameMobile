using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private CollectableType type;
    [SerializeField] private int collectableValue = 100;
    [SerializeField] private Sprite sprite
        ;
    

    private SpriteRenderer spriteRenderer;
    private int _collectableValue = 0;

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
