using ARA.Presenter;
using System.Collections.Generic;
using UnityEngine;

namespace ARA.Animation
{
    public class GridFloatView : MonoBehaviour, ITilePositionFloatView
    {
        [SerializeField]
        private GameObject _rootObject;

        [SerializeField]
        private float _distance;

        [SerializeField]
        private bool _isMirror;

        private Dictionary<Vector2Int, Transform> _transforms;
        public IReadOnlyDictionary<Vector2Int, Transform> Transforms => _transforms;

        private void Awake()
        {
            _transforms = new Dictionary<Vector2Int, Transform>();
        }

        public void Initialize(Vector2Int gridSize)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                for(int x = 0; x < gridSize.x; x++)
                {
                    Transform newTransform = new GameObject("Grid").transform;
                    newTransform.SetParent(_rootObject.transform);

                    if (_isMirror)
                    {
                        newTransform.position = _rootObject.transform.position - new Vector3(x, 0, y) * _distance;
                    }
                    else
                    {
                        newTransform.position = _rootObject.transform.position + new Vector3(x, 0, y) * _distance;
                    }
                    _transforms.Add(new Vector2Int(x, y), newTransform);
                }
            }
        }
    }
}
