namespace Member.LeeS.Code.Enums
{
    public enum NodeState
    {
        Locked,     // 아직 도달할 수 없는 노드
        Reachable,  // 현재 선택 가능한 노드
        Visited     // 이미 방문한 노드
    }
}