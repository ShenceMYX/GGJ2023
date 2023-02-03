using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class SpriteSortingManager : MonoBehaviour
	{
		private SortingGroup[] sortingGroups;

		//private int[] initialOrders;

        private void Start()
        {
            sortingGroups = FindObjectsOfType<SortingGroup>();
			//initialOrders = new int[spriteRenderers.Length];
			//for (int i = 0; i < spriteRenderers.Length; i++)
   //         {
			//	initialOrders[i] = spriteRenderers[i].sortingOrder;
   //         }
        }

        private void Update()
        {
            for (int i = 0; i < sortingGroups.Length; i++)
            {
                sortingGroups[i].sortingOrder = (int)(-100 * sortingGroups[i].transform.position.z);
            }
        }
    }
}
