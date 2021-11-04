using System.Collections;
using UnityEngine;

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

    //[SerializeField] private Animator animator;
    [SerializeField] private TextMesh jumpAmountText;

    [SerializeField] private cloudColor color;
    [SerializeField] private int _jumpAmount = 1;
    [Header("Collectables")]
    [SerializeField] private Collectables[] collectables;
    [SerializeField] private Transform spawnPoint;

    public int JumpAmount { get => jumpAmount; set => jumpAmount = value; }


    private int jumpAmount;

    private Vector2 moveDelta;
    private SpriteRenderer spriteRenderer;
    private bool enteredCloud = false;

    private bool firstActivation = false;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    private void Start()
    {
        GenerateRandomCollectable();
        jumpAmountText.text = jumpAmount.ToString();
        SetJumpAmount();
        spriteRenderer = GetComponent<SpriteRenderer>();

        int zufall = Random.Range(0, 2);
        if (zufall == 0)
        {
            if (moveHorizontal) moveDelta = Vector2.right;
            if (moveVertical) moveDelta = Vector2.up;
        }
        else
        {
            if (moveHorizontal) moveDelta = Vector2.left;
            if (moveVertical) moveDelta = Vector2.down;
        }
        UpdateText();
    }

    private void OnEnable()
    {
        if (firstActivation) GenerateRandomCollectable();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (GameManager.Main.GameState == GameState.Playing)
            ActiveColorehaviour();
    }

    private void GenerateRandomCollectable()
    {
        firstActivation = true;
        int number = Random.Range(0, collectables.Length);
        int number2 = Random.Range(0, 10);
        if (number2 > 6)
        {
            Collectables _collectables = Instantiate(collectables[number], spawnPoint.position, Quaternion.identity);
            _collectables.transform.parent = gameObject.transform;
        }
    }

    public void SetJumpAmount()
    {
        jumpAmount = _jumpAmount;
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
        jumpAmountText.text = jumpAmount.ToString();
    }

    IEnumerator EnableCloud()
    {
        yield return new WaitForSeconds(5);
        SetJumpAmount();
        UpdateText();
        BoxCollider2D[] box = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D col in box)
        {
            col.enabled = true;
        }
        spriteRenderer.enabled = true;
    }

    private void CheckJumps()
    {
        if (jumpAmount <= 0)
        {
            BoxCollider2D[] box = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D col in box)
            {
                col.enabled = false;
            }
            spriteRenderer.enabled = false;

            StartCoroutine(EnableCloud());
        }
    }

    private void ActiveColorehaviour()
    {
        switch (color)
        {
            case cloudColor.red:
                MoveRed();
                break;
            case cloudColor.blue:
                break;
            case cloudColor.green:
                MoveGreen();
                break;
            case cloudColor.yellow:
                break;
            case cloudColor.white:
                break;
            case cloudColor.black:
                break;
            default:
                break;
        }
    }

    private void MoveGreen()
    {
        transform.localPosition += (Vector3)moveDelta * Time.fixedDeltaTime * speed;
    }
    private void MoveRed()
    {
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
        //if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D character)
        //{
        //    if (character.Velocity.y <= 0 && GameManager.Main.GameState != GameState.LevelFinished)
        //    {
        //        character.SetYForce(addJumpForce);
        //        enteredCloud = true;
        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D character && enteredCloud)
        {
            if (GameManager.Main.GameState != GameState.LevelFinished)
            {
                jumpAmount--;
                UpdateText();
                CheckJumps();
                enteredCloud = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (player.Velocity.y <= 0 && GameManager.Main.GameState != GameState.LevelFinished)
            {
                player.SetYForce(addJumpForce);
                enteredCloud = true;
            }
        }
    }
}


