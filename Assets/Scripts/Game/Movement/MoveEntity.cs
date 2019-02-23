using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEntity : MonoBehaviour
{
    public float MovementForce;
    public float RotationForce;

    public InputButton ButtonMoveHorizontal;
    public InputButton ButtonMoveVertical;

    public InputButton ButtonRotation;

    public Camera Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(ButtonMoveHorizontal.KeyName))
        {
            float horizontal = Input.GetAxis(ButtonMoveHorizontal.KeyName) * MovementForce * Time.deltaTime;

            transform.Translate(Vector3.right * horizontal, Camera.transform);
        }

        if (Input.GetButton(ButtonMoveVertical.KeyName))
        {
            float vertical = Input.GetAxis(ButtonMoveVertical.KeyName) * MovementForce * Time.deltaTime;

            transform.Translate(Vector3.up * vertical, Camera.transform);
        }

        float rotation = Input.GetAxis(ButtonRotation.KeyName) * RotationForce * Time.deltaTime;

        transform.Rotate(Vector3.up, rotation);
    }
}
