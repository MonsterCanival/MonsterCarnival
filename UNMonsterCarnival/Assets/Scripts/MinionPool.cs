using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPool : MonoBehaviour {

    private List<GameObject> MinionList;
    public GameObject Minion;
    public int PoolSize;
    public int BurstSpawnAmount;

    private void Awake()
    {
        MinionList = new List<GameObject>();
        PoolSize = 100;
        BurstSpawnAmount = 50;
    }

    private void Start()
    {
        StartPool();
        BurstSpawn(BurstSpawnAmount);
        StartCoroutine("SpawnMinion");
    }


    public void BurstSpawn(int amount)
    {
        for(int i=0; i<amount; ++i)
        {
            ExecuteSpawnProcess();
        }
    }

    public IEnumerator SpawnMinion()
    {
        WaitForSeconds WFS = new WaitForSeconds(0.5f);
        while(MinionList.Count > 0)
        {
            ExecuteSpawnProcess();
            yield return WFS;
        }
    }

    public void ExecuteSpawnProcess()
    {
        GameObject TreatMinion = PopPool();
        if(TreatMinion != null)
        {
            TreatMinion.SetActive(true);
        }
    }
    public void StartPool()
    {
        for (int i = 0; i < PoolSize; ++i)
        {
            GameObject NewObject = Instantiate(Minion) as GameObject;
            NewObject.GetComponent<Minion>().Die();
        }
    }

    public GameObject PopPool()
    {
        if (MinionList.Count > 0) {
            GameObject poppedMinion = MinionList[0];
            MinionList.RemoveAt(0);
            return poppedMinion;
        }
        return null;
    }

    public void PushPool(GameObject pushedMinion)
    {
        MinionList.Add(pushedMinion);
        pushedMinion.SetActive(false);
    }
}
