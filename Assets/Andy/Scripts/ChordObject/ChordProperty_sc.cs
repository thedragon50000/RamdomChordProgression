using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Andy.Scripts;
using UnityEngine.Serialization;

public class ChordProperty_sc : MonoBehaviour, IPoolable<IMemoryPool>
{
    public EHarmonicFunction eHarmonicFunction;
    public List<int> chordsHF = new();

    IMemoryPool _pool;

    void Init(EHarmonicFunction lastHF)
    {
        switch (lastHF)
        {
            case EHarmonicFunction.D:
                eHarmonicFunction = EHarmonicFunction.T;
                break;
            case EHarmonicFunction.S:
                eHarmonicFunction = Random.Range(0, 2) == 0 ? EHarmonicFunction.T : EHarmonicFunction.D;
                break;
            case EHarmonicFunction.T:
                eHarmonicFunction = (EHarmonicFunction)Random.Range(1, 3); //Note: T = 0，所以才能這樣寫
                break;
        }

        SetHF(eHarmonicFunction);
    }

    private void SetHF(EHarmonicFunction myHF)
    {
        List<int> temp = new();
        List<int> tempList;
        switch (myHF)
        {
            case EHarmonicFunction.D:
                temp = Random.Range(0, 2) == 0 ? new() { 5, 7 } : new() { 5 };
                break;
            case EHarmonicFunction.S:
                // 2,4
                tempList = new() { 2, 4 };

                //決定長度
                int hfLength = Random.Range(0, tempList.Count) + 1;

                for (int i = 0; i < hfLength; i++)
                {
                    temp.Add(Picking(tempList));
                }

                break;

            case EHarmonicFunction.T:
                tempList = new() { 1, 3, 6 };
                int hfLength1 = Random.Range(0, tempList.Count) + 1;
                for (int i = 0; i < hfLength1; i++)
                {
                    temp.Add(Picking(tempList));
                }

                break;
        }

        chordsHF = temp;
    }

    T Picking<T>(List<T> list)
    {
        int i = Random.Range(0, list.Count);
        var foo = list[i];
        print($"Pick up {list[i]}");
        list.RemoveAt(i);
        return foo;
    }

    void Start()
    {
        print("Start() 有用嗎？");
    }

    public void RecycleSelf()
    {
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        print("Recycle by Pool");
        _pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        print("Create by factory");
        _pool = pool;
    }

    public class Factory : PlaceholderFactory<ChordProperty_sc>
    {
    }
}