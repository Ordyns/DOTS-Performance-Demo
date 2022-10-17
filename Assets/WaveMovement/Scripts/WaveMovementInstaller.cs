using Unity.Entities;
using UnityEngine;
using Zenject;

namespace WaveMovement
{
    public class WaveMovementInstaller : MonoInstaller
    {
        public override void InstallBindings(){
            SignalBusInstaller.Install(Container);

            Container.BindInstance(World.DefaultGameObjectInjectionWorld);

            Container.Bind<WaveMovementSettingsViewModel>().FromNew().AsSingle();
            Container.BindSystem<WaveMovement.WaveMovementSystem>().AsSingle().NonLazy();
            
            Container.DeclareSignal<WaveMovementSettingsUpdatedSignal>();
        }
    }
}