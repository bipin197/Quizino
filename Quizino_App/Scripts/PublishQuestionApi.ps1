# Define variables
$resourceGroupName = "BKS-South-IN"
$registryName = "quizinoregistry.azurecr.io"
$localImage = "questionapi"
$imageTag = "latest"
$imageName = "questionapi"

# Login to Azure
Write-Host "Logging in to Azure..."
az login

# Check if the ACR exists, if not, create it
$acrExists = az acr show --name $registryName --resource-group $resourceGroupName --query "name" --output tsv

if (-not $acrExists) {
    Write-Host "Creating Azure Container Registry..."
    az acr create --resource-group $resourceGroupName --name $registryName --sku Basic
} else {
    Write-Host "Azure Container Registry already exists."
}

# Log in to ACR
Write-Host "Logging in to Azure Container Registry..."
az acr login --name $registryName

# Tag the Docker image
$acrLoginServer = "$(az acr show --name $registryName --query "loginServer" --output tsv)"
$fullImageName = "$acrLoginServer/${imageName}:${imageTag}"
Write-Host "Tagging the image as $fullImageName..."
docker tag "${localImage}:${imageTag}" $fullImageName

# Push the Docker image to ACR
Write-Host "Pushing the image to ACR..."
docker push $fullImageName

# Verify the image is in ACR
Write-Host "Verifying the image in ACR..."
az acr repository list --name $registryName --output table