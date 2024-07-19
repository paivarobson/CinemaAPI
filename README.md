# CinemaAPI

## Descrição

Este projeto é uma API RESTful para gerenciamento de filmes e salas de cinema, desenvolvida com ASP.NET Core e Docker. A aplicação permite realizar operações CRUD em filmes e salas de cinema, e pode ser executada em um ambiente Dockerizado para facilitar o desenvolvimento e a implantação.

## Requisitos

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Estrutura do Projeto

- `Dockerfile` - Arquivo de configuração para construir a imagem Docker da aplicação.
- `docker-compose.yml` - Arquivo de configuração para orquestrar os contêineres Docker.
- `src/CinemaApi` - Código-fonte da API.