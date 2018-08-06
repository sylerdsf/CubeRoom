﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalItem : Item
{
    public override void SetEdited(bool edited)
    {

    }

    public override Vector2 GetDragOffset(Vector3 position)
    {
        return new Vector2(0, position.y);
    }

    public override Plane GetOffsetPlane(Vector3 position)
    {
        // x 和 z 任意即可
        float distance = position.z;
        return new Plane(Vector3.back, distance);
    }

    public override Vector3 PositionOffset()
    {
        return Vector3.zero;
    }
}
