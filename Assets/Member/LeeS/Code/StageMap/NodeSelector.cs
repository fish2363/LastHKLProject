using UnityEngine;

namespace Member.LeeS.Code.StageMap
{
    public class NodeSelector : MonoBehaviour
    {
        private Camera _mainCamera;
        [SerializeField] private MapGenerator _mapGenerator;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_mapGenerator == null) return;

                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hit))
                {                    
                    if (hit.collider.TryGetComponent<StageNode>(out var node))
                    {
                        // MapGenerator에게 노드 선택을 위임합니다.
                        _mapGenerator.SelectNode(node);
                    }
                }
            }
        }
    }
}
