# Define Kafka Connect REST API endpoint
$connectEndpoint = "http://localhost:8083/connectors"

# Debezium connector configuration
$debeziumConfig = @"
{
  "name": "ques_db-connector",
  "config": {
    "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
    "plugin.name": "pgoutput",
    "database.hostname": "postgres_ques_db",
    "database.port": "5432",
    "database.user": "db_user",
    "database.password": "db_password",
    "database.dbname": "ques_db",
    "database.server.name": "ques_db_server",
    "table.include.list": "public.question",
    "slot.name": "debezium",
    "publication.name": "dbz_publication",
    "database.history.kafka.bootstrap.servers": "kafka:9092",
    "database.history.kafka.topic": "schema-changes.ques_db",
    "include.schema.changes": "true"
  }
}
"@

# Kafka Connect sink connector configuration
$sinkConfig = @"
{
  "name": "quiz_db-sink-connector",
  "config": {
    "connector.class": "io.confluent.connect.jdbc.JdbcSinkConnector",
    "tasks.max": "1",
    "topics": "ques_db_server.public.question",
    "connection.url": "jdbc:postgresql://postgres_quiz_db:5432/quiz_db",
    "connection.user": "db_user",
    "connection.password": "db_password",
    "auto.create": "true",
    "auto.evolve": "true",
    "insert.mode": "upsert",
    "pk.mode": "record_key",
    "pk.fields": "id",
    "table.name.format": "question"
  }
}
"@

# Deploy Debezium connector
Invoke-RestMethod -Uri $connectEndpoint -Method Post -ContentType "application/json" -Body $debeziumConfig

# Deploy Kafka Connect sink connector
Invoke-RestMethod -Uri $connectEndpoint -Method Post -ContentType "application/json" -Body $sinkConfig