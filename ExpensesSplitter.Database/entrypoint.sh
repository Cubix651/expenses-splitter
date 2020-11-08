#!/bin/bash
/opt/mssql/bin/sqlservr &
/db/initialize_db.sh
wait
