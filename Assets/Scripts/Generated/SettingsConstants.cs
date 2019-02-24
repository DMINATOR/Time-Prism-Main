
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 24.02.2019 18:24.22
public class SettingsConstants
{

    public enum Name
    {
		BLOCK_SIZE,

    }

    public static void Load()
    {
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.BLOCK_SIZE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "100",
			MaxValue = "100000"
		});

    }
}
