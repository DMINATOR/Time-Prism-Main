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

    // Update is called once per frame
    void Update()
    {
        var vector = new Vector3();
        var horizontal = 0.0f;
        var vertical = 0.0f;

        if (Input.GetButton(ButtonMoveHorizontal.KeyName))
        {
            horizontal = Input.GetAxis(ButtonMoveHorizontal.KeyName) * MovementForce * Time.deltaTime;

            vector = Vector3.right * horizontal;
        }

        if (Input.GetButton(ButtonMoveVertical.KeyName))
        {
            vertical = Input.GetAxis(ButtonMoveVertical.KeyName) * MovementForce * Time.deltaTime;

            vector += Vector3.forward * vertical;
        }

        float rotation = Input.GetAxis(ButtonRotation.KeyName) * RotationForce * Time.deltaTime * Mathf.PI;

        if (horizontal != 0.0f  || vertical != 0.0f || rotation != 0.0f )
        {
            var rotationQ = transform.rotation * Quaternion.Euler(Vector3.up * rotation);

            Locator.TimeControlObject.LogAndTranslateTo(transform.position + transform.rotation * vector, rotationQ);

            Locator.Position.Translate(Locator.Ship.transform);
        }
    }
}
