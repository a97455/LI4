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
        e_mail varchar(40) NOT NULL UNIQUE,
        telefone int NOT NULL,
        rua varchar(30) NOT NULL,
        localidade varchar(30) NOT NULL,
        cidade varchar(30) NOT NULL,
        codigo_postal varchar(30) NOT NULL,
        pais_de_residencia varchar(30) NOT NULL,
        IBAN varchar(30) NOT NULL UNIQUE,
        palavra_passe varchar(30) NOT NULL,
        CONSTRAINT [PK_UTILIZADOR] PRIMARY KEY CLUSTERED
        (
            [e_mail] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Movimento_Artistico')
BEGIN
    CREATE TABLE [Movimento_Artistico] (
        id int IDENTITY(1,1) NOT NULL UNIQUE,
        nome varchar(40) NOT NULL,
        CONSTRAINT [PK_MOVIMENTO_ARTISTICO] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Estado')
BEGIN
    CREATE TABLE [Estado] (
        id int IDENTITY(1,1) NOT NULL UNIQUE,
        nome varchar(40) NOT NULL,
        CONSTRAINT [PK_ESTADO] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Pintura')
BEGIN
    CREATE TABLE [Pintura] (
        id int IDENTITY(1,1) NOT NULL UNIQUE,
        nome varchar(40) NOT NULL,
        altura float NOT NULL,
        largura float NOT NULL,
        peso float NOT NULL,
        descricao varchar(100) NOT NULL,
        foto varbinary(max) NOT NULL,
        artista varchar(40) NOT NULL,
        ano_criacao int NOT NULL,
        original bit NOT NULL,
        verificacao_autenticidade bit NOT NULL,
        id_vendedor varchar(40),
        id_movimento_artistico int,
        CONSTRAINT [PK_PINTURA] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );

    ALTER TABLE [Pintura] WITH CHECK ADD CONSTRAINT [Pintura_fk0] FOREIGN KEY ([id_vendedor]) REFERENCES [Utilizador]([e_mail])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Pintura] CHECK CONSTRAINT [Pintura_fk0];

    ALTER TABLE [Pintura] WITH CHECK ADD CONSTRAINT [Pintura_fk1] FOREIGN KEY ([id_movimento_artistico]) REFERENCES [Movimento_Artistico]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Pintura] CHECK CONSTRAINT [Pintura_fk1];
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Leilao')
BEGIN
    CREATE TABLE [Leilao] (
        id int IDENTITY(1,1) NOT NULL UNIQUE,
        data_inicio datetime2 NOT NULL,
        data_fim datetime2 NOT NULL,
        preco_inicial float NOT NULL,
        comprador_email varchar(40),
        pintura_id int,
        id_estado int,
        CONSTRAINT [PK_LEILAO] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );

    ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk0] FOREIGN KEY ([comprador_email]) REFERENCES [Utilizador]([e_mail])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk0];

    ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk1] FOREIGN KEY ([pintura_id]) REFERENCES [Pintura]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk1];

    ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk2] FOREIGN KEY ([id_estado]) REFERENCES [Estado]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk2];
END
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Licitacao')
BEGIN
    CREATE TABLE [Licitacao] (
        id int IDENTITY(1,1) NOT NULL UNIQUE,
        valor float NOT NULL,
        email_licitador varchar(40),
        id_leilao int,
        CONSTRAINT [PK_LICITACAO] PRIMARY KEY CLUSTERED
        (
            [id] ASC
        ) WITH (IGNORE_DUP_KEY = OFF)
    );

    ALTER TABLE [Licitacao] WITH CHECK ADD CONSTRAINT [Licitacao_fk0] FOREIGN KEY ([email_licitador]) REFERENCES [Utilizador]([e_mail])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Licitacao] CHECK CONSTRAINT [Licitacao_fk0];

    ALTER TABLE [Licitacao] WITH CHECK ADD CONSTRAINT [Licitacao_fk1] FOREIGN KEY ([id_leilao]) REFERENCES [Leilao]([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION

    ALTER TABLE [Licitacao] CHECK CONSTRAINT [Licitacao_fk1];
END
GO