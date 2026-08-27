FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet build --no-restore

RUN apt-get update \
    && apt-get install -y powershell \
    && rm -rf /var/lib/apt/lists/*

RUN pwsh bin/Debug/net10.0/playwright.ps1 install --with-deps chromium

CMD ["dotnet", "test", "--no-build"]
