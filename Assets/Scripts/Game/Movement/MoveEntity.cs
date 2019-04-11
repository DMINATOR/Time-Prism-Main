using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenWorldPosition))]
[RequireComponent(typeof(MoveEntityLocator))]
public class MoveEntity : MonoBehaviour
{
    public float MovementForce;
    public float RotationForce;

    public InputButton ButtonMoveHorizontal;
    public InputButton ButtonMoveVertical;

    public InputButton ButtonRotation;

    public MoveEntityLocator Locator;

    [Tooltip("Current Time Scale Instance assigned for this")]
    public TimeControlTimeScale TimeScaleInstance;

    /// <summary>
    /// Method to call, to correct when open world block changes
    /// </summary>
    public void CorrectOnWorldTranslateToCenter()
    {
        Locator.TimeControlObject.LogAndTranslateToPointInstantly(transform.position, transform.rotation);

        Locator.Position.Translate(Locator.Ship.transform);
    }

    void Start()
    {
        TimeScaleInstance = TimeControlController.Instance.CreateTimeScaleInstance(this);
    }

    // Update is called once per frame
    void Update()
    {
        var vector = new Vector3();
        var horizontal = 0.0f;
        var vertical = 0.0f;

        if (Input.GetButton(ButtonMoveHorizontal.KeyName))
        {
            horizontal = Input.GetAxis(ButtonMoveHorizontal.KeyName) * MovementForce * TimeScaleInstance.TimeScaleDelta;

            vector = Vector3.right * horizontal;
        }

        if (Input.GetButton(ButtonMoveVertical.KeyName))
        {
            vertical = Input.GetAxis(ButtonMoveVertical.KeyName) * MovementForce * TimeScaleInstance.TimeScaleDelta;

            vector += Vector3.forward * vertical;
        }

        float rotation = Input.GetAxis(ButtonRotation.KeyName) * RotationForce * TimeScaleInstance.TimeScaleDelta * Mathf.PI;

        if (horizontal != 0.0f  || vertical != 0.0f || rotation != 0.0f )
        {
            var rotationQ = transform.rotation * Quaternion.Euler(Vector3.up * rotation);

            Locator.TimeControlObject.LogAndTranslateTo(transform.position + transform.rotation * vector, rotationQ);

            Locator.Position.Translate(Locator.Ship.transform);
        }
    }
}
