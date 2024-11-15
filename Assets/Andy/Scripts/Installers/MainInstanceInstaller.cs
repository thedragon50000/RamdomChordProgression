using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MainInstanceInstaller", menuName = "Installers/MainInstanceInstaller")]
public class MainInstanceInstaller : ScriptableObjectInstaller<MainInstanceInstaller>
{
    public MainInstaller.Setting testSetting;

    public override void InstallBindings()
    {
        // warning: so只能放腳本跟prefab，不能拉場景上(已實例化的)的物件進來
        // Container.BindInstance(chordParent).WithId("chordParent").AsSingle();

        Container.BindInstance(testSetting).IfNotBound();
    }
}