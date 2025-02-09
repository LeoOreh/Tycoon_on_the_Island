using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse_ADD : MonoBehaviour
{
    public static List<RES_typ> res = new List<RES_typ>();
    static int totalResources = 100;


    public static void ADD_res(int count)
    {
        Land.state_buildings["warehouse"].all_res += count; // ��� �������� ���� ��������� ��������

        I();
        Distribute();
        Save_res();
    }


    public static void I()
    {
        res = new List<RES_typ>();
        res.Add(new RES_typ("stones", 65, 1));
        res.Add(new RES_typ("silicon", 15, 2));
        res.Add(new RES_typ("coal", 10, 3));
        res.Add(new RES_typ("granite", 5, 4));
        res.Add(new RES_typ("quartz", 4, 12));
        res.Add(new RES_typ("silver", 0.7f, 23));
        res.Add(new RES_typ("gold", 0.2f, 160));
        res.Add(new RES_typ("ruby", 0.05f, 340));
        res.Add(new RES_typ("diamond", 0.01f, 900));
    }

    public class RES_typ
    {
        public string name;
        public float probability;
        public int allocatedAmount;
        public int price;


        public RES_typ(string _name, float _probability, int _price)
        {
            name = _name;
            probability = _probability;
            price = _price;
        }
    }



    static void Distribute()
    {
        // ����� ������������
        float totalProbability = 0f;

        // ������� ����� ���� ������������
        foreach (var material in res)
        {
            totalProbability += material.probability;
        }

        // ������������ ������� � ������������ � �������������
        foreach (var material in res)
        {
            material.allocatedAmount = Mathf.RoundToInt((material.probability / totalProbability) * totalResources);
        }

        // ���������, ��� ����� ���������� ������������� �������� �� ��������� ���������� ���������
        int allocatedSum = 0;
        foreach (var material in res)
        {
            allocatedSum += material.allocatedAmount;
        }

        // ���� ���� �������, ������������ ��
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
