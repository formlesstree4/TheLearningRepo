using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimatorAndMovement : MonoBehaviour
{

    public List<Sprite> SPRITES;

    public Sprite DEATH_SPRITE;

    private Rigidbody2D rigidBody;

    public float startingSpeed = 0.4f;

    public float endingSpeed = 0.1f;

    public float secondsBetweenMovement = 0.4f;

    public float horizontalMovementSpeed = 0.08f;

    public float verticalMovementSpeed = 0.15f;
    
    public bool IsAwaitingToggle => movementToggled;

    public Sprite GetCurrentSprite => currentSprite;



    private Sprite currentSprite;

    private int spriteIndex = 0;

    private bool movementToggled = false;

    private bool isDead = false;

    private float timeTracking = 0.0f;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isDead) secondsBetweenMovement = startingSpeed;
        timeTracking += Time.deltaTime;
        UpdateSprite(timeTracking > secondsBetweenMovement);
        if (timeTracking > secondsBetweenMovement)
        {
            UpdateMovement();
            timeTracking = 0.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Assets.Constants.TriggeredByPlayerBullet(collision))
        {
            timeTracking = 0; // reset the animation tracker so we can hold spite longer
            isDead = true;
        }
    }

    private void UpdateSprite(bool updateIndex)
    {
        if (isDead)
        {
            currentSprite = DEATH_SPRITE;
            return;
        }
        if (updateIndex)
        {
            spriteIndex++;
        }
        if (spriteIndex == SPRITES.Count)
        {
            spriteIndex = 0;
        }
        currentSprite = SPRITES[spriteIndex];
    }

    private void UpdateMovement()
    {
        if (isDead)
        {
            Destroy(gameObject);
            return;
        }
        if (movementToggled)
        {
            horizontalMovementSpeed *= -1;
            rigidBody.position += new Vector2(0, -verticalMovementSpeed);
            movementToggled = false;
            return;
        }
        rigidBody.position += new Vector2(horizontalMovementSpeed, 0);
    }

    public void SwitchMovementDirections()
    {
        movementToggled = true;
    }

}
