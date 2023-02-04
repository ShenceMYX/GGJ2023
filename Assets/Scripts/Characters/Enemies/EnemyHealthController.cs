using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ns
{
    /// <summary>
    ///
    /// </summary>
    public class EnemyHealthController : MonoBehaviour
    {
        public bool isRooting = false;

        private bool enterMoundRange = false;

        [SerializeField] private float healthChangeSpeed = 0;
        [SerializeField] private float healthOriginalSpeed = -20;
        [SerializeField] private float healthIncreaseSpeed = 20;
        [SerializeField] private float healthDecreaseSpeed = -20;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float currentHealth;
        [Tooltip("最大生命的占比，也是与最大高度的比例，如0.5就是最大高度的一半")][SerializeField] [Range(0, 1)] private float maxHealthRatio = 1;
        //根据占比计算后的实际的最大高度
        private float totalHealth;

        [Tooltip("敌人的伤害：每秒扣除玩家的血量")][SerializeField] private float enemyDamage = 40;
        //这对healthmanager设置一次扣除的血量
        private bool decreaseOnce = false;

        private TreeHeight treeHeight;

        [SerializeField] private MMF_Player deathFeedbacks;

        public bool isDeath { get; private set; } = false;

        public bool enterSoilArea = false;

        public bool playerEnterRange = false;

        public float soilIncreaseSpeed;

        [SerializeField] private Transform[] partsToBeGreyscaleArr;

        [SerializeField] private float damageInterval = 0.75f;
        private float startDamageTimer;

        [SerializeField] private MMF_Player hitFeedbacks;
        private SpriteRenderer[] allSpriteRenderers;
        [SerializeField] private float flickerEffectsInterval = 0.1f;

        private void Start()
        {
            totalHealth = maxHealth * maxHealthRatio;
            currentHealth = totalHealth;
            treeHeight = GetComponentInChildren<TreeHeight>();

            allSpriteRenderers = transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            if (isDeath || HealthManager.Instance.isDeath) return;

            //初始-20，每秒自动扣20
            healthChangeSpeed = healthOriginalSpeed;

            //如果扎根了，每秒增加土壤给予的增量
            if (isRooting)
            {
                if (!enterSoilArea)
                {
                    //如果扎根在普通的地上，土壤给予的增量为 抵消初始降低数值的数值
                    healthChangeSpeed += -healthChangeSpeed;
                }
                else
                    healthChangeSpeed += soilIncreaseSpeed;
            }

            //如果玩家进去范围了，且双方扎根了，根据高度判定谁扣血
            if (playerEnterRange && isRooting && PlayerInputController.Instance.isRooting)
            {
                //玩家攻击敌人
                if (PlayerInputController.Instance.treeHeight.height >= treeHeight.height)
                {
                    EnergyManager.Instance.IncreaseEnergy();
                    healthChangeSpeed -= PlayerInputController.Instance.playerDamage;

                    if(startDamageTimer < Time.time)
                    {
                        hitFeedbacks?.PlayFeedbacks();
                        StartCoroutine(FlickerEffects());
                        startDamageTimer = Time.time + damageInterval;
                    }
                }
                else
                {
                    if (!decreaseOnce)
                    {
                        HealthManager.Instance.enemyDamage = -enemyDamage;
                        //HealthManager.Instance.AddHealthChangeSpeed(-enemyDamage);
                        //玩家将处于被敌人扣血状态
                        decreaseOnce = true;
                    }
                }
            }

            if (decreaseOnce)
            {
                if (startDamageTimer < Time.time)
                {
                    HealthManager.Instance.PlayHitEffects();
                    startDamageTimer = Time.time + damageInterval;
                }
            }

            //玩家将处于被敌人扣血状态并且玩家离开敌人攻击范围了
            if (decreaseOnce && !playerEnterRange)
            {
                HealthManager.Instance.AddHealthChangeSpeed(enemyDamage);
                decreaseOnce = false;
                HealthManager.Instance.enemyDamage = 0;
            }

            currentHealth += healthChangeSpeed * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);

            //UpdateHealthUI();

            if (currentHealth <= 0)
                Death();
        }

        private IEnumerator FlickerEffects()
        {
            for (int i = 0; i < allSpriteRenderers.Length; i++)
            {
                allSpriteRenderers[i].material.EnableKeyword("HITEFFECT_ON");
            }
            yield return new WaitForSeconds(flickerEffectsInterval);
            for (int i = 0; i < allSpriteRenderers.Length; i++)
            {
                allSpriteRenderers[i].material.DisableKeyword("HITEFFECT_ON");
            }
        }

        private void Death()
        {
            deathFeedbacks?.PlayFeedbacks();
            if (partsToBeGreyscaleArr != null && partsToBeGreyscaleArr.Length != 0)
                EnableGreyscaleEffectsForTreeBase();
            isDeath = true;
        }

        private void EnableGreyscaleEffectsForTreeBase()
        {
            for (int i = 0; i < partsToBeGreyscaleArr.Length; i++)
            {
                partsToBeGreyscaleArr[i].GetComponent<Renderer>().material.EnableKeyword("GREYSCALE_ON");
            }
        }

        private void OnTriggerEnter(Collider other) {if (other.CompareTag("Player")) playerEnterRange = true;}

        private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) playerEnterRange = false; }

        public float GetCurrentHealth() { return currentHealth; }
        public float GetMaxHealth() { return maxHealth; }
    }
}
