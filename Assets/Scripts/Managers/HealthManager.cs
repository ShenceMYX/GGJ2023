using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class HealthManager : MonoSingleton<HealthManager>
	{
		[Tooltip("初始有几个血条")] private int initialHealthCount = 3;
		[Tooltip("单个血条的最大生命值")] [SerializeField] private float maxHealth = 100;
        [Tooltip("每秒血条下降的速度")] [SerializeField] private float decreaseSpeed = 5;
        [Tooltip("每秒血条上升的速度")] [SerializeField] private float increaseSpeed = 5;

        //血量是否下降
        public bool isHealthDecreasing //{ get; set; }
            = true;

        //血量是否上升
        public bool isHealthIncreasing //{ get; set; }
            = false;

        [SerializeField] private float totalHealth;
        [SerializeField] private float currentHealth;

		[SerializeField] private Transform healthUIRootTrans;
		[SerializeField] private GameObject healthUIPrefab;

		private List<HealthUIController> healthUIControllerList = new List<HealthUIController>();

        public override void Init()
        {
            base.Init();

			initialHealthCount = healthUIRootTrans.childCount;
			for (int i = 0; i < healthUIRootTrans.childCount; i++)
            {
				healthUIControllerList.Add(healthUIRootTrans.GetChild(i).GetComponentInChildren<HealthUIController>());
            }
            totalHealth = CalculateTotalHealth();
            currentHealth = totalHealth;
        }

        private void Update()
        {
            if (isHealthDecreasing)
            {
                currentHealth -= decreaseSpeed * Time.deltaTime;
                if (currentHealth < 0) currentHealth = 0;

                UpdateHealthUI();

                if (currentHealth <= 0)
                    Death();
            }
            else if (isHealthIncreasing)
            {
                currentHealth += increaseSpeed * Time.deltaTime;
                if (currentHealth > totalHealth) currentHealth = totalHealth;

                UpdateHealthUI();
            }
        }

        private void UpdateHealthUI()
        {
            int currentDecreasingHealthUIIndex = Mathf.FloorToInt(currentHealth / maxHealth);
            healthUIControllerList[currentDecreasingHealthUIIndex].SetHealthUIFillAmount((currentHealth - currentDecreasingHealthUIIndex * maxHealth) / maxHealth);
        }

        private void Death()
        {
            Debug.Log("Death!!!!!");
        }

        private float CalculateTotalHealth()
        {
            return healthUIControllerList.Count * maxHealth;
        }

        /// <summary>
        /// 增加血量上限
        /// </summary>
        public void IncreaseMaxHealth()
        {
            GameObject newHealthUIGO = Instantiate(healthUIPrefab, healthUIRootTrans);

            healthUIControllerList.Add(newHealthUIGO.GetComponentInChildren<HealthUIController>());

            totalHealth = CalculateTotalHealth();
        }

        public void StopDecreasingHealth()
        {
            isHealthDecreasing = false;
        }

        public void ContinueDecreasingHealth()
        {
            isHealthDecreasing = true;
        }

        public void StartIncreasingHealth()
        {
            isHealthDecreasing = false;
            isHealthIncreasing = true;
        }

        public void StopIncreasingHealth()
        {
            isHealthDecreasing = true;
            isHealthIncreasing = false;
        }
    }
}
