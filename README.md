# Gateways

## Getting Started

These instructions are intended to configure and run the app.

### Prerequisites

1. To have installed Microsoft.NETCore.App 5.0.
2. To have installed dotnet 5.0 command line tool.
3. To have installed node 14.17.
4. To have installed and running a Sql Server instance.

### Configuring the app settings

Settings will be stablished in the appsettings.json file on the deployed folder **(output)**.

#### Connection string settings

Under **ConnectionStrings -> Gateways**  must be set a reachable Sql Server connection string.

**_Example:_**

```json
{
  "ConnectionStrings": {
    "Gateways": "Data Source=.;Initial Catalog=gateways;Integrated Security=true;"
  }
}
```

#### Gateway Settings

Under **GatewaySettings** must be set settings regarding to the Gateways managenent.

1. _MaxDeviceCount_: **(number)** Maximun number of devices allowed to be managed per Gateway. Default value: 10.

**_Example:_**

```json
{
  "GatewaySettings": {
    "MaxDeviceCount": 10
  }
}
```

### Build and Publish

This project uses a Cake to build and publish the app.

## Steps

1. Enter to the solution folder.
2. Execute **dotnet tool restore**.
3. Execute **dotnet cake**.
4. Enter to **output** directory.
5. Execute **dotnet Gateways.WebApi.dll**.
6. Navigate to **<https://localhost:5001>**.

## Authored by

* [**Lázaro Damian Martínez Pérez**](https://www.linkedin.com/in/l%C3%A1zaro-damian-mart%C3%ADnez-p%C3%A9rez-132735137/)
