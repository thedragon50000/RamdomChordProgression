using System;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public Transform chordParent;

    [Inject] Setting _setting = null;

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

    // Note: 等於把資源管理外包給SOInstaller，可以在這裡放各種設定參數和prefab之類的
    [Serializable]
    public class Setting
    {
        public GameObject preChord;
    }
}