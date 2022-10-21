using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public MapManager LoadMap;
    public string maptoload;

    private void Start()
    {
        LoadMap.LoadMap(maptoload);
    }

    private void Update()
    {

    }

}
