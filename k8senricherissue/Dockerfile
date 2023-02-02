FROM mcr.microsoft.com/dotnet/sdk:6.0 AS installer-env
WORKDIR /
EXPOSE 80
RUN mkdir -p home/site/wwwroot
COPY / /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4

ENV USERNAME=testuser
ENV GROUP=testgroup
ENV HOME=/home/${USERNAME}

# Create a group and user (system account) which will execute the app
RUN groupadd -r -g 3000 ${GROUP} &&\
    useradd -r -u 1000 -g ${GROUP} -d ${HOME} -s /sbin/nologin -c "utf-testapp-fa app user" ${USERNAME}
RUN mkdir -p tmp/Functions/Host

ENV APPINSIGHTS_INSTRUMENTATIONKEY=<inskey>
ENV COMPlus_EnableDiagnostics=0
ENV AzureWebJobsStorage=<ConnStr>
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true 

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]