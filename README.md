## Commands

```
docker build . -f .\ConsoleApp2\Dockerfile --no-cache
docker images
docker tag #IMAGE_ID# t03apt/testing

docker login --username=t03apt
docker push t03apt/testing

kubectl create job tpapp --image=t03apt/testing --namespace tpapp
kubectl logs #POD_NAME#

kubectl port-forward services/kibana-kibana 5601:5601 -n elk
http://localhost:5601
```
