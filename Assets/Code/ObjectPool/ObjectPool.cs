using UnityEngine;
using System.Collections.Generic;

/* INSTRUCTION:
 * 1. Create a serialized variable of ObjectPool type in the class that spawns objects.
 * 2. Create a prefab with PoolableObject script attached to it
 * 3. Drag the prefab (with PoolableObject components) onto the class (from step 1) in the inspector.
 */

public class ObjectPool
{
    PoolableObject prefab;

    //Cache
    Vector3 offscreen = new Vector3(-1000, -1000, -1000);

    //Pool lists
    List<PoolableObject> inactives = new List<PoolableObject>();
    public List<PoolableObject> actives { get; private set; } = new List<PoolableObject>();

    public ObjectPool(PoolableObject prefab)
    {
        this.prefab = prefab;
    }

    public PoolableObject Spawn(Vector3 pos)
    {
        Debug.Log("b inactives.count :" + inactives.Count);
        PoolableObject p = GetPrefab();
        p.Respawned(pos);
        return p;
    }

    public void DespawnAll ()
    {
        for (int i = actives.Count - 1; i >= 0; i--)
        {
            actives[i].Despawn();
        }
    }

    public void DirectDespawn(PoolableObject obj) //Without telling the despawned object
    {
        //Return to pool
        obj.transform.position = offscreen;
        inactives.Add(obj);
        actives.Remove(obj);
        obj.gameObject.SetActive(false);
    }

    PoolableObject GetPrefab()
    {
        PoolableObject p;
        if (inactives.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactives[0];

            Debug.Log("inactives.count :" + inactives.Count);
            p.gameObject.SetActive(true);
            inactives.RemoveAt(0);
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, offscreen, Quaternion.identity);
            p.Initialize_SetPool(this);
        }

        actives.Add(p);
        return p;
    }
}

/*
 

[System.Serializable]
public class ObjectPool
{
    [SerializeField] PoolableObject prefab;

    //Cache
    Vector3 offscreen = new Vector3(-1000, -1000, -1000);

    //Pool lists
    List<PoolableObject> inactives = new List<PoolableObject>();
    public List<PoolableObject> actives { get; private set; } = new List<PoolableObject>();

    public PoolableObject Spawn(Vector3 pos)
    {
        Debug.Log("b inactives.count :" + inactives.Count);
        PoolableObject p = GetPrefab();
        p.Respawned(pos);
        return p;
    }

    public void DespawnAll ()
    {
        for (int i = actives.Count - 1; i >= 0; i--)
        {
            actives[i].Despawn();
        }
    }

    public void DirectDespawn(PoolableObject obj) //Without telling the despawned object
    {
        //Return to pool
        obj.transform.position = offscreen;
        inactives.Add(obj);
        actives.Remove(obj);
        obj.gameObject.SetActive(false);
    }

    PoolableObject GetPrefab()
    {
        PoolableObject p;
        if (inactives.Count > 0)
        {
            //If object pool is not empty, then take an object from the pool and make it active
            p = inactives[0];

            Debug.Log("inactives.count :" + inactives.Count);
            p.gameObject.SetActive(true);
            inactives.RemoveAt(0);
        }
        else
        {
            //If object pool is empty, then spawn a new object.
            p = GameObject.Instantiate(prefab, offscreen, Quaternion.identity);
            p.Initialize_SetPool(this);
        }

        actives.Add(p);
        return p;
    }
}

 */

/*
 
    //Note: please use OverlapCircleAll, which is more efficient, than the following
    //... methods that loops through 
    public Transform ClosestUnitToLocation(Vector3 center, float sqrRange)
    {
        Transform closest = null;
        float shortestDist = sqrRange;
        foreach (GameObject go in active)
        {
            if (Vector3.SqrMagnitude(center - go.transform.position) < shortestDist)
            {
                closest = go.transform;
                shortestDist = Vector3.SqrMagnitude(center - go.transform.position);
            }
        }
        return closest;
    }

    public List<Transform> AllUnitsInRange (Vector3 center, float sqrRange)
    {
        List<Transform> neighbors = new List<Transform>();
        foreach (GameObject go in active)
        {
            if (Vector3.SqrMagnitude(center - go.transform.position) < sqrRange)
            {
                neighbors.Add(go.transform);
            }
        }
        return neighbors;
    }
 */