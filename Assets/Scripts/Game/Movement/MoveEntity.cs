﻿using System.Collections;
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

        if (Input.GetButton(ButtonMoveHorizontal.KeyName))
        {
            float horizontal = Input.GetAxis(ButtonMoveHorizontal.KeyName) * MovementForce * Time.deltaTime;

            vector = Vector3.right * horizontal;
        }

        if (Input.GetButton(ButtonMoveVertical.KeyName))
        {
            float vertical = Input.GetAxis(ButtonMoveVertical.KeyName) * MovementForce * Time.deltaTime;

            vector += Vector3.up * vertical;
        }

        float rotation = Input.GetAxis(ButtonRotation.KeyName) * RotationForce * Time.deltaTime;

        Locator.Position.Translate(vector, Locator.Camera.transform, Locator.Ship.transform);
        transform.Rotate(Vector3.up, rotation);
    }
}
