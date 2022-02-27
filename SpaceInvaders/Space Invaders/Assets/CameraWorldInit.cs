using Assets.Extensions;
using UnityEngine;

public class CameraWorldInit : MonoBehaviour
{

    public Camera world;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCollidersAcrossScreen();
    }

    void GenerateCollidersAcrossScreen()
    {
        Vector2 lDCorner = world.ViewportToWorldPoint(new Vector3(0, 0f, world.nearClipPlane));
        Vector2 rUCorner = world.ViewportToWorldPoint(new Vector3(1f, 1f, world.nearClipPlane));
        Vector2[] colliderpoints;
        var cameraBounds = world.OrthographicBounds();
        var windowHeight = Mathf.Abs(cameraBounds.min.y) + Mathf.Abs(cameraBounds.max.y);

        EdgeCollider2D upperEdge = new GameObject(Assets.Constants.UPPER_EDGE_COLLISION_BOX).AddComponent<EdgeCollider2D>();
        colliderpoints = upperEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, rUCorner.y + (rUCorner.y * 0.1f));
        colliderpoints[1] = new Vector2(rUCorner.x, rUCorner.y + (rUCorner.y * 0.1f));
        upperEdge.points = colliderpoints;

        EdgeCollider2D lowerEdge = new GameObject(Assets.Constants.LOWER_EDGE_COLLISION_BOX).AddComponent<EdgeCollider2D>();
        colliderpoints = lowerEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y + (lDCorner.y * 0.1f));
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y + (lDCorner.y * 0.1f));
        lowerEdge.points = colliderpoints;

        EdgeCollider2D leftEdge = new GameObject(Assets.Constants.LEFT_EDGE_COLLISION_BOX).AddComponent<EdgeCollider2D>();
        colliderpoints = leftEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
        colliderpoints[1] = new Vector2(lDCorner.x, rUCorner.y);
        leftEdge.points = colliderpoints;

        EdgeCollider2D rightEdge = new GameObject(Assets.Constants.RIGHT_EDGE_COLLISION_BOX).AddComponent<EdgeCollider2D>();
        colliderpoints = rightEdge.points;
        colliderpoints[0] = new Vector2(rUCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
        rightEdge.points = colliderpoints;

        EdgeCollider2D enemiesWin = new GameObject(Assets.Constants.ENEMY_WIN_COLLISION_BOX).AddComponent<EdgeCollider2D>();
        colliderpoints = enemiesWin.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y + (windowHeight / 3.0f));
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y + (windowHeight / 3.0f));
        enemiesWin.points = colliderpoints;
    }


}
