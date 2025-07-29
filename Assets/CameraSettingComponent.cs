using Unity.Cinemachine;
using UnityEngine;

public class CameraSettingComponent : MonoBehaviour,IEntityComponent
{
    public CinemachineCamera crouchCamera;
    public CinemachineCamera defaultCamera;

    public void Initialize(Entity entity)
    {
        ESCManager.Instance.LockMouse();
    }

    public void ChangeCamera(bool isCrouch)
    {
        crouchCamera.Priority = isCrouch ? 15 : 10;
        defaultCamera.Priority = !isCrouch ? 15 : 10;
    }
}
