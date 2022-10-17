namespace WaveMovement
{
    public struct WaveMovementSettingsUpdatedSignal
    {
        public WaveMovementSettingsUpdatedSignal(WaveMovementSettings settings){
            WaveMovementSettings = settings;
        }
    
        public WaveMovementSettings WaveMovementSettings;
    }
}
    