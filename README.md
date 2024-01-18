# XML Doc Structure Validation

## Description

This project is a simple example of how to validate an SOAP XML document against certain requirements.

## Run locally

Install Azure Functions Core Tools 4.x.

Create a `local.settings.json` file with the following content:

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
    }
}
```

Buidl and run the function app from the command-line:

```sh
func start
```

Install `Rest Client` for [VSCode](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) or [Visual Studio](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.RestClient).

Use the file `test-docs.rest` to test the API.

Sample docs are in the `test-docs` folder.
Run `generate-docs.sh` to regenerate some of the larger docs.

## Deploy to Azure

```sh
az login

location="australiaeast"
resourceGroup="xml-doc-structure-validation"
randomIdentifier=$(head /dev/urandom | tr -dc a-z0-9 | head -c 5 ; echo '')
storage="xmldoc$randomIdentifier"
functionApp="xml-verify-$randomIdentifier"
skuStorage="Standard_LRS"
functionsVersion="4"

az group create --name $resourceGroup --location "$location"
az storage account create --name $storage --location "$location" --resource-group $resourceGroup --sku $skuStorage
az functionapp create --name $functionApp --storage-account $storage --consumption-plan-location "$location" --resource-group $resourceGroup --functions-version $functionsVersion

func azure functionapp publish $functionApp
```

Copy `.env.template` to `.env` and add your secrets.

Use the file `test-docs.rest` to test the API.

## Test via APIM

Import the Function App from Azure into Azure APIM Management.

Copy `.env.template` to `.env` and add your secrets.

Use the file `test-apim.rest` to test the API.
