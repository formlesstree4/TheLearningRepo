using Assets.State;
using UnityEngine;

public class RegularEnemyBehavior : MonoBehaviour
{

    public bool shouldToggleMovement;

    public float shootingPercentage;

    public float secondsBetweenShootAttempt;

    public GameObject bulletPrefab;

    internal SimpleAnimatorAndMovement animationAndMovement;

    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider;

    private GameObject currentBullet;

    private float timeSinceLastBullet;


    public void ResetToggle()
    {
        shouldToggleMovement = false;
    }

    void Start()
    {
        animationAndMovement = GetComponent<SimpleAnimatorAndMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        spriteRenderer.sprite = animationAndMovement.GetCurrentSprite;
        timeSinceLastBullet += Time.deltaTime;
        if (timeSinceLastBullet > secondsBetweenShootAttempt)
        {
            timeSinceLastBullet = 0.0f;
            if (Random.Range(0.0f, 1.0f) < shootingPercentage)
            {
                FireBullet();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        shouldToggleMovement = Assets.Constants.TriggeredByWall(collision);
        if (Assets.Constants.TriggeredByDeathPlane(collision))
        {
            GameState.Died();
        }
    }

    void FireBullet()
    {
        if (currentBullet != null && currentBullet.activeSelf)
        {
            return;
        }
        currentBullet = Instantiate(bulletPrefab, transform);
        currentBullet.name = Assets.Constants.ENEMY_BULLET;
        var bulletLocation = currentBullet.GetComponent<Rigidbody2D>();
        var bulletCollider = currentBullet.GetComponent<BoxCollider2D>();
        bulletLocation.position -= new Vector2(0, (bulletCollider.size.y / 2) + (boxCollider.size.y / 2));
    }

}
