﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(OpenWorldControllerLocator))]
public class OpenWorldController : MonoBehaviour
{
    //Not exposed

    //position in the oldes block
    private int BlocksOldestElement = 0;

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "OpenWorld";


    //Public instance to game controller
    public static OpenWorldController Instance = null;

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public OpenWorldControllerLocator Locator;


    [Header("Prefabs")]

    [ReadOnly]
    [Tooltip("Settings to load BLOCK_SIZE value")]
    public GameObject OpenWorldBlockPrefab;


    [Header("Settings")]

    [ReadOnly]
    [Tooltip("Settings to load BLOCK_SIZE value")]
    public SettingsConstants.Name BLOCK_SIZE_SETTING_NAME = SettingsConstants.Name.BLOCK_SIZE;


    [ReadOnly]
    [Tooltip("Settings to load BLOCK_OUT_RESCALE value")]
    public SettingsConstants.Name BLOCK_OUT_RESCALE = SettingsConstants.Name.BLOCK_OUT_RESCALE;

    [Header("Loaded Settings")]

    [ReadOnly]
    [Tooltip("Half Block Size")]
    public int HalfBlockSize;

    [ReadOnly]
    [Tooltip("Current size of the block in Unity units (loaded from Settings)")]
    public int BlockSize;

    [ReadOnly]
    [Tooltip("Resets position back to center after leaving block ranges (loaded from Settings)")]
    public int BlockOutRescale;

    [Header("Variables")]

    [ReadOnly]
    [Tooltip("Current block position")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position")]
    public long BlockZ;

    [ReadOnly]
    [Tooltip("Current block that is considered to be a center 0,0")]
    public long BlockCenterX;

    [ReadOnly]
    [Tooltip("Current block that is considered to be a center 0,0")]
    public long BlockCenterZ;


    [Header("Status")]


    [ReadOnly]
    [Tooltip("Currently loaded and active blocks")]
    //These are loaded initially and re-used
    public OpenWorldBlock[] Blocks = null;

    [ReadOnly]
    [Tooltip("Current block the player is in")]
    public OpenWorldBlock CurrentBlock;

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
        BlockSize = SettingsController.Instance.GetValue<int>(BLOCK_SIZE_SETTING_NAME);
        HalfBlockSize = BlockSize / 2;

        BlockOutRescale = SettingsController.Instance.GetValue<int>(BLOCK_OUT_RESCALE);
    }

    public void SetUniverseCenter(long blockX, long blockZ)
    {
        BlockCenterX = blockX;
        BlockCenterZ = blockZ;
    }

    private void UpdateBlock(int element, long BlockX, long BlockZ)
    {
        OpenWorldBlock openWorldBlock = null;
            
        if (Blocks[element] == null )
        {
            //doesn't exist yet, first time load, create new instance:
            var gameObject = Instantiate(OpenWorldBlockPrefab, new Vector3(-BlockSize, 0, -BlockSize), Quaternion.identity, this.Locator.BlocksGameObjectParent.gameObject.transform);
            Blocks[element] = gameObject.GetComponent<OpenWorldBlock>();
        }
        //else, already exists

        openWorldBlock =  Blocks[element];

        openWorldBlock.Refresh(BlockX, BlockZ, true);

        CurrentBlock = openWorldBlock;
    }

    private void UpdateBlocks(long BlockX, long BlockZ)
    {
        if (Blocks.Length == 0)
        {
            //initial load - create blocks and fill them up
            Blocks = new OpenWorldBlock[9];

            UpdateBlock(0, BlockCenterX - 1, BlockCenterZ - 1);
            UpdateBlock(1, BlockCenterX, BlockCenterZ - 1);
            UpdateBlock(2, BlockCenterX + 1, BlockCenterZ - 1);

            UpdateBlock(3, BlockCenterX - 1, BlockCenterZ);
            UpdateBlock(4, BlockCenterX, BlockCenterZ);
            UpdateBlock(5, BlockCenterX + 1, BlockCenterZ);

            UpdateBlock(6, BlockCenterX - 1, BlockCenterZ + 1);
            UpdateBlock(7, BlockCenterX, BlockCenterZ + 1);
            UpdateBlock(8, BlockCenterX + 1, BlockCenterZ + 1);

            //assign first block as the oldest one
            BlocksOldestElement = 0;

            CurrentBlock = Blocks[4]; //center block will be considered current
        }
        else
        {
            //find matching block
            var index = Array.FindIndex(Blocks, x => x.BlockX == BlockX && x.BlockZ == BlockZ);

            if (index == -1)
            {
                UpdateBlock(BlocksOldestElement, BlockX, BlockZ);

                //move to the next
                BlocksOldestElement = (BlocksOldestElement + 1) >= Blocks.Length ? 0 : (BlocksOldestElement + 1);
            }
            else
            {
                //already loaded, remember current block
                CurrentBlock = Blocks[index];
            }
        }
    }

    private void ReCenter(long BlockX, long BlockZ)
    {
        Locator.MoveEntity.Locator.Position.TranslateToCenter(OpenWorldController.Instance.CurrentBlock);

        //move current object to center
        BlockCenterX = BlockX;
        BlockCenterZ = BlockZ;

        //correct all existing blocks based on current position
        foreach ( var block in Blocks )
        {
            block.Refresh( block.BlockX, block.BlockZ, false );
        }

        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Block [{BlockX}, {BlockZ}] Is now center of universe");
    }

    public void Reposition(long BlockX, long BlockZ)
    {
        if ( (Blocks.Length != 0) && (this.BlockX == BlockX ) && (this.BlockZ == BlockZ) )
        {
            //nothing changed - skip
        }
        else
        {
            //block position changed
            UpdateBlocks(BlockX, BlockZ);

            //remember current position
            this.BlockX = BlockX;
            this.BlockZ = BlockZ;

            if (BlockX > (BlockCenterX + BlockOutRescale) ||
                 BlockZ > (BlockCenterZ + BlockOutRescale) ||
                 BlockX < (BlockCenterX - BlockOutRescale) ||
                 BlockZ < (BlockCenterZ - BlockOutRescale)
                 )
            {
                //we moved outside the range, Recenter
                ReCenter(BlockX, BlockZ);
            }
            //else - skip, we are within the block limit range


            //assign current block and load it
            OpenWorldController.Instance.CurrentBlock.GenerateIfNotLoaded();
        }
    }
}
