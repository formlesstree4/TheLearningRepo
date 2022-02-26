using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimatorAndMovement : MonoBehaviour
{

    public List<Sprite> SPRITES;

    public Sprite DEATH_SPRITE;

    public Rigidbody2D rigidBody;

    public int frameDelay = 240;

    public int minimumFrameDelay = 80;

    public float horizontalMovementSpeed = 0.08f;

    public float verticalMovementSpeed = 0.15f;


    private Sprite currentSprite;

    private int spriteIndex = 0;

    private int frameCount = 0;

    private bool movementToggled = false;

    private bool isDead = false;


    void Update()
    {
        frameCount++;
        UpdateSprite(frameCount >= frameDelay);
        if (frameCount < frameDelay) return;
        UpdateMovement();
        frameCount = 0;
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
            Destroy(this.gameObject);
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

    public void SetIsDead()
    {
        isDead = true;
    }
    

    public bool IsAwaitingToggle => movementToggled;

    public Sprite GetCurrentSprite => currentSprite;

}
