#!/bin/bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

cd frontend && npm install && npm run dist
cd $SCRIPT_DIR
mkdir -p API/wwwroot
cp -r frontend/dist/* API/wwwroot/
cd API
dotnet publish