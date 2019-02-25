using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldController : MonoBehaviour
{
    //Cached blocks
    [ReadOnly]
    [Tooltip("Currently loaded and active blocks")]
    public OpenWorldBlock[] Blocks;

    [ReadOnly]
    [Tooltip("Current block position of the center")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position of the center")]
    public long BlockZ;

    //Public instance to game controller
    public static OpenWorldController Instance = null;

    private void Awake()
    {
        //Create instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateBlocks();
    }


    private void UpdateBlocks()
    {
        if( Blocks.Length != 9 )
        {
            //initial load
            Blocks = new OpenWorldBlock[9];

            for(int i = 0; i < Blocks.Length; i++)
            {
                //Blocks[i] = Instantiate(Instance.PhysicsModel, spawn_point.transform.position, Quaternion.identity);
            }
        }
        else
        {

        }
    }

    public void Reposition(long BlockX, long BlockZ )
    {
        if( (this.BlockX == BlockX ) && (this.BlockZ == BlockZ) )
        {
            //nothing changed - skip
        }
        else
        {
            //block position changed
            UpdateBlocks();
        }
    }
}
