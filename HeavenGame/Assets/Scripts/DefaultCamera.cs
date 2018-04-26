using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCamera : MonoBehaviour
{
    public GameObject target;
    public Vector2 focusAreaSize;
    public float verticalOffset;
    private FocusArea focusArea;
    public float minMapX, maxMapX, minMapY, maxMapY;
    // focus box for camera
    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;

        private float left, right, top, bottom;

        // constructor
        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = Vector2.zero;
        }

        // update focus area bounds
        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;

            // move x bound of focus area depending on where player has moved to
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }

            left += shiftX;
            right += shiftX;

            float shiftY = 0;

            // move y bound of focus area depending on where player has moved to
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }

            top += shiftY;
            bottom += shiftY;

            center = CalculateCenter();
            velocity = new Vector2(shiftX, shiftY);

        }

        private Vector2 CalculateCenter()
        {
            return new Vector2((left + right) / 2, (top + bottom) / 2);
        }
    }

    void Awake()
    {
        // Get player and instantiate focusArea with player's box collider bounds
        target = GameObject.FindGameObjectWithTag("Player");
        focusArea = new FocusArea(target.GetComponent<BoxCollider2D>().bounds, focusAreaSize);
        focusAreaSize.x = 5;
        focusAreaSize.y = 5;
    }

    // to help see focus area in scene
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    // all player movement is finished for frame
    private void LateUpdate()
    {
        // update camera focus area based on Player collider
        focusArea.Update(target.GetComponent<BoxCollider2D>().bounds);

        // move camera up from player by a certain offset
        Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

        Vector3 temp = transform.position;
        // change camera position based on focusPosition
        transform.position = (Vector3)focusPosition + Vector3.forward * -10; // camera is always infront of play area
        Camera cam = GetComponent<Camera>();
        if (transform.position.x < minMapX)
        {
            Vector3 newpos;
            newpos.x = minMapX;
            newpos.y = transform.position.y;
            newpos.z = transform.position.z;
            transform.position = newpos;
        }

        if (transform.position.x + cam.orthographicSize > maxMapX)
        {
            Vector3 newpos;
            newpos.x = maxMapX - cam.orthographicSize;
            newpos.y = transform.position.y;
            newpos.z = transform.position.z;
            transform.position = newpos;
        }

        if (transform.position.y + cam.orthographicSize > maxMapY)
        {
            Vector3 newpos;
            newpos.x = transform.position.x;
            newpos.y = maxMapY - cam.orthographicSize;
            newpos.z = transform.position.z;
            transform.position = newpos;
        }

        if (transform.position.y < minMapY)
        {
            Vector3 newpos;
            newpos.x = transform.position.x;
            newpos.y = minMapY;
            newpos.z = transform.position.z;
            transform.position = newpos;
        }
    }
}
