using UnityEngine;

public class RegularEnemyBehavior : MonoBehaviour
{

    public SimpleAnimatorAndMovement animationAndMovement;

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider;

    public bool shouldToggleMovement;

    void Update()
    {
        spriteRenderer.sprite = animationAndMovement.GetCurrentSprite;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        shouldToggleMovement = Assets.Constants.TriggeredByWall(collision);
        if (Assets.Constants.TriggeredByPlayerBullet(collision))
        {
            animationAndMovement.SetIsDead();
        }
    }

    public void ResetToggle()
    {
        shouldToggleMovement = false;
    }

}
