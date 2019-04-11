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

    [Tooltip("Current Time Scale Instance assigned for this")]
    public TimeControlTimeScale TimeScaleInstance;

    private void Start()
    {
        TimeScaleInstance = TimeControlController.Instance.CreateTimeScaleInstance(this);
        UpdateOnTimeScaleChanged();
    }

    public void UpdateOnTimeScaleChanged()
    {
        //TODO
        //Locator.RotateAroundAxis.Speed = Mathf.Abs(TimeControlController.Instance.TimeScale * RotationScale); //Always positive
        //Locator.TextSpeed.text = TimeControlController.Instance.TimeScale.ToString();
    }
}
