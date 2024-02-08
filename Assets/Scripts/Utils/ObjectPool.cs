using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    private static Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    public static GameObject Get(string name)
    {
        if (!pool.TryGetValue(name, out List<GameObject> list) || list.Count == 0)
        {
            Object prefab = Resources.Load(name);
            return (GameObject)GameObject.Instantiate(prefab);
        }
        GameObject p = list[0];
        list.RemoveAt(0);
        p.SetActive(true);
        return p;
    }

    public static void Release(string name, GameObject g)
    {
        List<GameObject> list;
        if (!pool.TryGetValue(name, out list))
        {
            list = new List<GameObject>();
            pool.Add(name, list);
        }

        list.Add(g);
    }

}

