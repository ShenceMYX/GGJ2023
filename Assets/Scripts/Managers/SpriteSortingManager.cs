using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class SpriteSortingManager : MonoBehaviour
	{
		private SpriteRenderer[] spriteRenderers;

		private int[] initialOrders;

        private void Start()
        {
			spriteRenderers = FindObjectsOfType<SpriteRenderer>();
			initialOrders = new int[spriteRenderers.Length];
			for (int i = 0; i < spriteRenderers.Length; i++)
            {
				initialOrders[i] = spriteRenderers[i].sortingOrder;
            }
        }

        private void Update()
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].sortingOrder = (int)(-100 * spriteRenderers[i].transform.position.z + initialOrders[i]);
            }
        }
    }
}
