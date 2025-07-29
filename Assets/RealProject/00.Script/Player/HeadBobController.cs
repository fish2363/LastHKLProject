using System;
using UnityEngine;

public class HeadBobController : MonoBehaviour, IEntityComponent
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.0014f;
    [SerializeField, Range(0, 30f)] private float _frequency = 10.5f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterMovement _controller;


    public void Initialize(Entity entity)
    {
        _controller = entity.GetCompo<CharacterMovement>();
        _startPos = _camera.localPosition;
    }

    public void SetAmplitude(float value) => _amplitude = value;
    public void SetFrequency(float value) => _frequency = value;

    private void CheckMotion()
    {
        float speed = new Vector3(_controller.characterController.velocity.x,0,_controller.characterController.velocity.z).magnitude * _controller.MoveSpeed;

        if (speed < _toggleSpeed) return;
        if (!_controller.characterController.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private void Update()
    {
        if (!enabled) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x,transform.position.y+_cameraHolder.localPosition.y,transform.position.z);
        pos += _cameraHolder.forward * 15f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition,_startPos,1*Time.deltaTime);
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos=Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency)*_amplitude;
        pos.x += Mathf.Sin(Time.time * _frequency/2) * _amplitude * 2;
        return pos;
    }

}
