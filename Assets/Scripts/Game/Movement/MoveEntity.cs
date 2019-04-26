using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenWorldPosition))]
[RequireComponent(typeof(MoveEntityLocator))]
public class MoveEntity : MoveOnKey
{
    public MoveEntityLocator Locator;

    /// <summary>
    /// Method to call, to correct when open world block changes
    /// </summary>
    public void CorrectOnWorldTranslateToCenter()
    {
        Locator.TimeControlObject.LogAndTranslateToPointInstantly(transform.position, transform.rotation);

        Locator.Position.Translate(Locator.Ship.transform);
    }

    protected override void OnPositionChange(Vector3 position, Quaternion rotation)
    {
        Locator.TimeControlObject.RotateAndPosition(position, rotation);

        Locator.Position.Translate(Locator.Ship.transform);
    }
}
