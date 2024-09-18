# Start from the official PostgreSQL 15.7 image based on Debian Bookworm
FROM postgres:15.7-bookworm

RUN apt-get update && \
    apt-get install -y postgresql-15-unit && \
    rm -rf /var/lib/apt/lists/*
