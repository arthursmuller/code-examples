﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [EMPRESA] (
        [ID_EMPRESA] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [NOME] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_EMPRESA] PRIMARY KEY ([ID_EMPRESA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [SITUACAO_ENVIO] (
        [ID_SITUACAO_ENVIO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SITUACAO_ENVIO] PRIMARY KEY ([ID_SITUACAO_ENVIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [SITUACAO_ENVIO_DETALHES] (
        [ID_SITUACAO_ENVIO_DETALHES] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SITUACAO_ENVIO_DETALHES] PRIMARY KEY ([ID_SITUACAO_ENVIO_DETALHES])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [EMAIL_FORNECEDOR] (
        [ID_EMAIL_FORNECEDOR] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [NOME_EXIBICAO] nvarchar(50) NULL,
        [USUARIO] varchar(50) NOT NULL,
        [SENHA] nvarchar(50) NOT NULL,
        [HOST] varchar(50) NOT NULL,
        [PORTA] int NOT NULL,
        [SSL] bit NOT NULL,
        [ID_EMPRESA] int NOT NULL,
        CONSTRAINT [PK_EMAIL_FORNECEDOR] PRIMARY KEY ([ID_EMAIL_FORNECEDOR]),
        CONSTRAINT [FK_EMAIL_FORNECEDOR_EMPRESA_ID_EMPRESA] FOREIGN KEY ([ID_EMPRESA]) REFERENCES [EMPRESA] ([ID_EMPRESA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [SMS_FORNECEDOR] (
        [ID_SMS_FORNECEDOR] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [NOME_EXIBICAO] nvarchar(50) NULL,
        [USUARIO] varchar(50) NOT NULL,
        [SENHA] nvarchar(50) NOT NULL,
        [CODIGO_AGRUPADOR] int NOT NULL,
        [ID_EMPRESA] int NOT NULL,
        CONSTRAINT [PK_SMS_FORNECEDOR] PRIMARY KEY ([ID_SMS_FORNECEDOR]),
        CONSTRAINT [FK_SMS_FORNECEDOR_EMPRESA_ID_EMPRESA] FOREIGN KEY ([ID_EMPRESA]) REFERENCES [EMPRESA] ([ID_EMPRESA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [EMAIL_MENSAGEM] (
        [ID_EMAIL_MENSAGEM] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [CODIGO_REFERENCIA_EMAIL] nvarchar(13) NOT NULL,
        [DESTINATARIO] nvarchar(max) NOT NULL,
        [COPIA] nvarchar(max) NULL,
        [ASSUNTO] nvarchar(100) NOT NULL,
        [MENSAGEM] nvarchar(max) NOT NULL,
        [PRIORITARIO] bit NOT NULL,
        [DataInsercao] datetime2 NOT NULL,
        [DATA_ENVIO] datetime2 NULL,
        [DATA_RECEBIMENTO] datetime2 NULL,
        [ID_EMAIL_FORNECEDOR] int NOT NULL,
        CONSTRAINT [PK_EMAIL_MENSAGEM] PRIMARY KEY ([ID_EMAIL_MENSAGEM]),
        CONSTRAINT [FK_EMAIL_MENSAGEM_EMAIL_FORNECEDOR_ID_EMAIL_FORNECEDOR] FOREIGN KEY ([ID_EMAIL_FORNECEDOR]) REFERENCES [EMAIL_FORNECEDOR] ([ID_EMAIL_FORNECEDOR])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE TABLE [SMS_MENSAGEM] (
        [ID_SMS_MENSAGEM] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [CODIGO_REFERENCIA_MENSAGEM] nvarchar(13) NOT NULL,
        [NUMERO_TELEFONE] nvarchar(14) NOT NULL,
        [MENSAGEM] nvarchar(max) NOT NULL,
        [OPERADORA] nvarchar(20) NULL,
        [PROCESSADO] bit NOT NULL,
        [DATA_INSERCAO] datetime2 NOT NULL,
        [DATA_ENVIO] datetime2 NULL,
        [DATA_RECEBIMENTO] datetime2 NULL,
        [ID_SMS_FORNECEDOR] int NOT NULL,
        [ID_SITUACAO_ENVIO] int NULL,
        [ID_SITUACAO_ENVIO_DETALHES] int NULL,
        CONSTRAINT [PK_SMS_MENSAGEM] PRIMARY KEY ([ID_SMS_MENSAGEM]),
        CONSTRAINT [FK_SMS_MENSAGEM_SITUACAO_ENVIO_ID_SITUACAO_ENVIO] FOREIGN KEY ([ID_SITUACAO_ENVIO]) REFERENCES [SITUACAO_ENVIO] ([ID_SITUACAO_ENVIO]),
        CONSTRAINT [FK_SMS_MENSAGEM_SITUACAO_ENVIO_DETALHES_ID_SITUACAO_ENVIO_DETALHES] FOREIGN KEY ([ID_SITUACAO_ENVIO_DETALHES]) REFERENCES [SITUACAO_ENVIO_DETALHES] ([ID_SITUACAO_ENVIO_DETALHES]),
        CONSTRAINT [FK_SMS_MENSAGEM_SMS_FORNECEDOR_ID_SMS_FORNECEDOR] FOREIGN KEY ([ID_SMS_FORNECEDOR]) REFERENCES [SMS_FORNECEDOR] ([ID_SMS_FORNECEDOR])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE INDEX [IX_EMAIL_FORNECEDOR_ID_EMPRESA] ON [EMAIL_FORNECEDOR] ([ID_EMPRESA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE INDEX [IX_EMAIL_MENSAGEM_ID_EMAIL_FORNECEDOR] ON [EMAIL_MENSAGEM] ([ID_EMAIL_FORNECEDOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE INDEX [IX_SMS_FORNECEDOR_ID_EMPRESA] ON [SMS_FORNECEDOR] ([ID_EMPRESA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE INDEX [IX_SMS_MENSAGEM_ID_SITUACAO_ENVIO] ON [SMS_MENSAGEM] ([ID_SITUACAO_ENVIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE INDEX [IX_SMS_MENSAGEM_ID_SITUACAO_ENVIO_DETALHES] ON [SMS_MENSAGEM] ([ID_SITUACAO_ENVIO_DETALHES]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    CREATE INDEX [IX_SMS_MENSAGEM_ID_SMS_FORNECEDOR] ON [SMS_MENSAGEM] ([ID_SMS_FORNECEDOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317202205_InitialMigrationEmailSms')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210317202205_InitialMigrationEmailSms', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210818182328_AddWhatsapp')
BEGIN
    CREATE TABLE [WHATSAPP_MENSAGEM] (
        [ID_WHATSAPP_MENSAGEM] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_TEMPLATE] uniqueidentifier NOT NULL,
        [DDD] varchar(3) NOT NULL,
        [TELEFONE] varchar(9) NOT NULL,
        [MENSAGEM_ENVIO] nvarchar(MAX) NOT NULL,
        [MENSAGEM_RETORNO_ERRO] varchar(4000) NULL,
        CONSTRAINT [PK_WHATSAPP_MENSAGEM] PRIMARY KEY ([ID_WHATSAPP_MENSAGEM])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210818182328_AddWhatsapp')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210818182328_AddWhatsapp', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210917134528_ToUpperColumn')
BEGIN
    EXEC sp_rename N'[EMAIL_MENSAGEM].[DataInsercao]', N'DATA_INSERCAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210917134528_ToUpperColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210917134528_ToUpperColumn', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WHATSAPP_MENSAGEM]') AND [c].[name] = N'DDD');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [WHATSAPP_MENSAGEM] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [WHATSAPP_MENSAGEM] DROP COLUMN [DDD];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WHATSAPP_MENSAGEM]') AND [c].[name] = N'TELEFONE');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [WHATSAPP_MENSAGEM] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [WHATSAPP_MENSAGEM] DROP COLUMN [TELEFONE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WHATSAPP_MENSAGEM]') AND [c].[name] = N'MENSAGEM_ENVIO');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [WHATSAPP_MENSAGEM] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [WHATSAPP_MENSAGEM] ALTER COLUMN [MENSAGEM_ENVIO] varchar(MAX) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    ALTER TABLE [WHATSAPP_MENSAGEM] ADD [CODIGO_REFERENCIA_MENSAGEM] varchar(13) NOT NULL DEFAULT '';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    ALTER TABLE [WHATSAPP_MENSAGEM] ADD [ID_WHATSAPP_FORNECEDOR] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    ALTER TABLE [WHATSAPP_MENSAGEM] ADD [NUMERO_TELEFONE] varchar(15) NOT NULL DEFAULT '';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE TABLE [TORPEDO_VOZ_FORNECEDOR] (
        [ID_TORPEDO_VOZ_FORNECEDOR] int NOT NULL IDENTITY,
        [NOME_EXIBICAO] nvarchar(20) NOT NULL,
        [CHAVE_ENVIO] nvarchar(50) NOT NULL,
        [ID_EMPRESA] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TORPEDO_VOZ_FORNECEDOR] PRIMARY KEY ([ID_TORPEDO_VOZ_FORNECEDOR]),
        CONSTRAINT [FK_TORPEDO_VOZ_FORNECEDOR_EMPRESA_ID_EMPRESA] FOREIGN KEY ([ID_EMPRESA]) REFERENCES [EMPRESA] ([ID_EMPRESA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE TABLE [WHATSAPP_FORNECEDOR] (
        [ID_WHATSAPP_FORNECEDOR] int NOT NULL IDENTITY,
        [NOME_EXIBICAO] nvarchar(50) NOT NULL,
        [CHAVE] nvarchar(100) NOT NULL,
        [ID_EMPRESA] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_WHATSAPP_FORNECEDOR] PRIMARY KEY ([ID_WHATSAPP_FORNECEDOR]),
        CONSTRAINT [FK_WHATSAPP_FORNECEDOR_EMPRESA_ID_EMPRESA] FOREIGN KEY ([ID_EMPRESA]) REFERENCES [EMPRESA] ([ID_EMPRESA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE TABLE [TORPEDO_VOZ_MENSAGEM] (
        [ID_TORPEDO_VOZ_MENSAGEM] int NOT NULL IDENTITY,
        [CODIGO_REFERENCIA_MENSAGEM] nvarchar(13) NOT NULL,
        [NUMERO_TELEFONE] varchar(12) NOT NULL,
        [MENSAGEM] varchar(200) NOT NULL,
        [SITUACAO] bit NOT NULL,
        [DATA_INSERCAO] datetime2 NOT NULL,
        [ID_TORPEDO_VOZ_FORNECEDOR] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TORPEDO_VOZ_MENSAGEM] PRIMARY KEY ([ID_TORPEDO_VOZ_MENSAGEM]),
        CONSTRAINT [FK_TORPEDO_VOZ_MENSAGEM_TORPEDO_VOZ_FORNECEDOR_ID_TORPEDO_VOZ_FORNECEDOR] FOREIGN KEY ([ID_TORPEDO_VOZ_FORNECEDOR]) REFERENCES [TORPEDO_VOZ_FORNECEDOR] ([ID_TORPEDO_VOZ_FORNECEDOR])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE INDEX [IX_WHATSAPP_MENSAGEM_ID_WHATSAPP_FORNECEDOR] ON [WHATSAPP_MENSAGEM] ([ID_WHATSAPP_FORNECEDOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE INDEX [IX_TORPEDO_VOZ_FORNECEDOR_ID_EMPRESA] ON [TORPEDO_VOZ_FORNECEDOR] ([ID_EMPRESA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE INDEX [IX_TORPEDO_VOZ_MENSAGEM_ID_TORPEDO_VOZ_FORNECEDOR] ON [TORPEDO_VOZ_MENSAGEM] ([ID_TORPEDO_VOZ_FORNECEDOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    CREATE INDEX [IX_WHATSAPP_FORNECEDOR_ID_EMPRESA] ON [WHATSAPP_FORNECEDOR] ([ID_EMPRESA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    ALTER TABLE [WHATSAPP_MENSAGEM] ADD CONSTRAINT [FK_WHATSAPP_MENSAGEM_WHATSAPP_FORNECEDOR_ID_WHATSAPP_FORNECEDOR] FOREIGN KEY ([ID_WHATSAPP_FORNECEDOR]) REFERENCES [WHATSAPP_FORNECEDOR] ([ID_WHATSAPP_FORNECEDOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220203122558_AddNovasComunicacoes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220203122558_AddNovasComunicacoes', N'5.0.3');
END;
GO

COMMIT;
GO

