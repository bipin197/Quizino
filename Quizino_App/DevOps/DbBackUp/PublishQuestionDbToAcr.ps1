# Variables
$containerName = "qs-ser"  # Name or ID of the running PostgreSQL container
$backupFileName = "pg_backup.sql"         # Name of the SQL dump file
$imageName = "qsdb"          # Name for the new Docker image
$tag = "latest"                       # Tag for the Docker image (e.g., 'latest', 'v1')
$acrName = "quizinoregistry"              # Azure Container Registry name
$acrLoginServer = "${acrName}.azurecr.io"   # ACR login server URL
$acrImageName = "${acrLoginServer}/${imageName}:${tag}"

# Step 1: Dump the PostgreSQL database from the running container
Write-Output "Dumping PostgreSQL database from the running container..."
docker exec ${containerName} pg_dumpall -U qs_user -f /tmp/$backupFileName

# Step 2: Copy the dump file from the container to the host machine
Write-Output "Copying the database dump from the container to the host..."
docker cp "${containerName}:/tmp/$backupFileName" .

# Step 3: Create a Dockerfile to include the dump in a new image
Write-Output "Creating a Dockerfile..."
$dockerfileContent = @"
FROM postgres:latest
COPY $backupFileName /docker-entrypoint-initdb.d/
"@
$dockerfileContent | Out-File -FilePath Dockerfile -Encoding utf8

# Step 4: Build a new Docker image with the dumped data
Write-Output "Building a new Docker image with the PostgreSQL dump..."
docker build -t "${imageName}:${tag}" .

# Step 5: Tag the image for Azure Container Registry
Write-Output "Tagging the image for Azure Container Registry..."
docker tag "${imageName}:$tag" "${acrImageName}"

# Step 6: Login to Azure Container Registry
Write-Output "Logging in to Azure Container Registry..."
az acr login --name ${acrName}

# Step 7: Push the image to Azure Container Registry
Write-Output "Pushing the image to Azure Container Registry..."
docker push "${acrImageName}"

# Step 8: Clean up local files (optional)
Write-Output "Cleaning up local files..."
Remove-Item -Path Dockerfile
Remove-Item -Path $backupFileName

Write-Output "PostgreSQL database image '${imageName}:${tag}' has been successfully pushed to ACR '${acrName}'."
