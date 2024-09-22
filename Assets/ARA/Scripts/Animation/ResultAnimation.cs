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
        [SerializeField] private PlayerTileInputManager MoveSelectGrid;
        [SerializeField] private EnemyTileViewManager EnemySelectGrid;
        [SerializeField] private InputAnimator _inputAnimator;

        public async UniTask PlayAnimation(NetworkResult result)
        {
            if (result.IsPrecedence)
            {
                _playerAnimator.SetTrigger("Move");
                _turnUI.SetText("自身の行動");
                await _playerAnimator.transform.DOMove(_playerGrid.Transforms[result.PlayerPosition].position, 1.0f).SetEase(Ease.InOutQuart);
                MoveSelectGrid.UpdateView(result.PlayerPosition, result.PlayerInputablePositions);
                _inputAnimator.UnDisplayAnimationObject();

                _enemyAnimator.SetTrigger("Move");
                _turnUI.SetText("敵の行動");
                await _enemyAnimator.transform.DOMove(_enemyGrid.Transforms[result.EnemyPosition].position, 1.0f).SetEase(Ease.InOutQuart);
                EnemySelectGrid.UpdateView(result.EnemyPosition, result.EnemyInputablePositions);
            }
            else
            {
                _enemyAnimator.SetTrigger("Move");
                _turnUI.SetText("敵の行動");
                await _enemyAnimator.transform.DOMove(_enemyGrid.Transforms[result.EnemyPosition].position, 1.0f).SetEase(Ease.InOutQuart);
                EnemySelectGrid.UpdateView(result.EnemyPosition, result.EnemyInputablePositions);

                _playerAnimator.SetTrigger("Move");
                _turnUI.SetText("自身の行動");
                await _playerAnimator.transform.DOMove(_playerGrid.Transforms[result.PlayerPosition].position, 1.0f).SetEase(Ease.InOutQuart);
                MoveSelectGrid.UpdateView(result.PlayerPosition, result.PlayerInputablePositions);
                _inputAnimator.UnDisplayAnimationObject();
            }

            _turnUI.SetText("待機中");
        }
    }
}
