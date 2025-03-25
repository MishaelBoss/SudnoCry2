using DG.Tweening;
using UnityEngine;

namespace ClickAnimationDOTween {
    public class ClicerAnimDOTween : MonoBehaviour
    {
        public float DOScale = 0.3f;
        public float DORotate = 0.3f;
        public float RestartTime = 0.2f;

        public void Click()
        {
            /*        DOTween.Sequence()
                        .Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), time))
                        .SetEase(Ease.Linear)
                        .AppendInterval(0.1f)
                        .Append(transform.DORotate(new Vector3(0, 0, 5f), time))
                        .AppendInterval(RestartTime)
                        .AppendCallback(Restart);*/
            transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), DOScale);
            transform.DORotate(new Vector3(0, 0, 5f), DORotate);
            Invoke("Restart", RestartTime);
        }

        private void Restart()
        {
            transform.DOScale(1f, 1f);
            transform.DORotate(new Vector3(0, 0, 0f), 0.5f);
        }
    }

}