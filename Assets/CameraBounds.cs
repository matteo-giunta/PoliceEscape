using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public float thickness = 1f; // spessore dei bordi

    void Start()
    {
        Camera cam = Camera.main;

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        CreateBoundary("Top", new Vector2(0, camHeight / 2 + thickness / 2),
            new Vector2(camWidth, thickness));

        CreateBoundary("Bottom", new Vector2(0, -camHeight / 2 - thickness / 2),
            new Vector2(camWidth, thickness));

        CreateBoundary("Left", new Vector2(-camWidth / 2 - thickness / 2, 0),
            new Vector2(thickness, camHeight));

        CreateBoundary("Right", new Vector2(camWidth / 2 + thickness / 2, 0),
            new Vector2(thickness, camHeight));
    }

    void CreateBoundary(string name, Vector2 position, Vector2 size)
    {
        GameObject border = new GameObject(name);
        border.transform.parent = transform;
        border.transform.position = position;

        BoxCollider2D collider = border.AddComponent<BoxCollider2D>();
        collider.size = size;
        collider.isTrigger = true;

        border.AddComponent<BorderKill>();
    }
}