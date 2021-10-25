using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public CharacterCollision collision; 
    [HideInInspector]
    public Collider2D col;

    [SerializeField] private LayerMask wallJumpLayer;
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private int groundRays = 5;
    [SerializeField] private bool debugRays = false;
    [SerializeField] private float sideRayLength = .02f;
    [SerializeField] private float bottomRayLength = .01f;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    public void HandleCollision()
    {
        CheckGrounded();
        CheckWalls();
        //CheckHeightToDash();
    }
    private void CheckGrounded()
    {
        Vector2 bottomLeft = col.bounds.center + new Vector3(-col.bounds.extents.x, -col.bounds.extents.y, 0f);
        Vector2 bottomRight = col.bounds.center + new Vector3(col.bounds.extents.x, -col.bounds.extents.y, 0f);

        collision.Grounded = CheckForCollision(bottomLeft, bottomRight, Vector2.down, bottomRayLength, groundLayer);
    }
    private void CheckWalls()
    {
        Vector2 bottomLeft = col.bounds.center + new Vector3(-col.bounds.extents.x, -col.bounds.extents.y, 0f);//left collision
        Vector2 topLeft = col.bounds.center + new Vector3(-col.bounds.extents.x, col.bounds.extents.y, 0f);

        Vector2 topRight = col.bounds.center + new Vector3(col.bounds.extents.x, col.bounds.extents.y, 0f);//right collision
        Vector2 bottomRight = col.bounds.center + new Vector3(col.bounds.extents.x, -col.bounds.extents.y, 0f);

        collision.rightCollision = CheckForCollision(topRight, bottomRight, Vector2.right, sideRayLength, wallJumpLayer);
        collision.leftCollision = CheckForCollision(bottomLeft, topLeft, Vector2.left, sideRayLength, wallJumpLayer);
        //collision.topCollision = CheckForCollision(topLeft, topRight, Vector2.up, sideRayLength);
        //collision.dashCollisionLeft = CheckForCollision(bottomLeft, topLeft, Vector2.left, .1f, dashCollisionLayer);
        //collision.dashCollisionRight = CheckForCollision(topRight, bottomRight, Vector2.right, .1f, dashCollisionLayer);
        collision.ceilingCollision = CheckForCollision(topLeft, topRight, Vector2.up, 0.06f, ceilingLayer);

        //collision.canWallHang = false;

        //if (collision.rightCollision) collision.canWallHang = Physics2D.Raycast(topRight, Vector2.right, sideRayLength, wallJumpLayer)
        //                                                        && Physics2D.Raycast(bottomRight, Vector2.right, sideRayLength, wallJumpLayer);
        //if (collision.leftCollision) collision.canWallHang = Physics2D.Raycast(topLeft, Vector2.left, sideRayLength, wallJumpLayer)
        //                                                        && Physics2D.Raycast(bottomLeft, Vector2.left, sideRayLength, wallJumpLayer);
    }
    private bool CheckForCollision(Vector2 point1, Vector2 point2, Vector2 direction, float rayLength, LayerMask mask)
    {
        bool hasHit = false;

        for (int i = 0; i < groundRays; i++)
        {
            if (hasHit) break;

            Vector2 origin = point1 + i * ((point2 - point1) / (groundRays - 1));
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, rayLength, mask);

            if (debugRays)
            {
                Debug.DrawRay(new Vector3(origin.x, origin.y, 0f), direction * rayLength, Color.red, .1f);
            }
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider == col || hit.collider.isTrigger) continue;

                hasHit = true;
            }
        }
        return hasHit;
    }
}

public struct CharacterCollision
{
    public delegate void ColEvent();
    public ColEvent OnLandedEvent;
    public ColEvent OnGroundLeftEvent;

    public bool Grounded
    {
        get => grounded; set
        {
            if (grounded != value)
            {
                grounded = value;
                if (grounded)
                {
                    OnLandedEvent?.Invoke();
                }
                else
                {
                    OnGroundLeftEvent?.Invoke();
                }
            }
        }
    }
    private bool grounded;
    //public bool dashHeightCollision;
    //public bool dashCollisionLeft;
    //public bool dashCollisionRight;
    public bool rightCollision;
    public bool leftCollision;
    public bool topCollision;
    //public GameObject topCollisionObject;
    public bool ceilingCollision;
    //public bool canWallHang;
}
