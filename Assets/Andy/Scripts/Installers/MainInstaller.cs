using System;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public Transform chordParent;

    [Inject] Setting _setting = null;

    /// <summary>
    /// 在所有安裝程式完成之前呼叫 Inject/Resolve/Instantiate，是不好的做法！
    /// 修正建議： 將 [Inject] 移除，並在需要的地方用以下方式解析：
    /// var preChord = Container.Resolve<ChordProperty_sc>();
    /// </summary>
    /*[Inject]*/
    // ChordProperty_sc _preChord;
    public override void InstallBindings()
    {
        Container.BindFactory<ChordProperty_sc, ChordProperty_sc.Factory>()
            .FromPoolableMemoryPool<ChordProperty_sc, ChordPool>(poolBinder => poolBinder
                .WithInitialSize(8)
                // .FromComponentInNewPrefab(_preChord)
                .FromComponentInNewPrefab(_setting.preChord)
                .UnderTransform(chordParent));
    }

    // Note: 物件池放在Installer最直觀
    private class ChordPool : MonoPoolableMemoryPool<IMemoryPool, ChordProperty_sc>
    {
    }

    // Note: 等於把資源管理外包給SOInstaller
    [Serializable]
    public class Setting
    {
        public GameObject preChord;
    }
}