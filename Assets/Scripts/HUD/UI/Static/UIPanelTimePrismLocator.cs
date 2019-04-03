using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanelTimePrismLocator : MonoBehaviour
{
    [Tooltip("Reference to the basic rotation script")]
    public RotateAroundAxis RotateAroundAxis;

    [Tooltip("Shows current speed")]
    public Text TextSpeed;

    [Tooltip("Event to call when triggered")]
    public UnityEvent EventOnTimeScaleChangedCallback;
}
