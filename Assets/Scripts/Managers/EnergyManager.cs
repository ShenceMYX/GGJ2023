using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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

		[SerializeField] private Image branchImg;

		[SerializeField] private float energyClearAnimationDuration = 1f;

        private void Start()
        {
            branchImg.fillAmount = currentEnergy / maxEnergy;
		}

        public void IncreaseEnergy()
        {
			currentEnergy += energyIncreaseSpeed * Time.deltaTime;
			currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

			energyFull = currentEnergy == maxEnergy;

			branchImg.fillAmount = currentEnergy / maxEnergy;
        }

		public void ClearAllEnergy()
        {
			DOTween.To((float pNewValue) => currentEnergy = pNewValue, maxEnergy, 0, energyClearAnimationDuration);
        }
	}
}
