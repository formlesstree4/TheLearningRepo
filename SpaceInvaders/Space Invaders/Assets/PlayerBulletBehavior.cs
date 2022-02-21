using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{

    public int frameDelay = 120;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D bulletRigidbody;

    public float verticalMovementSpeed = 0.15f;

    private int frameCount = 0;

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if (frameCount % 4 == 0) UpdateSprite();
        if (frameCount < frameDelay) return;
        UpdateMovement();
        frameCount = 0;
    }

    private void UpdateSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void UpdateMovement()
    {
        bulletRigidbody.position += new Vector2(0, verticalMovementSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Assets.Constants.TriggeredByPlayerHome(collision) || Assets.Constants.TriggeredByPlayer(collision))
        {
            return;
        }
        Destroy(gameObject);
    }

}
