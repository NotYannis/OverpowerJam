using System.Collections.Generic;
using UnityEngine;

public class ObjectPooled : MonoBehaviour
{

	public static ObjectPooled current;
	public GameObject pooledObject;
	public int pooledAmount = 20;
	public bool willGrow = true;

	List<GameObject> pooledObjects;

	private void Awake ()
	{
		current = this;
	}

	// Use this for initialization
	void Start ()
	{
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < pooledAmount; ++i)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject, transform);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject ()
	{
		for (int i = 0; i < pooledObjects.Count; ++i)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				pooledObjects[i].SetActive(true);
				return pooledObjects[i];
			}
		}

		if (willGrow)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject, transform);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}
}