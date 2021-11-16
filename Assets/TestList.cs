using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestList : MonoBehaviour
{
    private LList<int> intList;

    private void Start()
    {
        intList = new LList<int>();
        intList.AddLast(0);
        intList.AddLast(1);
        intList.AddLast(2);
        intList.AddLast(3);
        intList.AddLast(4);
        intList.AddLast(5);
        intList.AddLast(6);
        intList.AddLast(7);
        
        intList.RemoveTailUntil(4);
        PrintList(intList);
    }

    private void PrintList(LList<int> linkedList)
    {
        for (int i = 0; i < linkedList.Count; i++)
        {
            Debug.Log($"{i}: {linkedList[i]}");
        }
    }
}
