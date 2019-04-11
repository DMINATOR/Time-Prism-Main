
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Generated on: 09.04.2019 21:26.47
public class SettingsConstants
{

    public enum Name
    {
		BLOCK_SIZE,
		BLOCK_OUT_RESCALE,
		PROJECTILES_LIMIT,
		TIME_CONTROL_ELEMENTS_SIZE,
		TIME_CONTROL_CHANGE_DIFFERENCE,

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
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TIME_CONTROL_ELEMENTS_SIZE),
			Type = SettingValueType.Integer,
			MinValue = "0",
			DefaultValue = "100",
			MaxValue = "10000"
		});
		SettingsController.Instance.AddSetting(new SettingValue()
		{
			Name = Enum.GetName(typeof(SettingsConstants.Name), Name.TIME_CONTROL_CHANGE_DIFFERENCE),
			Type = SettingValueType.Float,
			MinValue = "0",
			DefaultValue = "0,2",
			MaxValue = "1"
		});

    }
}
