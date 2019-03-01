using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldPosition : MonoBehaviour
{
    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockZ;

    [ReadOnly]
    [Tooltip("Current position in Unity coordinates")]
    public float UnityX;

    [ReadOnly]
    [Tooltip("Current position in Unity coordinates")]
    public float UnityZ;

    public void Translate(Vector3 vector, Transform cameraTransform, Transform shipTransform)
    {
        this.transform.Translate(vector, cameraTransform);

        //save current unity coordinates
        UnityX = shipTransform.position.x;
        UnityZ = shipTransform.position.z;

        //block correction
        BlockX = (long)(shipTransform.position.x / OpenWorldController.Instance.HalfBlockSize);
        BlockZ = (long)(shipTransform.position.z / OpenWorldController.Instance.HalfBlockSize);

        OpenWorldController.Instance.Reposition(BlockX, BlockZ);
    }
}
