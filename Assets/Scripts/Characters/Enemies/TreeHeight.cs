using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class TreeHeight : MonoBehaviour
	{
        public float height { get; private set; }

        private void Awake()
        {
            height = transform.localScale.y;
        }

        private void Update()
        {
            height = transform.localScale.y;
        }
    }
}
