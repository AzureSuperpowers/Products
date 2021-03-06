# Products

## Pre-requisites  
* [Azure Cli](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)  
* [Azure Subscription](https://account.windowsazure.com/Subscriptions )  
* [Kubernetes Cli (kubectl)](https://kubernetes.io/docs/tasks/tools/install-kubectl/)

## Setting Up Environment  
> Most of the steps below can be done via the [Azure Portal](https://portal.azure.com)  
* Create Resource Group  
`az group create -n kube -l eastus`   
* Create Azure Container Service  
`az acs create --orchestrator-type kubernetes --resource-group kube --name myK8sCluster --generate-ssh-keys --agent-count 1`  
* Get Azure credentials to be able to use the Kubernetes Cli (kubectl)  
`az acs kubernetes get-credentials -g kube --name=myK8sCluster`  
* Confirm Kubernetes cluster by Checking Dashboard  
`az acs kubernetes browse -g kube -n myK8sCluster`  
* (Optional) Create secret for private Registry  
`kubectl create secret docker-registry regsecret --docker-server=[SERVER] --docker-username=[USERNAME] --docker-password=[PASSWORD] --docker-email=[EMAIL]`  

## Setting Up Elastic Search   
> I'm using a set of scripts available [here](https://github.com/kubernetes/kubernetes/tree/master/examples/elasticsearch) to create an instance of [Elastic Search](https://www.elastic.co/products/elasticsearch)  

* Create Service Account  
`kubectl apply -f .deployment/elastic/service-account.yaml`  
* Create Replication Controller  
`kubectl apply -f .deployment/elastic/es-rc.yaml`  
* Create Service  
`kubectl apply -f .deployment/elastic/es-svc.yaml`

## Building and Publishing  

* Build solution
* Navigate to `AzSp.Products` folder and Publish it  
`dotnet publish .\AzSp.Products.csproj -c Release -o ./obj/Docker/publish`  
* Create Image  
`docker build -t azsp.products .`
* (Optional) Login to private registry  
`docker login [Registry URL]`  
* (Optional) Tag Image  
`docker tag azsp.products [Registry URL]/azsp.products`
* Push Image
`docker push [Registry URL]/azsp.products`

## Deploying to Kubernetes  

* Creating Deployment workload  
`kubectl apply -f .deployment\deployment.yaml`  
* Creating Service workload  
`kubectl apply -f .deployment\service.yaml`  
* (Optional) Watch for Service to be finalized  
`kubectl get services lbproducts --watch`