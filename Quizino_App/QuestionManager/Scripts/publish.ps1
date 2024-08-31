# Define variables
$registryName = "quizinoregistry"
$imageName = "questionmanager"
$version = "latest"
$fullImageName = "$registryName.azurecr.io/${imageName}:${version}"

# Login to Azure
az login

# Login to Azure Container Registry
az acr login --name $registryName

# Tag the Docker image
docker tag "${imageName}:${version}" ${fullImageName}

# Push the Docker image to ACR
docker push $fullImageName

# Verify that the image is in the ACR
$repositoryList = az acr repository list --name ${registryName} --output table
Write-Output "Images in ACR:"
Write-Output $repositoryList
