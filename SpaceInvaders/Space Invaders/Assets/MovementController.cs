using UnityEngine;

public class MovementController : MonoBehaviour
{

    public Camera world;

    public Rigidbody2D body;

    public BoxCollider2D boxCollider;

    public GameObject bulletPrefab;

    private GameObject currentBullet;


    void Start()
    {
        
    }

    public void OnMove(UnityEngine.InputSystem.InputValue value)
    {
        body.velocity = value.Get<Vector2>() * 4;
    }

    public void OnFire(UnityEngine.InputSystem.InputValue value)
    {
        if (currentBullet != null && currentBullet.activeSelf)
        {
            return;
        }
        
        currentBullet = Instantiate(bulletPrefab, transform);
        currentBullet.name = Assets.Constants.PLAYER_BULLET;
        var bulletLocation = currentBullet.GetComponent<Rigidbody2D>();
        var bulletCollider = currentBullet.GetComponent<BoxCollider2D>();

        bulletLocation.position += new Vector2(0, (bulletCollider.size.y / 2) + (boxCollider.size.y / 2));
    }

}
