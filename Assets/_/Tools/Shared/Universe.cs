using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    public class Universe : MonoBehaviour
    {
        [SerializeField] private bool verbose;

        protected void Log(string message)
        {
            if (verbose)
            {
                Debug.Log(message);
            }
        }

        protected void Warn(string message)
        {
            if (verbose)
            {
                Debug.LogWarning(message + this);
            }  
        }
    }
}
