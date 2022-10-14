using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

namespace WaveMovement
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private int xCount = 10;
        [SerializeField] private int zCount = 10;
        [Space]
        [SerializeField] private Vector3 spacing = new Vector3(1, 0, 1);
        [Space]
        [SerializeField] private GameObject prefab;

        private void Start(){
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            var covertedPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            for (int x = 0; x < xCount; x++){
                for (int z = 0; z < zCount; z++){
                    var instance = entityManager.Instantiate(covertedPrefab);
                    Vector3 position = new Vector3(spacing.x * x, spacing.y, spacing.z * z);
                    entityManager.SetComponentData(instance, new Translation() { Value = position });
                }
            }
        }
    }
}
