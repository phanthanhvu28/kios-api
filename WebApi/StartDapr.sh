#!/bin/bash

dapr run --app-id kios-api --app-port 6000 --dapr-http-port 3500 --dapr-grpc-port 50001 --resources-path ../components/