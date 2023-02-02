using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        private new Rigidbody rigidbody;
        [SerializeField] private float moveSpeed = 5f;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Movement(Vector3 direction)
        {
            rigidbody.velocity = direction * moveSpeed;
        }
    }

}
