using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using MoreMountains.Feedbacks;
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
        //[Tooltip("每秒血条上升的速度")] [SerializeField] private float increaseSpeed = 5;
        [Tooltip("每秒血条上升的速度")] public float healthChangeSpeed = 0;
        //private float originalIncreaseSpeed;


        //血量是否下降
        //public bool isHealthDecreasing = true;

        //血量是否上升
        //public bool isHealthIncreasing = false;

        [SerializeField] private float totalHealth;
        [SerializeField] private float currentHealth;

		[SerializeField] private Transform healthUIRootTrans;
		[SerializeField] private GameObject healthUIPrefab;

		private List<HealthUIController> healthUIControllerList = new List<HealthUIController>();

        [SerializeField] private MMF_Player deathFeedbacks;

        [SerializeField] private Transform treeTrunksRootTrans;

        [Tooltip("玩家会不会死亡")] [SerializeField] private bool willTriggerDeath = false;

        private bool isDeath = false;

        [SerializeField] private GameObject extraHealthGO;

        public override void Init()
        {
            base.Init();

            //originalIncreaseSpeed = increaseSpeed;

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
            if (isDeath) return;

            currentHealth += healthChangeSpeed * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);

            UpdateHealthUI();

            if (currentHealth <= 0)
                Death();

            //if (isHealthDecreasing)
            //{
            //    currentHealth -= decreaseSpeed * Time.deltaTime;
            //    if (currentHealth < 0) currentHealth = 0;

            //    UpdateHealthUI();

            //    if (currentHealth <= 0)
            //        Death();
            //}
            //else if (isHealthIncreasing)
            //{
            //    currentHealth += increaseSpeed * Time.deltaTime;
            //    if (currentHealth > totalHealth) currentHealth = totalHealth;

            //    UpdateHealthUI();
            //}
        }

        private void UpdateHealthUI()
        {
            int currentDecreasingHealthUIIndex = Mathf.Clamp(Mathf.FloorToInt(currentHealth / maxHealth), 0, healthUIControllerList.Count - 1);
            healthUIControllerList[currentDecreasingHealthUIIndex].SetHealthUIFillAmount((currentHealth - currentDecreasingHealthUIIndex * maxHealth) / maxHealth);
        }

        public int GetHealthCount()
        {
            return healthUIControllerList.Count;
        }

        public float GetCurrentHealth()
        {
            return currentHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetHealthIndex()
        {
            return Mathf.Clamp(Mathf.FloorToInt(currentHealth / maxHealth), 0, healthUIControllerList.Count - 1);
        }

        private void Death()
        {
            if (!willTriggerDeath) return;

            deathFeedbacks?.PlayFeedbacks();
            EnableGreyscaleEffectsForTreeBase();
            GameManager.Instance.GameOver();
            isDeath = true;
        }

        private void EnableGreyscaleEffectsForTreeBase()
        {
            treeTrunksRootTrans.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("GREYSCALE_ON");
            //for (int i = 0; i < treeTrunksRootTrans.childCount; i++)
            //{
            //    treeTrunksRootTrans.GetChild(i).GetComponent<Renderer>().material.EnableKeyword("GREYSCALE_ON");
            //}
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
            //GameObject newHealthUIGO = Instantiate(healthUIPrefab, healthUIRootTrans);
            extraHealthGO.transform.parent = healthUIRootTrans;

            healthUIControllerList.Add(extraHealthGO.GetComponentInChildren<HealthUIController>());

            totalHealth = CalculateTotalHealth();
        }

        /// <summary>
        /// 设置血量降低速度
        /// </summary>
        public void AddHealthChangeSpeed(float value)
        {
            healthChangeSpeed += value;
        }

        /// <summary>
        /// 设置血量降低速度
        /// </summary>
        public void AddHealthChangeSpeed(float value, float delay)
        {
            StartCoroutine(LateAddHealthChangeSpeed(value, delay));
        }

        private IEnumerator LateAddHealthChangeSpeed(float value, float delay)
        {
            yield return new WaitForSeconds(delay);
            healthChangeSpeed += value;
        }

        //public void StopDecreasingHealth()
        //{
        //    isHealthDecreasing = false;
        //}

        //public void StopDecreasingHealth(float delay)
        //{
        //    Invoke("StopDecreasingHealth", delay);
        //}

        //public void ContinueDecreasingHealth()
        //{
        //    isHealthDecreasing = true;
        //}

        //public void ContinueDecreasingHealth(float delay)
        //{
        //    Invoke("ContinueDecreasingHealth", delay);
        //}

        //public void StartIncreasingHealth()
        //{
        //    isHealthDecreasing = false;
        //    isHealthIncreasing = true;
        //}

        //public void StartIncreasingHealth(float delay)
        //{
        //    Invoke("StartIncreasingHealth", delay);
        //}

        //public void StopIncreasingHealth()
        //{
        //    isHealthDecreasing = true;
        //    isHealthIncreasing = false;
        //}

        //public void StopIncreasingHealth(float delay)
        //{
        //    Invoke("StopIncreasingHealth", delay);
        //}
    }
}
