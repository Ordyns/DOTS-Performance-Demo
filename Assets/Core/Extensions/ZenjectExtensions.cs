using Unity.Entities;
using Zenject;

public static class ZenjectExtensions
{
    public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindSystem<T>(this DiContainer container) where T : ComponentSystemBase {
        return container.BindInterfacesAndSelfTo<T>().FromResolveGetter<World, T>(world => {
            T system = container.Instantiate<T>();
            world.AddSystem(system);
            world.GetExistingSystem<SimulationSystemGroup>().AddSystemToUpdateList(system);
            return system;
        });
    }
}
