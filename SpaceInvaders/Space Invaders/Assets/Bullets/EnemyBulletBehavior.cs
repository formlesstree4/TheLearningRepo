using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{

    public float verticalMovementSpeed;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D bulletRigidbody;

    private int frameCount = 0;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity -= new Vector2(0, verticalMovementSpeed);
    }

    void FixedUpdate()
    {
        frameCount++;
        if (frameCount % 4 == 0)
        {
            frameCount = 0;
            UpdateSprite();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Assets.Constants.TriggeredByWall(collision))
        {
            Destroy(gameObject);
        }
        if (!Assets.Constants.TriggeredByPlayer(collision))
        {
            return;
        }
        Destroy(gameObject);
    }

    private void UpdateSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

}
