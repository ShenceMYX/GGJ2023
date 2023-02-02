using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class HealthUIController : MonoBehaviour
	{
		private Image healthImg;

        private void Start()
        {
			healthImg = GetComponent<Image>();
        }

        public void SetHealthUIFillAmount(float ratio)
        {
			if (ratio <= 0.001) ratio = 0;

			healthImg.fillAmount = ratio;
        }
	}
}
