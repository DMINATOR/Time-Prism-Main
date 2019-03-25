using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldControllerLocator : MonoBehaviour
{
    [Tooltip("GameObject that will be used as a parent for blocks that are created")]
    public GameObject BlocksGameObjectParent;

    [Tooltip("Main movement entity")]
    public MoveEntity MoveEntity;
}
