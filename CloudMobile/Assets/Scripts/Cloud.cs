using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum cloudColor
{
    red,
    blue,
    green,
    yellow,
    white,
    black
}
public class Cloud : MonoBehaviour
{
    

    [SerializeField] private float speed = 5f;
    [SerializeField] private float addJumpForce = 5;
    [SerializeField] private bool moveVertical;
    [SerializeField] private bool moveHorizontal;

    [SerializeField] private cloudColor color;
    [SerializeField] private int jumpAmount = 1;

    [SerializeField] private TextMeshProUGUI text;

    List<PlayerCharacter2D> charPassengers = new List<PlayerCharacter2D>();

    private int _jumpAmount;

    private Vector2 moveDelta;
    private SpriteRenderer spriteRenderer;
    private bool enteredCloud = false;
    

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void Start()
    {
        _jumpAmount = jumpAmount;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (moveHorizontal) moveDelta = Vector2.right;
        if (moveVertical) moveDelta = Vector2.up;
        UpdateText();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(GameManager.Main.GameState == GameState.Playing)
            Move();
    }
    
    private void UpdateColor()
    {
        switch (color)
        {
            case cloudColor.red:
                spriteRenderer.color = new Color(1, 0, 0, 1);
                break;
            case cloudColor.blue:
                spriteRenderer.color = new Color(0, 0, 1, 1);
                break;
            case cloudColor.green:
                spriteRenderer.color = new Color(0, 1, 0, 1);
                break;
            case cloudColor.yellow:
                spriteRenderer.color = new Color(1, 0.92f, 0.016f, 1);
                break;
            case cloudColor.white:
                spriteRenderer.color = new Color(1, 1, 1, 1);
                break;
            case cloudColor.black:
                spriteRenderer.color = new Color(0, 0, 0, 1);
                break;
        }
    }

    private void UpdateText()
    {
        text.text = _jumpAmount.ToString();
    }

    IEnumerator EnableCloud()
    {
        yield return new WaitForSeconds(5);
        _jumpAmount = jumpAmount;
        UpdateText();
        BoxCollider2D[] box = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D col in box)
        {
            col.enabled = true;
        }
        spriteRenderer.enabled = true;
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = true;
    }

    private void CheckJumps()
    {
        if (_jumpAmount <= 0)
        {
            BoxCollider2D[] box = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D col in box)
            {
                col.enabled = false;
            }
            spriteRenderer.enabled = false;
            Canvas canvas = GetComponentInChildren<Canvas>();
            canvas.enabled = false;
            StartCoroutine(EnableCloud());
        }
    }

    

    private void Move()
    {
        //if (transform.localPosition.x < -1) moveDelta = Vector2.right;
        //if (transform.localPosition.x > 1) moveDelta = Vector2.left;
        if (transform.localPosition.y < -5) moveDelta = Vector2.up;
        if (transform.localPosition.y > 5) moveDelta = Vector2.down;
        transform.localPosition += (Vector3)moveDelta * Time.fixedDeltaTime * speed;
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
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D character)
        {
            if(character.Velocity.y <= 0)
            {
                character.SetYForce(addJumpForce);
                enteredCloud = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D character && enteredCloud)
        {
            _jumpAmount--;
            UpdateText();
            CheckJumps();
            enteredCloud = false;
        }
    }
}


