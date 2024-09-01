# Variables
$containerName = "qs-ser"  # Enclose in double quotes if it contains special characters
$imageName = "qsdb"          # Name for the new image
$tag = "latest"                       # Tag for the image (e.g., 'latest', 'v1')
$acrName = "quizinoregistry"              # Azure Container Registry name
$acrLoginServer = "$acrName.azurecr.io"   # ACR login server URL
$acrImageName = "${acrLoginServer}/${imageName}:${tag}"

# Step 1: Commit the running container to an image
Write-Output "Creating Docker image from the running container..."
docker commit "$containerName" "${imageName}:${tag}"

# Step 2: Tag the image for Azure Container Registry
Write-Output "Tagging the image for Azure Container Registry..."
docker tag "${imageName}:${tag}" "${acrImageName}"

# Step 3: Login to Azure Container Registry
Write-Output "Logging in to Azure Container Registry..."
az acr login --name ${acrName}

# Step 4: Push the image to Azure Container Registry
Write-Output "Pushing the image to Azure Container Registry..."
docker push "${acrImageName}"

# Step 5: Verify the image in Azure Container Registry
Write-Output "Verifying the image in Azure Container Registry..."
$repositoryList = az acr repository list --name ${acrName} --output table
Write-Output "Images in ACR:"
Write-Output $repositoryList

Write-Output "Docker image '${imageName}:${tag}' has been successfully pushed to ACR '${acrName}'."