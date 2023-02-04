using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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
                sortingGroups[i].sortingOrder = (int)(-10 * sortingGroups[i].transform.position.z);
            }
        }

        [Button("一键Sorting")]
        public void OneClickSorting()
        {
            GameObject[] environmentalObjectsToBeSorted = GameObject.FindGameObjectsWithTag("Environment");
            for (int i = 0; i < environmentalObjectsToBeSorted.Length; i++)
            {
                environmentalObjectsToBeSorted[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)(-10 * environmentalObjectsToBeSorted[i].transform.position.z);
            }

        }
    }
}
