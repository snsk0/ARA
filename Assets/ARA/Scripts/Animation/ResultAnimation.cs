using ARA.Game;
using ARA.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ARA.Animation
{
    public class ResultAnimation : MonoBehaviour, IGameAnimationPlayer
    {
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Animator _enemyAnimator;

        [SerializeField] private GridFloatView _playerGrid;
        [SerializeField] private GridFloatView _enemyGrid;

        [SerializeField] private TurnUI _turnUI;

        public async UniTask PlayAnimation(Vector2Int playerPosition, Vector2Int enemyPosition)
        {
            _playerAnimator.SetTrigger("Move");
            _turnUI.SetText("自身の行動");
            await _playerAnimator.transform.DOMove(_playerGrid.Transforms[playerPosition].position, 1.0f).SetEase(Ease.InOutQuart);

            _enemyAnimator.SetTrigger("Move");
            _turnUI.SetText("敵の行動");
            await _enemyAnimator.transform.DOMove(_enemyGrid.Transforms[enemyPosition].position, 1.0f).SetEase(Ease.InOutQuart);

            _turnUI.SetText("待機中");
        }
    }
}
