#!/bin/bash
TIMEOUT=60
export STATUS=1
i=0

while [[ $STATUS -ne 0 ]] && [[ $i -lt $TIMEOUT ]]; do
	i=$i+1
	/opt/mssql-tools/bin/sqlcmd -t 1 -U sa -P $SA_PASSWORD -Q "select 1" >> /dev/null
	STATUS=$?
done

if [ $STATUS -ne 0 ]; then 
	echo "Timeout exceeded when starting MSSQL Server"
	exit 1
fi

echo "Running initialize_db.sql"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i initialize_db.sql
