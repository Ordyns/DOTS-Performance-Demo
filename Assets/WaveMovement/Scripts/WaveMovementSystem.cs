using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

namespace WaveMovement
{
    [DisableAutoCreation]
    public partial class WaveMovementSystem : SystemBase
    {
        private Zenject.SignalBus _signalBus;

        private WaveMovementSettings _settings;

        [Zenject.Inject]
        private void Initialize(Zenject.SignalBus signalBus){
            _signalBus = signalBus;
            _signalBus.Subscribe<WaveMovementSettingsUpdatedSignal>(SettingsUpdated);
        }

        private void SettingsUpdated(WaveMovementSettingsUpdatedSignal signal){
            _settings = signal.WaveMovementSettings;
        }

        protected override void OnUpdate(){
            if(_settings.UseJobs){
                var job = new WaveMovementJob() { DeltaTime = Time.DeltaTime };
                job.Schedule();
            }
            else{
                MoveCubes();
            }
        }

        private void MoveCubes(){
            float deltaTime = Time.DeltaTime;
            var settings = _settings;

            Entities.ForEach((ref WaveMovementData waveMovementData, ref Translation translation) => {
                waveMovementData.Time += deltaTime;

                if(waveMovementData.Time > math.PI)
                    waveMovementData.Time = -math.PI;

                float a = waveMovementData.Time * settings.Speed + (translation.Value.x + translation.Value.z) / 4;
                translation.Value.y = math.sin(a);
            }).Run();
        }

        [BurstCompile]
        private partial struct WaveMovementJob : IJobEntity
        {
            public float DeltaTime;

            public void Execute(ref Translation translation, ref WaveMovementData waveMovementData){
                waveMovementData.Time += DeltaTime;

                if(waveMovementData.Time > math.PI)
                    waveMovementData.Time = -math.PI;

                float a = waveMovementData.Time + (translation.Value.x + translation.Value.z) / 4;
                translation.Value.y = math.sin(a);
            }
        }
    }
}