using System;
using UnityEngine;
using Shared;

namespace Player.Runtime
{
    public class PlayerController : Universe
    {
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");

        public enum PlayerState { Idle, Moving, Jumping, Falling }

        [Header("Movement Settings")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _addForceMultiplier = 1f;
        [SerializeField] private float _speedModifier = 1f;
        [SerializeField] private Movement.MovementType _movementType = Movement.MovementType.MovePosition;

        [Header("Jump Settings")]
        [SerializeField] private float _jumpForce = 5f;

        [Header("References")]
        [SerializeField] private Rigidbody _rigidbody; 
        public Transform _camera; // Reference to Cinemachine FreeLook Camera
        [SerializeField] private Animator _animator;
        [SerializeField] private InputReader _inputReader;

        private Vector3 _movementInput;
        private Vector3 _cameraInput;
        private PlayerState _currentState;
        public bool m_isGrounded;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _inputReader.Initialize();
            _inputReader.EnablePlayerMap();
            _inputReader.JumpEvent += Jump;
        }

        private void OnDisable()
        {
            _inputReader.JumpEvent -= Jump;
        }

        private void Update()
        {
            HandleMovementInput();

            if (_movementInput != Vector3.zero)
            {
                MovePlayer();
            }
            else
            {
                //_animator.SetBool(IsMoving, false);
                _currentState = PlayerState.Idle;
            }

            UpdateStates();
        }

        /*private void HandlePlayerState(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Idle:
                    canJump = true;
                    canRun = true;
                    grounded = true;
                    break;
                case PlayerState.Moving:
                    canJump = true;
                    canRun = true;
                    break;
                case PlayerState.Jumping:
                    canJump = false;
                    canRun = false;
                    grounded = false;
                    break;
                    
            }
        }
        */
        private void HandleMovementInput()
        {
            Vector2 input = _inputReader.m_playerMove;

            if (input == Vector2.zero)
            {
                _movementInput = Vector3.zero;
                return;
            }

            // Assure-toi que la caméra est bien initialisée
            if (_camera == null)
                _camera = Camera.main.transform;

            // Directions caméra
            Vector3 camForward = _camera.forward;
            Vector3 camRight = _camera.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            // Déplacement relatif à la caméra
            _movementInput = (camForward * input.y + camRight * input.x).normalized;

            // Rotation du joueur dans la direction du mouvement
            if (_movementInput != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(_movementInput);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
            }
        }


        private void MovePlayer()
        {
            //_animator.SetBool(IsMoving, true);
            _currentState = PlayerState.Moving;

            switch (_movementType)
            {
                case Movement.MovementType.AddForce:
                    _rigidbody.AddForce(_movementInput * (_speed * _addForceMultiplier * _speedModifier * Time.deltaTime), ForceMode.VelocityChange);
                    break;

                case Movement.MovementType.MovePosition:
                    Vector3 targetPos = transform.position + _movementInput * (_speed * _speedModifier * Time.deltaTime);
                    _rigidbody.MovePosition(targetPos);
                    break;

                case Movement.MovementType.Translate:
                    transform.Translate(_movementInput * (_speed * _speedModifier * Time.deltaTime), Space.World);
                    break;
            }
        }

        private void Jump()
        {
            if (m_isGrounded == false) return;
            _currentState = PlayerState.Jumping;

            Vector3 velocity = _rigidbody.linearVelocity;
            velocity.y = 0f;
            _rigidbody.linearVelocity = velocity;

            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            //_animator.SetTrigger(IsJumping);
        }

        private void UpdateStates()
        {
            switch (_currentState)
            {
                case PlayerState.Moving:
                    //_animator.SetBool(IsMoving, true);
                    break;
                case PlayerState.Jumping:
                    //_animator.SetTrigger(IsJumping);
                    break;
                case PlayerState.Falling:
                    break;
                default:
                    //_animator.SetBool(IsMoving, false);
                    break;
            }
        }
    }
}
