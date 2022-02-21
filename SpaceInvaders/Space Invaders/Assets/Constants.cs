using System;
using UnityEngine;

namespace Assets
{
    internal class Constants
    {
        public const string UPPER_EDGE_COLLISION_BOX = "upperEdge";
        public const string LOWER_EDGE_COLLISION_BOX = "lowerEdge";
        public const string LEFT_EDGE_COLLISION_BOX = "leftEdge";
        public const string RIGHT_EDGE_COLLISION_BOX = "rightEdge";
        public const string PLAYER_BULLET = "PlayerBullet";
        public const string ENEMY_BULLET = "EnemyBullet";
        public const string PLAYER_HOMES = "PlayerHomes";
        public const string PLAYER_NAME = "Player";


        public static bool TriggeredByWall(Collider2D collision)
        {
            return
                (collision.gameObject.name == UPPER_EDGE_COLLISION_BOX) ||
                (collision.gameObject.name == LOWER_EDGE_COLLISION_BOX) ||
                (collision.gameObject.name == LEFT_EDGE_COLLISION_BOX) ||
                (collision.gameObject.name == RIGHT_EDGE_COLLISION_BOX);
        }

        public static bool TriggeredByPlayer(Collider2D collision)
        {
            return collision.gameObject.name == PLAYER_NAME;
        }

        public static bool TriggeredByPlayerBullet(Collider2D collision)
        {
            return collision.gameObject.name == PLAYER_BULLET;
        }

        public static bool TriggeredByPlayerHome(Collider2D collision)
        {
            return collision.gameObject.name == PLAYER_HOMES;
        }

    }
}
