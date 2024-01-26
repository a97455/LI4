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
		[Data] datetime NOT NULL,
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
('emma.jones@gmail.com', 'Emma Jones', '912345678', '789 Pine Street', 'Greenfield', 'Springfield', '12345', 'United States', '123456789', 'US1234567890', 'spring789'),
('liam.wilson@hotmail.com', 'Liam Wilson', '936345666', '456 Oak Avenue', 'Hill Valley', 'Hill Valley', '54321', 'United States', '234567890', 'US0987654321', 'valley456'),
('olivia.smith@gmail.com', 'Olivia Smith', '958364828', '321 Cedar Street', 'Riverside', 'Riverside', '98765', 'United States', '345678901', 'US5225555550', 'river111'),
('noah.taylor@hotmail.co.uk', 'Noah Taylor', '917269077', '654 Birch Lane', 'Windsor', 'London', '54321', 'United Kingdom', '456789012', 'GB9876543210', 'wind999'),
('john.doe@gmail.com', 'John Doe', '911234567', '123 Main Street', 'Sunnydale', 'Los Angeles', '12345', 'United States', '567890123', 'US1233537890', 'sunshine123');


-- Inserting data into Movimento_Artistico table
INSERT INTO Movimento_Artistico (Nome)
VALUES 
('Antiguidade Ocidental e Oriental'),
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

