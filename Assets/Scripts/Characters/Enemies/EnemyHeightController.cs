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
	public class EnemyHeightController : MonoBehaviour
	{
		public Transform maskTrans;

		[SerializeField] private float heightMin = 3.24f;
		[SerializeField] private float heightMax = 8;

		[Space]
		[SerializeField] private Transform leavesTrans;
		[SerializeField] private float leavesYMin = 5;
		[SerializeField] private float leavesYMax = 5;

		[Space]
		[SerializeField] private Transform colliderRootTrans;
		[SerializeField] private float minColliderYScale = 1;
		[Tooltip("Collider y方向上最大的伸缩")] [SerializeField] private float maxColliderYScale = 3;

		private EnemyHealthController healthController;

        private void Start()
        {
			healthController = GetComponent<EnemyHealthController>();
        }

        private void Update()
		{
			ChangeHeight();
			ChangeLeavesHeight();
			ChangeColliderHeight();
		}

		private void ChangeColliderHeight()
		{
			float yScale = minColliderYScale + (maxColliderYScale - minColliderYScale) * healthController.GetCurrentHealth() / healthController.GetMaxHealth();
			colliderRootTrans.localScale = new Vector3(1, yScale, 1);
		}

		private void ChangeLeavesHeight()
		{
			float yPos = leavesYMin + (leavesYMax - leavesYMin) * healthController.GetCurrentHealth() / healthController.GetMaxHealth();
			leavesTrans.localPosition = new Vector3(0, yPos, 0);
		}

		public void ChangeHeight()
		{
			//0-600 -> 0-5
			float yScale = heightMin + (heightMax - heightMin) * healthController.GetCurrentHealth() / healthController.GetMaxHealth();
			maskTrans.localScale = new Vector3(1, yScale, 1);
		}


	}
}
