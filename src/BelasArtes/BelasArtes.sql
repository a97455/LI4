IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'BelasArtes')
BEGIN
    CREATE DATABASE BelasArtes;
END
GO

USE BelasArtes;
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Utilizador')
BEGIN
    CREATE TABLE [Utilizador] (
        [Email] varchar(40) NOT NULL UNIQUE,
        [Nome] varchar(100) NOT NULL,
        [Telefone] int NOT NULL,
        [Rua] varchar(30) NOT NULL,
        [Localidade] varchar(30) NOT NULL,
        [Cidade] varchar(30) NOT NULL,
        [CodigoPostal] varchar(30) NOT NULL,
        [PaisResidencia] varchar(30) NOT NULL,
        [NIG] varchar(40) NOT NULL,
        [IBAN] varchar(30) NOT NULL UNIQUE,
        [PalavraPasse] varchar(30) NOT NULL,
        CONSTRAINT [PK_UTILIZADOR] PRIMARY KEY CLUSTERED
        (
            [Email] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Movimento_Artistico')
BEGIN
    CREATE TABLE [Movimento_Artistico] (
        [Id] int IDENTITY(1,1) NOT NULL UNIQUE,
        [Nome] varchar(40) NOT NULL,
        CONSTRAINT [PK_MOVIMENTO_ARTISTICO] PRIMARY KEY CLUSTERED
        (
            [Id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Estado')
BEGIN
    CREATE TABLE [Estado] (
        [Id] int IDENTITY(1,1) NOT NULL UNIQUE,
        [Nome] varchar(40) NOT NULL,
        CONSTRAINT [PK_ESTADO] PRIMARY KEY CLUSTERED
        (
            [Id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Pintura')
BEGIN
    CREATE TABLE [Pintura] (
        [Id] int IDENTITY(1,1) NOT NULL UNIQUE,
        [Nome] varchar(40) NOT NULL,
        [Altura] float NOT NULL,
        [Largura] float NOT NULL,
        [Peso] float NOT NULL,
        [Descricao] varchar(100) NOT NULL,
        [Foto] varchar(40) NOT NULL,
        [Artista] varchar(40) NOT NULL,
        [AnoCriacao] int NOT NULL,
        [Original] bit NOT NULL,
        [VerificacaoAutenticidade] bit NOT NULL,
        [EmailVendedor] varchar(40),
        [CodMovimentoArtistico] int,
        CONSTRAINT [PK_PINTURA] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );

    ALTER TABLE [Pintura] WITH CHECK ADD CONSTRAINT [Pintura_fk0] FOREIGN KEY ([EmailVendedor]) REFERENCES [Utilizador]([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Pintura] CHECK CONSTRAINT [Pintura_fk0];

    ALTER TABLE [Pintura] WITH CHECK ADD CONSTRAINT [Pintura_fk1] FOREIGN KEY ([CodMovimentoArtistico]) REFERENCES [Movimento_Artistico]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Pintura] CHECK CONSTRAINT [Pintura_fk1];
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Leilao')
BEGIN
    CREATE TABLE [Leilao] (
        [Id] int IDENTITY(1,1) NOT NULL UNIQUE,
        [DataInicio] datetime2 NOT NULL,
        [DataFim] datetime2 NOT NULL,
        [PrecoInicial] float NOT NULL,
        [EmailComprador] varchar(40),
        [CodPintura] int,
        [CodEstado] int,
        CONSTRAINT [PK_LEILAO] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );

    ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk0] FOREIGN KEY ([EmailComprador]) REFERENCES [Utilizador]([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk0];

    ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk1] FOREIGN KEY ([CodPintura]) REFERENCES [Pintura]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk1];

    ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk2] FOREIGN KEY ([CodEstado]) REFERENCES [Estado]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk2];
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Licitacao')
BEGIN
    CREATE TABLE [Licitacao] (
        [Id] int IDENTITY(1,1) NOT NULL UNIQUE,
        [Valor] float NOT NULL,
        [EmailLicitador] varchar(40),
        [IdLeilao] int,
        CONSTRAINT [PK_LICITACAO] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );

    ALTER TABLE [Licitacao] WITH CHECK ADD CONSTRAINT [Licitacao_fk0] FOREIGN KEY ([EmailLicitador]) REFERENCES [Utilizador]([Email])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Licitacao] CHECK CONSTRAINT [Licitacao_fk0];

    ALTER TABLE [Licitacao] WITH CHECK ADD CONSTRAINT [Licitacao_fk1] FOREIGN KEY ([IdLeilao]) REFERENCES [Leilao]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Licitacao] CHECK CONSTRAINT [Licitacao_fk1];
END
GO

-- Inserting data into Utilizador table
INSERT INTO Utilizador (Email, Nome, Telefone, Rua, Localidade, Cidade, CodigoPostal, PaisResidencia, NIG, IBAN, PalavraPasse)
VALUES 
('user1@example.com','user1', 123456789, 'Street 1', 'Location 1', 'City 1', '12345', 'Country 1','00000000 0 XY0', 'IBAN123', 'password1'),
('user2@example.com','user2', 987654321, 'Street 2', 'Location 2', 'City 2', '54321', 'Country 2','00000000 0 AY9', 'IBAN456', 'password2'),
('user3@example.com','user3', 555555555, 'Street 3', 'Location 3', 'City 3', '67890', 'Country 3','00000000 0 KY6', 'IBAN789', 'password3'),
('user4@example.com','user4', 111111111, 'Street 4', 'Location 4', 'City 4', '98765', 'Country 4','00000000 2 XY0', 'IBAN987', 'password4'),
('user5@example.com','user5', 999999999, 'Street 5', 'Location 5', 'City 5', '54321', 'Country 5','00000000 5 XY0', 'IBAN654', 'password5');

-- Inserting data into Movimento_Artistico table
INSERT INTO Movimento_Artistico (Nome)
VALUES 
('Antiguidade Ocidental e Oriental,'),
('Idade Média'),
('Renascentista e Barroco'),
('Moderno'),
('Contemporâneo'),
('Outro');

-- Inserting data into Estado table
INSERT INTO Estado (Nome)
VALUES 
('Agendado'),
('A decorrer'),
('Terminado');

-- Inserting data into Pintura table
INSERT INTO Pintura (Nome, Altura, Largura, Peso, Descricao, Foto, Artista, AnoCriacao, Original, VerificacaoAutenticidade, EmailVendedor, CodMovimentoArtistico)
VALUES 
('Painting 1', 50.5, 30.2, 2.5, 'Description 1', 'leilao1.jpg', 'Artist 1', 2022, 1, 1, 'user1@example.com', 1),
('Painting 2', 40.0, 20.0, 3.0, 'Description 2', 'leilao2.jpg', 'Artist 2', 2021, 1, 0, 'user2@example.com', 2),
('Painting 3', 60.0, 40.0, 4.0, 'Description 3', 'leilao3.jpg', 'Artist 3', 2023, 1, 1, 'user3@example.com', 3),
('Painting 4', 45.5, 25.5, 2.0, 'Description 4', 'leilao4.jpg', 'Artist 4', 2020, 1, 1, 'user4@example.com', 4),
('Painting 5', 55.0, 35.0, 3.5, 'Description 5', 'leilao5.jpg', 'Artist 5', 2022, 0, 0, 'user5@example.com', 5),
('Painting 6', 60.0, 35.0, 3.5, 'Description 6', 'leilao6.jpg', 'Artist 6', 2022, 0, 0, 'user5@example.com', 4),
('Painting 7', 65.0, 35.0, 3.5, 'Description 7', 'leilao7.jpg', 'Artist 7', 2021, 0, 0, 'user5@example.com', 3),
('Painting 8', 70.0, 35.0, 3.5, 'Description 8', 'leilao8.jpg', 'Artist 8', 2022, 0, 0, 'user5@example.com', 2),
('Painting 9', 75.0, 35.0, 3.5, 'Description 9', 'leilao9.jpg', 'Artist 9', 2023, 0, 0, 'user5@example.com', 1),
('Painting 10', 80.0, 35.0, 3.5, 'Description 10', 'leilao10.jpg', 'Artist 10', 2023, 0, 0, 'user5@example.com', 1);


-- Inserting data into Leilao table
INSERT INTO Leilao (DataInicio, DataFim, PrecoInicial, EmailComprador, CodPintura, CodEstado)
VALUES 
('2023-01-18 12:00:00', '2023-01-20 12:00:00', 100.00, 'user2@example.com', 1, 3),
('2023-01-19 12:00:00', '2023-01-21 12:00:00', 150.00, 'user1@example.com', 2, 3),
('2022-12-20 12:00:00', '2021-02-22 12:00:00', 200.00, 'user1@example.com', 3, 3),
('2023-01-01 12:00:00', '2024-12-23 12:00:00', 120.00, NULL, 4, 2),
('2023-01-02 12:00:00', '2024-12-24 12:00:00', 180.00, NULL, 5, 2),
('2023-01-02 12:00:00', '2024-12-24 12:00:00', 180.00, NULL, 6, 2),
('2023-01-02 12:00:00', '2024-12-24 12:00:00', 180.00, NULL, 7, 2),
('2023-01-02 12:00:00', '2024-12-24 12:00:00', 180.00, NULL, 8, 2),
('2023-01-02 12:00:00', '2024-12-24 12:00:00', 180.00, NULL, 9, 2),
('2023-01-02 12:00:00', '2024-12-24 12:00:00', 180.00, NULL, 10, 2);


-- Inserting data into Licitacao table
INSERT INTO Licitacao (Valor, EmailLicitador, IdLeilao)
VALUES 
(120.00, 'user2@example.com', 1),
(170.00, 'user1@example.com', 2),
(270.00, 'user1@example.com', 3),
(220.00, 'user4@example.com', 4),
(230.00, 'user4@example.com', 4),
(160.00, 'user3@example.com', 5),
(200.00, 'user1@example.com', 6),
(260.00, 'user2@example.com', 7),
(360.00, 'user3@example.com', 8),
(380.00, 'user3@example.com', 8),
(190.00, 'user4@example.com', 9),
(190.00, 'user1@example.com', 10);