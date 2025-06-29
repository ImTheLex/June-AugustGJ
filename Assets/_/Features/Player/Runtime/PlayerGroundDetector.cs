using System;
using Shared;
using UnityEngine;

namespace Player.Runtime
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerGroundDetector : Universe
    {
        private PlayerController _pc;
        private void Awake()
        {
            _pc = GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                Log("Grounded");
                _pc.m_isGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                Log("Not Grounded");
                _pc.m_isGrounded = false;
            }
        }
    }
}
