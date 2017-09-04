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
* Confirm Kubernetes cluster by getting its nodes  
`kubectl get nodes`  
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

## Building and Packing  
