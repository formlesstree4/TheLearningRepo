using Assets.State;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Camera world;

    public GameObject bulletPrefab;

    public Sprite deathSprite;

    private Rigidbody2D body;

    private BoxCollider2D boxCollider;

    private GameObject currentBullet;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    private bool isDead;

    private float waitTimeForDeath = 1.0f;

    private float timeSinceDeath = 0.0f;

    private AudioClip shoot;

    private AudioClip explosion;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        shoot = Resources.Load<AudioClip>("SFX\\shoot");
        explosion = Resources.Load<AudioClip>("SFX\\explosion");
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            timeSinceDeath += Time.deltaTime;
            if (timeSinceDeath > waitTimeForDeath)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        if (isDead) return;
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.velocity = value.Get<Vector2>() * 4;
    }

    public void OnFire(UnityEngine.InputSystem.InputValue value)
    {
        if (isDead) return;
        if (currentBullet != null && currentBullet.activeSelf)
        {
            return;
        }
        currentBullet = Instantiate(bulletPrefab, transform);
        currentBullet.name = Assets.Constants.PLAYER_BULLET;
        var bulletLocation = currentBullet.GetComponent<Rigidbody2D>();
        var bulletCollider = currentBullet.GetComponent<BoxCollider2D>();
        bulletLocation.position += new Vector2(0, (bulletCollider.size.y / 2) + (boxCollider.size.y / 2));
        audioSource.clip = shoot;
        audioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Assets.Constants.TriggeredByEnemyBullet(collision))
        {
            audioSource.clip = explosion;
            audioSource.Play();
            isDead = true;
            spriteRenderer.sprite = deathSprite;
        }
    }

    void OnDestroy()
    {
        GameState.Died();
    }

}
