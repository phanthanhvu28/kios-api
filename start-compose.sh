#!/bin/bash#

docker-compose -f docker-compose.yml -f docker-compose-grafana.yml -f docker-compose-mysql.yml -f docker-compose-redis.yml -f docker-compose-kafka.yml up 