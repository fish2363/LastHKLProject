using System.Collections.Generic;
using UnityEngine;
using Member.LeeS.Code.Enums;

namespace Member.LeeS.Code.StageMap
{
    public class StageNode : MonoBehaviour
    {
        [field: SerializeField] public StageType StageType { get; private set; }
        public List<StageNode> sellectableStages = new List<StageNode>();
        public NodeState CurrentState { get; private set; }

        private MeshRenderer _meshRenderer;
        private Color _baseColor;

        private void Awake()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            CurrentState = NodeState.Locked; // 기본 상태는 잠김
        }

        public void Initialize(StageType newType)
        {
            this.StageType = newType;
            switch (StageType)
            {
                case StageType.Combat: _baseColor = new Color(0.8f, 0.2f, 0.2f); break;
                case StageType.Boss: _baseColor = Color.black; transform.localScale = Vector3.one * 1.5f; break;
                default: _baseColor = new Color(0.9f, 0.8f, 0.3f); break;
            }
            SetState(NodeState.Locked);
        }

        public void SetState(NodeState newState)
        {
            CurrentState = newState;
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            // 상태에 따라 머티리얼 색상 변경
            switch (CurrentState)
            {
                case NodeState.Locked:
                    _meshRenderer.material.color = _baseColor * 0.4f; // 어둡게
                    break;
                case NodeState.Reachable:
                    _meshRenderer.material.color = _baseColor * 1.5f; // 밝게 (강조)
                    break;
                case NodeState.Visited:
                    _meshRenderer.material.color = Color.gray; // 회색
                    break;
            }
        }

        // 이제 NodeSelector가 MapGenerator를 통해 로직을 처리하므로 이 함수는 직접 사용되지 않습니다.
        public void OnNodeClicked()
        {
            Debug.Log($"Clicked on a {StageType} stage, but logic is now in MapGenerator.");
        }
    }
}

