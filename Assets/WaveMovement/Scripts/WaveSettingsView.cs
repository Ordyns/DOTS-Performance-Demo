using UnityEngine;
using TMPro;

namespace WaveMovement
{
    public class WaveSettingsView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField speedInputField;
    
        private WaveMovementSettingsViewModel _viewModel;
        private WaveMovement.WaveMovementSettings _currentSettings = new WaveMovementSettings() { Speed = 1 };
    
        [Zenject.Inject]
        private void Initialize(WaveMovementSettingsViewModel settingsViewModel){
            _viewModel = settingsViewModel;
    
            speedInputField.onValueChanged.AddListener(OnInputFieldsValueChanged);

            OnInputFieldsValueChanged(string.Empty);
        }
    
        private void OnInputFieldsValueChanged(string value){
            _currentSettings.Speed = ParseNumberInput(speedInputField.text);
    
            _viewModel.SetNewSettings(_currentSettings);
        }
    
        private float ParseNumberInput(string input){
            float.TryParse(input, out float floatInput);
            return floatInput;
        }
    }
}
