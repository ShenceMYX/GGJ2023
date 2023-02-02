using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class Soil : MonoBehaviour
	{
        [SerializeField] private float decreaseSpeedRatio = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerInputController>().enterSoilArea = true;
                HealthManager.Instance.SetHealthIncreaseSpeed(decreaseSpeedRatio);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerInputController>().enterSoilArea = false;
                HealthManager.Instance.SetHealthIncreaseSpeed(1);
            }
        }
    }
}
