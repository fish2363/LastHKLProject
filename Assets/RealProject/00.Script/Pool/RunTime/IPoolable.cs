using UnityEngine;

public interface IPoolable
{
    public PoolingItemSO PoolType { get; }
    public GameObject GameObject { get; }
    public void SetUpPool(Pool pool);
    public void ResetItem();
}
