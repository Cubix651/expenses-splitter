FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /vsdbg

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
            unzip \
    && rm -rf /var/lib/apt/lists/* \
    && curl -sSL https://aka.ms/getvsdbgsh \
        | bash /dev/stdin -v latest -l /vsdbg
ENV DOTNET_USE_POLLING_FILE_WATCHER 1

RUN dotnet tool install --global dotnet-ef
ENV PATH "$PATH:/root/.dotnet/tools"

WORKDIR /app
CMD [ "/bin/bash", "./entrypoint.sh" ]
