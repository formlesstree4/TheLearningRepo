using UnityEngine;

namespace Assets
{
    internal class Constants
    {
        public const string UPPER_EDGE_COLLISION_BOX = "upperEdge";
        public const string LOWER_EDGE_COLLISION_BOX = "lowerEdge";
        public const string LEFT_EDGE_COLLISION_BOX = "leftEdge";
        public const string RIGHT_EDGE_COLLISION_BOX = "rightEdge";
        public const string ENEMY_WIN_COLLISION_BOX = "enemiesWin";
        public const string PLAYER_BULLET = "PlayerBullet";
        public const string ENEMY_BULLET = "EnemyBullet";
        public const string PLAYER_HOMES = "PlayerHomes";
        public const string PLAYER_NAME = "Player";

        public const string ENEMY_TAG = "enemy";


        private static readonly string[] PLAYER_BULLET_IGNORE = new[]
        {
            PLAYER_BULLET,
            PLAYER_HOMES,
            ENEMY_WIN_COLLISION_BOX,
            PLAYER_NAME
        };


        internal static bool TriggeredByWall(Collider2D collision)
        {
            return
                (collision.gameObject.name == UPPER_EDGE_COLLISION_BOX) ||
                (collision.gameObject.name == LOWER_EDGE_COLLISION_BOX) ||
                (collision.gameObject.name == LEFT_EDGE_COLLISION_BOX) ||
                (collision.gameObject.name == RIGHT_EDGE_COLLISION_BOX);
        }

        internal static bool TriggeredByPlayer(Collider2D collision)
        {
            return collision.gameObject.name == PLAYER_NAME;
        }

        internal static bool PlayerBulletIgnoreCollision(Collider2D collision)
        {
            foreach(var ignore in PLAYER_BULLET_IGNORE)
            {
                if (ignore == collision.gameObject.name)
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool TriggeredByPlayerBullet(Collider2D collision)
        {
            return collision.gameObject.name == PLAYER_BULLET;
        }

        internal static bool TriggeredByEnemyBullet(Collider2D collision)
        {
            return collision.gameObject.name == ENEMY_BULLET;
        }

        internal static bool TriggeredByDeathPlane(Collider2D collision)
        {
            return collision.gameObject.name == ENEMY_WIN_COLLISION_BOX;
        }

    }
}
