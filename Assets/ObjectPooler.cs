using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Attributes

    private const int defaultPoolSize = 20;

    public static bool expandWhenNecessary = true;

    public static Dictionary<string, List<GameObject>> 
        objectPools = new Dictionary<string, List<GameObject>>();

    #endregion

    #region Object Pool Checks

    private static bool PoolExistsForPrefab(string prefabPath)
    {
        return objectPools.ContainsKey(prefabPath);
    }

    private static bool IsAvailableForReuse(GameObject gameObject)
    {
        return !gameObject.activeSelf;
    }
    #endregion

    #region Object Pool Creation

    private static GameObject ExpandPoolAndGetObject (string prefabPath, List<GameObject> pool)
    {
        if (!expandWhenNecessary) return null;

        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        GameObject instance = Object.Instantiate(prefab) as GameObject;

        pool.Add(instance);

        return instance;
    }



    public static List<GameObject> CreateObjectPool(string prefabPath, int count)
    {
        if (count <= 0) count = 1;

        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        List<GameObject> objects = new List<GameObject>();

        for (int i=0; i<count;i++)
        {
            GameObject instance = Object.Instantiate<GameObject>(prefab);

            objects.Add(instance);

            instance.SetActive(false);
        }

        objectPools.Add(prefabPath, objects);

        return objects;
    }

    #endregion

    #region Object Retrieval

    public static GameObject GetPooledObject(string prefabPath, int poolSize= defaultPoolSize)
    {
        if(!PoolExistsForPrefab(prefabPath))
        {
            return CreateAndRetrieveFromPool(prefabPath, poolSize);
        }

        var pool = objectPools[prefabPath];

        GameObject instance;

        return (instance = FindFirstAvailablePooledObject(pool)) != null ?
            instance : ExpandPoolAndGetObject(prefabPath, pool);
    }
    
    private static GameObject CreateAndRetrieveFromPool(string prefabPath, int poolSize = defaultPoolSize)
    {
        CreateObjectPool(prefabPath, poolSize);
        
        return GetPooledObject(prefabPath);
    }

    private static GameObject FindFirstAvailablePooledObject(List<GameObject> pool)
    {
        for (int i=0; i<pool.Count;i++)
        {
            if(IsAvailableForReuse(pool[i]))
            {
                return pool[i];
            }
        }

        return null;
    }
    #endregion 
}