-- Inserindo dados na tabela Pintura com descrições adaptadas
INSERT INTO Pintura (Nome, Altura, Largura, Peso, Descricao, Foto, Artista, AnoCriacao, Original, VerificacaoAutenticidade, EmailVendedor, CodMovimentoArtistico)
VALUES
('O quarto', 50.5, 30.2, 2.5, 'Composição vibrante com cores e formas hipnotizantes.', 'leilao1.jpg', 'Pablo Picasso', 1972, 1, 1, 'emma.jones@gmail.com',1),
('Doze girassóis', 40.0, 20.0, 3.0, 'Obra atemporal com uma mistura de cores vibrantes.', 'leilao2.jpg', 'Vincent van Gogh', 1930, 1, 0, 'emma.jones@gmail.com',4),
('A Noite Estrelada', 60.0, 40.0, 4.0, 'Peça contemporânea com expressões artísticas modernas.', 'leilao3.jpg', 'Banksy', 2021, 1, 1, 'emma.jones@gmail.com',2),
('Auto-retrato', 45.5, 25.5, 2.0, 'Criação intrigante com uma mistura única de cores e texturas.', 'leilao4.jpg', 'Claude Monet', 2010, 1, 1, 'emma.jones@gmail.com',3),
('Pela estrada', 55.0, 35.0, 3.5, 'Composição abstrata capturando a essência do movimento artístico.', 'leilao5.jpg', 'Jackson Pollock', 1985, 0, 0, 'liam.wilson@hotmail.com',4),
('Céu estrelado', 60.0, 35.0, 3.5, 'Pintura antiga refletindo as tendências artísticas do século XIX.', 'leilao6.jpg', 'Claude Monet', 1870, 0, 0, 'liam.wilson@hotmail.com',2),
('Terraço do Café à Noite', 65.0, 35.0, 3.5, 'Obra contemporânea explorando conceitos e formas inovadoras.', 'leilao7.jpg', 'Yayoi Kusama', 2005, 0, 0, 'liam.wilson@hotmail.com',1),
('Árvore de amora', 70.0, 35.0, 3.5, 'Pintura recente apresentando uma interação dinâmica de cores e texturas.', 'leilao8.jpg', 'Banksy', 2022, 0, 0, 'liam.wilson@hotmail.com',1),
('Campo de Verão', 75.0, 35.0, 3.5, 'Visão futurista explorando temas imaginativos e especulativos.', 'leilao9.jpg', 'Ai Weiwei', 2005, 0, 0, 'olivia.smith@gmail.com',2),
('A cesta', 80.0, 35.0, 3.5, 'Criação inovadora empurrando os limites da arte contemporânea.', 'leilao10.jpg', 'Yayoi Kusama', 2023, 0, 0, 'olivia.smith@gmail.com',3),
('Lírios', 42.0, 25.0, 2.0, 'Criação dinâmica com elementos únicos e cativantes.', 'leilao11.jpg', 'Banksy', 2018, 1, 1, 'olivia.smith@gmail.com',4),
('Campo de Trigo com Corvos', 65.0, 35.0, 3.5, 'Composição colorida mostrando expressões artísticas vibrantes.', 'leilao12.jpg', 'Yayoi Kusama', 2015, 1, 1, 'olivia.smith@gmail.com',3),
('Amendoeira em Flor', 48.0, 28.0, 2.8, 'Criação abstrata com uma mistura única de cores.', 'leilao13.jpg', 'Jackson Pollock', 1950, 1, 0, 'noah.taylor@hotmail.co.uk',1),
('A Vinha Encarnada', 55.0, 40.0, 4.0, 'Obra whimsical com elementos surrealistas.', 'leilao14.jpg', 'Salvador Dali', 1965, 1, 1, 'noah.taylor@hotmail.co.uk',1),
('Raízes de árvore', 72.0, 40.0, 3.0, 'Composição contemporânea com características dinâmicas e expressivas.', 'leilao15.jpg', 'Banksy', 2019, 1, 1, 'noah.taylor@hotmail.co.uk',3),
('Campo de trigo sob nuvens de tempestade', 38.0, 20.0, 2.0, 'Toque surrealista com elementos intrigantes.', 'leilao16.jpg', 'Salvador Dali', 1955, 1, 0, 'noah.taylor@hotmail.co.uk',4),
('Vaso com Lírios Contra um Fundo Amarelo', 55.0, 35.0, 3.5, 'Expressão abstrata com características inovadoras.', 'leilao17.jpg', 'Jackson Pollock', 1960, 0, 0, 'john.doe@gmail.com',4),
('Jardim com casal cortejando', 68.0, 35.0, 4.5, 'Criação inovadora com elementos visualmente impactantes.', 'leilao18.jpg', 'Yayoi Kusama', 2017, 1, 1, 'john.doe@gmail.com',2),
('Vaso com rosas cor-de-rosa', 80.0, 45.0, 5.0, 'Composição abstrata com características atemporais.', 'leilao19.jpg', 'Jackson Pollock', 1975, 0, 0, 'john.doe@gmail.com',3),
('O bom samaritano', 60.0, 40.0, 3.0, 'Composição contemporânea com características dinâmicas e expressivas.', 'leilao20.jpg', 'Ai Weiwei', 2020, 1, 1, 'john.doe@gmail.com',3);



