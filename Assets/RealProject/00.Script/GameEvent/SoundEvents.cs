using Blade.Core;
using UnityEngine;

public static class SoundEvents
{
    public static BigSoundDetection BigSoundDetection = new();
}

public class BigSoundDetection : GameEvent {
    public Transform soundPoint;
}
