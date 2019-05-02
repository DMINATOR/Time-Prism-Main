using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UpdateTextFromTimeScale : MonoBehaviour
{
    [Tooltip("Current Time Scale Instance assigned for this object")]
    [SerializeField]
    private TimeControlTimeScale TimeScaleInstance;

    //Control to update text for
    private Text _text;

    void Start()
    {
        _text = gameObject.GetComponent<Text>();
        TimeScaleInstance = TimeControlController.Instance.CreateTimeScaleInstance(this);
    }

    void Update()
    {
        TimeScaleInstance.Update();

        _text.text = $"{TimeScaleInstance.AffectionName} : {TimeScaleInstance.TimeScale.ToString("0.00")} = {TimeScaleInstance.CurrentTime.ToString("0.00")} - {TimeScaleInstance.TimeScaleDelta.ToString("0.00")} Delta";
    }
}
