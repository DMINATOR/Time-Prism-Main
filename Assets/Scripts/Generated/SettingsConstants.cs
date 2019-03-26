
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 25.03.2019 21:06.26
public class SettingsConstants
{

    public enum Name
    {
		BLOCK_SIZE,
		BLOCK_OUT_RESCALE,
		PROJECTILES_LIMIT,

    }

    public static void Load()
    {
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.BLOCK_SIZE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "10",
			MaxValue = "100000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.BLOCK_OUT_RESCALE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "3",
			MaxValue = "100000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.PROJECTILES_LIMIT),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "100",
			MaxValue = "10000"
		});

    }
}
