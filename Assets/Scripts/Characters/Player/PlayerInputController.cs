using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerInputController : MonoBehaviour
    {
        private float xInput;
        private float yInput;

        private CharacterMotor motor;

        private void Start()
        {
            motor = GetComponent<CharacterMotor>();
        }

        private void Update()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");

            if (xInput != 0 || yInput != 0) 
            {
                motor.Movement(new Vector3(xInput, 0, yInput).normalized);
            }
        }
    }
}
