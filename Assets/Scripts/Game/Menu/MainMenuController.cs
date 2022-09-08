using DG.Tweening;
using ProjectCard.Game.Managers;
using UnityEngine;

namespace ProjectCard.Game.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private RectTransform playButton = null;
        [SerializeField] private RectTransform quitButton = null;

        public void HideMenuElements()
        {
            var currentPlayPos = playButton.anchoredPosition;
            var targetPlayPos = currentPlayPos - Vector2.right * -2000;
            var currentQuitPos = quitButton.anchoredPosition;
            var targetQuitPos = currentQuitPos - Vector2.right * 2000;

            var currentAnimationAlpha = 0F;
            
            DOTween.To(() => currentAnimationAlpha, x => currentAnimationAlpha = x, 1F, 3F)
                .OnUpdate(() =>
                {
                    playButton.anchoredPosition = Vector2.Lerp(currentPlayPos, targetPlayPos, currentAnimationAlpha);
                    quitButton.anchoredPosition = Vector2.Lerp(currentQuitPos, targetQuitPos, currentAnimationAlpha);

                }).SetEase(Ease.OutExpo).OnComplete(()=> LevelManager.Instance.LoadGameScene());
            
        }
    }
}