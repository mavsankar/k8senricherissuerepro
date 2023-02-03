# Repro

# Steps

1. Replace the instrumentation key and AzureWebJobsStorage conn string with appropriate values\
2. Build the k8senricherissue.csproj
3. Open command prompt and navigate to the path: k8senricherissue\bin\Debug\net6.0
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
