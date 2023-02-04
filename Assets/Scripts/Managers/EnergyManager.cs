using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class EnergyManager : MonoSingleton<EnergyManager>
	{
		[SerializeField] private float maxEnergy = 100;
		[SerializeField] private float currentEnergy = 0;
		[SerializeField] private float energyIncreaseSpeed = 20;

		public bool energyFull = false;

		public void IncreaseEnergy()
        {
			currentEnergy += energyIncreaseSpeed * Time.deltaTime;
			currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

			energyFull = currentEnergy == maxEnergy;
        }
	}
}
