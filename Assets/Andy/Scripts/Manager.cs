using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Andy.Scripts
{
    public class Manager : MonoBehaviour
    {
        /// 12音階
        public readonly string[] TwelveScaleName = { "C", "C#", "D", "Db", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private readonly ReactiveProperty<int> _bars = new();

        [Inject] private ChordProperty_sc.Factory _chordFactory;
        [Inject(Id = "chordParent")] private Transform _chordParent;


        /// 幾小節(限定在4~8，再多再少沒啥意義)
        public Slider sliderBars;

        public Button btnCreate;

        private void Start()
        {
            _bars.Subscribe(_ => { print(_ + "小節"); });
            sliderBars.onValueChanged.AddListener(v => { _bars.Value = (int)v; });
            btnCreate.OnClickAsObservable().Subscribe(_ => { RandomTsdProgression(_bars.Value); });
            // 預設4小節
            _bars.Value = 4;

            // for (int i = 0; i < 3; i++)
            // {
            //     _chordFactory.Create();
            // }
        }

        void CreateProgression(List<EHarmonicFunction> tsd)
        {
            // 清空
            List<ChordProperty_sc> chords = new();
            ChordProperty_sc[] all = _chordParent.GetComponentsInChildren<ChordProperty_sc>();
            foreach (var prefab in all)
            {
                prefab.RecycleSelf();
            }

            for (int i = 0; i < _bars.Value; i++)
            {
                chords.Add(_chordFactory.Create());
            }

        }

        void RandomTsdProgression(int bars)
        {
            List<EHarmonicFunction> tsd = new();
            for (int i = 0; i < bars; i++)
            {
                // 第一個不用判斷
                var temp = (EHarmonicFunction)Random.Range(0, 3);
                if (i > 0)
                {
                    //判斷是否可接?
                    if (tsd[i - 1] == EHarmonicFunction.D && temp == EHarmonicFunction.S) //屬不可接副屬
                    {
                        i -= 1;
                        continue;
                    }

                    if (tsd[i - 1] == temp) //一樣的不要互接
                    {
                        i -= 1;
                        continue;
                    }
                }

                tsd.Add(temp);
            }

            print(string.Join(", ", tsd));

            CreateProgression(tsd);
        }
    }

    public enum EHarmonicFunction
    {
        T, // 主和弦、家
        S, // 副屬、橋
        D, // 屬、外面
    }
}