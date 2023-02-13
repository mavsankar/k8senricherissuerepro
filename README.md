Steps to run:
1. Replace the instrumentation key and AzureWebJobsStorage conn string with appropriate values in the docker file
2. Build the k8senricherissue.csproj
3. Open command prompt and navigate to the path: k8senricherissue\bin\Debug\net6.0
4. Run docker build . -t k8senricherissue
5. Run kubectl apply -f deploy.yaml
6. Expose port 80 of the pod: kubectl expose pod <PodName> --type=LoadBalancer --port=80
7. To test the application go to http://localhost/api/Function1, to see the healthcheck go to http://localhost
8. To redeploy after making any changes run kubectl delete -f deploy.yaml and repeat the steps from 2 to 7
