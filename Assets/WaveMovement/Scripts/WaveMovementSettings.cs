using UnityEngine;

namespace WaveMovement
{
    public class WaveMovementSettings : MonoBehaviour
    {
        public static WaveMovementSettings Instance { get; private set; }
    
        [field:SerializeField] public bool UseJobs { get; private set; }
    
        private void Awake() {
            if(Instance != null)
                throw new System.NotSupportedException();
    
            Instance = this;
        }
    }
}