-- Inserting data into Leilao table
INSERT INTO Leilao (DataInicio, DataFim, PrecoInicial, EmailComprador, CodPintura, CodEstado)
VALUES 
('2024-01-21 19:40:00', '2024-01-22 17:30:00', 1140.00, 'liam.wilson@hotmail.com', 1, 3),
('2024-01-22 20:20:00', '2024-01-23 16:00:00', 470.00, 'olivia.smith@gmail.com', 2, 3),
('2024-01-22 17:15:00', '2024-01-23 13:00:00', 2350.00, 'noah.taylor@hotmail.co.uk', 3, 3),
('2024-01-23 13:10:00', '2024-01-24 13:45:00', 450.00, 'john.doe@gmail.com', 4, 3),
('2024-01-23 15:45:00', '2024-01-24 21:45:00', 950.00, 'emma.jones@gmail.com', 5, 3),
('2024-01-23 11:45:00', '2024-01-30 23:00:00', 2450.00,NULL, 6, 2),
('2024-01-23 12:30:00', '2024-01-30 11:45:00', 200.00, NULL, 7, 2),
('2024-01-23 14:30:00', '2024-01-30 12:30:00', 340.00, NULL, 8, 2),
('2024-01-24 10:45:00', '2024-01-29 23:45:00', 260.00, NULL, 9, 2),
('2024-01-24 19:00:00', '2024-01-29 22:30:00', 200.00, NULL, 10, 2),
('2024-01-24 20:30:00', '2024-01-29 18:00:00', 120.00, NULL, 11, 2),
('2024-01-24 09:30:00', '2024-01-29 23:30:00', 180.00, NULL, 12, 2),
('2024-01-25 10:45:00', '2024-01-29 23:30:00', 500.00, NULL, 13, 2),
('2024-01-25 11:00:00', '2024-01-30 23:30:00', 940.00, NULL, 14, 2),
('2024-01-25 19:15:00', '2024-01-31 19:15:00', 250.00, NULL, 15, 2),
('2024-01-26 20:30:00', '2024-01-30 20:30:00', 780.00, NULL, 16, 2),
('2024-01-30 21:45:00', '2024-02-02 21:45:00', 920.00, NULL, 17, 1),
('2024-01-31 13:15:00', '2024-01-31 23:15:00', 100.00, NULL, 18, 1),
('2024-02-02 20:20:00', '2024-02-03 15:15:00', 1040.00, NULL, 19, 1),
('2024-02-03 20:20:00', '2024-02-05 16:00:00', 300.00, NULL, 20, 1);


-- Inserting data into Licitacao table
INSERT INTO Licitacao (Valor, EmailLicitador, IdLeilao, Data)
VALUES 
(1200.00,'liam.wilson@hotmail.com' , 1,'2024-01-21 19:45:10'),
(530.00, 'olivia.smith@gmail.com', 2,'2024-01-23 12:28:20'),
(560.00, 'noah.taylor@hotmail.co.uk', 2,'2024-01-23 12:35:03'),
(630.00, 'olivia.smith@gmail.com', 2,'2024-01-23 13:40:40'),
(2470.00, 'john.doe@gmail.com', 3,'2024-01-22 19:17:23'),
(2600.00, 'noah.taylor@hotmail.co.uk', 3,'2024-01-22 21:15:25'),
(480.00, 'john.doe@gmail.com', 4,'2024-01-23 22:15:25'),
(1000.00, 'olivia.smith@gmail.com', 5,'2024-01-24 13:26:10'),
(1100.00, 'emma.jones@gmail.com', 5,'2024-01-24 15:29:12'),
(2570.00, 'john.doe@gmail.com', 6,'2024-01-27 17:23:05'),
(2670.00, 'noah.taylor@hotmail.co.uk', 6,'2024-01-27 17:56:05'),
(260.00, 'olivia.smith@gmail.com', 7,'2024-01-27 12:50:30'),
(480.00, 'noah.taylor@hotmail.co.uk', 8,'2024-01-27 15:10:49'),
(285.00, 'emma.jones@gmail.com', 9,'2024-01-28 12:02:00'),
(315.00, 'john.doe@gmail.com', 9,'2024-01-28 12:02:00'),
(290.00, 'john.doe@gmail.com', 10,'2024-01-28 22:03:23'),
(220.00, 'noah.taylor@hotmail.co.uk', 11, '2024-01-29 11:45:00'),
(450.00, 'john.doe@gmail.com', 11, '2024-01-29 13:15:00'),
(300.00, 'emma.jones@gmail.com', 12, '2024-01-29 10:50:00'),
(340.00, 'john.doe@gmail.com', 12, '2024-01-29 12:20:00'),
(570.00, 'emma.jones@gmail.com', 13, '2024-01-29 23:25:00'),
(1050.00, 'olivia.smith@gmail.com', 14, '2024-01-29 12:40:00'),
(1200.00, 'john.doe@gmail.com', 14, '2024-01-29 14:10:00'),
(350.00, 'olivia.smith@gmail.com', 15, '2024-01-29 14:15:00');