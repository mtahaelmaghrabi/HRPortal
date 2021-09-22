## First Milestone:
# Get the AKS cluster running with 2 microservices talking to each other syncronosouly

### Create the Employee Service + docker file

### Create the Leave request + docker file

### Create Kubernetes Clusetr (Using AKS for me)



## Second Milestone:
# Using NGINX Ingress controller as Api-Gateway
ingress nginx deployment
https://kubernetes.github.io/ingress-nginx/deploy/

- docker-desktop
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.0/deploy/static/provider/cloud/deploy.yaml

- azure



## Storage & SQL Server
```
kubectl get storageclass
```

### For Storage > we need
1. Persistent Volume Claim
2. Persistent Volume
3. Storage Class

### applying the PVC file
```
kubectl apply -f local-pvc.yaml
kubectl get pvc
```
### To create a secret
```
kubectl create secret generic mssql --from-literal=SA_PASSWORD="P@ssw0rd"
```
### Apply SQL server YAML file (mssql-plat-depl.yaml)
```
kubectl apply -f mssql-plat-depl.yaml
```



## Communication between services

### Using RabbitMQ (AMQP protocol)
#### Install RabbitMQ on K8S cluster as a Container

Deploy\Kuberenetes\rabbitmq-depl.yaml
1 deployment
2 services (ClusterIP & LoadBalancer)

Will use the port [15672] in the load balancer in order to use it from local dev. machine

Access the management interface
http://localhost:15672/
use: guest for user & password

```
kubectl apply -f rabbitmq-depl.yaml
```
#### Using RabbitMQ from the HRPortal application
1. add the required libraries
package: RabbitMQ.Client

2. Add the configuration on:
    - appsettings.Development.json
    - appsettings.Depolyment.json

3. Add Dtos: EmployeePublishDto & profile as well

4. Creating MessageBusClient [AsyncDataServices]
    - IMessageBusClient
    -  MessageBusClient
And Register them as Singelton in the startup.cs class

5. Using the message buss client:
    - 
6. Receiving the Messages
[Event Processor] folder 
    - 
Receiver


Later I think I am going to use Azure Service Bus instead.