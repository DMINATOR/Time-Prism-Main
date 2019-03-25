using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldBlockLocator : MonoBehaviour
{
    public LineRenderer DebugLineRenderer;
    public TextMesh DebugBlockName;

    [Tooltip("Current block position in the universe")]
    public GameObject ProjectilesContainer;
}
