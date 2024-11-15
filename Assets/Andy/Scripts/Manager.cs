using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Andy.Scripts
{
    public class Manager : MonoBehaviour
    {
        /// 12音階
        public readonly string[] TwelveScaleName = { "C", "C#", "D", "Db", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private readonly ReactiveProperty<float> _bars = new();
        public Transform parentGameObjectChord;
        [Inject] private ChordProperty_sc.Factory _chordFactory;


        /// 幾小節(限定在4~8，再多再少沒啥意義)
        public Slider sliderBars;

        private void Start()
        {
            _bars.Subscribe(_ => { print(_ + "小節"); });
            sliderBars.onValueChanged.AddListener(v => { _bars.Value = v; });
            // 預設4小節
            _bars.Value = 4;


            // for (int i = 0; i < 3; i++)
            // {
            //     ChordProperty_sc cp = _chordFactory.Create();
            //     cp.gameObject.transform.SetParent(parentGameObjectChord);
            // }
        }

        void CreateProgression(int bars)
        {
            List<ChordProperty_sc> chords = new();
        }
    }

    public enum EHarmonicFunction
    {
        T, // 主和弦、家
        S, // 副屬、橋
        D, // 屬、外面
    }
}