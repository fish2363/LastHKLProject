using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ami.Extension.EditorScriptingExtension;

namespace Ami.BroAudio.Editor
{
#if BroAudio_DevOnly
	[CreateAssetMenu(menuName = nameof(BroAudio) + "/Editor Setting", fileName = BroEditorUtility.EditorSettingPath)]
#endif
	public class EditorSetting : ScriptableObject
	{
        public enum ReferenceConversionDecision
        {
            AlwaysAsk,
            OnlyConvert,
            ConvertAndSetAddressables,
            ConvertAndClearAllReferences,
        }

		[System.Serializable]
		public struct AudioTypeSetting
		{
            // AudioEntityPropertyDrawer rely on the immutability, don't chagne to class without refactoring it
            public BroAudioType AudioType;
			public Color Color;
			public DrawedProperty DrawedProperty;

			public AudioTypeSetting(BroAudioType audioType,string colorString, DrawedProperty drawedProperty)
			{
				AudioType = audioType;
				ColorUtility.TryParseHtmlString(colorString, out Color);
				DrawedProperty = drawedProperty;
			}

			public bool CanDraw(DrawedProperty target) => DrawedProperty.Contains(target);
		}

        public string AssetOutputPath;
        public bool ShowAudioTypeOnSoundID = FactorySettings.ShowAudioTypeOnSoundID;
		public bool ShowVUColorOnVolumeSlider = FactorySettings.ShowVUColorOnVolumeSlider;
		public bool ShowMasterVolumeOnClipListHeader = FactorySettings.ShowMasterVolumeOnClipListHeader;
        public bool ShowPlayButtonWhenEntityCollapsed = FactorySettings.ShowPlayButtonWhenEntityIsFolded;
        public bool ShowWarningWhenEntityHasNoLoopInChainedMode = FactorySettings.ShowWarningWhenEntityHasNoLoopInChainedMode;
        public bool OpenLastEditAudioAsset = FactorySettings.OpenLastEditAudioAsset;
        public string LastEditAudioAsset;
        public ReferenceConversionDecision DirectReferenceDecision = FactorySettings.DirectReferenceDecision;
        public ReferenceConversionDecision AddressableDecision = FactorySettings.AddressableDecision;

        public bool EditTheNewClipAfterSaveAs = FactorySettings.EditTheNewClipAfterSaveAs;
        public bool PingTheNewClipAfterSaveAs = FactorySettings.PingTheNewClipAfterSaveAs;

        public List<AudioTypeSetting> AudioTypeSettings;
        public List<Color> SpectrumBandColors; 

		public Color GetAudioTypeColor(BroAudioType audioType)
		{
			if(TryGetAudioTypeSetting(audioType,out var setting))
			{
				return setting.Color;
			}
			return default;
		}

		public bool TryGetAudioTypeSetting(BroAudioType audioType,out AudioTypeSetting result)
		{
			result = default;

			// For temp asset
			if(audioType == BroAudioType.None)
			{
				result = new AudioTypeSetting()
				{
					AudioType = audioType,
					Color = Setting.BroAudioGUISetting.FalseColor,
					DrawedProperty = DrawedProperty.All,
				};
				return true;
			}

			if(AudioTypeSettings == null)
			{
				CreateNewAudioTypeSettings();
			}

            foreach (var setting in AudioTypeSettings)
			{
				if(audioType == setting.AudioType)
				{
					result = setting;
					return true;
				}
			}
			return false;
		}

		public bool WriteAudioTypeSetting(BroAudioType audioType, AudioTypeSetting newSetting)
		{       
			for(int i = 0; i < AudioTypeSettings.Count; i++)
			{
				if (audioType == AudioTypeSettings[i].AudioType)
				{
                    UnityEditor.Undo.RecordObject(this, $"Change {audioType} Setting");
                    AudioTypeSettings[i] = newSetting;
					return true;
				}
			}
			return false;
		}

        public Color GetSpectrumColor(int index)
        {
            if(SpectrumBandColors == null || SpectrumBandColors.Count == 0)
            {
                CreateDefaultSpectrumColors();
                UnityEditor.EditorUtility.SetDirty(this);
            }

            if(index >= 0 && index < SpectrumBandColors.Count)
            {
                return SpectrumBandColors[index];
            }
            return new Color(1f, 1f, 1f, 0.2f);
        }

