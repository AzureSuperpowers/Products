cd AzSp.Products
dotnet publish .\AzSp.Products.csproj -c Release -o ./obj/Docker/publish
docker build -t azsp.products .
cd ..
docker tag azsp.products azuresuperpowers.azurecr.io/azsp.products:latest
docker push azuresuperpowers.azurecr.io/azsp.products:latest
kubectl apply -f .deployment\deployment.yaml