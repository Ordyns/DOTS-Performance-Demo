namespace WaveMovement
{
    public class WaveMovementSettingsViewModel
    {
        private Zenject.SignalBus _signalBus;

        public WaveMovementSettingsViewModel(Zenject.SignalBus signalBus){
            _signalBus = signalBus;    
        }

        public void SetNewSettings(WaveMovement.WaveMovementSettings settings){
            _signalBus.Fire(new WaveMovementSettingsUpdatedSignal(settings));
        }
    }
}
