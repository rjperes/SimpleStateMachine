using Microsoft.Extensions.DependencyInjection;

namespace SimpleStateMachine.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStateMachine<T>(this IServiceCollection services, T initialState, Action<IStateMachine<T>> configure) where T : struct, Enum
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configure);
    
            var stateMachine = initialState.Create();
            configure(stateMachine);

            return services.AddSingleton<IStateMachine<T>>(stateMachine);
        }
    }
}
