using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerWithVirtualCamera : MonoBehaviour
{
    [Header("Mobile Input Reference")]
    [SerializeField] GameObject _uiMobileObject;
    [SerializeField] UIVirtualJoystick _virtualJoystickInput;
    [SerializeField] UITouchPanel _touchPanelInput;

    [Header("Player Inputs")]
    private Vector2 _inputs;

    private PlayerInput Player;
    private InputActionMap inputMap;
    private InputAction runAction;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    [Header("Animation Section")]
    private Animator _animator;
    private int _animSpeedId;
    private int _animRunId;
    private float _animSpeed;

    private Rigidbody _rigidBody;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _playerSpeed;
    private float _standardSpeed = 2.0f;
    private float _runSpeed = 5.0f;
    private float jumpForce = 1.0f;

    [Header("Camera Reference")]
    [SerializeField] private GameObject _cinemachineTargetObject;
    private Camera _mainCamera;
    private Vector2 _cameraMovement;
    private float _cinemachineTargetX;
    private float _cinemachineTargetY;
    [SerializeField] IsOnGround isOnGround;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        Player = GetComponent<PlayerInput>();
        inputMap = Player.currentActionMap;
        moveAction = inputMap["Move"];
        lookAction = inputMap["Look"];
        runAction = inputMap["Run"];
        jumpAction = inputMap["Jump"];

        _animator = GetComponent<Animator>();
        _animSpeedId = Animator.StringToHash("Speed");
        _animRunId = Animator.StringToHash("isPlayerRunning");

        _mainCamera = Camera.main;
    }
    private void Update()
    {
        InputFromDevices();
        CameraRotation();
    }
    private void FixedUpdate()
    {
        Movement();
    }

    #region DeviceInput
    private void InputFromDevices()
    {
        _inputs = _virtualJoystickInput.VectorOutput();
        //_inputs = moveAction.ReadValue<Vector2>();
        _cameraMovement = 85f * Time.deltaTime * -_touchPanelInput.VectorOutput();
    }

    private bool IsPlayerRunning()
    {
        return runAction.ReadValue<float>() != 0 ? true : false;
    }
#endregion

#region MovementSection
    private void Movement()
    {
        _playerSpeed = IsPlayerRunning() ? _runSpeed : _standardSpeed;
        _animSpeed = Mathf.Abs(_inputs.x) + Mathf.Abs(_inputs.y);

        if (_inputs != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(_inputs.x, _inputs.y) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, 0.12f);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _rigidBody.velocity = new Vector3(targetDirection.x * _playerSpeed, _rigidBody.velocity.y, targetDirection.z * _playerSpeed);

            Animations(_animSpeed, IsPlayerRunning());
        }
        else
        {
            _playerSpeed = 0.0f;
            _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
            Animations(_animSpeed, false);
        }

        if (jumpAction.ReadValue<float>() != 0 && isPlayerOnGround())
        {
            _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        }

    bool isPlayerOnGround()
    {

        return isOnGround.GetBoolGround();
    }

#endregion

#region AnimationSection
    private void Animations(float animSpeed, bool isRunning)
    {
        _animator.SetFloat(_animSpeedId, animSpeed);
        _animator.SetBool(_animRunId, isRunning);
    }
#endregion

#region CameraSection
    private void CameraRotation()
    {
        _cinemachineTargetX += -_cameraMovement.x;
        _cinemachineTargetY += _cameraMovement.y;

        _cinemachineTargetX = CameraClampAngle(_cinemachineTargetX, float.MinValue, float.MaxValue);
        _cinemachineTargetY = CameraClampAngle(_cinemachineTargetY, -30.0f, 70.0f);

        _cinemachineTargetObject.transform.rotation = Quaternion.Euler(_cinemachineTargetY + 0.0f, _cinemachineTargetX, 0.0f);
    }

    private float CameraClampAngle(float angle, float angleMin, float angleMax)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, angleMin, angleMax);
    }
#endregion
}


