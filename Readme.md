# Repro

## Steps

1. Replace the instrumentation key and AzureWebJobsStorage conn string with appropriate values\
2. Build the k8senricherissue.csproj
3. Open command prompt and navigate to the path: `k8senricherissue\bin\Debug\net6.0`
4. Run 
    ```shell
    docker build . -t k8senricherissue
    kubectl delete -f deploy.yaml
    kubectl apply -f deploy.yaml
    ```
5. Expose port 80 of the pod: 
   1. kubectl expose pod $PodName --type=LoadBalancer --port=80
   2. To test the application go to http://localhost/api/Function1, 
   3. to see the healthcheck go to http://localhost
6. To redeploy after making any changes run line below and repeat the steps from 2 to 7
    ```shell
    kubectl delete -f deploy.yaml
    ```

## How to Enable Microsoft.ApplicationInsights.Kubernetes for Azure Function (InProc)

* Add reference to to these package:

    ```xml
    <PackageReference Include="Microsoft.Azure.Functions.Extensions" Version="1.1.0" />
    <PackageReference Include="Microsoft.NET.Sdk.Functions" Version="4.1.1" />
    <PackageReference Include="Microsoft.Azure.WebJobs.Extensions.Http" Version="3.1.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="7.0.0" />
    ```

* Skip clean output, refer to [k8senricherissue.csproj](./k8senricherissue/k8senricherissue.csproj) for details.

    ```xml
    <PropertyGroup>
        <_FunctionsSkipCleanOutput>true</_FunctionsSkipCleanOutput>
    </PropertyGroup>
    ```

* Create / Update `Startup.cs`. See [Startup.cs](./k8senricherissue/Startup.cs) for an example.
  * Update needed to bypass the registering the hosted service - no public package yet.


## Issues Summary

There are 2 ways to run Azure Function - in-proc or isolated. Here we talk about `in-proc` mode. For more info about isolated azure function, refer to <https://github.com/xiaomi7732/AzureFuncIsoDocker>.

* Challenges

    There are several challenges to enable `Microsoft.ApplicationInsights.Kubernetes`:

    * Azure Function Runtime (tested on SDK 4.1.1) does not allow any hosted service (IHostedService or BackgroundService) being registered in the Dependency Injection container, while the hosted service is the mechanism used by Microsoft.ApplicationInsights.Kubernetes, refer to [this line of code](https://github.com/microsoft/ApplicationInsights-Kubernetes/blob/c1368b7695c3bf8796c386cae3f1d58df0da5c90/src/ApplicationInsights.Kubernetes/Extensions/KubernetesServiceCollectionBuilder.cs#L146). Refer to [this GitHub issue](https://github.com/Azure/azure-functions-host/issues/5447#issuecomment-575368316) for more details about the limitation.
      * A workaround: it is possible to bootstrap it without the background service by using `IK8sInfoBootstrap`.
        * To use this work-around, several things needs to happen:
          * `Application Insights Kubernetes` package needs to support skipping registering the hosted service - otherwise, the function runtime won't start.
          * `IK8sInfoBootstrap` needs to be exposed publicly - it has been internal.
          * There needs a good place to call `Run` on `IK8sInfoBootstrap` object. Refer to [ApplicationInsightsK8sBootstrap.cs](./k8senricherissue/ApplicationInsightsK8sBootstrap.cs) for an example.
    
    * Azure function uses `System.Text.Json 6.0.0` while `Microsoft.ApplicationInsights.Kubernetes` depends on `System.Text.Json 7.0.1`.
      * To make it work, add `<_FunctionsSkipCleanOutput>true</_FunctionsSkipCleanOutput>` to project file, see [k8senricherissue.csproj](./k8senricherissue/k8senricherissue.csproj) for an example.



