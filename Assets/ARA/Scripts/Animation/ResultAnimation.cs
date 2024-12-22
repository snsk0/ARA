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
        [SerializeField] private PlayerCardInputManager PlayerCardSelectView;
        [SerializeField] private PlayerCardInputManager EnemyCardSelectView;
        [SerializeField] private InputAnimator _inputAnimator;

        public async UniTask PlayAnimation(NetworkResult playerResult, NetworkResult enemyResult)
        {
            if (playerResult.IsFormer)
            {
                _playerAnimator.SetTrigger("Move");
                _turnUI.SetText("自身の行動");
                await _playerAnimator.transform.DOMove(_playerGrid.Transforms[playerResult.Position].position, 1.0f).SetEase(Ease.InOutQuart);
                MoveSelectGrid.UpdateView(playerResult.Position, playerResult.MovablePositions);
                _inputAnimator.UnDisplayAnimationObject();

                _enemyAnimator.SetTrigger("Move");
                _turnUI.SetText("敵の行動");
                await _enemyAnimator.transform.DOMove(_enemyGrid.Transforms[enemyResult.Position].position, 1.0f).SetEase(Ease.InOutQuart);
                EnemySelectGrid.UpdateView(enemyResult.Position, enemyResult.MovablePositions);
            }
            else
            {
                _enemyAnimator.SetTrigger("Move");
                _turnUI.SetText("敵の行動");
                await _enemyAnimator.transform.DOMove(_enemyGrid.Transforms[enemyResult.Position].position, 1.0f).SetEase(Ease.InOutQuart);
                EnemySelectGrid.UpdateView(enemyResult.Position, enemyResult.MovablePositions);

                _playerAnimator.SetTrigger("Move");
                _turnUI.SetText("自身の行動");
                await _playerAnimator.transform.DOMove(_playerGrid.Transforms[playerResult.Position].position, 1.0f).SetEase(Ease.InOutQuart);
                MoveSelectGrid.UpdateView(playerResult.Position, playerResult.MovablePositions);
                _inputAnimator.UnDisplayAnimationObject();
            }

            PlayerCardSelectView.SetDeckList(playerResult.UsableCardIds);
            EnemyCardSelectView.SetDeckList(enemyResult.UsableCardIds);

            _turnUI.SetText("待機中");
        }
    }
}
