# Products

## Pre-requisites  
* [Azure Cli](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)  
* [Azure Subscription](https://account.windowsazure.com/Subscriptions )  
* [Kubernetes Cli (kubectl)](https://kubernetes.io/docs/tasks/tools/install-kubectl/)

## Steps  
> Most of the steps below can be done via the [Azure Portal](https://portal.azure.com)  
* Create Resource Group  
`az group create -n kube -l eastus`   
* Create Azure Container Service  
`az acs create --orchestrator-type kubernetes --resource-group kube --name myK8sCluster --generate-ssh-keys --agent-count 1`  
* Get Azure credentials to be able to use the Kubernetes Cli (kubectl)  
`az acs kubernetes get-credentials -g kube --name=myK8sCluster`