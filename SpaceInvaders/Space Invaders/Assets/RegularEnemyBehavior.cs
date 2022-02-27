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

    private AudioSource audioSource;

    private float timeSinceLastBullet;

    
    private AudioClip shoot;
    private AudioClip explosion;


    public void ResetToggle()
    {
        shouldToggleMovement = false;
    }

    void Start()
    {
        animationAndMovement = GetComponent<SimpleAnimatorAndMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        shoot = Resources.Load<AudioClip>("SFX\\shoot");
        explosion = Resources.Load<AudioClip>("SFX\\invaderkilled");
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
        if (Assets.Constants.TriggeredByPlayerBullet(collision))
        {
            audioSource.clip = explosion;
            audioSource.Play();
        }
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
        audioSource.clip = shoot;
        audioSource.Play();
    }

    void OnDestroy()
    {
        if (currentBullet != null)
        {
            currentBullet.transform.parent = null;
        }
    }

}
