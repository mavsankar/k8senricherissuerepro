Steps to run:
1. Replace the instrumentation key and AzureWebJobsStorage conn string with appropriate values in the docker file
2. Build the k8senricherissue.csprojOpen command prompt and navigate to the path: k8senricherissue\bin\Debug\net6.0
3. Run docker build . -t k8senricherissue
4. Run kubectl apply -f deploy.yaml
5. Expose port 80 of the pod: kubectl expose pod <PodName> --type=LoadBalancer --port=80
6. To test the application go to http://localhost/api/Function1, to see the healthcheck go to http://localhost
7. To redeploy after making any changes run kubectl delete -f deploy.yaml and repeat the steps from 2 to 7