		public void ResetToFactorySettings()
		{
			ShowVUColorOnVolumeSlider = FactorySettings.ShowVUColorOnVolumeSlider;
			ShowAudioTypeOnSoundID = FactorySettings.ShowAudioTypeOnSoundID;
			ShowMasterVolumeOnClipListHeader = FactorySettings.ShowMasterVolumeOnClipListHeader;
            ShowWarningWhenEntityHasNoLoopInChainedMode = FactorySettings.ShowWarningWhenEntityHasNoLoopInChainedMode;
            DirectReferenceDecision = FactorySettings.DirectReferenceDecision;
            AddressableDecision = FactorySettings.AddressableDecision;
            EditTheNewClipAfterSaveAs = FactorySettings.EditTheNewClipAfterSaveAs;
            PingTheNewClipAfterSaveAs = FactorySettings.PingTheNewClipAfterSaveAs;
            ShowPlayButtonWhenEntityCollapsed = FactorySettings.ShowPlayButtonWhenEntityIsFolded;
            OpenLastEditAudioAsset = FactorySettings.OpenLastEditAudioAsset;
            CreateNewAudioTypeSettings();
            CreateDefaultSpectrumColors();
		}

		private void CreateNewAudioTypeSettings()
		{
			AudioTypeSettings = new List<AudioTypeSetting>
			{
				 new AudioTypeSetting(BroAudioType.Music, FactorySettings.MusicColor, FactorySettings.MusicDrawedProperties),
				 new AudioTypeSetting(BroAudioType.UI, FactorySettings.UIColor, FactorySettings.UIDrawedProperties),
				 new AudioTypeSetting(BroAudioType.Ambience, FactorySettings.AmbienceColor, FactorySettings.AmbienceDrawedProperties),
				 new AudioTypeSetting(BroAudioType.SFX, FactorySettings.SFXColor, FactorySettings.SFXDrawedProperties),
				 new AudioTypeSetting(BroAudioType.VoiceOver, FactorySettings.VoiceOverColor, FactorySettings.VoiceOverDrawedProperties),
			};
		}

        internal void CreateDefaultSpectrumColors()
        {
            float alpha = 150f / 256f;
            SpectrumBandColors = new List<Color>()
            {
                GetColor("#7CAEFF"),GetColor("#62EBFA"),GetColor("#77FFA7"),GetColor("#C5FF78"),GetColor("#FF646E"),
                GetColor("#FF64C5"),GetColor("#CC7EFF"),GetColor("#5B39FF"),GetColor("#6DBDFF"),GetColor("#6CFF75"),
            };

            Color GetColor(string htmlString)
            {
                Color color = ColorUtility.TryParseHtmlString(htmlString, out color) ? color : Color.black;
                return color.SetAlpha(alpha);
            }
        }

        public class FactorySettings
		{
			public const bool ShowAudioTypeOnSoundID = true;
			public const bool ShowVUColorOnVolumeSlider = true;
			public const bool ShowMasterVolumeOnClipListHeader = false;
            public const bool OpenLastEditAudioAsset = false;
            public const bool ShowPlayButtonWhenEntityIsFolded = false;
            public const bool ShowWarningWhenEntityHasNoLoopInChainedMode = true;

            public const string MusicColor = "#012F874C";
			public const string UIColor = "#0E9C884C";
			public const string AmbienceColor = "#00B0284C";
			public const string SFXColor = "#FD803D96";
			public const string VoiceOverColor = "#EEC6374C";

			public const DrawedProperty BasicDrawedProperty = DrawedProperty.Volume | DrawedProperty.PlaybackPosition | DrawedProperty.Fade | DrawedProperty.ClipPreview | DrawedProperty.MasterVolume;

			public const DrawedProperty MusicDrawedProperties = BasicDrawedProperty | DrawedProperty.Loop;
			public const DrawedProperty UIDrawedProperties = BasicDrawedProperty | DrawedProperty.PlaybackGroup;
			public const DrawedProperty AmbienceDrawedProperties = BasicDrawedProperty | DrawedProperty.Loop | DrawedProperty.SpatialSettings;
			public const DrawedProperty SFXDrawedProperties = BasicDrawedProperty | DrawedProperty.Loop | DrawedProperty.SpatialSettings | DrawedProperty.Pitch | DrawedProperty.PlaybackGroup;
			public const DrawedProperty VoiceOverDrawedProperties = BasicDrawedProperty | DrawedProperty.PlaybackGroup;

            public const ReferenceConversionDecision DirectReferenceDecision = ReferenceConversionDecision.AlwaysAsk;
            public const ReferenceConversionDecision AddressableDecision = ReferenceConversionDecision.AlwaysAsk;

            public const bool EditTheNewClipAfterSaveAs = true;
            public const bool PingTheNewClipAfterSaveAs = true;
        }
	}
}