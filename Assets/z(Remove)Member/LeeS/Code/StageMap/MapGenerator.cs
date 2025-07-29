using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Member.LeeS.Code.Enums;

namespace Member.LeeS.Code.StageMap
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Layout Settings")]
        [SerializeField] private int _numberOfLayers = 8;
        [SerializeField] private int _minNodesPerLayer = 2;
        [SerializeField] private int _maxNodesPerLayer = 4;
        [SerializeField] private float _horizontalSpacing = 4f;
        [SerializeField] private float _verticalSpacing = 3f;
        [SerializeField] private float _nodeRandomness = 0.7f;

        [Header("Connection Settings")]
        [Tooltip("두 번째 경로가 생성될 확률 (0.0 ~ 1.0)")]
        [Range(0f, 1f)]
        [SerializeField] private float _secondConnectionChance = 0.25f;

        [Header("Stage Type Settings")]
        [Tooltip("이벤트와 전투 노드의 등장 비율 (Event/Combat)")]
        [SerializeField] private float _eventToCombatRatio = 1.5f;

        [Header("Prefabs")]
        [SerializeField] private GameObject _nodePrefab;
        [SerializeField] private LineRenderer _linePrefab;

        private readonly List<List<StageNode>> _mapLayers = new List<List<StageNode>>();
        private StageNode _currentNode;

        private void Start()
        {
            if (_nodePrefab == null || _linePrefab == null)
            {
                Debug.LogError("Node Prefab 또는 Line Prefab이 할당되지 않았습니다!");
                return;
            }
            GenerateMap();
        }

        private void GenerateMap()
        {
            CreateNodes();
            ConnectNodes();
            SetInitialState();
        }

        public void SelectNode(StageNode selectedNode)
        {
            if (selectedNode.CurrentState != NodeState.Reachable)
            {
                Debug.LogWarning($"{selectedNode.name} is not reachable.");
                return;
            }

            if (_currentNode != null)
            {
                _currentNode.SetState(NodeState.Visited);
            }

            _currentNode = selectedNode;
            _currentNode.SetState(NodeState.Visited);

            Debug.Log($"Player moved to {selectedNode.name} ({selectedNode.StageType})");

            UpdateAllNodeStates();
        }

        private void SetInitialState()
        {
            _currentNode = _mapLayers[0][0];
            UpdateAllNodeStates();
        }

        private void UpdateAllNodeStates()
        {
            foreach (var layer in _mapLayers)
            {
                foreach (var node in layer)
                {
                    if (node.CurrentState != NodeState.Visited)
                    {
                        node.SetState(NodeState.Locked);
                    }
                }
            }

            if (_currentNode != null)
            {
                foreach (var reachableNode in _currentNode.sellectableStages)
                {
                    reachableNode.SetState(NodeState.Reachable);
                }
            }
        }

        private void CreateNodes()
        {
            _mapLayers.Clear();
            var startLayer = new List<StageNode>();
            StageNode startNode = CreateNode(0, 0, 1, StageType.Event1);
            startLayer.Add(startNode);
            _mapLayers.Add(startLayer);

            for (int i = 1; i < _numberOfLayers - 1; i++)
            {
                int nodesInLayer = Random.Range(_minNodesPerLayer, _maxNodesPerLayer + 1);
                var newLayer = new List<StageNode>();
                for (int j = 0; j < nodesInLayer; j++)
                {
                    StageType type = GetRandomStageType();
                    StageNode node = CreateNode(i, j, nodesInLayer, type);
                    newLayer.Add(node);
                }
                _mapLayers.Add(newLayer);
            }

            var bossLayer = new List<StageNode>();
            StageNode bossNode = CreateNode(_numberOfLayers - 1, 0, 1, StageType.Boss);
            bossLayer.Add(bossNode);
            _mapLayers.Add(bossLayer);
        }

        private StageNode CreateNode(int layerIndex, int nodeIndex, int totalNodesInLayer, StageType type)
        {
            float x = layerIndex * _horizontalSpacing;
            float y = nodeIndex * _verticalSpacing - (totalNodesInLayer - 1) * _verticalSpacing / 2f;
            x += Random.Range(-_nodeRandomness, _nodeRandomness);
            y += Random.Range(-_nodeRandomness, _nodeRandomness);

            GameObject nodeGO = Instantiate(_nodePrefab, new Vector3(x, y, 0), Quaternion.identity, this.transform);
            nodeGO.name = $"Node_{layerIndex}_{nodeIndex}";
            
            StageNode node = nodeGO.GetComponent<StageNode>();
            node.Initialize(type);
            return node;
        }

        private void ConnectNodes()
        {
            for (int i = 0; i < _mapLayers.Count - 1; i++)
            {
                foreach (StageNode fromNode in _mapLayers[i])
                {
                    var candidates = _mapLayers[i + 1]
                        .OrderBy(toNode => Vector3.Distance(fromNode.transform.position, toNode.transform.position))
                        .ToList();

                    if (candidates.Count > 0)
                    {
                        StageNode closestNode = candidates[0];
                        fromNode.sellectableStages.Add(closestNode);
                        DrawConnection(fromNode, closestNode);

                        if (candidates.Count > 1 && Random.value < _secondConnectionChance)
                        {
                            StageNode secondClosestNode = candidates[1];
                            fromNode.sellectableStages.Add(secondClosestNode);
                            DrawConnection(fromNode, secondClosestNode);
                        }
                    }
                }
            }
            EnsureAllNodesAreReachable();
        }

        private void EnsureAllNodesAreReachable()
        {
            for (int i = 1; i < _mapLayers.Count; i++)
            {
                foreach (StageNode toNode in _mapLayers[i])
                {
                    bool isConnected = _mapLayers[i - 1].Any(fromNode => fromNode.sellectableStages.Contains(toNode));
                    if (!isConnected)
                    {
                        StageNode closestFromNode = _mapLayers[i - 1]
                            .OrderBy(fromNode => Vector3.Distance(fromNode.transform.position, toNode.transform.position))
                            .First();
                        
                        closestFromNode.sellectableStages.Add(toNode);
                        DrawConnection(closestFromNode, toNode);
                    }
                }
            }
        }

        private void DrawConnection(StageNode from, StageNode to)
        {
            LineRenderer line = Instantiate(_linePrefab, transform);
            line.positionCount = 2;
            line.SetPosition(0, from.transform.position);
            line.SetPosition(1, to.transform.position);
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
        }

        private StageType GetRandomStageType()
        {
            float rand = Random.value;
            float combatChance = 1 / (1 + _eventToCombatRatio);

            if (rand < combatChance)
            {
                return StageType.Combat;
            }
            else
            {
                return (StageType)Random.Range((int)StageType.Event1, (int)StageType.Event6 + 1);
            }
        }
    }
}

