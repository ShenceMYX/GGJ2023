using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

namespace ns
{
	/// <summary>
	///
	/// </summary>
	public class GameManager : MonoSingleton<GameManager>
	{
		[SerializeField] private PlayerInputController playerInputController;
		[SerializeField] private PostProcessVolume gameoverVolume;
		[Tooltip("屏幕变灰效果延迟")] [SerializeField] private float greyOutEffectDelay = 1f;
		[Tooltip("屏幕变灰持续时间")] [SerializeField] private float greyOutEffectDuration = 3f;

		[SerializeField] private CanvasGroup gameoverUICanvasGroup;
		[SerializeField] private float gameoverUIFadeInDuration = 2f;

        public void GameOver()
        {
			playerInputController.canInput = false;
			DOTween.To((float pNewValue) => gameoverVolume.weight = pNewValue, 0, 1, greyOutEffectDuration).SetDelay(greyOutEffectDelay)
				.OnComplete(() => {
					gameoverVolume.weight = 1;
					DOTween.To((float pNewValue) => gameoverUICanvasGroup.alpha = pNewValue, 0, 1, gameoverUIFadeInDuration);
				});
		}

		public void Restart()
        {
			playerInputController.canInput = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}
