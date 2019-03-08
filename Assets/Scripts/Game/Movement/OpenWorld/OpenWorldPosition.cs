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

        float blockCorrectionX = 0;
        float blockCorrectionZ = 0;

        if(shipTransform.position.x > 0 )
        {
            blockCorrectionX = OpenWorldController.Instance.BlockSize;
        }
        else
        {
            blockCorrectionX = -OpenWorldController.Instance.BlockSize;
        }

        if (shipTransform.position.z > 0)
        {
            blockCorrectionZ = OpenWorldController.Instance.BlockSize;
        }
        else
        {
            blockCorrectionZ = -OpenWorldController.Instance.BlockSize;
        }

        //block calculation
        BlockX = (int)((shipTransform.position.x + blockCorrectionX) / OpenWorldController.Instance.BlockSize);
        BlockZ = (int)((shipTransform.position.z + blockCorrectionZ) / OpenWorldController.Instance.BlockSize);

        OpenWorldController.Instance.Reposition(BlockX, BlockZ);
    }
}
