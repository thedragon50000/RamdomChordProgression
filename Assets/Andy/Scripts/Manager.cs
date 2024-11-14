using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace Andy.Scripts
{
    public class Manager : MonoBehaviour
    {
        /// 12音階
        public readonly string[] TwelveScaleName = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private readonly ReactiveProperty<float> _bars = new();

        /// 幾小節(限定在4~8，再多再少沒啥意義)
        public Slider sliderBars;

        private void Start()
        {
            _bars.Subscribe(_ => { print(_ + "小節"); });
            sliderBars.onValueChanged.AddListener((v) => { _bars.Value = v; });

            _bars.Value = 4;
        }

        void CreateProgression(int bars)
        {
            List<ChordProperty_sc> chords = new();
        }
    }

    public enum EHarmonicFunction
    {
        T,  // 主和弦、家
        S,  // 副屬、橋
        D,  // 屬、外面
    }
}