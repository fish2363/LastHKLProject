using Blade.Core;

public static class PlayerEvents
{
    public static readonly PlayerDead PlayerDead = new PlayerDead();
}

public class PlayerDead : GameEvent
{ }