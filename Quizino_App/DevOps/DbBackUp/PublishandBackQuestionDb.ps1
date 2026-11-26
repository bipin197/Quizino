# Variables
$dockerImageName = "quizinoqs"
$dockerTag = "latest"
$registryUrl = "bks190784"  # Change this to your registry URL if not using Docker Hub
#$acrName = "myregistry.azurecr.io"    # Comment out this line if not using Azure
#$acrImageName = "${acrName}/${dockerImageName}:${dockerTag}"  # Use this for Azure Container Registry
$dockerHubImageName = "${registryUrl}/${dockerImageName}:${dockerTag}"  # Use this for Docker Hub
$dockerToken = "dckr_pat_zF9TaRVc-RzN47V1_WfkZK6R_JM"  # Your generated token

# Build Docker Image
Write-Output "Building Docker image..."
$dockerBuildCommand = "docker build -t ${dockerImageName}:${dockerTag} ."
Invoke-Expression $dockerBuildCommand
Write-Output "Docker image built: ${dockerImageName}:${dockerTag}"

Write-Output "Logging into Docker Hub..."
$dockerLoginCommand = "docker login --username $dockerUsername --password-stdin"
$dockerToken | docker login --username $dockerUsername --password-stdin
Write-Output "Docker login successful."

# Tag Docker Image
Write-Output "Tagging Docker image..."
# Use the appropriate tag based on the registry
$dockerTagCommand = "docker tag ${dockerImageName}:${dockerTag} ${dockerHubImageName}"  # For Docker Hub
# $dockerTagCommand = "docker tag ${dockerImageName}:${dockerTag} ${acrImageName}"      # For Azure Container Registry
Invoke-Expression $dockerTagCommand
Write-Output "Docker image tagged: $dockerHubImageName"  # Or $acrImageName if using Azure

# Push Docker Image to Registry
Write-Output "Pushing Docker image to registry..."
$dockerPushCommand = "docker push ${dockerHubImageName}"  # For Docker Hub
# $dockerPushCommand = "docker push ${acrImageName}"      # For Azure Container Registry
Invoke-Expression $dockerPushCommand
Write-Output "Docker image pushed to registry: $dockerHubImageName"  # Or $acrImageName if using Azure

Write-Output "Process completed."
