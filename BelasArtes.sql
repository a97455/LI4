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
  [e-mail] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Leilao] (
	id int NOT NULL UNIQUE,
	data_inicio datetime2 NOT NULL,
	data_fim datetime2 NOT NULL,
	preco_inicial float NOT NULL,
	comprador_email varchar(40) NOT NULL,
	pintura_id int NOT NULL,
	id_estado int NOT NULL,
  CONSTRAINT [PK_LEILAO] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Licitacao] (
	id int NOT NULL UNIQUE,
	valor float NOT NULL,
	email_licitador varchar(40) NOT NULL,
	id_leilao int NOT NULL,
  CONSTRAINT [PK_LICITACAO] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Estado] (
	id int NOT NULL UNIQUE,
	nome varchar(40) NOT NULL,
  CONSTRAINT [PK_ESTADO] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Movimento_Artistico] (
	id int NOT NULL UNIQUE,
	nome varchar(40) NOT NULL,
  CONSTRAINT [PK_MOVIMENTO_ARTISTICO] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Pintura] (
	id int NOT NULL UNIQUE,
	nome varchar(40) NOT NULL,
	altura float NOT NULL,
	largura float NOT NULL,
	peso float NOT NULL,
	descricao varchar(100) NOT NULL,
	foto image NOT NULL,
	artista varchar(40) NOT NULL,
	ano_criacao int NOT NULL,
	original boolean NOT NULL,
	verificacao_autenticidade boolean NOT NULL,
	id_vendedor varchar(40) NOT NULL,
	id_movimento_artistico int NOT NULL,
  CONSTRAINT [PK_PINTURA] PRIMARY KEY CLUSTERED
  (
  [id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO


ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk0] FOREIGN KEY ([comprador_email]) REFERENCES [Utilizador]([e-mail])
ON UPDATE CASCADE
GO
ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk0]
GO
ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk1] FOREIGN KEY ([pintura_id]) REFERENCES [Pintura]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk1]
GO
ALTER TABLE [Leilao] WITH CHECK ADD CONSTRAINT [Leilao_fk2] FOREIGN KEY ([id_estado]) REFERENCES [Estado]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [Leilao] CHECK CONSTRAINT [Leilao_fk2]
GO


ALTER TABLE [Licitacao] WITH CHECK ADD CONSTRAINT [Licitacao_fk0] FOREIGN KEY ([email_licitador]) REFERENCES [Utilizador]([e-mail])
ON UPDATE CASCADE
GO
ALTER TABLE [Licitacao] CHECK CONSTRAINT [Licitacao_fk0]
GO
ALTER TABLE [Licitacao] WITH CHECK ADD CONSTRAINT [Licitacao_fk1] FOREIGN KEY ([id_leilao]) REFERENCES [Leilao]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [Licitacao] CHECK CONSTRAINT [Licitacao_fk1]
GO



ALTER TABLE [Pintura] WITH CHECK ADD CONSTRAINT [Pintura_fk0] FOREIGN KEY ([id_vendedor]) REFERENCES [Utilizador]([e-mail])
ON UPDATE CASCADE
GO
ALTER TABLE [Pintura] CHECK CONSTRAINT [Pintura_fk0]
GO
ALTER TABLE [Pintura] WITH CHECK ADD CONSTRAINT [Pintura_fk1] FOREIGN KEY ([id_movimento_artistico]) REFERENCES [Movimento_Artistico]([id])
ON UPDATE CASCADE
GO
ALTER TABLE [Pintura] CHECK CONSTRAINT [Pintura_fk1]
GO

