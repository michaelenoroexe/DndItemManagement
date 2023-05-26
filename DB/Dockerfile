FROM mcr.microsoft.com/mssql/server AS sql
WORKDIR /app
COPY DB.sql ./
ENTRYPOINT [ "../opt/mssql-tools/bin/sqlcmd", "-S localhost", "-U SA", "-P ${{secrets.SA_PASSWORD}}", "-i ./DB.sql" ]    
EXPOSE 1433