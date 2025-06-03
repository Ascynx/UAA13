using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtility
{
    public static Vector2 SnapToDirection(Vector2 dir)
    {
        float absX = Mathf.Abs(dir.x);
        float absY = Mathf.Abs(dir.y);

        if (absX > absY)
        {
            return dir.x > 0 ? Vector2.right : Vector2.left;
        } else if (absX < absY)
        {
            return dir.y > 0 ? Vector2.up : Vector2.down;
        } else
        {
            return Vector2.zero;
        }
    }
}
