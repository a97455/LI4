IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'cards')
BEGIN
    CREATE DATABASE cards;
END
GO

USE cards;
GO

DROP TABLE IF EXISTS cards;
CREATE TABLE cards ( 
id int PRIMARY KEY IDENTITY, 
name varchar(200) NOT NULL, 
description varchar(300), 
image varchar(max), 
date datetime NOT NULL, time_in_minutes int NOT NULL, 
image_action int)

-- Inserir o primeiro registro
INSERT INTO cards (name, description, image, date, time_in_minutes, image_action)
VALUES ('Carta 1', 'Descrição da Carta 1', 'caminho/imagem1.jpg', GETDATE(), 30, 1);

-- Inserir o segundo registro
INSERT INTO cards (name, description, image, date, time_in_minutes, image_action)
VALUES ('Carta 2', 'Descrição da Carta 2', 'caminho/imagem2.jpg', GETDATE(), 45, 2);