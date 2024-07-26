using ServiceStack;
using FamilyPortal.ServiceModel;

namespace FamilyPortal.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}