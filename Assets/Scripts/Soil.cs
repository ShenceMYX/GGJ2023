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
        [SerializeField] private float increaseSpeed = 40;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerInputController.Instance.enterSoilArea = true;
                PlayerInputController.Instance.soilIncreaseSpeed = increaseSpeed;
            }
            else if (other.CompareTag("Enemy"))
            {
                other.transform.parent.parent.GetComponent<EnemyHealthController>().enterSoilArea = true;
                other.transform.parent.parent.GetComponent<EnemyHealthController>().soilIncreaseSpeed = increaseSpeed;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerInputController.Instance.enterSoilArea = false;
            }
        }
    }
}
