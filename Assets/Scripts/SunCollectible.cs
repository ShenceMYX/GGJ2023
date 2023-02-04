using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class SunCollectible : MonoBehaviour
	{
		[SerializeField] private MMF_Player collectFeedbacks;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (EnergyManager.Instance.energyFull)
                {
                    collectFeedbacks.gameObject.transform.parent = null;
                    collectFeedbacks?.PlayFeedbacks();
                    Destroy(gameObject, 0.2f);
                }
            }
        }
    }
}
