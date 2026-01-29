#!/bin/bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

# Generate Openapi definition
cd $SCRIPT_DIR/API && dotnet build

# Generate Website and copy to API
cd $SCRIPT_DIR/frontend && npm install && npm run dist
mkdir -p $SCRIPT_DIR/API/wwwroot && cp -r $SCRIPT_DIR/frontend/dist/* $SCRIPT_DIR/API/wwwroot/

# Generate API
cd $SCRIPT_DIR/API
dotnet publish