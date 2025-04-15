# Gerenciador de Tarefas

Este projeto é uma API RESTful para gerenciamento de tarefas, permitindo que usuários organizem e monitorem suas atividades diárias. A aplicação é dividida em várias partes, cada uma responsável por uma funcionalidade específica.

## Estrutura do Projeto

O projeto é organizado da seguinte forma:

- **src/GerenciadorTarefas.API**: Contém a implementação da API, incluindo controladores, DTOs e middlewares.
- **src/GerenciadorTarefas.Core**: Contém as entidades, enums e interfaces que definem a lógica de negócios.
- **src/GerenciadorTarefas.Aplicacao**: Contém os serviços que implementam a lógica de negócios.
- **src/GerenciadorTarefas.Infraestrutura**: Contém a configuração do banco de dados e repositórios para persistência de dados.
- **tests**: Contém testes unitários e de integração para garantir a qualidade do código.

## Funcionalidades

A API oferece as seguintes funcionalidades:

- **Listagem de Projetos**: Permite listar todos os projetos do usuário.
- **Visualização de Tarefas**: Permite visualizar todas as tarefas de um projeto específico.
- **Criação de Projetos**: Permite criar um novo projeto.
- **Criação de Tarefas**: Permite adicionar uma nova tarefa a um projeto.
- **Atualização de Tarefas**: Permite atualizar o status ou detalhes de uma tarefa.
- **Remoção de Tarefas**: Permite remover uma tarefa de um projeto.

## Regras de Negócio

- Cada tarefa deve ter uma prioridade (baixa, média, alta) que não pode ser alterada após a criação.
- Um projeto não pode ser removido se houver tarefas pendentes associadas.
- O histórico de alterações de cada tarefa é registrado, incluindo o que foi modificado, a data da modificação e o usuário que fez a modificação.
- Cada projeto tem um limite máximo de 20 tarefas.
- Relatórios de desempenho estão disponíveis apenas para usuários com a função de "gerente".
- Os usuários podem adicionar comentários às tarefas, que também são registrados no histórico de alterações.

## Execução

Para executar o projeto, siga os passos abaixo:

1. Clone o repositório:
   ```
   git clone <URL do repositório>
   ```

2. Navegue até o diretório do projeto:
   ```
   cd GerenciadorTarefas
   ```

3. Execute o Docker Compose para iniciar os serviços:
   ```
   docker-compose up
   ```

4. A API estará disponível em `http://localhost:5000`.

## Testes

Os testes unitários e de integração podem ser executados utilizando o seguinte comando:
```
dotnet test
```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## Licença

Este projeto está licenciado sob a MIT License. Veja o arquivo LICENSE para mais detalhes.

## Perguntas para Refinamento com o PO

Perguntas estratégicas para orientar discussões de refinamento com o Product Owner:

1. Visão e Métricas: Qual é a visão de longo prazo para este produto nos próximos 12 meses e quais métricas são utilizadas para medir seu sucesso?

2. Priorização e Feedback: Quais funcionalidades os usuários mais solicitam atualmente e como isso se alinha com nossas prioridades de desenvolvimento?

3. Experiência do Usuário: Quais áreas da aplicação geram mais confusão ou tickets de suporte, e como poderíamos melhorar a experiência nesses pontos?

4. Escalabilidade: Considerando o crescimento esperado de usuários, quais aspectos da arquitetura atual deveríamos preparar para escalar prioritariamente?

5. Integrações: Quais sistemas ou ferramentas externas nossos usuários mais utilizam que poderiam se beneficiar de uma integração com nossa plataforma?

## Melhorias Propostas

Esta seção apresenta cinco melhorias estratégicas para o projeto:

1. Arquitetura CQRS: Implementar o padrão Command Query Responsibility Segregation para separar operações de leitura e escrita, otimizando a performance e escalabilidade da aplicação, especialmente útil para o histórico de alterações de tarefas.

2. Microsserviços: Evoluir a arquitetura para microsserviços, separando funcionalidades como gerenciamento de projetos, tarefas e usuários em serviços independentes, facilitando a manutenção e permitindo escalabilidade seletiva.

3. Sistema de Notificações: Desenvolver um sistema de alertas e notificações para prazos de tarefas, atualizações importantes e menções em comentários, entregando via e-mail, push notifications ou dentro da própria aplicação.

4. Segurança Avançada: Implementar OAuth 2.0/OpenID Connect para autenticação e autorização robusta, com gerenciamento de permissões granular e proteção contra ameaças comuns como CSRF e XSS.

5. Infraestrutura CI/CD: Estabelecer pipelines de integração e entrega contínua com testes automatizados, garantindo qualidade de código e permitindo implantações mais frequentes e confiáveis.