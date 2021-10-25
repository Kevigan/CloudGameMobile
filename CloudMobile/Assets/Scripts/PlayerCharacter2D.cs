using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter2D : MonoBehaviour
{

    [Header("Höhenmeter Einstellungen")]
    [SerializeField] Scrollbar höhenMeter;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float jumpForce = 5;
    public float JumpForce { get => jumpForce; set { jumpForce = value; } }

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravityMultipier = 5f;

    [Range(1, 2)]
    [SerializeField] private float acceloratorSpeed;

    public bool YVelocityIsActive { get; private set; } = true;
    private Vector2 velocity = Vector2.zero;
    public Vector2 Velocity
    {
        get => new Vector2(xForceSet ? setForce.x : velocity.x, yForceSet ? setForce.y : velocity.y);
        set => SetForce(value);
    }

    private bool yForceSet, xForceSet = false;
    private Vector2 setForce = new Vector2(0, 0);
    private Vector2 addForce = new Vector2(0, 0);

    public CharacterCollision collision;

    private bool moveAllowed;
    //private Collider2D col;
    private Rigidbody2D rigid;

    private Vector2 moveDirection = Vector2.zero;
    Vector2 touchPosition = Vector2.zero;

    private CollisionDetection collisionDetection;
    private bool movingRight;
    private bool movingLeft;

    private GameManager gameManager;

    private void Start()
    {
        GameManager.Main.Life = GameManager.Main.MaxLife;
        GameManager.Main.ChangeGameState(GameState.Playing);
        collisionDetection = GetComponent<CollisionDetection>();
        rigid = GetComponent<Rigidbody2D>();
        //col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (GameManager.Main.GyroScopeInput) GyroscopeInput();
        if (GameManager.Main.TouchInput)TouchInput();
        HandleHöhenmeter();
        collisionDetection.HandleCollision();
    }
    private void FixedUpdate()
    {
        if (rigid.position != touchPosition && GameManager.Main.GameState == GameState.Playing)
        {
            CalculateYVelocity();
            ApplyVelocity();
        }
    }

    private void HandleHöhenmeter()
    {
        höhenMeter.value = ((transform.position.y - startPoint.transform.position.y) / (endPoint.transform.position.y - startPoint.transform.position.y));
    }

    private void ApplyVelocity()
    {
        velocity += addForce;
        addForce = Vector2.zero;

        velocity.x = xForceSet ? setForce.x : velocity.x;
        velocity.y = yForceSet ? setForce.y : velocity.y;

        xForceSet = yForceSet = false;

        velocity.y = YVelocityIsActive ? velocity.y : 0f;
        rigid.MovePosition(rigid.position + (new Vector2(moveDirection.x, velocity.y) * Time.fixedDeltaTime * speed));
    }
    public void AddForce(Vector2 value)
    {
        addForce += value;

    }
    public void SetForce(Vector2 value) //F?r Walljump
    {
        setForce = value;
        yForceSet = xForceSet = true;
    }
    public void SetXForce(float newXForce)
    {
        setForce.x = newXForce;
        xForceSet = true;
    }
    public void SetYForce(float newYForce)
    {
        setForce.y = newYForce;
        yForceSet = true;
    }
    public void CalculateXVelocity(float currentInput)
    {
        velocity.x = currentInput * speed;
    }

    public void CalculateYVelocity()
    {
        if (!collision.Grounded)
        {
            velocity.y -= 9.81f * Time.fixedDeltaTime * gravityMultipier;
            if (velocity.y < -2.5f) velocity.y = -2.5f;
        }
        else if (velocity.y < 0)
        {
            velocity.y = 0;
        }
    }

    private void GyroscopeInput()
    {
        moveDirection.x = Input.acceleration.x * acceloratorSpeed;

    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (collisionDetection.col == touchedCollider)
                {
                    moveAllowed = true;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (!movingLeft && !movingRight) moveDirection = Vector2.zero;
                moveAllowed = false;
            }
        }
    }

    public void GetTouchInputLeft()
    {
        movingLeft = true;
        moveDirection = Vector2.left;
    }

    public void GetTouchInputRight()
    {
        movingRight = true;
        moveDirection = Vector2.right;
    }
    public void CancelMovingLeft()
    {
        movingLeft = false;
    }
    public void CanvelMovingRight()
    {
        movingRight = false;
    }

}


