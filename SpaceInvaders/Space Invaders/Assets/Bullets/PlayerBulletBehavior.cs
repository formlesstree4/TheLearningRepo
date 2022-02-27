using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{

    public float verticalMovementSpeed;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D bulletRigidbody;

    private int frameCount = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity += new Vector2(0, verticalMovementSpeed);
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

    private void UpdateSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Assets.Constants.PlayerBulletIgnoreCollision(collision))
        {
            return;
        }
        Destroy(gameObject);
    }

}
