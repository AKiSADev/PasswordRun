using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    public List<GameObject> prefabList;
    public  Player player;

    private List<GameObject> istantiated;
    private int rand;
    private int i = 0;
    private GameObject lastGO;
    private Vector3 lastEndPosition;


    private void Start()
    {
        istantiated = new List<GameObject>();

        lastGO = Instantiate<GameObject>(prefabList.ElementAt(0), new Vector3(i, 0, 0), Quaternion.identity);

        istantiated.Add(lastGO);

        InstantiateNext();
    }

    private void Update()
    {
        if (Vector3.Distance(player.gameObject.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            InstantiateNext();

        DeistantiatePreviouses();
    }

    private void InstantiateNext()
    {
        rand = Random.Range(0, prefabList.Count);
        i = i + 10;
        lastGO = Instantiate<GameObject>(prefabList.ElementAt(rand), new Vector3(i, 0, 0), Quaternion.identity);
        lastEndPosition = lastGO.transform.Find("EndObject").position;
        istantiated.Add(lastGO);
    }

    private void DeistantiatePreviouses()
    {
        if (istantiated.Count > 25)
        {
            Destroy(istantiated[0]);
        }
    }



}
