using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DozerBullMovement : BaseMovement
{
    public override void Move(in Vector2 dir)
    {
        //base.Move(ManhattanDistance(dir));
        //Debug.Log("ManhattanDistance: " + ManhattanDistance(dir));
        base.Move(ManhattanDistance(dir));
    }

    private Vector2 ManhattanDistance(in Vector2 dir)
    {
        float x = Mathf.Abs(dir.x);
        float y = Mathf.Abs(dir.y);

        Vector2 manhattanDistance = Vector2.zero;

        if (Mathf.Abs(x - y) <= 1)
        {
            return dir;
        }

        if (x > y)
        {
            manhattanDistance.x = dir.x;
        }
        else if (x < y)
        {
            manhattanDistance.y = dir.y;
        }
        else
        {
            manhattanDistance = dir;
        }

        return manhattanDistance.normalized;
    }
}
