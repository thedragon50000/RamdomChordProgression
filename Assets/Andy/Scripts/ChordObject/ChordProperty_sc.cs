using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Andy.Scripts;
using TMPro;
using UnityEngine.Serialization;

public class ChordProperty_sc : MonoBehaviour, IPoolable<IMemoryPool>
{
    /// 12音階    Note: Ioneon 0,2,4,5,7,9,11
    public readonly string[] TwelveScaleName = { "C", "C#", "D", "Db", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    public readonly string[] ChordsType = { "Maj", "m", "m7", "Maj", "7", "m7", "ø" };

    public EHarmonicFunction eHarmonicFunction;
    public List<int> chordsHF = new();
    public TMP_Text text;

    IMemoryPool _pool;
    private readonly List<string> _temp = new();

    public void SetHF(EHarmonicFunction myHF)
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

        TurnNumberIntoChord(chordsHF);
    }

    private void TurnNumberIntoChord(List<int> numbers)
    {
        // print("TurnNumberIntoChord(); ");
        _temp.Clear();
        int[] realindex = { 0, 2, 4, 5, 7, 9, 11 };
        foreach (int index in numbers)
        {
            int k = realindex[index - 1];
            _temp.Add(TwelveScaleName[k] + ChordsType[index - 1]);
        }

        string s = string.Join(", ", _temp);
        text.text = string.Join("→ ", _temp) + "→ ";
    }

    T Picking<T>(List<T> list)
    {
        int i = Random.Range(0, list.Count);
        var foo = list[i];
        // print($"Pick up {list[i]}");
        list.RemoveAt(i);
        return foo;
    }

    void Start()
    {
        // print("Start() 有用嗎？");
    }

    public void RecycleSelf()
    {
        _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        // print("Recycle by Pool");
        _pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        // print("Create by factory");
        _pool = pool;
    }

    public class Factory : PlaceholderFactory<ChordProperty_sc>
    {
    }
}