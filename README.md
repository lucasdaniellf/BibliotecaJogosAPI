# BibliotecaJogosAPI
API para acesso de uma biblioteca de jogos utilizando .net 6 e Entity Framework

O banco de dados utilizado foi o PostgreSQL e a ConnectionString foi removida do projeto. Caso deseje realizar testes com este projeto é necessário
utilizar a sua senha do banco de dados na ConnectionString, apagar as Migrações e realizá-las novamente utilizando o EF 
(lembrando de configurar a FK da tabela jogo como onDelete: SetNull).
