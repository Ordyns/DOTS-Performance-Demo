using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

namespace WaveMovement
{
    public partial class WaveMovementSystem : SystemBase
    {
        protected override void OnUpdate(){
            if(WaveMovementSettings.Instance.UseJobs){
                var job = new WaveMovementJob() { DeltaTime = Time.DeltaTime };
                job.Schedule();
            }
            else{
                MoveCubes();
            }
        }

        private void MoveCubes(){
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref WaveMovementData waveMovementData, ref Translation translation) => {
                waveMovementData.Time += deltaTime;

                if(waveMovementData.Time > math.PI)
                    waveMovementData.Time = -math.PI;

                float a = waveMovementData.Time + (translation.Value.x + translation.Value.z) / 4;
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