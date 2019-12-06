# Umbraco.Heartcore

## Prerequisites
* .NET Core 3.0

## Setup project on development environment

1. Create a Umbraco Heartcore project: ```https://umbraco.com/try-umbraco-heartcore/```
1. Clone this project
1. Change the ``ProjectAlias`` in ```appsettings.json``` to whatever your instance of Umbraco Heartcore is named

## Usage

1. Create document types of your choice in the SaaS environment
1. Publish a page
1. Start this project using F5 and go to ```https://localhost:44328/api/v1/page/[GUID]```
