using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateChannel", message: "state change to [NewValue]", category: "Events", id: "61a1cc943de0502c4ffcea3b6bcb680d")]
public sealed partial class StateChannel : EventChannel<MonsterState> { }

