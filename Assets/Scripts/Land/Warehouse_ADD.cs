using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse_ADD : MonoBehaviour
{
    static List<RES_typ> res = new List<RES_typ>();
    static int totalResources = 100;


    public static void ADD_res(int count)
    {
        Land.state_buildings["warehouse"].all_res += count; // дл€ подсчета всех собранных ресурсов

        I();
        Distribute();
        Save_res();
    }


    static void I()
    {
        res = new List<RES_typ>();
        res.Add(new RES_typ("stones", 65));
        res.Add(new RES_typ("silicon", 15));
        res.Add(new RES_typ("coal", 10));
        res.Add(new RES_typ("granite", 5));
        res.Add(new RES_typ("quartz", 4));
        res.Add(new RES_typ("silver", 0.7f));
        res.Add(new RES_typ("gold", 0.2f));
        res.Add(new RES_typ("ruby", 0.05f));
        res.Add(new RES_typ("diamond", 0.01f));
    }

    public class RES_typ
    {
        public string name;
        public float probability;
        public int allocatedAmount;

        public RES_typ(string _name, float _probability)
        {
            name = _name;
            probability = _probability;
        }
    }



    static void Distribute()
    {
        // сумма веро€тностей
        float totalProbability = 0f;

        // —читаем сумму всех веро€тностей
        foreach (var material in res)
        {
            totalProbability += material.probability;
        }

        // –аспредел€ем ресурсы в соответствии с веро€тност€ми
        foreach (var material in res)
        {
            material.allocatedAmount = Mathf.RoundToInt((material.probability / totalProbability) * totalResources);
        }

        // ѕровер€ем, что общее количество распределЄнных ресурсов не превышает количество доступных
        int allocatedSum = 0;
        foreach (var material in res)
        {
            allocatedSum += material.allocatedAmount;
        }

        // ≈сли есть остатки, распредел€ем их
        int remainingResources = totalResources - allocatedSum;
        for (int i = 0; i < remainingResources; i++)
        {
            res[i % res.Count].allocatedAmount++;
        }
    }


    static void Save_res()
    {
        foreach (RES_typ r in res)
        {
            Debug.Log($"{r.name}: {r.allocatedAmount}");
            Land.state_res[r.name].count += r.allocatedAmount;
            Land.state_res[r.name].count_all += r.allocatedAmount;
        }
    }
}
