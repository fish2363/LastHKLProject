using UnityEngine;

public abstract class JumpScareComponent : MonoBehaviour, IEntityComponent, IJumpscare
{
    public void Excute()
    {
        JumpScareEvent();
    }

    public void Initialize(Entity entity)
    {
    }

    protected abstract void JumpScareEvent();
}
