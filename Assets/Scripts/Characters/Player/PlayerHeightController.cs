using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class PlayerHeightController : MonoBehaviour
	{
		//min:1 max:6 length:5 maxHealth:6
		public Transform maskTrans;

		//mask y scale最高是5
		[SerializeField] private int heightLimit = 5;
		//最多六颗树叶
		private const int healthLimit = 6;

		[SerializeField] private int minFOV = 50;
		[SerializeField] private int maxFOV = 90;

		[SerializeField] private CinemachineVirtualCamera vcam;

		[SerializeField] private Transform leavesTrans;

		[SerializeField] private Transform colliderRootTrans;
		[Tooltip("Collider y方向上最大的伸缩")][SerializeField] private float maxColliderYScale = 3;
		//Collider y方向上最小的伸缩
		private const float minColliderYScale = 1;

		private void Update()
        {
			ChangeHeight();
			ChangeCameraFOV();
			ChangeLeavesHeight();
			ChangeColliderHeight();
		}

		private void ChangeColliderHeight()
        {
			float yScale = minColliderYScale + (maxColliderYScale - minColliderYScale) * HealthManager.Instance.GetCurrentHealth() / (healthLimit * HealthManager.Instance.GetMaxHealth());
			colliderRootTrans.localScale = new Vector3(1, yScale, 1);
        }

		private void ChangeLeavesHeight()
        {
			float yPos = heightLimit * HealthManager.Instance.GetCurrentHealth() / (healthLimit * HealthManager.Instance.GetMaxHealth());
			leavesTrans.localPosition = new Vector3(0, yPos, 0);
        }

        private void ChangeCameraFOV()
        {
			float fov = minFOV + (maxFOV - minFOV) * HealthManager.Instance.GetCurrentHealth() / (healthLimit * HealthManager.Instance.GetMaxHealth());
			vcam.m_Lens.FieldOfView = fov;
		}

        public void ChangeHeight()
        {
			//0-600 -> 0-5
			float yScale = heightLimit * HealthManager.Instance.GetCurrentHealth() / (healthLimit * HealthManager.Instance.GetMaxHealth());
			maskTrans.localScale = new Vector3(1, yScale, 1);
        }

		
	}
}
