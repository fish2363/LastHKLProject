using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/AnimationChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "AnimationChannel", message: "change to [NewValue]", category: "Events", id: "2ee8ce32941daa4ba0e863a7df02b2db")]
public sealed partial class AnimationChannel : EventChannel<string> { }

