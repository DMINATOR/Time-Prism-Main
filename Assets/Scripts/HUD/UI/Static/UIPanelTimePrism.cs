using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIPanelTimePrismLocator))]
public class UIPanelTimePrism : MonoBehaviour
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public UIPanelTimePrismLocator Locator;


    [Tooltip("Additional scale to apply for rotation")]
    public float RotationScale;

    private void Start()
    {
        UpdateOnTimeScaleChanged();
    }

    public void UpdateOnTimeScaleChanged()
    {
        Locator.RotateAroundAxis.Speed = TimeControlController.Instance.TimeScale * RotationScale;
        Locator.TextSpeed.text = TimeControlController.Instance.TimeScale.ToString();
    }
}
