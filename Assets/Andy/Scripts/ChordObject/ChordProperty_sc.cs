using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using Andy.Scripts;

public class ChordProperty_sc : MonoBehaviour
{
    public EHarmonicFunction eHarmonicFunction;

    void Init(int index)
    {
        eHarmonicFunction = (EHarmonicFunction)index;
    }

    void Start()
    {
        print("Create by factory");
    }


    public class Factory : PlaceholderFactory<ChordProperty_sc>
    {
    }
}