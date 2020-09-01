using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdPersonMovement = null;

    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "Jumping";
    const string FallState = "Falling";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // subscribes and unsubscribes methods to the actions
    private void OnEnable()
    {
        _thirdPersonMovement.Idle += OnIdle;
        _thirdPersonMovement.StartRunning += OnStartRunning;
        _thirdPersonMovement.Jump += OnJump;
        _thirdPersonMovement.StartFalling += OnStartFalling;
    }

    private void OnDisable()
    {
        _thirdPersonMovement.Idle -= OnIdle;
        _thirdPersonMovement.StartRunning -= OnStartRunning;
        _thirdPersonMovement.Jump -= OnJump;
        _thirdPersonMovement.StartFalling -= OnStartFalling;
    }

    // changes animations when events are recieved
    private void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    private void OnStartRunning()
    {
        _animator.CrossFadeInFixedTime(RunState, .2f);
    }

    private void OnJump()
    {
        _animator.CrossFadeInFixedTime(JumpState, .2f);
    }

    private void OnStartFalling()
    {
        _animator.CrossFadeInFixedTime(FallState, .2f);
    }
}
