using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DozerBullMovement : BaseMovement
{
    public override void Move(in Vector2 dir)
    {
        //base.Move(ManhattanDistance(dir));
        base.Move(dir);
    }

    private Vector2 ManhattanDistance(in Vector2 dir)
    {
        Vector2 manhattanDistance = Vector2.zero;

        if (dir.x > dir.y)
        {
            manhattanDistance.x = dir.x;
        }
        else if (dir.x < dir.y)
        {
            manhattanDistance.y = dir.y;
        }
        else
        {
            manhattanDistance = dir;
        }

        return manhattanDistance;
    }
}
