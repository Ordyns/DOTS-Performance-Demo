using Unity.Entities;

namespace WaveMovement
{
    [GenerateAuthoringComponent]
    public struct WaveMovementData : IComponentData
    {
        public float Time;
    }
}
