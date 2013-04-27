using System.ServiceModel;
using System.ServiceModel.Description;

namespace Rebuild.Extensions.Wcf
{
    public static class ServiceHostExtensions
    {
        public static ServiceHost AddOrReplaceServiceBehavior<TServiceBehavior>(this ServiceHost serviceHost, TServiceBehavior behavior) where TServiceBehavior : IServiceBehavior
        {
            var behaviors = serviceHost.Description.Behaviors;
            var index = behaviors.IndexOfType<TServiceBehavior>();
            
            if (index == -1)
                behaviors.Add(behavior);
            else
                behaviors[index] = behavior;

            return serviceHost;
        }
    }
}
