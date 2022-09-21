using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2DNoRot : MonoBehaviour
{
    public Vector3 velocity;

    public LayerMask collisionMask;

    const float skinWidth = 0.015f;
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    float horizontalExtent;
    float verticalExtent;

    BoxCollider2D playerCollider;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        horizontalExtent = playerCollider.bounds.extents.x;
        verticalExtent = playerCollider.bounds.extents.y;
        CalculateRaySpacing();
        collisions.faceDir = 1;
    }

    public void Move(Vector3 move)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (move.x != 0)
        {
            collisions.faceDir = (int)Mathf.Sign(move.x);
        }

        HorizontalCollisions(ref move);

        if (move.y != 0)
        {
            VerticalCollisions(ref move);
        }

        transform.Translate(move);

        velocity = move;
    }

    void HorizontalCollisions(ref Vector3 move)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(move.x) + skinWidth;

        if (Mathf.Abs(move.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        // check left
        // left defined as -1
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.botLeft;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.left, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.left * rayLength * 5f, Color.red);

            if (hit)
            {
                if(move.x < 0) move.x = (hit.distance - skinWidth) * -1;
                rayLength = hit.distance;

                collisions.left = true;
            }
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.botRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * rayLength * 5f, Color.red);

            if (hit)
            {
                if(move.x >= 0) move.x = (hit.distance - skinWidth);
                rayLength = hit.distance;

                collisions.right = true;
            }
        }

        //sure, should refactor, but whatever right?
    }

    void VerticalCollisions(ref Vector3 move)
    {
        float directionY = Mathf.Sign(move.y);
        float rayLength = Mathf.Abs(move.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.botLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + move.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.down * rayLength * 5f, Color.red);

            if (hit)
            {
                if (directionY < 0) move.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = true;
            }
        }

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + move.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * rayLength * 5f, Color.red);

            if (hit)
            {
                if (directionY > 0) move.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.above = true;
            }
        }
    }

    void GroundCollisionsCheck(Vector3 move)
    {
        float rayLength = 0.125f + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.botLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + move.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, collisionMask);

            if (hit)
            {
                collisions.below = true;
            }
        }
    }

    public bool IsGrounded()
    {
        return collisions.below;
    }

    public bool IsColliding()
    {
        return (collisions.below || collisions.above || collisions.left || collisions.right);
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = playerCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.botLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.botRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = playerCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 botLeft, botRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool bound;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
        public int faceDir;
        public bool kill;
    }
}
