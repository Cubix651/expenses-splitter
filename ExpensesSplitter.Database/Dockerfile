FROM mcr.microsoft.com/mssql/server:2019-latest

WORKDIR /db
COPY ./* ./
USER root
RUN chmod +x ./*.sh
USER mssql
ENTRYPOINT [ "./entrypoint.sh" ]
