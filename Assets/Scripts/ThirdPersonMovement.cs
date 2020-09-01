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

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float jumpSpeed = 1f;
    private float _vSpeed = 0f;

    private float _gravity = 12f;

    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    bool _ismoving = false;

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

        if (controller.isGrounded)
        {
            _vSpeed = 0f;
            if (Input.GetKeyDown("space"))
            {
                Jump?.Invoke();
                _vSpeed = jumpSpeed;
            }
        }

        if (direction.magnitude >= .1f || _vSpeed != 0)
        {
            CheckIfStartedMoving();
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            moveDir *= speed;

            _vSpeed -= _gravity * Time.deltaTime;
            moveDir.y = _vSpeed;
            controller.Move(moveDir * Time.deltaTime);
        }
        else
        {
            CheckIfStoppedMoving();
        }
    }

    private void CheckIfStartedMoving()
    {
        if (_ismoving == false)
        {
            StartRunning?.Invoke();
            Debug.Log("Started");
        }

        _ismoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if (_ismoving == true)
        {
            Idle?.Invoke();
            Debug.Log("Stopped");
        }

        _ismoving = false;
    }
}
