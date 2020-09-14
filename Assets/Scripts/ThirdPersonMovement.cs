using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action Jump = delegate { };
    public event Action StartFalling = delegate { };
    public event Action StartSprinting = delegate { };

    public CharacterController controller;
    public Transform cam;

    [SerializeField] PlayerCharacterAnimator _playerCharacterAnimator;

    public float speed = 6f;
    public float sprintSpeed = 60f;
    public float jumpSpeed = 1f;
    private float _vSpeed = 0f;

    private float _gravity = 12f;

    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    bool _isMoving = false;
    bool _isAirborne = false;
    bool _isFalling = false;
    bool _isSprinting = false;

    private void Start()
    {
        Idle?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (!_isAirborne && _isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_isSprinting)
            {
                _isSprinting = true;
                StartSprinting?.Invoke();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && _isSprinting)
        {
            _isSprinting = false;
            if (!_isAirborne && _isMoving)
            {
                StartRunning?.Invoke();
            }
        }

        if (controller.isGrounded)
        {
            _vSpeed = 0f;
            if (Input.GetKeyDown("space"))
            {
                Jump?.Invoke();
                _vSpeed = jumpSpeed;
                _isAirborne = true;
            }
        }
        else
        {
            if (!_playerCharacterAnimator.IsPlayingAny())
            {
                if (!_isFalling)
                {
                    StartFalling?.Invoke();
                    Debug.Log("Started Falling");
                    _isFalling = true;
                }
            }
        }

        if (direction.magnitude >= .1f || _vSpeed != 0)
        {
            CheckIfStartedMoving();
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = new Vector3();
            if (direction.magnitude >= .1f) {
                moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            }
            
            if (!_isSprinting)
            {
                moveDir *= speed;
            }
            else
            {
                moveDir *= sprintSpeed;
            }

            _vSpeed -= _gravity * Time.deltaTime;
            moveDir.y = _vSpeed;
            controller.Move(moveDir * Time.deltaTime);

            CheckIfLanded();
        }
        else
        {
            CheckIfStoppedMoving();
        }
    }

    private void CheckIfLanded()
    {
        if (controller.isGrounded && _isAirborne)
        {
            StartRunning?.Invoke();
            Debug.Log("Landed");
            _isAirborne = false;
            _isFalling = false;
        }
    }

    private void CheckIfStartedMoving()
    {
        if (_isMoving == false)
        {
            if (!_isAirborne)
            {
                StartRunning?.Invoke();
            }
            Debug.Log("Started");
        }

        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if (_isMoving == true)
        {
            Idle?.Invoke();
            Debug.Log("Stopped");
        }

        _isMoving = false;
    }
}
