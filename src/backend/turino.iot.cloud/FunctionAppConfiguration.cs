using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace turino.iot.cloud
{
    internal class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            builder.Setup((serviceCollection, commandRegistry) =>
                {
                    serviceCollection
                        .AddLogging();
                })
                .Functions(functions => functions
                    .EventHubFunction
                );
        }
    }
}