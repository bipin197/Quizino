import psycopg2
import csv

# Connection details for the source database
source_conn_params = {
    'dbname': 'ques_db',
    'user': 'bks',
    'password': '(B1i2p3i4n5#)',
    'host': 'erm-pgsql.postgres.database.azure.com',
    'port': '5432'
}

# Connection details for the target database
target_conn_params = {
    'dbname': 'qz_db',
    'user': 'qz_user',
    'password': 'qz_user',
    'host': 'localhost',
    'port': '5432'
}

# Query to select data from the source table
source_query = "SELECT * FROM public.question"

# Path to save the CSV file
csv_file_path = 'question.csv'

# Name of the target table
target_table = 'public.question'

try:
    # Connect to the source database
    source_conn = psycopg2.connect(**source_conn_params)
    source_cursor = source_conn.cursor()
    
    # Execute the query to fetch data
    source_cursor.execute(source_query)
    
    # Fetch all rows from the query
    rows = source_cursor.fetchall()
    
    # Get column names
    column_names = [desc[0] for desc in source_cursor.description]
    
    # Write data to CSV
    with open(csv_file_path, 'w', newline='') as csv_file:
        csv_writer = csv.writer(csv_file)
        csv_writer.writerow(column_names)  # Write header
        csv_writer.writerows(rows)  # Write data
    
    print(f"Data exported successfully to {csv_file_path}")
    
    # Close the source cursor and connection
    source_cursor.close()
    source_conn.close()
    
    # Connect to the target database
    target_conn = psycopg2.connect(**target_conn_params)
    target_cursor = target_conn.cursor()
    
    # Open the CSV file and copy data to the target table
    with open(csv_file_path, 'r') as csv_file:
        next(csv_file)  # Skip the header row
        target_cursor.copy_expert(f"COPY {target_table} FROM STDIN WITH CSV HEADER", csv_file)
    
    target_conn.commit()
    print(f"Data imported successfully into {target_table}")
    
except Exception as e:
    print(f"Error: {e}")
finally:
    # Close the cursors and connections if they exist and are open
    if 'source_cursor' in locals() and not source_cursor.closed:
        source_cursor.close()
    if 'source_conn' in locals() and not source_conn.closed:
        source_conn.close()
    if 'target_cursor' in locals() and not target_cursor.closed:
        target_cursor.close()
    if 'target_conn' in locals() and not target_conn.closed:
        target_conn.close()
