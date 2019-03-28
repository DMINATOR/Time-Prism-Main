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

    public void TranslateToCenter(OpenWorldBlock block)
    {
        Vector3 sourcePoint = new Vector3(
           OpenWorldController.Instance.BlockSize * block.BlockDeltaX,
           this.transform.position.y,
           OpenWorldController.Instance.BlockSize * block.BlockDeltaZ);

        Vector3 targetPoint = new Vector3(
           this.transform.position.x,
           this.transform.position.y,
           this.transform.position.z);

        Vector3 currentVector = targetPoint - sourcePoint;

        //translate to the new position

        this.transform.position = currentVector;
    }

    public void Translate(Transform shipTransform)
    {
        //save current unity coordinates
        UnityX = shipTransform.position.x;
        UnityZ = shipTransform.position.z;

        float blockCorrectionX = 0;
        float blockCorrectionZ = 0;

        long worldCorrectionX = OpenWorldController.Instance.BlockCenterX;
        long worldCorrectionZ = OpenWorldController.Instance.BlockCenterZ;

        if (shipTransform.position.x > 0)
        {
            blockCorrectionX = OpenWorldController.Instance.BlockSize;
            worldCorrectionX -= 1;
        }
        else if (shipTransform.position.x < 0)
        {
            blockCorrectionX = -OpenWorldController.Instance.BlockSize;
        }
        //else 0

        if (shipTransform.position.z > 0)
        {
            blockCorrectionZ = OpenWorldController.Instance.BlockSize;
            worldCorrectionZ -= 1;
        }
        else if (shipTransform.position.z < 0)
        {
            blockCorrectionZ = -OpenWorldController.Instance.BlockSize;
        }
        //else 0

        //block calculation
        BlockX = worldCorrectionX + (int)((shipTransform.position.x + blockCorrectionX) / OpenWorldController.Instance.BlockSize);
        BlockZ = worldCorrectionZ + (int)((shipTransform.position.z + blockCorrectionZ) / OpenWorldController.Instance.BlockSize);

        OpenWorldController.Instance.Reposition(BlockX, BlockZ);
    }
}
