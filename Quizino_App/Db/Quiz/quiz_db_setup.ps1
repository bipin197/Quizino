param (
    [string]$ContainerName,
	[string]$User,
    [string]$DBName,
    [string]$DBUser,
    [string]$DBPass,
    [string]$SchemaName,
    [string]$SqlScriptPath
)

# Function to execute SQL commands inside the Docker container
function Execute-Sql {
    param (
        [string]$SqlQuery
    )

    $command = "docker exec -i $ContainerName psql -U $User -c `"$SqlQuery`""
    Invoke-Expression $command
}

# Function to execute SQL commands on a specific database inside the Docker container
function Execute-SqlOnDb {
    param (
        [string]$DbName,
        [string]$SqlQuery
    )

    $command = "docker exec -i $ContainerName psql -U $User -d $DbName -c `"$SqlQuery`""
    Invoke-Expression $command
}

# Step 1: Create a new user with a password
Execute-Sql "CREATE USER $DBUser WITH PASSWORD '$DBPass';"

# Step 2: Create a new database owned by the new user
Execute-Sql "CREATE DATABASE $DBName OWNER $DBUser;"

# Step 3: Grant all privileges on the new database to the new user
Execute-Sql "GRANT ALL PRIVILEGES ON DATABASE $DBName TO $DBUser;"

# Step 4: Connect to the new database and create the schema
Execute-SqlOnDb $DBName "CREATE SCHEMA $SchemaName AUTHORIZATION $DBUser;"

# Step 5: Read and process the provided SQL script to replace the schema placeholder
$createTableSql = Get-Content -Path $SqlScriptPath -Raw

# Replace the placeholder with the actual schema name and user
$createTableSql = $createTableSql -replace "public", $SchemaName
$createTableSql = $createTableSql -replace "bks", $DBUser

Execute-SqlOnDb $DBName $createTableSql
