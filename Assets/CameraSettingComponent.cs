using Member.Isac._1.Scripts.Entities;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSettingComponent : MonoBehaviour,IEntityComponent
{
    public CinemachineCamera crouchCamera;
    public CinemachineCamera defaultCamera;

    public void Initialize(Entity entity)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChangeCamera(bool isCrouch)
    {
        crouchCamera.Priority = isCrouch ? 15 : 10;
        defaultCamera.Priority = !isCrouch ? 15 : 10;
    }
}
