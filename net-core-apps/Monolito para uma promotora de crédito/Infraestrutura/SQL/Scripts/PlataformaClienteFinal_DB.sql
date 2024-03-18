IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720193724_PrimeiraMigraton')
BEGIN
    CREATE TABLE [LEADS] (
        [ID] int NOT NULL IDENTITY,
        [CPF] nvarchar(50) NULL,
        [Telefone] nvarchar(50) NULL,
        [Email] nvarchar(100) NULL,
        [Convenio] nvarchar(20) NULL,
        [Latitude] float NOT NULL,
        [Longitude] float NOT NULL,
        [EnderecoIp] nvarchar(50) NULL,
        [OrigemRequisicaoPalavraChave] nvarchar(max) NULL,
        [OrigemRequisicaoMidia] nvarchar(max) NULL,
        [OrigemRequisicaoConteudo] nvarchar(max) NULL,
        [OrigemRequisicaoTermo] nvarchar(max) NULL,
        [OrigemRequisicaoCampanha] nvarchar(max) NULL,
        CONSTRAINT [PK_LEADS] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720193724_PrimeiraMigraton')
BEGIN
    CREATE TABLE [LOJAS] (
        [ID] int NOT NULL IDENTITY,
        [Nome] nvarchar(100) NULL,
        [Latitude] float NOT NULL,
        [Longitude] float NOT NULL,
        [Endereco] nvarchar(255) NULL,
        [Cidade] nvarchar(100) NULL,
        [Estado] nvarchar(2) NULL,
        [Bairro] nvarchar(100) NULL,
        [Cep] nvarchar(8) NULL,
        CONSTRAINT [PK_LOJAS] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720193724_PrimeiraMigraton')
BEGIN
    CREATE TABLE [USUARIOS] (
        [ID] int NOT NULL IDENTITY,
        [Nome] nvarchar(100) NULL,
        [Email] nvarchar(100) NULL,
        [CPF] nvarchar(50) NULL,
        [Senha] nvarchar(50) NULL,
        CONSTRAINT [PK_USUARIOS] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720193724_PrimeiraMigraton')
BEGIN
    CREATE TABLE [TELEFONES_LOJAS] (
        [ID] int NOT NULL IDENTITY,
        [Telefone] nvarchar(100) NULL,
        [ID_LOJA] int NOT NULL,
        CONSTRAINT [PK_TELEFONES_LOJAS] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_TELEFONES_LOJAS_LOJAS_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJAS] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720193724_PrimeiraMigraton')
BEGIN
    CREATE INDEX [IX_TELEFONES_LOJAS_ID_LOJA] ON [TELEFONES_LOJAS] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720193724_PrimeiraMigraton')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200720193724_PrimeiraMigraton', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200723143438_AdicaoParametrosOperacao')
BEGIN
    CREATE TABLE [PARAMETROS_OPERACAO] (
        [ID] int NOT NULL IDENTITY,
        [InstituicaoFinanceira] nvarchar(100) NULL,
        [Convenio] nvarchar(100) NULL,
        [TipoOperacao] nvarchar(100) NULL,
        [QuantidadeParcelas] nvarchar(100) NULL,
        [TaxaPlano] nvarchar(100) NULL,
        [TentativaRetencao] bit NOT NULL,
        CONSTRAINT [PK_PARAMETROS_OPERACAO] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200723143438_AdicaoParametrosOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200723143438_AdicaoParametrosOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200728124616_CriacaoAnexos')
BEGIN
    CREATE TABLE [ANEXOS] (
        [ID] int NOT NULL IDENTITY,
        [Link] nvarchar(100) NULL,
        [Tipo] int NOT NULL,
        [IdUsuario] int NOT NULL,
        CONSTRAINT [PK_ANEXOS] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_ANEXOS_USUARIOS_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [USUARIOS] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200728124616_CriacaoAnexos')
BEGIN
    CREATE INDEX [IX_ANEXOS_IdUsuario] ON [ANEXOS] ([IdUsuario]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200728124616_CriacaoAnexos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200728124616_CriacaoAnexos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200728132312_MudancaMaxColunaLinkAnexo')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ANEXOS]') AND [c].[name] = N'Link');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ANEXOS] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ANEXOS] ALTER COLUMN [Link] nvarchar(255) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200728132312_MudancaMaxColunaLinkAnexo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200728132312_MudancaMaxColunaLinkAnexo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200731195545_RemocaoEnderecoIp')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEADS]') AND [c].[name] = N'EnderecoIp');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LEADS] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [LEADS] DROP COLUMN [EnderecoIp];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200731195545_RemocaoEnderecoIp')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ANEXOS]') AND [c].[name] = N'Link');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ANEXOS] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ANEXOS] ALTER COLUMN [Link] nvarchar(4000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200731195545_RemocaoEnderecoIp')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200731195545_RemocaoEnderecoIp', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813203017_CrudConvenioEProduto')
BEGIN
    CREATE TABLE [CONVENIOS] (
        [ID] int NOT NULL IDENTITY,
        [NOME] nvarchar(100) NOT NULL,
        [CODIGO] nvarchar(6) NOT NULL,
        CONSTRAINT [PK_CONVENIOS] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813203017_CrudConvenioEProduto')
BEGIN
    CREATE TABLE [PRODUTOS] (
        [ID] int NOT NULL IDENTITY,
        [NOME] nvarchar(100) NOT NULL,
        [SIGLA] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_PRODUTOS] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813203017_CrudConvenioEProduto')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200813203017_CrudConvenioEProduto', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234123_CrudTipoOperacao')
BEGIN
    ALTER TABLE [PRODUTOS] DROP CONSTRAINT [PK_PRODUTOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234123_CrudTipoOperacao')
BEGIN
    EXEC sp_rename N'[PRODUTOS]', N'TIPOS_OPERACAO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234123_CrudTipoOperacao')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPOS_OPERACAO]') AND [c].[name] = N'SIGLA');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [TIPOS_OPERACAO] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [TIPOS_OPERACAO] ALTER COLUMN [SIGLA] nvarchar(5) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234123_CrudTipoOperacao')
BEGIN
    ALTER TABLE [TIPOS_OPERACAO] ADD CONSTRAINT [PK_TIPOS_OPERACAO] PRIMARY KEY ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234123_CrudTipoOperacao')
BEGIN
    CREATE TABLE [TiposOperacao] (
        [ID] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Sigla] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TiposOperacao] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234123_CrudTipoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200813234123_CrudTipoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234857_CorrecaoCrudTipoOperacao')
BEGIN
    DROP TABLE [TiposOperacao];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234857_CorrecaoCrudTipoOperacao')
BEGIN
    CREATE TABLE [PRODUTOS] (
        [ID] int NOT NULL IDENTITY,
        [NOME] nvarchar(100) NOT NULL,
        [SIGLA] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_PRODUTOS] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813234857_CorrecaoCrudTipoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200813234857_CorrecaoCrudTipoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814154455_TesteParametrosOperacao')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PARAMETROS_OPERACAO]') AND [c].[name] = N'TipoOperacao');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [PARAMETROS_OPERACAO] DROP COLUMN [TipoOperacao];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814154455_TesteParametrosOperacao')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD [IdTipoOperacao] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814154455_TesteParametrosOperacao')
BEGIN
    CREATE INDEX [IX_PARAMETROS_OPERACAO_IdTipoOperacao] ON [PARAMETROS_OPERACAO] ([IdTipoOperacao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814154455_TesteParametrosOperacao')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD CONSTRAINT [FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao] FOREIGN KEY ([IdTipoOperacao]) REFERENCES [TIPOS_OPERACAO] ([ID]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814154455_TesteParametrosOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200814154455_TesteParametrosOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814200819_RelacionamentoParametrosOperacaoConvenio')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PARAMETROS_OPERACAO]') AND [c].[name] = N'Convenio');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [PARAMETROS_OPERACAO] DROP COLUMN [Convenio];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814200819_RelacionamentoParametrosOperacaoConvenio')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD [IdConvenio] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814200819_RelacionamentoParametrosOperacaoConvenio')
BEGIN
    CREATE INDEX [IX_PARAMETROS_OPERACAO_IdConvenio] ON [PARAMETROS_OPERACAO] ([IdConvenio]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814200819_RelacionamentoParametrosOperacaoConvenio')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD CONSTRAINT [FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio] FOREIGN KEY ([IdConvenio]) REFERENCES [CONVENIOS] ([ID]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200814200819_RelacionamentoParametrosOperacaoConvenio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200814200819_RelacionamentoParametrosOperacaoConvenio', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815004947_CorrecaoDeleteAction')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815004947_CorrecaoDeleteAction')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815004947_CorrecaoDeleteAction')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD CONSTRAINT [FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio] FOREIGN KEY ([IdConvenio]) REFERENCES [CONVENIOS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815004947_CorrecaoDeleteAction')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD CONSTRAINT [FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao] FOREIGN KEY ([IdTipoOperacao]) REFERENCES [TIPOS_OPERACAO] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815004947_CorrecaoDeleteAction')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200815004947_CorrecaoDeleteAction', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [FK_PARAMETROS_OPERACAO_CONVENIOS_IdConvenio];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_IdTipoOperacao];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[TentativaRetencao]', N'TENTATIVA_RETENCAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[TaxaPlano]', N'TAXA_PLANO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[QuantidadeParcelas]', N'QUANTIDADE_PARCELAS', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[InstituicaoFinanceira]', N'INSTITUICAO_FINANCEIRA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[IdTipoOperacao]', N'ID_TIPO_OPERACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[IdConvenio]', N'ID_CONVENIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[IX_PARAMETROS_OPERACAO_IdTipoOperacao]', N'IX_PARAMETROS_OPERACAO_ID_TIPO_OPERACAO', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO].[IX_PARAMETROS_OPERACAO_IdConvenio]', N'IX_PARAMETROS_OPERACAO_ID_CONVENIO', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PRODUTOS]') AND [c].[name] = N'SIGLA');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PRODUTOS] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [PRODUTOS] ALTER COLUMN [SIGLA] nvarchar(5) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD CONSTRAINT [FK_PARAMETROS_OPERACAO_CONVENIOS_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIOS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD CONSTRAINT [FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPOS_OPERACAO] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200815032915_AjusteNomeMaiusculo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200815032915_AjusteNomeMaiusculo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao')
BEGIN
    CREATE TABLE [SITUACOES_INTENCAO_OPERACAO] (
        [ID] int NOT NULL IDENTITY,
        [NOME] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SITUACOES_INTENCAO_OPERACAO] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao')
BEGIN
    CREATE TABLE [INTENCOES_OPERACAO] (
        [ID] int NOT NULL IDENTITY,
        [ID_TIPO_OPERACAO] int NOT NULL,
        [ID_SITUACAO] int NOT NULL,
        [ID_LOJA] int NULL,
        [PRESTACAO] decimal(18,2) NOT NULL,
        [VALOR_AUXILIO_FINANCEIRO] decimal(18,2) NOT NULL,
        [TAXA_MES] decimal(18,2) NOT NULL,
        [TAXA_ANO] decimal(18,2) NOT NULL,
        [VALOR_FINANCIADO] decimal(18,2) NOT NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_INTENCOES_OPERACAO] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_INTENCOES_OPERACAO_LOJAS_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJAS] ([ID]),
        CONSTRAINT [FK_INTENCOES_OPERACAO_SITUACOES_INTENCAO_OPERACAO_ID_SITUACAO] FOREIGN KEY ([ID_SITUACAO]) REFERENCES [SITUACOES_INTENCAO_OPERACAO] ([ID]),
        CONSTRAINT [FK_INTENCOES_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPOS_OPERACAO] ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao')
BEGIN
    CREATE INDEX [IX_INTENCOES_OPERACAO_ID_LOJA] ON [INTENCOES_OPERACAO] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao')
BEGIN
    CREATE INDEX [IX_INTENCOES_OPERACAO_ID_SITUACAO] ON [INTENCOES_OPERACAO] ([ID_SITUACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao')
BEGIN
    CREATE INDEX [IX_INTENCOES_OPERACAO_ID_TIPO_OPERACAO] ON [INTENCOES_OPERACAO] ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200825151456_AdicionadaIntencaoOperacaoESituacaoIntencaoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SITUACOES_INTENCAO_OPERACAO]') AND [c].[name] = N'NOME');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [SITUACOES_INTENCAO_OPERACAO] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [SITUACOES_INTENCAO_OPERACAO] ALTER COLUMN [NOME] nvarchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] ADD [ID_LEAD] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] ADD [ID_USUARIO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    CREATE INDEX [IX_INTENCOES_OPERACAO_ID_LEAD] ON [INTENCOES_OPERACAO] ([ID_LEAD]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    CREATE INDEX [IX_INTENCOES_OPERACAO_ID_USUARIO] ON [INTENCOES_OPERACAO] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] ADD CONSTRAINT [FK_INTENCOES_OPERACAO_LEADS_ID_LEAD] FOREIGN KEY ([ID_LEAD]) REFERENCES [LEADS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] ADD CONSTRAINT [FK_INTENCOES_OPERACAO_USUARIOS_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIOS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200825215043_AdicionadosLeadEUsuarioNaIntencaoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[USUARIOS].[Senha]', N'SENHA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[USUARIOS].[Nome]', N'NOME', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[USUARIOS].[Email]', N'EMAIL', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Nome]', N'NOME', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Longitude]', N'LONGITUDE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Latitude]', N'LATITUDE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Estado]', N'ESTADO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Endereco]', N'ENDERECO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Cidade]', N'CIDADE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Cep]', N'CEP', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LOJAS].[Bairro]', N'BAIRRO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[Telefone]', N'TELEFONE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[Longitude]', N'LONGITUDE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[Latitude]', N'LATITUDE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[Email]', N'EMAIL', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[Convenio]', N'CONVENIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[OrigemRequisicaoTermo]', N'ORIGEM_REQUISICAO_TERMO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[OrigemRequisicaoPalavraChave]', N'ORIGEM_REQUISICAO_PALAVRA_CHAVE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[OrigemRequisicaoMidia]', N'ORIGEM_REQUISICAO_MIDIA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[OrigemRequisicaoConteudo]', N'ORIGEM_REQUISICAO_CONTEUDO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    EXEC sp_rename N'[LEADS].[OrigemRequisicaoCampanha]', N'ORIGEM_REQUISICAO_CAMPANHA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    ALTER TABLE [USUARIOS] ADD [ID_LOJA] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    ALTER TABLE [LEADS] ADD [ID_LOJA] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    CREATE INDEX [IX_USUARIOS_ID_LOJA] ON [USUARIOS] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    CREATE INDEX [IX_LEADS_ID_LOJA] ON [LEADS] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    ALTER TABLE [LEADS] ADD CONSTRAINT [FK_LEADS_LOJAS_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJAS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    ALTER TABLE [USUARIOS] ADD CONSTRAINT [FK_USUARIOS_LOJAS_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJAS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200827150532_AdicionadoIdLojaNaLeadEUsuario')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200827150532_AdicionadoIdLojaNaLeadEUsuario', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200903155247_AdicionadaDataAtualizacaoLead')
BEGIN
    ALTER TABLE [LEADS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200903155247_AdicionadaDataAtualizacaoLead')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200903155247_AdicionadaDataAtualizacaoLead', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200904154042_AdicionadaNomeLead')
BEGIN
    ALTER TABLE [LEADS] ADD [Nome] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200904154042_AdicionadaNomeLead')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200904154042_AdicionadaNomeLead', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200904163554_AdicionadaNomeNaLead')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEADS]') AND [c].[name] = N'Nome');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [LEADS] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [LEADS] ALTER COLUMN [Nome] nvarchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200904163554_AdicionadaNomeNaLead')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200904163554_AdicionadaNomeNaLead', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEADS]') AND [c].[name] = N'CONVENIO');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [LEADS] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [LEADS] DROP COLUMN [CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    EXEC sp_rename N'[LEADS].[Nome]', N'NOME', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    ALTER TABLE [LEADS] ADD [ConvenioID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    ALTER TABLE [LEADS] ADD [ID_CONVENIO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    CREATE INDEX [IX_LEADS_ConvenioID] ON [LEADS] ([ConvenioID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    ALTER TABLE [LEADS] ADD CONSTRAINT [FK_LEADS_CONVENIOS_ConvenioID] FOREIGN KEY ([ConvenioID]) REFERENCES [CONVENIOS] ([ID]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205312_AtualizacaoConvenioLead')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200909205312_AtualizacaoConvenioLead', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205652_AtualizacaoChavesConvenioLead')
BEGIN
    ALTER TABLE [LEADS] DROP CONSTRAINT [FK_LEADS_CONVENIOS_ConvenioID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205652_AtualizacaoChavesConvenioLead')
BEGIN
    DROP INDEX [IX_LEADS_ConvenioID] ON [LEADS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205652_AtualizacaoChavesConvenioLead')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEADS]') AND [c].[name] = N'ConvenioID');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [LEADS] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [LEADS] DROP COLUMN [ConvenioID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205652_AtualizacaoChavesConvenioLead')
BEGIN
    CREATE INDEX [IX_LEADS_ID_CONVENIO] ON [LEADS] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205652_AtualizacaoChavesConvenioLead')
BEGIN
    ALTER TABLE [LEADS] ADD CONSTRAINT [FK_LEADS_CONVENIOS_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIOS] ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909205652_AtualizacaoChavesConvenioLead')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200909205652_AtualizacaoChavesConvenioLead', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    EXEC sp_rename N'[TELEFONES_LOJAS].[Telefone]', N'TELEFONE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    ALTER TABLE [TELEFONES_LOJAS] ADD [MENSAGEM_APRESENTACAO] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    ALTER TABLE [TELEFONES_LOJAS] ADD [POSSUI_CONTA_WHATSAPP] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    ALTER TABLE [LOJAS] ADD [MENSAGEM_APRESENTACAO] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    ALTER TABLE [LEADS] ADD [DESEJA_CONTATO_WHATSAPP] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    ALTER TABLE [LEADS] ADD [LINK_CONTATO_WHATSAPP_LOJA] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915212801_AdicionadasColunasParaWhatsApp')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200915212801_AdicionadasColunasParaWhatsApp', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915224659_LeadConvenioPermitirNull')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEADS]') AND [c].[name] = N'ID_CONVENIO');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [LEADS] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [LEADS] ALTER COLUMN [ID_CONVENIO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915224659_LeadConvenioPermitirNull')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200915224659_LeadConvenioPermitirNull', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [USUARIOS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [USUARIOS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [TIPOS_OPERACAO] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [TIPOS_OPERACAO] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [TELEFONES_LOJAS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [TELEFONES_LOJAS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [SITUACOES_INTENCAO_OPERACAO] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [SITUACOES_INTENCAO_OPERACAO] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [PRODUTOS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [PRODUTOS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [LOJAS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [LOJAS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [LEADS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [CONVENIOS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [CONVENIOS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [ANEXOS] ADD [DATA_ATUALIZACAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    ALTER TABLE [ANEXOS] ADD [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    CREATE TABLE [TiposConta] (
        [ID] int NOT NULL IDENTITY,
        [UsuarioAtualizacao] nvarchar(max) NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        [Nome] nvarchar(max) NULL,
        [Sigla] nvarchar(max) NULL,
        CONSTRAINT [PK_TiposConta] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    CREATE TABLE [TiposVinculoInsticional] (
        [ID] int NOT NULL IDENTITY,
        [UsuarioAtualizacao] nvarchar(max) NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        [Nome] nvarchar(max) NULL,
        CONSTRAINT [PK_TiposVinculoInsticional] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200918201733_InclusaoPropriedadesUsuarioAtualizacaoEDataAtualizacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    ALTER TABLE [TiposVinculoInsticional] DROP CONSTRAINT [PK_TiposVinculoInsticional];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    ALTER TABLE [TiposConta] DROP CONSTRAINT [PK_TiposConta];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TiposVinculoInsticional]', N'TIPOS_VINCULO_INSTITUCIONAL';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TiposConta]', N'TIPOS_CONTA';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_VINCULO_INSTITUCIONAL].[Nome]', N'NOME', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_VINCULO_INSTITUCIONAL].[UsuarioAtualizacao]', N'USUARIO_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_VINCULO_INSTITUCIONAL].[DataAtualizacao]', N'DATA_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_CONTA].[Sigla]', N'SIGLA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_CONTA].[Nome]', N'NOME', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_CONTA].[UsuarioAtualizacao]', N'USUARIO_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    EXEC sp_rename N'[TIPOS_CONTA].[DataAtualizacao]', N'DATA_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPOS_VINCULO_INSTITUCIONAL]') AND [c].[name] = N'NOME');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [TIPOS_VINCULO_INSTITUCIONAL] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [TIPOS_VINCULO_INSTITUCIONAL] ALTER COLUMN [NOME] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPOS_VINCULO_INSTITUCIONAL]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [TIPOS_VINCULO_INSTITUCIONAL] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [TIPOS_VINCULO_INSTITUCIONAL] ALTER COLUMN [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPOS_CONTA]') AND [c].[name] = N'SIGLA');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [TIPOS_CONTA] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [TIPOS_CONTA] ALTER COLUMN [SIGLA] nvarchar(2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPOS_CONTA]') AND [c].[name] = N'NOME');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [TIPOS_CONTA] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [TIPOS_CONTA] ALTER COLUMN [NOME] nvarchar(20) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPOS_CONTA]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [TIPOS_CONTA] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [TIPOS_CONTA] ALTER COLUMN [USUARIO_ATUALIZACAO] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    ALTER TABLE [TIPOS_VINCULO_INSTITUCIONAL] ADD CONSTRAINT [PK_TIPOS_VINCULO_INSTITUCIONAL] PRIMARY KEY ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    ALTER TABLE [TIPOS_CONTA] ADD CONSTRAINT [PK_TIPOS_CONTA] PRIMARY KEY ([ID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200918201958_InclusaoEntidadesTipoContaETipoVinculoInstitucional', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_ANEXOS_IdUsuario] ON [ANEXOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_INTENCOES_OPERACAO_ID_LEAD] ON [INTENCOES_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_INTENCOES_OPERACAO_ID_LOJA] ON [INTENCOES_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_INTENCOES_OPERACAO_ID_SITUACAO] ON [INTENCOES_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_INTENCOES_OPERACAO_ID_TIPO_OPERACAO] ON [INTENCOES_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_INTENCOES_OPERACAO_ID_USUARIO] ON [INTENCOES_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_LEADS_ID_CONVENIO] ON [LEADS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_LEADS_ID_LOJA] ON [LEADS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_PARAMETROS_OPERACAO_ID_CONVENIO] ON [PARAMETROS_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_PARAMETROS_OPERACAO_ID_TIPO_OPERACAO] ON [PARAMETROS_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_TELEFONES_LOJAS_ID_LOJA] ON [TELEFONES_LOJAS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    DROP INDEX [IX_USUARIOS_ID_LOJA] ON [USUARIOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200925200410_RemocaoDeTodosOsIndicesIX')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200925200410_RemocaoDeTodosOsIndicesIX', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [ANEXOS] DROP CONSTRAINT [FK_ANEXOS_USUARIOS_IdUsuario];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] DROP CONSTRAINT [FK_INTENCOES_OPERACAO_LEADS_ID_LEAD];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] DROP CONSTRAINT [FK_INTENCOES_OPERACAO_LOJAS_ID_LOJA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] DROP CONSTRAINT [FK_INTENCOES_OPERACAO_SITUACOES_INTENCAO_OPERACAO_ID_SITUACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] DROP CONSTRAINT [FK_INTENCOES_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] DROP CONSTRAINT [FK_INTENCOES_OPERACAO_USUARIOS_ID_USUARIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LEADS] DROP CONSTRAINT [FK_LEADS_CONVENIOS_ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LEADS] DROP CONSTRAINT [FK_LEADS_LOJAS_ID_LOJA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [FK_PARAMETROS_OPERACAO_CONVENIOS_ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [FK_PARAMETROS_OPERACAO_TIPOS_OPERACAO_ID_TIPO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TELEFONES_LOJAS] DROP CONSTRAINT [FK_TELEFONES_LOJAS_LOJAS_ID_LOJA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [USUARIOS] DROP CONSTRAINT [FK_USUARIOS_LOJAS_ID_LOJA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [USUARIOS] DROP CONSTRAINT [PK_USUARIOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TIPOS_VINCULO_INSTITUCIONAL] DROP CONSTRAINT [PK_TIPOS_VINCULO_INSTITUCIONAL];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TIPOS_OPERACAO] DROP CONSTRAINT [PK_TIPOS_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TIPOS_CONTA] DROP CONSTRAINT [PK_TIPOS_CONTA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TELEFONES_LOJAS] DROP CONSTRAINT [PK_TELEFONES_LOJAS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [SITUACOES_INTENCAO_OPERACAO] DROP CONSTRAINT [PK_SITUACOES_INTENCAO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PRODUTOS] DROP CONSTRAINT [PK_PRODUTOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PARAMETROS_OPERACAO] DROP CONSTRAINT [PK_PARAMETROS_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LOJAS] DROP CONSTRAINT [PK_LOJAS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LEADS] DROP CONSTRAINT [PK_LEADS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCOES_OPERACAO] DROP CONSTRAINT [PK_INTENCOES_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [CONVENIOS] DROP CONSTRAINT [PK_CONVENIOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [ANEXOS] DROP CONSTRAINT [PK_ANEXOS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[USUARIOS]', N'USUARIO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TIPOS_VINCULO_INSTITUCIONAL]', N'TIPO_VINCULO_INSTITUCIONAL';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TIPOS_OPERACAO]', N'TIPO_OPERACAO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TIPOS_CONTA]', N'TIPO_CONTA';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TELEFONES_LOJAS]', N'TELEFONE_LOJA';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[SITUACOES_INTENCAO_OPERACAO]', N'SITUACAO_INTENCAO_OPERACAO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[PRODUTOS]', N'PRODUTO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[PARAMETROS_OPERACAO]', N'PARAMETRO_OPERACAO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[LOJAS]', N'LOJA';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[LEADS]', N'LEAD';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[INTENCOES_OPERACAO]', N'INTENCAO_OPERACAO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[CONVENIOS]', N'CONVENIO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[ANEXOS]', N'ANEXO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[USUARIO].[ID]', N'ID_USUARIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TIPO_VINCULO_INSTITUCIONAL].[ID]', N'ID_TIPO_VINCULO_INSTITUCIONAL', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TIPO_OPERACAO].[ID]', N'ID_TIPO_OPERACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TIPO_CONTA].[ID]', N'ID_TIPO_CONTA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[TELEFONE_LOJA].[ID]', N'ID_TELEFONE_LOJA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[SITUACAO_INTENCAO_OPERACAO].[ID]', N'ID_SITUACAO_INTENCAO_OPERACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[PRODUTO].[ID]', N'ID_PRODUTO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[PARAMETRO_OPERACAO].[ID]', N'ID_PARAMETRO_OPERACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[LOJA].[ID]', N'ID_LOJA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[LEAD].[ID]', N'ID_LEAD', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[INTENCAO_OPERACAO].[ID]', N'ID_INTENCAO_OPERACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[CONVENIO].[ID]', N'ID_CONVENIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    EXEC sp_rename N'[ANEXO].[ID]', N'ID_ANEXO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [USUARIO] ADD CONSTRAINT [PK_USUARIO] PRIMARY KEY ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TIPO_VINCULO_INSTITUCIONAL] ADD CONSTRAINT [PK_TIPO_VINCULO_INSTITUCIONAL] PRIMARY KEY ([ID_TIPO_VINCULO_INSTITUCIONAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TIPO_OPERACAO] ADD CONSTRAINT [PK_TIPO_OPERACAO] PRIMARY KEY ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TIPO_CONTA] ADD CONSTRAINT [PK_TIPO_CONTA] PRIMARY KEY ([ID_TIPO_CONTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TELEFONE_LOJA] ADD CONSTRAINT [PK_TELEFONE_LOJA] PRIMARY KEY ([ID_TELEFONE_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ADD CONSTRAINT [PK_SITUACAO_INTENCAO_OPERACAO] PRIMARY KEY ([ID_SITUACAO_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PRODUTO] ADD CONSTRAINT [PK_PRODUTO] PRIMARY KEY ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] ADD CONSTRAINT [PK_PARAMETRO_OPERACAO] PRIMARY KEY ([ID_PARAMETRO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LOJA] ADD CONSTRAINT [PK_LOJA] PRIMARY KEY ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LEAD] ADD CONSTRAINT [PK_LEAD] PRIMARY KEY ([ID_LEAD]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [PK_INTENCAO_OPERACAO] PRIMARY KEY ([ID_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [CONVENIO] ADD CONSTRAINT [PK_CONVENIO] PRIMARY KEY ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [PK_ANEXO] PRIMARY KEY ([ID_ANEXO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [FK_ANEXO_USUARIO_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [USUARIO] ([ID_USUARIO]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_LEAD_ID_LEAD] FOREIGN KEY ([ID_LEAD]) REFERENCES [LEAD] ([ID_LEAD]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_LOJA_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJA] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO] FOREIGN KEY ([ID_SITUACAO]) REFERENCES [SITUACAO_INTENCAO_OPERACAO] ([ID_SITUACAO_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPO_OPERACAO] ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LEAD] ADD CONSTRAINT [FK_LEAD_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [LEAD] ADD CONSTRAINT [FK_LEAD_LOJA_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJA] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] ADD CONSTRAINT [FK_PARAMETRO_OPERACAO_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] ADD CONSTRAINT [FK_PARAMETRO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPO_OPERACAO] ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [TELEFONE_LOJA] ADD CONSTRAINT [FK_TELEFONE_LOJA_LOJA_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJA] ([ID_LOJA]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    ALTER TABLE [USUARIO] ADD CONSTRAINT [FK_USUARIO_LOJA_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJA] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930171604_AlteracaoNomeTabelaSingular')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200930171604_AlteracaoNomeTabelaSingular', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001120727_InclusaoUnidadesFederativas')
BEGIN
    CREATE TABLE [UNIDADE_FEDERATIVA] (
        [ID_UNIDADE_FEDERATIVA] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] nvarchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [NOME] nvarchar(20) NOT NULL,
        [SIGLA] nvarchar(2) NOT NULL,
        CONSTRAINT [PK_UNIDADE_FEDERATIVA] PRIMARY KEY ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201001120727_InclusaoUnidadesFederativas')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201001120727_InclusaoUnidadesFederativas', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002121919_InclusaoBancos')
BEGIN
    CREATE TABLE [BANCO] (
        [ID_BANCO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] nvarchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [NOME] nvarchar(60) NOT NULL,
        [CNPJ] nvarchar(14) NULL,
        [CODIGO] nvarchar(4) NOT NULL,
        CONSTRAINT [PK_BANCO] PRIMARY KEY ([ID_BANCO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002121919_InclusaoBancos')
BEGIN
    CREATE UNIQUE INDEX [IX_BANCO_CODIGO] ON [BANCO] ([CODIGO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201002121919_InclusaoBancos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201002121919_InclusaoBancos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005124747_IncluirOrgaosDeConvenio')
BEGIN
    CREATE TABLE [CONVENIO_ORGAO] (
        [ID_CONVENIO_ORGAO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] nvarchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [CODIGO] nvarchar(5) NOT NULL,
        [NOME] nvarchar(100) NOT NULL,
        [CNPJ] nvarchar(14) NULL,
        [ID_UNIDADE_FEDERATIVA] int NULL,
        CONSTRAINT [PK_CONVENIO_ORGAO] PRIMARY KEY ([ID_CONVENIO_ORGAO]),
        CONSTRAINT [FK_CONVENIO_ORGAO_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA] FOREIGN KEY ([ID_UNIDADE_FEDERATIVA]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005124747_IncluirOrgaosDeConvenio')
BEGIN
    CREATE UNIQUE INDEX [IX_CONVENIO_ORGAO_CODIGO] ON [CONVENIO_ORGAO] ([CODIGO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005124747_IncluirOrgaosDeConvenio')
BEGIN
    CREATE INDEX [IX_CONVENIO_ORGAO_ID_UNIDADE_FEDERATIVA] ON [CONVENIO_ORGAO] ([ID_UNIDADE_FEDERATIVA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005124747_IncluirOrgaosDeConvenio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005124747_IncluirOrgaosDeConvenio', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005182446_IncluirGrausInstrucaoEstadoCivil')
BEGIN
    CREATE TABLE [ESTADO_CIVIL] (
        [ID_ESTADO_CIVIL] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] nvarchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] nvarchar(30) NOT NULL,
        [SIGLA] nvarchar(1) NOT NULL,
        CONSTRAINT [PK_ESTADO_CIVIL] PRIMARY KEY ([ID_ESTADO_CIVIL])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005182446_IncluirGrausInstrucaoEstadoCivil')
BEGIN
    CREATE TABLE [GRAU_INSTRUCAO] (
        [ID_GRAU_INSTRUCAO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] nvarchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_GRAU_INSTRUCAO] PRIMARY KEY ([ID_GRAU_INSTRUCAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005182446_IncluirGrausInstrucaoEstadoCivil')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005182446_IncluirGrausInstrucaoEstadoCivil', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ANEXO]') AND [c].[name] = N'Tipo');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [ANEXO] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [ANEXO] DROP COLUMN [Tipo];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    EXEC sp_rename N'[ANEXO].[Link]', N'LINK', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    ALTER TABLE [ANEXO] ADD [ID_TIPO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    CREATE TABLE [TIPO_DOCUMENTO] (
        [ID_TIPO_DOCUMENTO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] nvarchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [NOME] nvarchar(30) NOT NULL,
        [CODIGO] nvarchar(7) NOT NULL,
        CONSTRAINT [PK_TIPO_DOCUMENTO] PRIMARY KEY ([ID_TIPO_DOCUMENTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    CREATE INDEX [IX_ANEXO_ID_TIPO] ON [ANEXO] ([ID_TIPO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO] FOREIGN KEY ([ID_TIPO]) REFERENCES [TIPO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005205547_AdicionarTipoAnexoDocumentoContext')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005205547_AdicionarTipoAnexoDocumentoContext', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201007171630_UpdateAnexoLinkVarchar')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ANEXO]') AND [c].[name] = N'LINK');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [ANEXO] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [ANEXO] ALTER COLUMN [LINK] varchar(4000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201007171630_UpdateAnexoLinkVarchar')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201007171630_UpdateAnexoLinkVarchar', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009023145_AddGeolocalizacaoLoja')
BEGIN
    ALTER TABLE [LOJA] ADD [GEOLOCALIZACAO] geography NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201009023145_AddGeolocalizacaoLoja')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201009023145_AddGeolocalizacaoLoja', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'SENHA');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [SENHA] varchar(50) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'NOME');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [NOME] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'EMAIL');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [EMAIL] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'CPF');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [CPF] varchar(50) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UNIDADE_FEDERATIVA]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [UNIDADE_FEDERATIVA] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [UNIDADE_FEDERATIVA] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UNIDADE_FEDERATIVA]') AND [c].[name] = N'SIGLA');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [UNIDADE_FEDERATIVA] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [UNIDADE_FEDERATIVA] ALTER COLUMN [SIGLA] varchar(2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UNIDADE_FEDERATIVA]') AND [c].[name] = N'NOME');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [UNIDADE_FEDERATIVA] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [UNIDADE_FEDERATIVA] ALTER COLUMN [NOME] varchar(40) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_VINCULO_INSTITUCIONAL]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_VINCULO_INSTITUCIONAL] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [TIPO_VINCULO_INSTITUCIONAL] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_VINCULO_INSTITUCIONAL]') AND [c].[name] = N'NOME');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_VINCULO_INSTITUCIONAL] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [TIPO_VINCULO_INSTITUCIONAL] ALTER COLUMN [NOME] varchar(8000) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_OPERACAO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_OPERACAO] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [TIPO_OPERACAO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_OPERACAO]') AND [c].[name] = N'SIGLA');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_OPERACAO] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [TIPO_OPERACAO] ALTER COLUMN [SIGLA] varchar(5) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_OPERACAO]') AND [c].[name] = N'NOME');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_OPERACAO] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [TIPO_OPERACAO] ALTER COLUMN [NOME] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_DOCUMENTO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_DOCUMENTO] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [TIPO_DOCUMENTO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_DOCUMENTO]') AND [c].[name] = N'NOME');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_DOCUMENTO] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [TIPO_DOCUMENTO] ALTER COLUMN [NOME] varchar(30) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var34 sysname;
    SELECT @var34 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_DOCUMENTO]') AND [c].[name] = N'CODIGO');
    IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_DOCUMENTO] DROP CONSTRAINT [' + @var34 + '];');
    ALTER TABLE [TIPO_DOCUMENTO] ALTER COLUMN [CODIGO] varchar(7) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var35 sysname;
    SELECT @var35 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_CONTA]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_CONTA] DROP CONSTRAINT [' + @var35 + '];');
    ALTER TABLE [TIPO_CONTA] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var36 sysname;
    SELECT @var36 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_CONTA]') AND [c].[name] = N'SIGLA');
    IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_CONTA] DROP CONSTRAINT [' + @var36 + '];');
    ALTER TABLE [TIPO_CONTA] ALTER COLUMN [SIGLA] varchar(2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var37 sysname;
    SELECT @var37 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_CONTA]') AND [c].[name] = N'NOME');
    IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_CONTA] DROP CONSTRAINT [' + @var37 + '];');
    ALTER TABLE [TIPO_CONTA] ALTER COLUMN [NOME] varchar(20) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var38 sysname;
    SELECT @var38 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TELEFONE_LOJA]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [TELEFONE_LOJA] DROP CONSTRAINT [' + @var38 + '];');
    ALTER TABLE [TELEFONE_LOJA] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var39 sysname;
    SELECT @var39 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TELEFONE_LOJA]') AND [c].[name] = N'TELEFONE');
    IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [TELEFONE_LOJA] DROP CONSTRAINT [' + @var39 + '];');
    ALTER TABLE [TELEFONE_LOJA] ALTER COLUMN [TELEFONE] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var40 sysname;
    SELECT @var40 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TELEFONE_LOJA]') AND [c].[name] = N'MENSAGEM_APRESENTACAO');
    IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [TELEFONE_LOJA] DROP CONSTRAINT [' + @var40 + '];');
    ALTER TABLE [TELEFONE_LOJA] ALTER COLUMN [MENSAGEM_APRESENTACAO] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var41 sysname;
    SELECT @var41 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SITUACAO_INTENCAO_OPERACAO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] DROP CONSTRAINT [' + @var41 + '];');
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var42 sysname;
    SELECT @var42 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SITUACAO_INTENCAO_OPERACAO]') AND [c].[name] = N'NOME');
    IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] DROP CONSTRAINT [' + @var42 + '];');
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ALTER COLUMN [NOME] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var43 sysname;
    SELECT @var43 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PRODUTO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [PRODUTO] DROP CONSTRAINT [' + @var43 + '];');
    ALTER TABLE [PRODUTO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var44 sysname;
    SELECT @var44 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PRODUTO]') AND [c].[name] = N'SIGLA');
    IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [PRODUTO] DROP CONSTRAINT [' + @var44 + '];');
    ALTER TABLE [PRODUTO] ALTER COLUMN [SIGLA] varchar(5) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var45 sysname;
    SELECT @var45 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PRODUTO]') AND [c].[name] = N'NOME');
    IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [PRODUTO] DROP CONSTRAINT [' + @var45 + '];');
    ALTER TABLE [PRODUTO] ALTER COLUMN [NOME] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var46 sysname;
    SELECT @var46 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PARAMETRO_OPERACAO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [PARAMETRO_OPERACAO] DROP CONSTRAINT [' + @var46 + '];');
    ALTER TABLE [PARAMETRO_OPERACAO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var47 sysname;
    SELECT @var47 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PARAMETRO_OPERACAO]') AND [c].[name] = N'TAXA_PLANO');
    IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [PARAMETRO_OPERACAO] DROP CONSTRAINT [' + @var47 + '];');
    ALTER TABLE [PARAMETRO_OPERACAO] ALTER COLUMN [TAXA_PLANO] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var48 sysname;
    SELECT @var48 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PARAMETRO_OPERACAO]') AND [c].[name] = N'QUANTIDADE_PARCELAS');
    IF @var48 IS NOT NULL EXEC(N'ALTER TABLE [PARAMETRO_OPERACAO] DROP CONSTRAINT [' + @var48 + '];');
    ALTER TABLE [PARAMETRO_OPERACAO] ALTER COLUMN [QUANTIDADE_PARCELAS] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var49 sysname;
    SELECT @var49 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PARAMETRO_OPERACAO]') AND [c].[name] = N'INSTITUICAO_FINANCEIRA');
    IF @var49 IS NOT NULL EXEC(N'ALTER TABLE [PARAMETRO_OPERACAO] DROP CONSTRAINT [' + @var49 + '];');
    ALTER TABLE [PARAMETRO_OPERACAO] ALTER COLUMN [INSTITUICAO_FINANCEIRA] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var50 sysname;
    SELECT @var50 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var50 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var50 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var51 sysname;
    SELECT @var51 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'NOME');
    IF @var51 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var51 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [NOME] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var52 sysname;
    SELECT @var52 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'MENSAGEM_APRESENTACAO');
    IF @var52 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var52 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [MENSAGEM_APRESENTACAO] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var53 sysname;
    SELECT @var53 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'ESTADO');
    IF @var53 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var53 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [ESTADO] varchar(2) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var54 sysname;
    SELECT @var54 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'ENDERECO');
    IF @var54 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var54 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [ENDERECO] varchar(255) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var55 sysname;
    SELECT @var55 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'CIDADE');
    IF @var55 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var55 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [CIDADE] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var56 sysname;
    SELECT @var56 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'CEP');
    IF @var56 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var56 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [CEP] varchar(8) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var57 sysname;
    SELECT @var57 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'BAIRRO');
    IF @var57 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var57 + '];');
    ALTER TABLE [LOJA] ALTER COLUMN [BAIRRO] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var58 sysname;
    SELECT @var58 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var58 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var58 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var59 sysname;
    SELECT @var59 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'TELEFONE');
    IF @var59 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var59 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [TELEFONE] varchar(50) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var60 sysname;
    SELECT @var60 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'ORIGEM_REQUISICAO_TERMO');
    IF @var60 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var60 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [ORIGEM_REQUISICAO_TERMO] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var61 sysname;
    SELECT @var61 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'ORIGEM_REQUISICAO_PALAVRA_CHAVE');
    IF @var61 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var61 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [ORIGEM_REQUISICAO_PALAVRA_CHAVE] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var62 sysname;
    SELECT @var62 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'ORIGEM_REQUISICAO_MIDIA');
    IF @var62 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var62 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [ORIGEM_REQUISICAO_MIDIA] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var63 sysname;
    SELECT @var63 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'ORIGEM_REQUISICAO_CONTEUDO');
    IF @var63 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var63 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [ORIGEM_REQUISICAO_CONTEUDO] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var64 sysname;
    SELECT @var64 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'ORIGEM_REQUISICAO_CAMPANHA');
    IF @var64 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var64 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [ORIGEM_REQUISICAO_CAMPANHA] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var65 sysname;
    SELECT @var65 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'NOME');
    IF @var65 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var65 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [NOME] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var66 sysname;
    SELECT @var66 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'LINK_CONTATO_WHATSAPP_LOJA');
    IF @var66 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var66 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [LINK_CONTATO_WHATSAPP_LOJA] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var67 sysname;
    SELECT @var67 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'EMAIL');
    IF @var67 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var67 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [EMAIL] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var68 sysname;
    SELECT @var68 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LEAD]') AND [c].[name] = N'CPF');
    IF @var68 IS NOT NULL EXEC(N'ALTER TABLE [LEAD] DROP CONSTRAINT [' + @var68 + '];');
    ALTER TABLE [LEAD] ALTER COLUMN [CPF] varchar(50) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var69 sysname;
    SELECT @var69 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[INTENCAO_OPERACAO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var69 IS NOT NULL EXEC(N'ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [' + @var69 + '];');
    ALTER TABLE [INTENCAO_OPERACAO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var70 sysname;
    SELECT @var70 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GRAU_INSTRUCAO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var70 IS NOT NULL EXEC(N'ALTER TABLE [GRAU_INSTRUCAO] DROP CONSTRAINT [' + @var70 + '];');
    ALTER TABLE [GRAU_INSTRUCAO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var71 sysname;
    SELECT @var71 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GRAU_INSTRUCAO]') AND [c].[name] = N'DESCRICAO');
    IF @var71 IS NOT NULL EXEC(N'ALTER TABLE [GRAU_INSTRUCAO] DROP CONSTRAINT [' + @var71 + '];');
    ALTER TABLE [GRAU_INSTRUCAO] ALTER COLUMN [DESCRICAO] varchar(30) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var72 sysname;
    SELECT @var72 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ESTADO_CIVIL]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var72 IS NOT NULL EXEC(N'ALTER TABLE [ESTADO_CIVIL] DROP CONSTRAINT [' + @var72 + '];');
    ALTER TABLE [ESTADO_CIVIL] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var73 sysname;
    SELECT @var73 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ESTADO_CIVIL]') AND [c].[name] = N'SIGLA');
    IF @var73 IS NOT NULL EXEC(N'ALTER TABLE [ESTADO_CIVIL] DROP CONSTRAINT [' + @var73 + '];');
    ALTER TABLE [ESTADO_CIVIL] ALTER COLUMN [SIGLA] varchar(1) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var74 sysname;
    SELECT @var74 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ESTADO_CIVIL]') AND [c].[name] = N'DESCRICAO');
    IF @var74 IS NOT NULL EXEC(N'ALTER TABLE [ESTADO_CIVIL] DROP CONSTRAINT [' + @var74 + '];');
    ALTER TABLE [ESTADO_CIVIL] ALTER COLUMN [DESCRICAO] varchar(30) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var75 sysname;
    SELECT @var75 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO_ORGAO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var75 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO_ORGAO] DROP CONSTRAINT [' + @var75 + '];');
    ALTER TABLE [CONVENIO_ORGAO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var76 sysname;
    SELECT @var76 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO_ORGAO]') AND [c].[name] = N'NOME');
    IF @var76 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO_ORGAO] DROP CONSTRAINT [' + @var76 + '];');
    ALTER TABLE [CONVENIO_ORGAO] ALTER COLUMN [NOME] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DROP INDEX [IX_CONVENIO_ORGAO_CODIGO] ON [CONVENIO_ORGAO];
    DECLARE @var77 sysname;
    SELECT @var77 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO_ORGAO]') AND [c].[name] = N'CODIGO');
    IF @var77 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO_ORGAO] DROP CONSTRAINT [' + @var77 + '];');
    ALTER TABLE [CONVENIO_ORGAO] ALTER COLUMN [CODIGO] varchar(5) NOT NULL;
    CREATE UNIQUE INDEX [IX_CONVENIO_ORGAO_CODIGO] ON [CONVENIO_ORGAO] ([CODIGO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var78 sysname;
    SELECT @var78 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO_ORGAO]') AND [c].[name] = N'CNPJ');
    IF @var78 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO_ORGAO] DROP CONSTRAINT [' + @var78 + '];');
    ALTER TABLE [CONVENIO_ORGAO] ALTER COLUMN [CNPJ] varchar(14) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var79 sysname;
    SELECT @var79 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var79 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO] DROP CONSTRAINT [' + @var79 + '];');
    ALTER TABLE [CONVENIO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var80 sysname;
    SELECT @var80 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO]') AND [c].[name] = N'NOME');
    IF @var80 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO] DROP CONSTRAINT [' + @var80 + '];');
    ALTER TABLE [CONVENIO] ALTER COLUMN [NOME] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var81 sysname;
    SELECT @var81 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO]') AND [c].[name] = N'CODIGO');
    IF @var81 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO] DROP CONSTRAINT [' + @var81 + '];');
    ALTER TABLE [CONVENIO] ALTER COLUMN [CODIGO] varchar(6) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var82 sysname;
    SELECT @var82 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BANCO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var82 IS NOT NULL EXEC(N'ALTER TABLE [BANCO] DROP CONSTRAINT [' + @var82 + '];');
    ALTER TABLE [BANCO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var83 sysname;
    SELECT @var83 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BANCO]') AND [c].[name] = N'NOME');
    IF @var83 IS NOT NULL EXEC(N'ALTER TABLE [BANCO] DROP CONSTRAINT [' + @var83 + '];');
    ALTER TABLE [BANCO] ALTER COLUMN [NOME] varchar(60) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DROP INDEX [IX_BANCO_CODIGO] ON [BANCO];
    DECLARE @var84 sysname;
    SELECT @var84 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BANCO]') AND [c].[name] = N'CODIGO');
    IF @var84 IS NOT NULL EXEC(N'ALTER TABLE [BANCO] DROP CONSTRAINT [' + @var84 + '];');
    ALTER TABLE [BANCO] ALTER COLUMN [CODIGO] varchar(4) NOT NULL;
    CREATE UNIQUE INDEX [IX_BANCO_CODIGO] ON [BANCO] ([CODIGO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var85 sysname;
    SELECT @var85 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BANCO]') AND [c].[name] = N'CNPJ');
    IF @var85 IS NOT NULL EXEC(N'ALTER TABLE [BANCO] DROP CONSTRAINT [' + @var85 + '];');
    ALTER TABLE [BANCO] ALTER COLUMN [CNPJ] varchar(14) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    DECLARE @var86 sysname;
    SELECT @var86 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ANEXO]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var86 IS NOT NULL EXEC(N'ALTER TABLE [ANEXO] DROP CONSTRAINT [' + @var86 + '];');
    ALTER TABLE [ANEXO] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133932_AtualizacaoUnicodeStrings')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201013133932_AtualizacaoUnicodeStrings', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013211850_IncluirClienteStubTelefones')
BEGIN
    CREATE TABLE [CLIENTE] (
        [ID_CLIENTE] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_CLIENTE] PRIMARY KEY ([ID_CLIENTE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013211850_IncluirClienteStubTelefones')
BEGIN
    CREATE TABLE [TELEFONE_CLIENTE] (
        [ID_TELEFONE_CLIENTE] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DDD] varchar(3) NOT NULL,
        [FONE] varchar(9) NOT NULL,
        [ID_CLIENTE] int NOT NULL,
        [DELETADO] bit NOT NULL,
        [PRINCIPAL] bit NOT NULL,
        CONSTRAINT [PK_TELEFONE_CLIENTE] PRIMARY KEY ([ID_TELEFONE_CLIENTE]),
        CONSTRAINT [FK_TELEFONE_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013211850_IncluirClienteStubTelefones')
BEGIN
    CREATE INDEX [IX_TELEFONE_CLIENTE_ID_CLIENTE] ON [TELEFONE_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013211850_IncluirClienteStubTelefones')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201013211850_IncluirClienteStubTelefones', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014190329_AddOrgaoEmissorIdentificacao')
BEGIN
    CREATE TABLE [ORGAO_EMISSOR_IDENTIFICACAO] (
        [ID_ORGAO_EMISSOR_IDENTIFICACAO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [CODIGO] varchar(10) NOT NULL,
        [DESCRICAO] varchar(50) NOT NULL,
        CONSTRAINT [PK_ORGAO_EMISSOR_IDENTIFICACAO] PRIMARY KEY ([ID_ORGAO_EMISSOR_IDENTIFICACAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201014190329_AddOrgaoEmissorIdentificacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201014190329_AddOrgaoEmissorIdentificacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015145224_AddGeneros')
BEGIN
    CREATE TABLE [GENERO] (
        [ID_GENERO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] varchar(30) NOT NULL,
        [SIGLA] varchar(1) NOT NULL,
        CONSTRAINT [PK_GENERO] PRIMARY KEY ([ID_GENERO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015145224_AddGeneros')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201015145224_AddGeneros', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015224440_AddMunicipios')
BEGIN
    CREATE TABLE [MUNICIPIO] (
        [ID_MUNICIPIO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] varchar(40) NOT NULL,
        [ID_UNIDADE_FEDERATIVA] int NOT NULL,
        CONSTRAINT [PK_MUNICIPIO] PRIMARY KEY ([ID_MUNICIPIO]),
        CONSTRAINT [FK_MUNICIPIO_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA] FOREIGN KEY ([ID_UNIDADE_FEDERATIVA]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015224440_AddMunicipios')
BEGIN
    CREATE INDEX [IX_MUNICIPIO_ID_UNIDADE_FEDERATIVA] ON [MUNICIPIO] ([ID_UNIDADE_FEDERATIVA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201015224440_AddMunicipios')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201015224440_AddMunicipios', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    CREATE TABLE [DOCUMENTO_IDENTIFICACAO_CLIENTE] (
        [ID_DOCUMENTO_IDENTIFICACAO_CLIENTE] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_CLIENTE] int NOT NULL,
        [ID_TIPO_DOCUMENTO] int NOT NULL,
        [ID_ORGAO_EMISSOR] int NOT NULL,
        [ID_UNIDADE_FEDERATIVA] int NOT NULL,
        [NUMERO] varchar(30) NULL,
        [DATA_EMISSAO] datetime2 NOT NULL,
        [DELETADO] bit NOT NULL,
        CONSTRAINT [PK_DOCUMENTO_IDENTIFICACAO_CLIENTE] PRIMARY KEY ([ID_DOCUMENTO_IDENTIFICACAO_CLIENTE]),
        CONSTRAINT [FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]),
        CONSTRAINT [FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_ORGAO_EMISSOR_IDENTIFICACAO_ID_ORGAO_EMISSOR] FOREIGN KEY ([ID_ORGAO_EMISSOR]) REFERENCES [ORGAO_EMISSOR_IDENTIFICACAO] ([ID_ORGAO_EMISSOR_IDENTIFICACAO]),
        CONSTRAINT [FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO] FOREIGN KEY ([ID_TIPO_DOCUMENTO]) REFERENCES [TIPO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]),
        CONSTRAINT [FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA] FOREIGN KEY ([ID_UNIDADE_FEDERATIVA]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    CREATE INDEX [IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_CLIENTE] ON [DOCUMENTO_IDENTIFICACAO_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    CREATE INDEX [IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_ORGAO_EMISSOR] ON [DOCUMENTO_IDENTIFICACAO_CLIENTE] ([ID_ORGAO_EMISSOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    CREATE INDEX [IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_TIPO_DOCUMENTO] ON [DOCUMENTO_IDENTIFICACAO_CLIENTE] ([ID_TIPO_DOCUMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    CREATE INDEX [IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_ID_UNIDADE_FEDERATIVA] ON [DOCUMENTO_IDENTIFICACAO_CLIENTE] ([ID_UNIDADE_FEDERATIVA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_NUMERO_ID_TIPO_DOCUMENTO_ID_UNIDADE_FEDERATIVA] ON [DOCUMENTO_IDENTIFICACAO_CLIENTE] ([NUMERO], [ID_TIPO_DOCUMENTO], [ID_UNIDADE_FEDERATIVA]) WHERE [NUMERO] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201025230738_AddDocumentoIdentificacaoCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201025230738_AddDocumentoIdentificacaoCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027150315_AddDescricaoEmConvenio')
BEGIN
    ALTER TABLE [CONVENIO] ADD [DESCRICAO] varchar(150) NOT NULL DEFAULT '';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201027150315_AddDescricaoEmConvenio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201027150315_AddDescricaoEmConvenio', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028133355_TiposLogradouros')
BEGIN
    CREATE TABLE [TIPO_LOGRADOURO] (
        [ID_TIPO_LOGRADOURO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] varchar(40) NOT NULL,
        [CODIGO] varchar(12) NOT NULL,
        CONSTRAINT [PK_TIPO_LOGRADOURO] PRIMARY KEY ([ID_TIPO_LOGRADOURO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201028133355_TiposLogradouros')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201028133355_TiposLogradouros', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029140210_BaseCEP')
BEGIN
    CREATE TABLE [BASE_CEP] (
        [ID_BASE_CEP] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [CEP] varchar(8) NOT NULL,
        [LOGRADOURO] varchar(100) NOT NULL,
        [BAIRRO] varchar(100) NOT NULL,
        [INFORMACAO_ADICIONAL] varchar(100) NULL,
        [PERMITE_AJUSTE_LOGRADOURO] bit NOT NULL,
        [IdTipoLogradouro] int NOT NULL,
        [IdMunicipio] int NOT NULL,
        [IdUnidadeFederativa] int NOT NULL,
        CONSTRAINT [PK_BASE_CEP] PRIMARY KEY ([ID_BASE_CEP]),
        CONSTRAINT [FK_BASE_CEP_MUNICIPIO_IdMunicipio] FOREIGN KEY ([IdMunicipio]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO]),
        CONSTRAINT [FK_BASE_CEP_TIPO_LOGRADOURO_IdTipoLogradouro] FOREIGN KEY ([IdTipoLogradouro]) REFERENCES [TIPO_LOGRADOURO] ([ID_TIPO_LOGRADOURO]),
        CONSTRAINT [FK_BASE_CEP_UNIDADE_FEDERATIVA_IdUnidadeFederativa] FOREIGN KEY ([IdUnidadeFederativa]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029140210_BaseCEP')
BEGIN
    CREATE UNIQUE INDEX [IX_BASE_CEP_CEP] ON [BASE_CEP] ([CEP]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029140210_BaseCEP')
BEGIN
    CREATE INDEX [IX_BASE_CEP_IdMunicipio] ON [BASE_CEP] ([IdMunicipio]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029140210_BaseCEP')
BEGIN
    CREATE INDEX [IX_BASE_CEP_IdTipoLogradouro] ON [BASE_CEP] ([IdTipoLogradouro]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029140210_BaseCEP')
BEGIN
    CREATE INDEX [IX_BASE_CEP_IdUnidadeFederativa] ON [BASE_CEP] ([IdUnidadeFederativa]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029140210_BaseCEP')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201029140210_BaseCEP', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029141737_InssEspecieBeneficiosESiapeFucionalTipos')
BEGIN
    CREATE TABLE [INSS_ESPECIE_BENEFICIO] (
        [ID_INSS_ESPECIE_BENEFICIO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] varchar(200) NOT NULL,
        [CODIGO] varchar(3) NOT NULL,
        CONSTRAINT [PK_INSS_ESPECIE_BENEFICIO] PRIMARY KEY ([ID_INSS_ESPECIE_BENEFICIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029141737_InssEspecieBeneficiosESiapeFucionalTipos')
BEGIN
    CREATE TABLE [SIAPE_TIPO_FUNCIONAL] (
        [ID_SIAPE_TIPO_FUNCIONAL] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [DESCRICAO] varchar(60) NOT NULL,
        [CODIGO] varchar(1) NOT NULL,
        CONSTRAINT [PK_SIAPE_TIPO_FUNCIONAL] PRIMARY KEY ([ID_SIAPE_TIPO_FUNCIONAL])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201029141737_InssEspecieBeneficiosESiapeFucionalTipos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201029141737_InssEspecieBeneficiosESiapeFucionalTipos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [DATA_NASCIMENTO] date NOT NULL DEFAULT '0001-01-01';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [DEFICIENTE_VISUAL] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [EMAIL] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [FILIACAO1] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [FILIACAO2] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_CIDADE_NATAL] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_ESTADO_CIVIL] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_GENERO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_GRAU_INSTRUCAO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [IdUsuario] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [NOME] varchar(100) NOT NULL DEFAULT '';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_CIDADE_NATAL] ON [CLIENTE] ([ID_CIDADE_NATAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_ESTADO_CIVIL] ON [CLIENTE] ([ID_ESTADO_CIVIL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_GENERO] ON [CLIENTE] ([ID_GENERO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_GRAU_INSTRUCAO] ON [CLIENTE] ([ID_GRAU_INSTRUCAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    CREATE INDEX [IX_CLIENTE_IdUsuario] ON [CLIENTE] ([IdUsuario]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_MUNICIPIO_ID_CIDADE_NATAL] FOREIGN KEY ([ID_CIDADE_NATAL]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_ESTADO_CIVIL_ID_ESTADO_CIVIL] FOREIGN KEY ([ID_ESTADO_CIVIL]) REFERENCES [ESTADO_CIVIL] ([ID_ESTADO_CIVIL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_GENERO_ID_GENERO] FOREIGN KEY ([ID_GENERO]) REFERENCES [GENERO] ([ID_GENERO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_GRAU_INSTRUCAO_ID_GRAU_INSTRUCAO] FOREIGN KEY ([ID_GRAU_INSTRUCAO]) REFERENCES [GRAU_INSTRUCAO] ([ID_GRAU_INSTRUCAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_USUARIO_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [USUARIO] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174639_AddDadosBasicosCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201030174639_AddDadosBasicosCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174941_AddClienteIdUsuarioMaiusculo')
BEGIN
    ALTER TABLE [CLIENTE] DROP CONSTRAINT [FK_CLIENTE_USUARIO_IdUsuario];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174941_AddClienteIdUsuarioMaiusculo')
BEGIN
    EXEC sp_rename N'[CLIENTE].[IdUsuario]', N'ID_USUARIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174941_AddClienteIdUsuarioMaiusculo')
BEGIN
    EXEC sp_rename N'[CLIENTE].[IX_CLIENTE_IdUsuario]', N'IX_CLIENTE_ID_USUARIO', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174941_AddClienteIdUsuarioMaiusculo')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201030174941_AddClienteIdUsuarioMaiusculo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201030174941_AddClienteIdUsuarioMaiusculo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106181732_AddEnderecoCliente')
BEGIN
    CREATE TABLE [ENDERECO_CLIENTE] (
        [ID_ENDERECO_CLIENTE] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_CLIENTE] int NOT NULL,
        [TITULO] varchar(100) NULL,
        [ENDERECO] varchar(255) NULL,
        [ID_MUNICIPIO] int NOT NULL,
        [BAIRRO] varchar(100) NULL,
        [CEP] varchar(8) NULL,
        [DELETADO] bit NOT NULL,
        [PRINCIPAL] bit NOT NULL,
        CONSTRAINT [PK_ENDERECO_CLIENTE] PRIMARY KEY ([ID_ENDERECO_CLIENTE]),
        CONSTRAINT [FK_ENDERECO_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]),
        CONSTRAINT [FK_ENDERECO_CLIENTE_MUNICIPIO_ID_MUNICIPIO] FOREIGN KEY ([ID_MUNICIPIO]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106181732_AddEnderecoCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201106181732_AddEnderecoCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    DECLARE @var87 sysname;
    SELECT @var87 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ENDERECO_CLIENTE]') AND [c].[name] = N'ENDERECO');
    IF @var87 IS NOT NULL EXEC(N'ALTER TABLE [ENDERECO_CLIENTE] DROP CONSTRAINT [' + @var87 + '];');
    ALTER TABLE [ENDERECO_CLIENTE] DROP COLUMN [ENDERECO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    ALTER TABLE [ENDERECO_CLIENTE] ADD [COMPLEMENTO] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    ALTER TABLE [ENDERECO_CLIENTE] ADD [ID_TIPO_LOGRADOURO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    ALTER TABLE [ENDERECO_CLIENTE] ADD [LOGRADOURO] varchar(255) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    ALTER TABLE [ENDERECO_CLIENTE] ADD [NUMERO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    CREATE INDEX [IX_ENDERECO_CLIENTE_ID_TIPO_LOGRADOURO] ON [ENDERECO_CLIENTE] ([ID_TIPO_LOGRADOURO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    ALTER TABLE [ENDERECO_CLIENTE] ADD CONSTRAINT [FK_ENDERECO_CLIENTE_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO] FOREIGN KEY ([ID_TIPO_LOGRADOURO]) REFERENCES [TIPO_LOGRADOURO] ([ID_TIPO_LOGRADOURO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201115030359_AtualizaEnderecoCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201115030359_AtualizaEnderecoCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201116185124_AjusteFKUsuarioEmCliente')
BEGIN
    DROP INDEX [IX_CLIENTE_ID_USUARIO] ON [CLIENTE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201116185124_AjusteFKUsuarioEmCliente')
BEGIN
    CREATE UNIQUE INDEX [IX_CLIENTE_ID_USUARIO] ON [CLIENTE] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201116185124_AjusteFKUsuarioEmCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201116185124_AjusteFKUsuarioEmCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    DECLARE @var88 sysname;
    SELECT @var88 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'ID_GRAU_INSTRUCAO');
    IF @var88 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var88 + '];');
    ALTER TABLE [CLIENTE] ALTER COLUMN [ID_GRAU_INSTRUCAO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    DECLARE @var89 sysname;
    SELECT @var89 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'ID_GENERO');
    IF @var89 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var89 + '];');
    ALTER TABLE [CLIENTE] ALTER COLUMN [ID_GENERO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    DECLARE @var90 sysname;
    SELECT @var90 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'ID_ESTADO_CIVIL');
    IF @var90 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var90 + '];');
    ALTER TABLE [CLIENTE] ALTER COLUMN [ID_ESTADO_CIVIL] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    DECLARE @var91 sysname;
    SELECT @var91 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'ID_CIDADE_NATAL');
    IF @var91 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var91 + '];');
    ALTER TABLE [CLIENTE] ALTER COLUMN [ID_CIDADE_NATAL] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    DECLARE @var92 sysname;
    SELECT @var92 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'DEFICIENTE_VISUAL');
    IF @var92 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var92 + '];');
    ALTER TABLE [CLIENTE] ALTER COLUMN [DEFICIENTE_VISUAL] bit NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    DECLARE @var93 sysname;
    SELECT @var93 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'DATA_NASCIMENTO');
    IF @var93 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var93 + '];');
    ALTER TABLE [CLIENTE] ALTER COLUMN [DATA_NASCIMENTO] date NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117025120_AtualizacaoCamposNulable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117025120_AtualizacaoCamposNulable', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117200630_AddIndiceIdUsarioEmCliente')
BEGIN
    DECLARE @var94 sysname;
    SELECT @var94 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'SENHA');
    IF @var94 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var94 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [SENHA] varchar(50) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117200630_AddIndiceIdUsarioEmCliente')
BEGIN
    DECLARE @var95 sysname;
    SELECT @var95 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'NOME');
    IF @var95 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var95 + '];');
    ALTER TABLE [USUARIO] ALTER COLUMN [NOME] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117200630_AddIndiceIdUsarioEmCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117200630_AddIndiceIdUsarioEmCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE] (
        [ID_RENDIMENTO_CLIENTE] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_CLIENTE] int NOT NULL,
        [ID_CONVENIO] int NOT NULL,
        [ID_CONVENIO_ORGAO] int NOT NULL,
        [ID_UF] int NOT NULL,
        [ID_BANCO] int NOT NULL,
        [ID_TIPO_CONTA] int NOT NULL,
        [AGENCIA] varchar(15) NOT NULL,
        [CONTA] varchar(15) NOT NULL,
        [VALOR_RENDIMENTO] decimal(18,2) NOT NULL,
        [Deletado] bit NOT NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_BANCO_ID_BANCO] FOREIGN KEY ([ID_BANCO]) REFERENCES [BANCO] ([ID_BANCO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONVENIO_ORGAO_ID_CONVENIO_ORGAO] FOREIGN KEY ([ID_CONVENIO_ORGAO]) REFERENCES [CONVENIO_ORGAO] ([ID_CONVENIO_ORGAO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA] FOREIGN KEY ([ID_TIPO_CONTA]) REFERENCES [TIPO_CONTA] ([ID_TIPO_CONTA]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_UNIDADE_FEDERATIVA_ID_UF] FOREIGN KEY ([ID_UF]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE_INSS] (
        [ID_RENDIMENTO_CLIENTE_INSS] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_RENDIMENTO_CLIENTE] int NOT NULL,
        [ID_INSS_ESPECIE_BENEFICIO] int NOT NULL,
        [MATRICULA] varchar(20) NOT NULL,
        [DATA_INSCRICAO] date NOT NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE_INSS] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE_INSS]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_INSS_INSS_ESPECIE_BENEFICIO_ID_INSS_ESPECIE_BENEFICIO] FOREIGN KEY ([ID_INSS_ESPECIE_BENEFICIO]) REFERENCES [INSS_ESPECIE_BENEFICIO] ([ID_INSS_ESPECIE_BENEFICIO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_INSS_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE_SIAPE] (
        [ID_RENDIMENTO_CLIENTE_SIAPE] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_RENDIMENTO_CLIENTE] int NOT NULL,
        [ID_SIAPE_TIPO_FUNCIONAL] int NOT NULL,
        [MATRICULA] varchar(20) NOT NULL,
        [MATRICULA_INSTITUIDOR] varchar(20) NULL,
        [POSSUI_REPRESENTACAO_POR_PROCURADOR] bit NOT NULL,
        [DATA_ADMISSAO] date NOT NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE_SIAPE] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE_SIAPE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_SIAPE_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_SIAPE_SIAPE_TIPO_FUNCIONAL_ID_SIAPE_TIPO_FUNCIONAL] FOREIGN KEY ([ID_SIAPE_TIPO_FUNCIONAL]) REFERENCES [SIAPE_TIPO_FUNCIONAL] ([ID_SIAPE_TIPO_FUNCIONAL])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_BANCO] ON [RENDIMENTO_CLIENTE] ([ID_BANCO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CLIENTE] ON [RENDIMENTO_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CONVENIO] ON [RENDIMENTO_CLIENTE] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CONVENIO_ORGAO] ON [RENDIMENTO_CLIENTE] ([ID_CONVENIO_ORGAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_TIPO_CONTA] ON [RENDIMENTO_CLIENTE] ([ID_TIPO_CONTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_UF] ON [RENDIMENTO_CLIENTE] ([ID_UF]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_INSS_ID_INSS_ESPECIE_BENEFICIO] ON [RENDIMENTO_CLIENTE_INSS] ([ID_INSS_ESPECIE_BENEFICIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE UNIQUE INDEX [IX_RENDIMENTO_CLIENTE_INSS_ID_RENDIMENTO_CLIENTE] ON [RENDIMENTO_CLIENTE_INSS] ([ID_RENDIMENTO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE UNIQUE INDEX [IX_RENDIMENTO_CLIENTE_SIAPE_ID_RENDIMENTO_CLIENTE] ON [RENDIMENTO_CLIENTE_SIAPE] ([ID_RENDIMENTO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_SIAPE_ID_SIAPE_TIPO_FUNCIONAL] ON [RENDIMENTO_CLIENTE_SIAPE] ([ID_SIAPE_TIPO_FUNCIONAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123163653_AddRendimentos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201123163653_AddRendimentos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201216133956_AddNomeInstituidor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE_SIAPE] ADD [NOME_INSTITUIDOR] varchar(80) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201216133956_AddNomeInstituidor')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201216133956_AddNomeInstituidor', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201218195716_AddConvenioFKOrgaos')
BEGIN
    ALTER TABLE [CONVENIO_ORGAO] ADD [ID_CONVENIO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201218195716_AddConvenioFKOrgaos')
BEGIN
    CREATE INDEX [IX_CONVENIO_ORGAO_ID_CONVENIO] ON [CONVENIO_ORGAO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201218195716_AddConvenioFKOrgaos')
BEGIN
    ALTER TABLE [CONVENIO_ORGAO] ADD CONSTRAINT [FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201218195716_AddConvenioFKOrgaos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201218195716_AddConvenioFKOrgaos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201229214137_AddDataImportacaoCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [DATA_IMPORTACAO] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201229214137_AddDataImportacaoCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201229214137_AddDataImportacaoCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210120135044_UsuarioRecuperacaoSenha')
BEGIN
    CREATE TABLE [USUARIO_RECUPERACAO_SENHA] (
        [ID_USUARIO_RECUPERACAO_SENHA] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [TOKEN] varchar(100) NOT NULL,
        [DATA_REQUISICAO] datetime2 NOT NULL,
        [ID_USUARIO] int NOT NULL,
        [VALIDO] bit NOT NULL DEFAULT CAST(1 AS bit),
        CONSTRAINT [PK_USUARIO_RECUPERACAO_SENHA] PRIMARY KEY ([ID_USUARIO_RECUPERACAO_SENHA]),
        CONSTRAINT [FK_USUARIO_RECUPERACAO_SENHA_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210120135044_UsuarioRecuperacaoSenha')
BEGIN
    CREATE INDEX [IX_USUARIO_RECUPERACAO_SENHA_ID_USUARIO] ON [USUARIO_RECUPERACAO_SENHA] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210120135044_UsuarioRecuperacaoSenha')
BEGIN
    CREATE UNIQUE INDEX [IX_USUARIO_RECUPERACAO_SENHA_TOKEN] ON [USUARIO_RECUPERACAO_SENHA] ([TOKEN]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210120135044_UsuarioRecuperacaoSenha')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210120135044_UsuarioRecuperacaoSenha', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    CREATE TABLE [TEMPLATE_EMAIL_FINALIDADE] (
        [ID] int NOT NULL,
        [DESCRICAO] nvarchar(max) NOT NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TEMPLATE_EMAIL_FINALIDADE] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    CREATE TABLE [TEMPLATE_EMAIL_TIPO] (
        [ID] int NOT NULL,
        [DESCRICAO] nvarchar(max) NOT NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        CONSTRAINT [PK_TEMPLATE_EMAIL_TIPO] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    CREATE TABLE [TEMPLATE_EMAIL] (
        [ID_TEMPLATE_EMAIL] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [CONTEUDO] nvarchar(max) NOT NULL,
        [TIPO] int NOT NULL,
        [FINALIDADE] int NOT NULL,
        CONSTRAINT [PK_TEMPLATE_EMAIL] PRIMARY KEY ([ID_TEMPLATE_EMAIL]),
        CONSTRAINT [FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE] FOREIGN KEY ([FINALIDADE]) REFERENCES [TEMPLATE_EMAIL_FINALIDADE] ([ID]),
        CONSTRAINT [FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_TIPO] FOREIGN KEY ([TIPO]) REFERENCES [TEMPLATE_EMAIL_TIPO] ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'DATA_ATUALIZACAO', N'DESCRICAO') AND [object_id] = OBJECT_ID(N'[TEMPLATE_EMAIL_FINALIDADE]'))
        SET IDENTITY_INSERT [TEMPLATE_EMAIL_FINALIDADE] ON;
    EXEC(N'INSERT INTO [TEMPLATE_EMAIL_FINALIDADE] ([ID], [DATA_ATUALIZACAO], [DESCRICAO])
    VALUES (0, ''2021-02-01T09:18:11.1465820-03:00'', N''Default''),
    (1, ''2021-02-01T09:18:11.1466380-03:00'', N''RecuperacaoSenha'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'DATA_ATUALIZACAO', N'DESCRICAO') AND [object_id] = OBJECT_ID(N'[TEMPLATE_EMAIL_FINALIDADE]'))
        SET IDENTITY_INSERT [TEMPLATE_EMAIL_FINALIDADE] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'DataAtualizacao', N'DESCRICAO') AND [object_id] = OBJECT_ID(N'[TEMPLATE_EMAIL_TIPO]'))
        SET IDENTITY_INSERT [TEMPLATE_EMAIL_TIPO] ON;
    EXEC(N'INSERT INTO [TEMPLATE_EMAIL_TIPO] ([ID], [DataAtualizacao], [DESCRICAO])
    VALUES (0, ''2021-02-01T09:18:11.1280210-03:00'', N''Content''),
    (1, ''2021-02-01T09:18:11.1438020-03:00'', N''Header''),
    (2, ''2021-02-01T09:18:11.1438300-03:00'', N''Footer'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ID', N'DataAtualizacao', N'DESCRICAO') AND [object_id] = OBJECT_ID(N'[TEMPLATE_EMAIL_TIPO]'))
        SET IDENTITY_INSERT [TEMPLATE_EMAIL_TIPO] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    CREATE INDEX [IX_TEMPLATE_EMAIL_FINALIDADE] ON [TEMPLATE_EMAIL] ([FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    CREATE UNIQUE INDEX [IX_TEMPLATE_EMAIL_TIPO_FINALIDADE] ON [TEMPLATE_EMAIL] ([TIPO], [FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201121811_TemplateEmail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210201121811_TemplateEmail', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210204151310_RegistroEmail')
BEGIN
    EXEC sp_rename N'[TEMPLATE_EMAIL_TIPO].[DataAtualizacao]', N'DATA_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210204151310_RegistroEmail')
BEGIN
    CREATE TABLE [REGISTRO_EMAIL] (
        [ID_REGISTRO_EMAIL] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [FINALIDADE] int NOT NULL,
        [DESTINATARIOS] nvarchar(max) NOT NULL,
        [ID_USUARIO] int NULL,
        CONSTRAINT [PK_REGISTRO_EMAIL] PRIMARY KEY ([ID_REGISTRO_EMAIL]),
        CONSTRAINT [FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE] FOREIGN KEY ([FINALIDADE]) REFERENCES [TEMPLATE_EMAIL_FINALIDADE] ([ID]),
        CONSTRAINT [FK_REGISTRO_EMAIL_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210204151310_RegistroEmail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210204151310_RegistroEmail', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC(N'DELETE FROM [TEMPLATE_EMAIL_FINALIDADE]
    WHERE [ID] = 0;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC(N'DELETE FROM [TEMPLATE_EMAIL_FINALIDADE]
    WHERE [ID] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC(N'DELETE FROM [TEMPLATE_EMAIL_TIPO]
    WHERE [ID] = 0;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC(N'DELETE FROM [TEMPLATE_EMAIL_TIPO]
    WHERE [ID] = 1;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC(N'DELETE FROM [TEMPLATE_EMAIL_TIPO]
    WHERE [ID] = 2;
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC sp_rename N'[TEMPLATE_EMAIL_TIPO].[ID]', N'ID_TEMPLATE_EMAIL_TIPO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    EXEC sp_rename N'[TEMPLATE_EMAIL_FINALIDADE].[ID]', N'ID_TEMPLATE_EMAIL_FINALIDADE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    ALTER TABLE [TEMPLATE_EMAIL_TIPO] ADD [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    ALTER TABLE [TEMPLATE_EMAIL_FINALIDADE] ADD [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210205190115_TemplateEmailAtualizacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210205190115_TemplateEmailAtualizacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [CONVENIO_ORGAO] DROP CONSTRAINT [FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [LEAD] DROP CONSTRAINT [FK_LEAD_CONVENIO_ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] DROP CONSTRAINT [FK_PARAMETRO_OPERACAO_CONVENIO_ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] DROP CONSTRAINT [FK_PARAMETRO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [TIPO_OPERACAO] ADD [ID_TEMP_TIPO_OPERACAO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    UPDATE dbo.TIPO_OPERACAO SET ID_TEMP_TIPO_OPERACAO = ID_TIPO_OPERACAO
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [TIPO_OPERACAO] DROP CONSTRAINT [PK_TIPO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    DECLARE @var96 sysname;
    SELECT @var96 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_OPERACAO]') AND [c].[name] = N'ID_TIPO_OPERACAO');
    IF @var96 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_OPERACAO] DROP CONSTRAINT [' + @var96 + '];');
    ALTER TABLE [TIPO_OPERACAO] DROP COLUMN [ID_TIPO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    EXEC sp_rename N'[TIPO_OPERACAO].[ID_TEMP_TIPO_OPERACAO]', N'ID_TIPO_OPERACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [TIPO_OPERACAO] ADD CONSTRAINT [PK_TIPO_OPERACAO] PRIMARY KEY ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [CONVENIO] ADD [ID_TEMP_CONVENIO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    UPDATE dbo.CONVENIO SET ID_TEMP_CONVENIO = ID_CONVENIO
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [CONVENIO] DROP CONSTRAINT [PK_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    DECLARE @var97 sysname;
    SELECT @var97 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONVENIO]') AND [c].[name] = N'ID_CONVENIO');
    IF @var97 IS NOT NULL EXEC(N'ALTER TABLE [CONVENIO] DROP CONSTRAINT [' + @var97 + '];');
    ALTER TABLE [CONVENIO] DROP COLUMN [ID_CONVENIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    EXEC sp_rename N'[CONVENIO].[ID_TEMP_CONVENIO]', N'ID_CONVENIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [CONVENIO] ADD CONSTRAINT [PK_CONVENIO] PRIMARY KEY ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [CONVENIO_ORGAO] ADD CONSTRAINT [FK_CONVENIO_ORGAO_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPO_OPERACAO] ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [LEAD] ADD CONSTRAINT [FK_LEAD_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] ADD CONSTRAINT [FK_PARAMETRO_OPERACAO_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [PARAMETRO_OPERACAO] ADD CONSTRAINT [FK_PARAMETRO_OPERACAO_TIPO_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPO_OPERACAO] ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208143715_RefactorTipoOperacaoConvenioRenomeado')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210208143715_RefactorTipoOperacaoConvenioRenomeado', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [FK_INTENCAO_OPERACAO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    DECLARE @var98 sysname;
    SELECT @var98 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[INTENCAO_OPERACAO]') AND [c].[name] = N'ID_SITUACAO');
    IF @var98 IS NOT NULL EXEC(N'ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [' + @var98 + '];');
    ALTER TABLE [INTENCAO_OPERACAO] DROP COLUMN [ID_SITUACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ADD [DESCRICAO_PADRAO] varchar(250) NOT NULL DEFAULT '';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ADD [PERMITE_ATUALIZACOES] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ADD [PERMITE_SITUACAO_EXTRAORDINARIA] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [SITUACAO_INTENCAO_OPERACAO] ADD [SITUACAO_EXTRAORDINARIA] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [PRODUTO] ADD [REQUER_CONVENIO] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [LEAD] ADD [ID_PRODUTO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [ID_PRODUTO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE TABLE [INTENCAO_OPERACAO_HISTORICO] (
        [ID_INTENCAO_OPERACAO_HISTORICO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_INTENCAO_OPERACAO] int NOT NULL,
        [ID_SITUACAO_INTENCAO_OPERACAO] int NOT NULL,
        [DESCRICAO_ESPECIFICA] varchar(250) NULL,
        CONSTRAINT [PK_INTENCAO_OPERACAO_HISTORICO] PRIMARY KEY ([ID_INTENCAO_OPERACAO_HISTORICO]),
        CONSTRAINT [FK_INTENCAO_OPERACAO_HISTORICO_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO] FOREIGN KEY ([ID_INTENCAO_OPERACAO]) REFERENCES [INTENCAO_OPERACAO] ([ID_INTENCAO_OPERACAO]) ON DELETE CASCADE,
        CONSTRAINT [FK_INTENCAO_OPERACAO_HISTORICO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO] FOREIGN KEY ([ID_SITUACAO_INTENCAO_OPERACAO]) REFERENCES [SITUACAO_INTENCAO_OPERACAO] ([ID_SITUACAO_INTENCAO_OPERACAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE TABLE [PRODUTO_INTENCAO_OPERACAO_PASSO] (
        [ID_PRODUTO_INTENCAO_OPERACAO_PASSO] int NOT NULL IDENTITY,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        [ID_PRODUTO] int NOT NULL,
        [ID_SITUACAO_INTENCAO_OPERACAO] int NOT NULL,
        [ID_PROXIMO_PASSO] int NULL,
        [ID_PROXIMO_PASSO_EXCECAO] int NULL,
        [PASSO_INICIAL] bit NOT NULL,
        CONSTRAINT [PK_PRODUTO_INTENCAO_OPERACAO_PASSO] PRIMARY KEY ([ID_PRODUTO_INTENCAO_OPERACAO_PASSO]),
        CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO]) ON DELETE CASCADE,
        CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO] FOREIGN KEY ([ID_PROXIMO_PASSO]) REFERENCES [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PRODUTO_INTENCAO_OPERACAO_PASSO]),
        CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO_EXCECAO] FOREIGN KEY ([ID_PROXIMO_PASSO_EXCECAO]) REFERENCES [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PRODUTO_INTENCAO_OPERACAO_PASSO]),
        CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_SITUACAO_INTENCAO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO] FOREIGN KEY ([ID_SITUACAO_INTENCAO_OPERACAO]) REFERENCES [SITUACAO_INTENCAO_OPERACAO] ([ID_SITUACAO_INTENCAO_OPERACAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_LEAD_ID_PRODUTO] ON [LEAD] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_ID_PRODUTO] ON [INTENCAO_OPERACAO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_HISTORICO_ID_INTENCAO_OPERACAO] ON [INTENCAO_OPERACAO_HISTORICO] ([ID_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_HISTORICO_ID_SITUACAO_INTENCAO_OPERACAO] ON [INTENCAO_OPERACAO_HISTORICO] ([ID_SITUACAO_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PROXIMO_PASSO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PROXIMO_PASSO_EXCECAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PROXIMO_PASSO_EXCECAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_SITUACAO_INTENCAO_OPERACAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_SITUACAO_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    CREATE UNIQUE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_SITUACAO_INTENCAO_OPERACAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PRODUTO], [ID_SITUACAO_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PRODUTO], [PASSO_INICIAL]) WHERE [PASSO_INICIAL] = 1');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    ALTER TABLE [LEAD] ADD CONSTRAINT [FK_LEAD_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210209194704_RelacoesProdutoIntencaoOperacaoSituacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [LEAD] DROP CONSTRAINT [FK_LEAD_PRODUTO_ID_PRODUTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [PRODUTO_INTENCAO_OPERACAO_PASSO] DROP CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_ID_PRODUTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [PRODUTO] ADD [ID_TEMP_PRODUTO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    UPDATE dbo.PRODUTO SET ID_TEMP_PRODUTO = ID_PRODUTO
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [PRODUTO] DROP CONSTRAINT [PK_PRODUTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    DECLARE @var99 sysname;
    SELECT @var99 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PRODUTO]') AND [c].[name] = N'ID_PRODUTO');
    IF @var99 IS NOT NULL EXEC(N'ALTER TABLE [PRODUTO] DROP CONSTRAINT [' + @var99 + '];');
    ALTER TABLE [PRODUTO] DROP COLUMN [ID_PRODUTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    EXEC sp_rename N'[PRODUTO].[ID_TEMP_PRODUTO]', N'ID_PRODUTO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [PRODUTO] ADD CONSTRAINT [PK_PRODUTO] PRIMARY KEY ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [LEAD] ADD CONSTRAINT [FK_LEAD_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    ALTER TABLE [PRODUTO_INTENCAO_OPERACAO_PASSO] ADD CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209204800_RefactorProdutoEnum')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210209204800_RefactorProdutoEnum', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210193401_HistoricoIntencaoOperacaoPendenciaUsuario')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO_HISTORICO] ADD [PENDENCIA_USUARIO] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210193401_HistoricoIntencaoOperacaoPendenciaUsuario')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210210193401_HistoricoIntencaoOperacaoPendenciaUsuario', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    ALTER TABLE [REGISTRO_EMAIL] DROP CONSTRAINT [FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    ALTER TABLE [TEMPLATE_EMAIL] DROP CONSTRAINT [FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_FINALIDADE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    ALTER TABLE [TEMPLATE_EMAIL] DROP CONSTRAINT [FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_TIPO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    DROP INDEX [IX_TEMPLATE_EMAIL_FINALIDADE] ON [TEMPLATE_EMAIL];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    DROP INDEX [IX_TEMPLATE_EMAIL_TIPO_FINALIDADE] ON [TEMPLATE_EMAIL];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    EXEC sp_rename N'[TEMPLATE_EMAIL].[FINALIDADE]', N'ID_FINALIDADE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    EXEC sp_rename N'[TEMPLATE_EMAIL].[TIPO]', N'ID_TIPO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    EXEC sp_rename N'[REGISTRO_EMAIL].[FINALIDADE]', N'ID_FINALIDADE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    CREATE INDEX [IX_TEMPLATE_EMAIL_ID_FINALIDADE] ON [TEMPLATE_EMAIL] ([ID_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    CREATE UNIQUE INDEX [IX_TEMPLATE_EMAIL_ID_TIPO_ID_FINALIDADE] ON [TEMPLATE_EMAIL] ([ID_TIPO], [ID_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    CREATE INDEX [IX_REGISTRO_EMAIL_ID_FINALIDADE] ON [REGISTRO_EMAIL] ([ID_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    ALTER TABLE [REGISTRO_EMAIL] ADD CONSTRAINT [FK_REGISTRO_EMAIL_TEMPLATE_EMAIL_FINALIDADE_ID_FINALIDADE] FOREIGN KEY ([ID_FINALIDADE]) REFERENCES [TEMPLATE_EMAIL_FINALIDADE] ([ID_TEMPLATE_EMAIL_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    ALTER TABLE [TEMPLATE_EMAIL] ADD CONSTRAINT [FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_FINALIDADE_ID_FINALIDADE] FOREIGN KEY ([ID_FINALIDADE]) REFERENCES [TEMPLATE_EMAIL_FINALIDADE] ([ID_TEMPLATE_EMAIL_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    ALTER TABLE [TEMPLATE_EMAIL] ADD CONSTRAINT [FK_TEMPLATE_EMAIL_TEMPLATE_EMAIL_TIPO_ID_TIPO] FOREIGN KEY ([ID_TIPO]) REFERENCES [TEMPLATE_EMAIL_TIPO] ([ID_TEMPLATE_EMAIL_TIPO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210211172416_EmailAlteracaoNomeIds')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210211172416_EmailAlteracaoNomeIds', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219195135_RemoveRendimentos')
BEGIN
    DROP TABLE [RENDIMENTO_CLIENTE_INSS];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219195135_RemoveRendimentos')
BEGIN
    DROP TABLE [RENDIMENTO_CLIENTE_SIAPE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219195135_RemoveRendimentos')
BEGIN
    DROP TABLE [RENDIMENTO_CLIENTE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219195135_RemoveRendimentos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210219195135_RemoveRendimentos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE] (
        [ID_RENDIMENTO_CLIENTE] int NOT NULL IDENTITY,
        [ID_CLIENTE] int NOT NULL,
        [ID_CONVENIO] int NOT NULL,
        [ID_CONVENIO_ORGAO] int NOT NULL,
        [ID_UF] int NOT NULL,
        [ID_BANCO] int NOT NULL,
        [ID_TIPO_CONTA] int NOT NULL,
        [AGENCIA] varchar(15) NOT NULL,
        [CONTA] varchar(15) NOT NULL,
        [VALOR_RENDIMENTO] decimal(18,2) NOT NULL,
        [MATRICULA] varchar(20) NOT NULL,
        [DELETADO] bit NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_BANCO_ID_BANCO] FOREIGN KEY ([ID_BANCO]) REFERENCES [BANCO] ([ID_BANCO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONVENIO_ID_CONVENIO] FOREIGN KEY ([ID_CONVENIO]) REFERENCES [CONVENIO] ([ID_CONVENIO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONVENIO_ORGAO_ID_CONVENIO_ORGAO] FOREIGN KEY ([ID_CONVENIO_ORGAO]) REFERENCES [CONVENIO_ORGAO] ([ID_CONVENIO_ORGAO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA] FOREIGN KEY ([ID_TIPO_CONTA]) REFERENCES [TIPO_CONTA] ([ID_TIPO_CONTA]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_UNIDADE_FEDERATIVA_ID_UF] FOREIGN KEY ([ID_UF]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE_INSS] (
        [ID_RENDIMENTO_CLIENTE] int NOT NULL,
        [ID_INSS_ESPECIE_BENEFICIO] int NOT NULL,
        [DATA_INSCRICAO] date NOT NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE_INSS] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_INSS_INSS_ESPECIE_BENEFICIO_ID_INSS_ESPECIE_BENEFICIO] FOREIGN KEY ([ID_INSS_ESPECIE_BENEFICIO]) REFERENCES [INSS_ESPECIE_BENEFICIO] ([ID_INSS_ESPECIE_BENEFICIO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_INSS_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE_SIAPE] (
        [ID_RENDIMENTO_CLIENTE] int NOT NULL,
        [ID_SIAPE_TIPO_FUNCIONAL] int NOT NULL,
        [MATRICULA_INSTITUIDOR] varchar(20) NULL,
        [NOME_INSTITUIDOR] varchar(80) NULL,
        [POSSUI_REPRESENTACAO_POR_PROCURADOR] bit NOT NULL,
        [DATA_ADMISSAO] date NOT NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE_SIAPE] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_SIAPE_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_SIAPE_SIAPE_TIPO_FUNCIONAL_ID_SIAPE_TIPO_FUNCIONAL] FOREIGN KEY ([ID_SIAPE_TIPO_FUNCIONAL]) REFERENCES [SIAPE_TIPO_FUNCIONAL] ([ID_SIAPE_TIPO_FUNCIONAL])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_BANCO] ON [RENDIMENTO_CLIENTE] ([ID_BANCO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CLIENTE] ON [RENDIMENTO_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CONVENIO] ON [RENDIMENTO_CLIENTE] ([ID_CONVENIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CONVENIO_ORGAO] ON [RENDIMENTO_CLIENTE] ([ID_CONVENIO_ORGAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_TIPO_CONTA] ON [RENDIMENTO_CLIENTE] ([ID_TIPO_CONTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_UF] ON [RENDIMENTO_CLIENTE] ([ID_UF]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_INSS_ID_INSS_ESPECIE_BENEFICIO] ON [RENDIMENTO_CLIENTE_INSS] ([ID_INSS_ESPECIE_BENEFICIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_SIAPE_ID_SIAPE_TIPO_FUNCIONAL] ON [RENDIMENTO_CLIENTE_SIAPE] ([ID_SIAPE_TIPO_FUNCIONAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210219202748_NovoRendimentosHeranca')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210219202748_NovoRendimentosHeranca', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210225222008_AddsIntencaoOperacaoContrato')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [ID_RENDIMENTO_CLIENTE] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210225222008_AddsIntencaoOperacaoContrato')
BEGIN
    CREATE TABLE [INTENCAO_OPERACAO_CONTRATO] (
        [ID_INTENCAO_OPERACAO_CONTRATO] int NOT NULL IDENTITY,
        [CONTRATO] varchar(50) NOT NULL,
        [ID_INTENCAO_OPERACAO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_INTENCAO_OPERACAO_CONTRATO] PRIMARY KEY ([ID_INTENCAO_OPERACAO_CONTRATO]),
        CONSTRAINT [FK_INTENCAO_OPERACAO_CONTRATO_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO] FOREIGN KEY ([ID_INTENCAO_OPERACAO]) REFERENCES [INTENCAO_OPERACAO] ([ID_INTENCAO_OPERACAO]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210225222008_AddsIntencaoOperacaoContrato')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_ID_RENDIMENTO_CLIENTE] ON [INTENCAO_OPERACAO] ([ID_RENDIMENTO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210225222008_AddsIntencaoOperacaoContrato')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO] ON [INTENCAO_OPERACAO_CONTRATO] ([ID_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210225222008_AddsIntencaoOperacaoContrato')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210225222008_AddsIntencaoOperacaoContrato')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210225222008_AddsIntencaoOperacaoContrato', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226001138_AddsUKIntencaoOperacaoContrato')
BEGIN
    DROP INDEX [IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO] ON [INTENCAO_OPERACAO_CONTRATO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226001138_AddsUKIntencaoOperacaoContrato')
BEGIN
    DECLARE @var100 sysname;
    SELECT @var100 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RENDIMENTO_CLIENTE]') AND [c].[name] = N'MATRICULA');
    IF @var100 IS NOT NULL EXEC(N'ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [' + @var100 + '];');
    ALTER TABLE [RENDIMENTO_CLIENTE] ALTER COLUMN [MATRICULA] varchar(10) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226001138_AddsUKIntencaoOperacaoContrato')
BEGIN
    CREATE UNIQUE INDEX [IX_INTENCAO_OPERACAO_CONTRATO_ID_INTENCAO_OPERACAO_CONTRATO] ON [INTENCAO_OPERACAO_CONTRATO] ([ID_INTENCAO_OPERACAO], [CONTRATO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210226001138_AddsUKIntencaoOperacaoContrato')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210226001138_AddsUKIntencaoOperacaoContrato', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210301195559_AddUsuarioAdministradorExclusivoToken')
BEGIN
    ALTER TABLE [USUARIO] ADD [ADMINISTRADOR] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210301195559_AddUsuarioAdministradorExclusivoToken')
BEGIN
    ALTER TABLE [USUARIO] ADD [EXCLUSIVO_TENANT] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210301195559_AddUsuarioAdministradorExclusivoToken')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210301195559_AddUsuarioAdministradorExclusivoToken', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210302190729_AddUsuarioRequisicaoLog')
BEGIN
    CREATE TABLE [USUARIO_REQUISICAO_LOG] (
        [ID_USUARIO_REQUISICAO_LOG] int NOT NULL IDENTITY,
        [ID_USUARIO] int NOT NULL,
        [USUARIO_TENANT] varchar(100) NULL,
        [URL_REQUISICAO] varchar(8000) NULL,
        [TOKEN_JWT] varchar(8000) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_USUARIO_REQUISICAO_LOG] PRIMARY KEY ([ID_USUARIO_REQUISICAO_LOG]),
        CONSTRAINT [FK_USUARIO_REQUISICAO_LOG_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210302190729_AddUsuarioRequisicaoLog')
BEGIN
    CREATE INDEX [IX_USUARIO_REQUISICAO_LOG_ID_USUARIO] ON [USUARIO_REQUISICAO_LOG] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210302190729_AddUsuarioRequisicaoLog')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210302190729_AddUsuarioRequisicaoLog', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303185557_AlterCamposUsuarioEUsuarioLog')
BEGIN
    DECLARE @var101 sysname;
    SELECT @var101 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO_REQUISICAO_LOG]') AND [c].[name] = N'TOKEN_JWT');
    IF @var101 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO_REQUISICAO_LOG] DROP CONSTRAINT [' + @var101 + '];');
    ALTER TABLE [USUARIO_REQUISICAO_LOG] DROP COLUMN [TOKEN_JWT];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303185557_AlterCamposUsuarioEUsuarioLog')
BEGIN
    DECLARE @var102 sysname;
    SELECT @var102 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'EXCLUSIVO_TENANT');
    IF @var102 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var102 + '];');
    ALTER TABLE [USUARIO] DROP COLUMN [EXCLUSIVO_TENANT];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303185557_AlterCamposUsuarioEUsuarioLog')
BEGIN
    ALTER TABLE [USUARIO_REQUISICAO_LOG] ADD [CORPO_REQUISICAO] varchar(8000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303185557_AlterCamposUsuarioEUsuarioLog')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210303185557_AlterCamposUsuarioEUsuarioLog', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210312192118_intencaoOperacaoDataInclusaoPrazo')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [DATA_INCLUSAO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210312192118_intencaoOperacaoDataInclusaoPrazo')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [PRAZO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210312192118_intencaoOperacaoDataInclusaoPrazo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210312192118_intencaoOperacaoDataInclusaoPrazo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE TABLE [TIPO_TERMO] (
        [ID_TIPO_TERMO] int NOT NULL,
        [NOME] varchar(100) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TIPO_TERMO] PRIMARY KEY ([ID_TIPO_TERMO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE TABLE [TERMO] (
        [ID_TERMO] int NOT NULL IDENTITY,
        [ID_TIPO_TERMO] int NOT NULL,
        [NOME] varchar(100) NOT NULL,
        [DESCRICAO] varchar(8000) NULL,
        [DATA_INICIO_VIGENCIA] datetime2 NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TERMO] PRIMARY KEY ([ID_TERMO]),
        CONSTRAINT [FK_TERMO_TIPO_TERMO_ID_TIPO_TERMO] FOREIGN KEY ([ID_TIPO_TERMO]) REFERENCES [TIPO_TERMO] ([ID_TIPO_TERMO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE TABLE [USUARIO_TERMO] (
        [ID_USUARIO_TERMO] int NOT NULL IDENTITY,
        [ID_USUARIO] int NOT NULL,
        [ID_TERMO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_USUARIO_TERMO] PRIMARY KEY ([ID_USUARIO_TERMO]),
        CONSTRAINT [FK_USUARIO_TERMO_TERMO_ID_TERMO] FOREIGN KEY ([ID_TERMO]) REFERENCES [TERMO] ([ID_TERMO]),
        CONSTRAINT [FK_USUARIO_TERMO_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE INDEX [IX_TERMO_ID_TIPO_TERMO] ON [TERMO] ([ID_TIPO_TERMO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE UNIQUE INDEX [IX_TIPO_TERMO_NOME] ON [TIPO_TERMO] ([NOME]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE INDEX [IX_USUARIO_TERMO_ID_TERMO] ON [USUARIO_TERMO] ([ID_TERMO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    CREATE INDEX [IX_USUARIO_TERMO_ID_USUARIO] ON [USUARIO_TERMO] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322165120_AddTermo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210322165120_AddTermo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210329131001_AnexoDataCadastro')
BEGIN
    ALTER TABLE [ANEXO] DROP CONSTRAINT [FK_ANEXO_USUARIO_IdUsuario];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210329131001_AnexoDataCadastro')
BEGIN
    EXEC sp_rename N'[ANEXO].[IdUsuario]', N'ID_USUARIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210329131001_AnexoDataCadastro')
BEGIN
    ALTER TABLE [ANEXO] ADD [DATA_CADASTRO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210329131001_AnexoDataCadastro')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [FK_ANEXO_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210329131001_AnexoDataCadastro')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210329131001_AnexoDataCadastro', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210330173643_IntencaoOperacaoAddDataVencimento')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [PRIMEIRO_VENCIMENTO] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210330173643_IntencaoOperacaoAddDataVencimento')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210330173643_IntencaoOperacaoAddDataVencimento', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    ALTER TABLE [BASE_CEP] DROP CONSTRAINT [FK_BASE_CEP_MUNICIPIO_IdMunicipio];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    ALTER TABLE [BASE_CEP] DROP CONSTRAINT [FK_BASE_CEP_TIPO_LOGRADOURO_IdTipoLogradouro];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    ALTER TABLE [BASE_CEP] DROP CONSTRAINT [FK_BASE_CEP_UNIDADE_FEDERATIVA_IdUnidadeFederativa];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    EXEC sp_rename N'[BASE_CEP].[IdUnidadeFederativa]', N'ID_UNIDADE_FEDERATIVA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    EXEC sp_rename N'[BASE_CEP].[IdTipoLogradouro]', N'ID_TIPO_LOGRADOURO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    EXEC sp_rename N'[BASE_CEP].[IdMunicipio]', N'ID_MUNICIPIO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    EXEC sp_rename N'[BASE_CEP].[IX_BASE_CEP_IdUnidadeFederativa]', N'IX_BASE_CEP_ID_UNIDADE_FEDERATIVA', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    EXEC sp_rename N'[BASE_CEP].[IX_BASE_CEP_IdTipoLogradouro]', N'IX_BASE_CEP_ID_TIPO_LOGRADOURO', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    EXEC sp_rename N'[BASE_CEP].[IX_BASE_CEP_IdMunicipio]', N'IX_BASE_CEP_ID_MUNICIPIO', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    ALTER TABLE [BASE_CEP] ADD CONSTRAINT [FK_BASE_CEP_MUNICIPIO_ID_MUNICIPIO] FOREIGN KEY ([ID_MUNICIPIO]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    ALTER TABLE [BASE_CEP] ADD CONSTRAINT [FK_BASE_CEP_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO] FOREIGN KEY ([ID_TIPO_LOGRADOURO]) REFERENCES [TIPO_LOGRADOURO] ([ID_TIPO_LOGRADOURO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    ALTER TABLE [BASE_CEP] ADD CONSTRAINT [FK_BASE_CEP_UNIDADE_FEDERATIVA_ID_UNIDADE_FEDERATIVA] FOREIGN KEY ([ID_UNIDADE_FEDERATIVA]) REFERENCES [UNIDADE_FEDERATIVA] ([ID_UNIDADE_FEDERATIVA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210402224944_AddRenameColumnsBaseCep')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210402224944_AddRenameColumnsBaseCep', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE TABLE [NOTIFICACAO_FINALIDADE] (
        [ID_NOTIFICACAO_FINALIDADE] int NOT NULL,
        [DESCRICAO] nvarchar(max) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_NOTIFICACAO_FINALIDADE] PRIMARY KEY ([ID_NOTIFICACAO_FINALIDADE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE TABLE [NOTIFICACAO_SEVERIDADE] (
        [ID_NOTIFICACAO_SEVERIDADE] int NOT NULL,
        [DESCRICAO] nvarchar(max) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_NOTIFICACAO_SEVERIDADE] PRIMARY KEY ([ID_NOTIFICACAO_SEVERIDADE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE TABLE [NOTIFICACAO_TEMPLATE] (
        [ID_NOTIFICACAO_TEMPLATE] int NOT NULL IDENTITY,
        [TITULO] varchar(50) NOT NULL,
        [DESCRICAO] varchar(300) NOT NULL,
        [URL_REFERENCIA] varchar(50) NOT NULL,
        [ID_NOTIFICACAO_SEVERIDADE] int NOT NULL,
        [ID_NOTIFICACAO_FINALIDADE] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_NOTIFICACAO_TEMPLATE] PRIMARY KEY ([ID_NOTIFICACAO_TEMPLATE]),
        CONSTRAINT [FK_NOTIFICACAO_TEMPLATE_NOTIFICACAO_FINALIDADE_ID_NOTIFICACAO_FINALIDADE] FOREIGN KEY ([ID_NOTIFICACAO_FINALIDADE]) REFERENCES [NOTIFICACAO_FINALIDADE] ([ID_NOTIFICACAO_FINALIDADE]),
        CONSTRAINT [FK_NOTIFICACAO_TEMPLATE_NOTIFICACAO_SEVERIDADE_ID_NOTIFICACAO_SEVERIDADE] FOREIGN KEY ([ID_NOTIFICACAO_SEVERIDADE]) REFERENCES [NOTIFICACAO_SEVERIDADE] ([ID_NOTIFICACAO_SEVERIDADE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE TABLE [NOTIFICACAO] (
        [ID_NOTIFICACAO] int NOT NULL IDENTITY,
        [ID_USUARIO] int NOT NULL,
        [ID_TEMPLATE_NOTIFICACAO] int NOT NULL,
        [TITULO] varchar(50) NOT NULL,
        [DESCRICAO] varchar(300) NOT NULL,
        [URL_REFERENCIA] varchar(50) NOT NULL,
        [DATA_VISUALIZACAO] datetime2 NULL,
        [ID_NOTIFICACAO_SEVERIDADE] int NOT NULL,
        [ID_NOTIFICACAO_FINALIDADE] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_NOTIFICACAO] PRIMARY KEY ([ID_NOTIFICACAO]),
        CONSTRAINT [FK_NOTIFICACAO_NOTIFICACAO_FINALIDADE_ID_NOTIFICACAO_FINALIDADE] FOREIGN KEY ([ID_NOTIFICACAO_FINALIDADE]) REFERENCES [NOTIFICACAO_FINALIDADE] ([ID_NOTIFICACAO_FINALIDADE]),
        CONSTRAINT [FK_NOTIFICACAO_NOTIFICACAO_SEVERIDADE_ID_NOTIFICACAO_SEVERIDADE] FOREIGN KEY ([ID_NOTIFICACAO_SEVERIDADE]) REFERENCES [NOTIFICACAO_SEVERIDADE] ([ID_NOTIFICACAO_SEVERIDADE]),
        CONSTRAINT [FK_NOTIFICACAO_NOTIFICACAO_TEMPLATE_ID_TEMPLATE_NOTIFICACAO] FOREIGN KEY ([ID_TEMPLATE_NOTIFICACAO]) REFERENCES [NOTIFICACAO_TEMPLATE] ([ID_NOTIFICACAO_TEMPLATE]),
        CONSTRAINT [FK_NOTIFICACAO_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE INDEX [IX_NOTIFICACAO_ID_NOTIFICACAO_FINALIDADE] ON [NOTIFICACAO] ([ID_NOTIFICACAO_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE INDEX [IX_NOTIFICACAO_ID_NOTIFICACAO_SEVERIDADE] ON [NOTIFICACAO] ([ID_NOTIFICACAO_SEVERIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE INDEX [IX_NOTIFICACAO_ID_TEMPLATE_NOTIFICACAO] ON [NOTIFICACAO] ([ID_TEMPLATE_NOTIFICACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE INDEX [IX_NOTIFICACAO_ID_USUARIO] ON [NOTIFICACAO] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE UNIQUE INDEX [IX_NOTIFICACAO_TEMPLATE_ID_NOTIFICACAO_FINALIDADE] ON [NOTIFICACAO_TEMPLATE] ([ID_NOTIFICACAO_FINALIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    CREATE INDEX [IX_NOTIFICACAO_TEMPLATE_ID_NOTIFICACAO_SEVERIDADE] ON [NOTIFICACAO_TEMPLATE] ([ID_NOTIFICACAO_SEVERIDADE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416130555_Notificacoes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210416130555_Notificacoes', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416145039_RendimentoConsultaMargemSiape')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE_SIAPE] ADD [DATA_LIBERACAO_CONSULTA_MARGEM] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210416145039_RendimentoConsultaMargemSiape')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210416145039_RendimentoConsultaMargemSiape', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419210216_LojaDeUsuarioParaCliente')
BEGIN
    ALTER TABLE [USUARIO] DROP CONSTRAINT [FK_USUARIO_LOJA_ID_LOJA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419210216_LojaDeUsuarioParaCliente')
BEGIN
    DECLARE @var103 sysname;
    SELECT @var103 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USUARIO]') AND [c].[name] = N'ID_LOJA');
    IF @var103 IS NOT NULL EXEC(N'ALTER TABLE [USUARIO] DROP CONSTRAINT [' + @var103 + '];');
    ALTER TABLE [USUARIO] DROP COLUMN [ID_LOJA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419210216_LojaDeUsuarioParaCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_LOJA] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419210216_LojaDeUsuarioParaCliente')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_LOJA] ON [CLIENTE] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419210216_LojaDeUsuarioParaCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_LOJA_ID_LOJA] FOREIGN KEY ([ID_LOJA]) REFERENCES [LOJA] ([ID_LOJA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210419210216_LojaDeUsuarioParaCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210419210216_LojaDeUsuarioParaCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    DECLARE @var104 sysname;
    SELECT @var104 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'ESTADO');
    IF @var104 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var104 + '];');
    ALTER TABLE [LOJA] DROP COLUMN [ESTADO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    DECLARE @var105 sysname;
    SELECT @var105 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'LATITUDE');
    IF @var105 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var105 + '];');
    ALTER TABLE [LOJA] DROP COLUMN [LATITUDE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    DECLARE @var106 sysname;
    SELECT @var106 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'LONGITUDE');
    IF @var106 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var106 + '];');
    ALTER TABLE [LOJA] DROP COLUMN [LONGITUDE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    EXEC sp_rename N'[LOJA].[ENDERECO]', N'LOGRADOURO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    DECLARE @var107 sysname;
    SELECT @var107 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LOJA]') AND [c].[name] = N'CIDADE');
    IF @var107 IS NOT NULL EXEC(N'ALTER TABLE [LOJA] DROP CONSTRAINT [' + @var107 + '];');
    ALTER TABLE [LOJA] DROP COLUMN [CIDADE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    ALTER TABLE [LOJA] ADD [COMPLEMENTO] varchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    ALTER TABLE [LOJA] ADD [ID_MUNICIPIO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    ALTER TABLE [LOJA] ADD [ID_TIPO_LOGRADOURO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    ALTER TABLE [LOJA] ADD [NUMERO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    CREATE INDEX [IX_LOJA_ID_MUNICIPIO] ON [LOJA] ([ID_MUNICIPIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    CREATE INDEX [IX_LOJA_ID_TIPO_LOGRADOURO] ON [LOJA] ([ID_TIPO_LOGRADOURO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    ALTER TABLE [LOJA] ADD CONSTRAINT [FK_LOJA_MUNICIPIO_ID_MUNICIPIO] FOREIGN KEY ([ID_MUNICIPIO]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    ALTER TABLE [LOJA] ADD CONSTRAINT [FK_LOJA_TIPO_LOGRADOURO_ID_TIPO_LOGRADOURO] FOREIGN KEY ([ID_TIPO_LOGRADOURO]) REFERENCES [TIPO_LOGRADOURO] ([ID_TIPO_LOGRADOURO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210427133355_RefactorLojaEndereco')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210427133355_RefactorLojaEndereco', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430200417_AddValoresMargemRendimento')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD [MARGEM_DISPONIVEL] decimal(18,2) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430200417_AddValoresMargemRendimento')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD [MARGEM_DISPONIVEL_CARTAO] decimal(18,2) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430200417_AddValoresMargemRendimento')
BEGIN
    CREATE TABLE [ConsultaBeneficiosInssCliente] (
        [ID] int NOT NULL IDENTITY,
        [IdCliente] int NOT NULL,
        [IdPaperlessDocumento] int NULL,
        [IdAnexoArquivoTermo] int NULL,
        [ChaveAutorizacao] nvarchar(max) NULL,
        [DataGeracaoArquivoTermo] datetime2 NOT NULL,
        [ClienteID] int NULL,
        [AnexoArquivoTermoID] int NULL,
        [UsuarioAtualizacao] nvarchar(max) NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        CONSTRAINT [PK_ConsultaBeneficiosInssCliente] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_ConsultaBeneficiosInssCliente_ANEXO_AnexoArquivoTermoID] FOREIGN KEY ([AnexoArquivoTermoID]) REFERENCES [ANEXO] ([ID_ANEXO]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ConsultaBeneficiosInssCliente_CLIENTE_ClienteID] FOREIGN KEY ([ClienteID]) REFERENCES [CLIENTE] ([ID_CLIENTE]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430200417_AddValoresMargemRendimento')
BEGIN
    CREATE INDEX [IX_ConsultaBeneficiosInssCliente_AnexoArquivoTermoID] ON [ConsultaBeneficiosInssCliente] ([AnexoArquivoTermoID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430200417_AddValoresMargemRendimento')
BEGIN
    CREATE INDEX [IX_ConsultaBeneficiosInssCliente_ClienteID] ON [ConsultaBeneficiosInssCliente] ([ClienteID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430200417_AddValoresMargemRendimento')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210430200417_AddValoresMargemRendimento', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    ALTER TABLE [ConsultaBeneficiosInssCliente] DROP CONSTRAINT [FK_ConsultaBeneficiosInssCliente_ANEXO_AnexoArquivoTermoID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    ALTER TABLE [ConsultaBeneficiosInssCliente] DROP CONSTRAINT [FK_ConsultaBeneficiosInssCliente_CLIENTE_ClienteID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    ALTER TABLE [ConsultaBeneficiosInssCliente] DROP CONSTRAINT [PK_ConsultaBeneficiosInssCliente];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    DROP INDEX [IX_ConsultaBeneficiosInssCliente_AnexoArquivoTermoID] ON [ConsultaBeneficiosInssCliente];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    DROP INDEX [IX_ConsultaBeneficiosInssCliente_ClienteID] ON [ConsultaBeneficiosInssCliente];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    DECLARE @var108 sysname;
    SELECT @var108 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsultaBeneficiosInssCliente]') AND [c].[name] = N'AnexoArquivoTermoID');
    IF @var108 IS NOT NULL EXEC(N'ALTER TABLE [ConsultaBeneficiosInssCliente] DROP CONSTRAINT [' + @var108 + '];');
    ALTER TABLE [ConsultaBeneficiosInssCliente] DROP COLUMN [AnexoArquivoTermoID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    DECLARE @var109 sysname;
    SELECT @var109 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsultaBeneficiosInssCliente]') AND [c].[name] = N'ClienteID');
    IF @var109 IS NOT NULL EXEC(N'ALTER TABLE [ConsultaBeneficiosInssCliente] DROP CONSTRAINT [' + @var109 + '];');
    ALTER TABLE [ConsultaBeneficiosInssCliente] DROP COLUMN [ClienteID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[ConsultaBeneficiosInssCliente]', N'CONSULTA_BENEFICIO_INSS_CLIENTE';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[UsuarioAtualizacao]', N'USUARIO_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[IdPaperlessDocumento]', N'ID_PAPERLESS_DOCUMENTO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[IdCliente]', N'ID_CLIENTE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[IdAnexoArquivoTermo]', N'ID_ANEXO_ARQUIVO_TERMO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[DataGeracaoArquivoTermo]', N'DATA_GERACAO_ARQUIVO_TERMO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[DataAtualizacao]', N'DATA_ATUALIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[ChaveAutorizacao]', N'CHAVE_AUTORIZACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    EXEC sp_rename N'[CONSULTA_BENEFICIO_INSS_CLIENTE].[ID]', N'ID_CONSULTA_BENEFICIO_INSS_CLIENTE', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    DECLARE @var110 sysname;
    SELECT @var110 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONSULTA_BENEFICIO_INSS_CLIENTE]') AND [c].[name] = N'USUARIO_ATUALIZACAO');
    IF @var110 IS NOT NULL EXEC(N'ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] DROP CONSTRAINT [' + @var110 + '];');
    ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] ALTER COLUMN [USUARIO_ATUALIZACAO] varchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] ADD CONSTRAINT [PK_CONSULTA_BENEFICIO_INSS_CLIENTE] PRIMARY KEY ([ID_CONSULTA_BENEFICIO_INSS_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    CREATE INDEX [IX_CONSULTA_BENEFICIO_INSS_CLIENTE_ID_ANEXO_ARQUIVO_TERMO] ON [CONSULTA_BENEFICIO_INSS_CLIENTE] ([ID_ANEXO_ARQUIVO_TERMO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    CREATE INDEX [IX_CONSULTA_BENEFICIO_INSS_CLIENTE_ID_CLIENTE] ON [CONSULTA_BENEFICIO_INSS_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] ADD CONSTRAINT [FK_CONSULTA_BENEFICIO_INSS_CLIENTE_ANEXO_ID_ANEXO_ARQUIVO_TERMO] FOREIGN KEY ([ID_ANEXO_ARQUIVO_TERMO]) REFERENCES [ANEXO] ([ID_ANEXO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] ADD CONSTRAINT [FK_CONSULTA_BENEFICIO_INSS_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430203110_AddConsultaBeneficioInssClienteDominio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210430203110_AddConsultaBeneficioInssClienteDominio', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430205029_AlterColumnChaveAutorizacaoConsultaBeneficio')
BEGIN
    DECLARE @var111 sysname;
    SELECT @var111 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CONSULTA_BENEFICIO_INSS_CLIENTE]') AND [c].[name] = N'CHAVE_AUTORIZACAO');
    IF @var111 IS NOT NULL EXEC(N'ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] DROP CONSTRAINT [' + @var111 + '];');
    ALTER TABLE [CONSULTA_BENEFICIO_INSS_CLIENTE] ALTER COLUMN [CHAVE_AUTORIZACAO] varchar(200) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430205029_AlterColumnChaveAutorizacaoConsultaBeneficio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210430205029_AlterColumnChaveAutorizacaoConsultaBeneficio', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210510194008_AddColumnsClienteAceiteImportacao')
BEGIN
    ALTER TABLE [CLIENTE] ADD [DATA_AUTORIZACAO_IMPORTACAO_DADOS] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210510194008_AddColumnsClienteAceiteImportacao')
BEGIN
    ALTER TABLE [CLIENTE] ADD [IMPORTACAO_DADOS_AUTORIZADA] bit NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210510194008_AddColumnsClienteAceiteImportacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210510194008_AddColumnsClienteAceiteImportacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511202733_notificacao_completude')
BEGIN
    ALTER TABLE [NOTIFICACAO] ADD [COMPLETO] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511202733_notificacao_completude')
BEGIN
    ALTER TABLE [NOTIFICACAO] ADD [DATA_CRIACAO] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511202733_notificacao_completude')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210511202733_notificacao_completude', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [IntencaoOperacaoPortabilidadeID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    ALTER TABLE [BANCO] ADD [PERMITE_PORTABILIDADE] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    CREATE TABLE [INTENCAO_OPERACAO_PORTABILIDADE] (
        [ID_INTENCAO_OPERACAO_PORTABILIDADE] int NOT NULL IDENTITY,
        [ID_INTENCAO_OPERACAO] int NOT NULL,
        [ID_BANCO_ORIGINADOR] int NOT NULL,
        [PRAZO_RESTANTE] int NOT NULL,
        [PRAZO_TOTAL] int NOT NULL,
        [SALDO_DEVEDOR] decimal(18,2) NOT NULL,
        [PLANO_REFINANCIAMENTO] varchar(3) NULL,
        [PRAZO_REFINANCIAMENTO] int NOT NULL,
        [VALOR_PRESTACAO_REFINANCIAMENTO] decimal(18,2) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_INTENCAO_OPERACAO_PORTABILIDADE] PRIMARY KEY ([ID_INTENCAO_OPERACAO_PORTABILIDADE]),
        CONSTRAINT [FK_INTENCAO_OPERACAO_PORTABILIDADE_BANCO_ID_BANCO_ORIGINADOR] FOREIGN KEY ([ID_BANCO_ORIGINADOR]) REFERENCES [BANCO] ([ID_BANCO]),
        CONSTRAINT [FK_INTENCAO_OPERACAO_PORTABILIDADE_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO] FOREIGN KEY ([ID_INTENCAO_OPERACAO]) REFERENCES [INTENCAO_OPERACAO] ([ID_INTENCAO_OPERACAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_IntencaoOperacaoPortabilidadeID] ON [INTENCAO_OPERACAO] ([IntencaoOperacaoPortabilidadeID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_PORTABILIDADE_ID_BANCO_ORIGINADOR] ON [INTENCAO_OPERACAO_PORTABILIDADE] ([ID_BANCO_ORIGINADOR]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    CREATE UNIQUE INDEX [IX_INTENCAO_OPERACAO_PORTABILIDADE_ID_INTENCAO_OPERACAO] ON [INTENCAO_OPERACAO_PORTABILIDADE] ([ID_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_IntencaoOperacaoPortabilidadeID] FOREIGN KEY ([IntencaoOperacaoPortabilidadeID]) REFERENCES [INTENCAO_OPERACAO_PORTABILIDADE] ([ID_INTENCAO_OPERACAO_PORTABILIDADE]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210513203539_AddIntencaoOperacaoPortabilidade')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210513203539_AddIntencaoOperacaoPortabilidade', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514130227_AlterPrazoRefinanciamentoNull')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_IntencaoOperacaoPortabilidadeID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514130227_AlterPrazoRefinanciamentoNull')
BEGIN
    EXEC sp_rename N'[INTENCAO_OPERACAO].[IntencaoOperacaoPortabilidadeID]', N'PortabilidadeID', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514130227_AlterPrazoRefinanciamentoNull')
BEGIN
    EXEC sp_rename N'[INTENCAO_OPERACAO].[IX_INTENCAO_OPERACAO_IntencaoOperacaoPortabilidadeID]', N'IX_INTENCAO_OPERACAO_PortabilidadeID', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514130227_AlterPrazoRefinanciamentoNull')
BEGIN
    DECLARE @var112 sysname;
    SELECT @var112 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[INTENCAO_OPERACAO_PORTABILIDADE]') AND [c].[name] = N'PRAZO_REFINANCIAMENTO');
    IF @var112 IS NOT NULL EXEC(N'ALTER TABLE [INTENCAO_OPERACAO_PORTABILIDADE] DROP CONSTRAINT [' + @var112 + '];');
    ALTER TABLE [INTENCAO_OPERACAO_PORTABILIDADE] ALTER COLUMN [PRAZO_REFINANCIAMENTO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514130227_AlterPrazoRefinanciamentoNull')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD CONSTRAINT [FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_PortabilidadeID] FOREIGN KEY ([PortabilidadeID]) REFERENCES [INTENCAO_OPERACAO_PORTABILIDADE] ([ID_INTENCAO_OPERACAO_PORTABILIDADE]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514130227_AlterPrazoRefinanciamentoNull')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210514130227_AlterPrazoRefinanciamentoNull', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514132807_AlterRelacionamentoIntencaoOperacaoPortabilidade')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [FK_INTENCAO_OPERACAO_INTENCAO_OPERACAO_PORTABILIDADE_PortabilidadeID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514132807_AlterRelacionamentoIntencaoOperacaoPortabilidade')
BEGIN
    DROP INDEX [IX_INTENCAO_OPERACAO_PortabilidadeID] ON [INTENCAO_OPERACAO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514132807_AlterRelacionamentoIntencaoOperacaoPortabilidade')
BEGIN
    DECLARE @var113 sysname;
    SELECT @var113 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[INTENCAO_OPERACAO]') AND [c].[name] = N'PortabilidadeID');
    IF @var113 IS NOT NULL EXEC(N'ALTER TABLE [INTENCAO_OPERACAO] DROP CONSTRAINT [' + @var113 + '];');
    ALTER TABLE [INTENCAO_OPERACAO] DROP COLUMN [PortabilidadeID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210514132807_AlterRelacionamentoIntencaoOperacaoPortabilidade')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210514132807_AlterRelacionamentoIntencaoOperacaoPortabilidade', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520193809_LeadFlagQuitacao')
BEGIN
    ALTER TABLE [LEAD] ADD [Quitacao] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520193809_LeadFlagQuitacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520193809_LeadFlagQuitacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520213459_LeadCorrespondente')
BEGIN
    CREATE TABLE [LEAD_CORRESPONDENTE] (
        [ID_LEAD_CORRESPONDENTE] int NOT NULL IDENTITY,
        [CNPJ] varchar(20) NOT NULL,
        [NOME] varchar(100) NOT NULL,
        [TELEFONE] varchar(20) NOT NULL,
        [EMAIL] varchar(100) NOT NULL,
        [ID_MUNICIPIO] int NOT NULL,
        [ATIVIDADES] varchar(300) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_LEAD_CORRESPONDENTE] PRIMARY KEY ([ID_LEAD_CORRESPONDENTE]),
        CONSTRAINT [FK_LEAD_CORRESPONDENTE_MUNICIPIO_ID_MUNICIPIO] FOREIGN KEY ([ID_MUNICIPIO]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520213459_LeadCorrespondente')
BEGIN
    CREATE INDEX [IX_LEAD_CORRESPONDENTE_ID_MUNICIPIO] ON [LEAD_CORRESPONDENTE] ([ID_MUNICIPIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210520213459_LeadCorrespondente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210520213459_LeadCorrespondente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210524170631_LeadCelular')
BEGIN
    ALTER TABLE [LEAD] ADD [CELULAR] varchar(50) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210524170631_LeadCelular')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210524170631_LeadCelular', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525192142_FeatureFlags')
BEGIN
    CREATE TABLE [FEATURE_FLAG] (
        [ID_FEATURE_FLAG] int NOT NULL IDENTITY,
        [CHAVE] varchar(50) NOT NULL,
        [HABILITADO] bit NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_FEATURE_FLAG] PRIMARY KEY ([ID_FEATURE_FLAG])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525192142_FeatureFlags')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210525192142_FeatureFlags', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610192603_AddLoginSocial')
BEGIN
    CREATE TABLE [REDE_SOCIAL] (
        [ID_TEMPLATE_EMAIL_TIPO] int NOT NULL,
        [NOME] varchar(100) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_REDE_SOCIAL] PRIMARY KEY ([ID_TEMPLATE_EMAIL_TIPO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610192603_AddLoginSocial')
BEGIN
    CREATE TABLE [USUARIO_REDE_SOCIAL] (
        [ID_USUARIO_REDE_SOCIAL] int NOT NULL IDENTITY,
        [ID_USUARIO] int NOT NULL,
        [ID_REDE_SOCIAL] int NOT NULL,
        [LOGIN] varchar(100) NOT NULL,
        [ATIVO] bit NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_USUARIO_REDE_SOCIAL] PRIMARY KEY ([ID_USUARIO_REDE_SOCIAL]),
        CONSTRAINT [FK_USUARIO_REDE_SOCIAL_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610192603_AddLoginSocial')
BEGIN
    CREATE UNIQUE INDEX [IX_USUARIO_REDE_SOCIAL_ID_USUARIO_ID_REDE_SOCIAL] ON [USUARIO_REDE_SOCIAL] ([ID_USUARIO], [ID_REDE_SOCIAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610192603_AddLoginSocial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210610192603_AddLoginSocial', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715182644_AddSolicitacaoAcessoDadosPessoais')
BEGIN
    CREATE TABLE [SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] (
        [ID_SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] int NOT NULL IDENTITY,
        [NOME] varchar(50) NOT NULL,
        [SOBRENOME] varchar(50) NOT NULL,
        [DATA_NASCIMENTO] datetime2 NOT NULL,
        [NOME_MAE] varchar(100) NULL,
        [EMAIL] varchar(100) NOT NULL,
        [TELEFONE_COMPLETO] varchar(20) NOT NULL,
        [NOTIFICACAO_ENVIADA] bit NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] PRIMARY KEY ([ID_SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715182644_AddSolicitacaoAcessoDadosPessoais')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210715182644_AddSolicitacaoAcessoDadosPessoais', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715210933_AlterSolicitacaoAcessoDadosPessoais')
BEGIN
    DECLARE @var114 sysname;
    SELECT @var114 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE]') AND [c].[name] = N'DATA_NASCIMENTO');
    IF @var114 IS NOT NULL EXEC(N'ALTER TABLE [SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] DROP CONSTRAINT [' + @var114 + '];');
    ALTER TABLE [SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] ALTER COLUMN [DATA_NASCIMENTO] date NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210715210933_AlterSolicitacaoAcessoDadosPessoais')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210715210933_AlterSolicitacaoAcessoDadosPessoais', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210719202535_IntencaoOperacaoPlanoQuatroDigitos')
BEGIN
    DECLARE @var115 sysname;
    SELECT @var115 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[INTENCAO_OPERACAO_PORTABILIDADE]') AND [c].[name] = N'PLANO_REFINANCIAMENTO');
    IF @var115 IS NOT NULL EXEC(N'ALTER TABLE [INTENCAO_OPERACAO_PORTABILIDADE] DROP CONSTRAINT [' + @var115 + '];');
    ALTER TABLE [INTENCAO_OPERACAO_PORTABILIDADE] ALTER COLUMN [PLANO_REFINANCIAMENTO] varchar(4) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210719202535_IntencaoOperacaoPlanoQuatroDigitos')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210719202535_IntencaoOperacaoPlanoQuatroDigitos', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721185245_AddMotivoSolicitacaoAcessoDados')
BEGIN
    DECLARE @var116 sysname;
    SELECT @var116 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE]') AND [c].[name] = N'NOME_MAE');
    IF @var116 IS NOT NULL EXEC(N'ALTER TABLE [SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] DROP CONSTRAINT [' + @var116 + '];');
    ALTER TABLE [SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] ALTER COLUMN [NOME_MAE] varchar(100) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721185245_AddMotivoSolicitacaoAcessoDados')
BEGIN
    ALTER TABLE [SOLICITACAO_ACESSO_DADOS_PESSOAIS_CLIENTE] ADD [MOTIVO] varchar(8000) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210721185245_AddMotivoSolicitacaoAcessoDados')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210721185245_AddMotivoSolicitacaoAcessoDados', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210729191354_AddColumnsIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [CUSTO_EFETIVO_TOTAL_ANO] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210729191354_AddColumnsIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [CUSTO_EFETIVO_TOTAL_MES] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210729191354_AddColumnsIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [IMPOSTO_OPERACAO_FINANCEIRA] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210729191354_AddColumnsIntencaoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210729191354_AddColumnsIntencaoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    ALTER TABLE [ANEXO] DROP CONSTRAINT [FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    DROP INDEX [IX_DOCUMENTO_IDENTIFICACAO_CLIENTE_NUMERO_ID_TIPO_DOCUMENTO_ID_UNIDADE_FEDERATIVA] ON [DOCUMENTO_IDENTIFICACAO_CLIENTE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    EXEC sp_rename N'[ANEXO].[ID_TIPO]', N'ID_TIPO_DOCUMENTO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    EXEC sp_rename N'[ANEXO].[IX_ANEXO_ID_TIPO]', N'IX_ANEXO_ID_TIPO_DOCUMENTO', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    ALTER TABLE [ANEXO] ADD [EXTENSAO] varchar(5) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    ALTER TABLE [ANEXO] ADD [ID_CLIENTE] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    CREATE INDEX [IX_ANEXO_ID_CLIENTE] ON [ANEXO] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [FK_ANEXO_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO] FOREIGN KEY ([ID_TIPO_DOCUMENTO]) REFERENCES [TIPO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210806153217_AlterColumnsDocumentoIdentificacaoEAnexo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153944_RemoveUsuarioAnexo')
BEGIN
    ALTER TABLE [ANEXO] DROP CONSTRAINT [FK_ANEXO_USUARIO_ID_USUARIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153944_RemoveUsuarioAnexo')
BEGIN
    DECLARE @var117 sysname;
    SELECT @var117 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ANEXO]') AND [c].[name] = N'ID_USUARIO');
    IF @var117 IS NOT NULL EXEC(N'ALTER TABLE [ANEXO] DROP CONSTRAINT [' + @var117 + '];');
    ALTER TABLE [ANEXO] DROP COLUMN [ID_USUARIO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806153944_RemoveUsuarioAnexo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210806153944_RemoveUsuarioAnexo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [ANEXO] DROP CONSTRAINT [FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [DOCUMENTO_IDENTIFICACAO_CLIENTE] DROP CONSTRAINT [FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [TIPO_DOCUMENTO] ADD [ID_TEMP_TIPO_DOCUMENTO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    UPDATE dbo.TIPO_DOCUMENTO SET ID_TEMP_TIPO_DOCUMENTO = ID_TIPO_DOCUMENTO
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [TIPO_DOCUMENTO] DROP CONSTRAINT [PK_TIPO_DOCUMENTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    DECLARE @var118 sysname;
    SELECT @var118 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_DOCUMENTO]') AND [c].[name] = N'ID_TIPO_DOCUMENTO');
    IF @var118 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_DOCUMENTO] DROP CONSTRAINT [' + @var118 + '];');
    ALTER TABLE [TIPO_DOCUMENTO] DROP COLUMN [ID_TIPO_DOCUMENTO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    EXEC sp_rename N'[TIPO_DOCUMENTO].[ID_TEMP_TIPO_DOCUMENTO]', N'ID_TIPO_DOCUMENTO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [TIPO_DOCUMENTO] ADD CONSTRAINT [PK_TIPO_DOCUMENTO] PRIMARY KEY ([ID_TIPO_DOCUMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [ANEXO] ADD CONSTRAINT [FK_ANEXO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO] FOREIGN KEY ([ID_TIPO_DOCUMENTO]) REFERENCES [TIPO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    ALTER TABLE [DOCUMENTO_IDENTIFICACAO_CLIENTE] ADD CONSTRAINT [FK_DOCUMENTO_IDENTIFICACAO_CLIENTE_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO] FOREIGN KEY ([ID_TIPO_DOCUMENTO]) REFERENCES [TIPO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210826201230_EnumTipoDocumento')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210826201230_EnumTipoDocumento', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    DROP INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_SITUACAO_INTENCAO_OPERACAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    DROP INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL] ON [PRODUTO_INTENCAO_OPERACAO_PASSO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    ALTER TABLE [PRODUTO_INTENCAO_OPERACAO_PASSO] ADD [ID_TIPO_OPERACAO] int NOT NULL DEFAULT 1;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    ALTER TABLE [INTENCAO_OPERACAO] ADD [PROPOSTA] varchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    CREATE UNIQUE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_ID_TIPO_OPERACAO_ID_SITUACAO_INTENCAO_OPERACAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PRODUTO], [ID_TIPO_OPERACAO], [ID_SITUACAO_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_PRODUTO_PASSO_INICIAL_ID_TIPO_OPERACAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_PRODUTO], [PASSO_INICIAL], [ID_TIPO_OPERACAO]) WHERE [PASSO_INICIAL] = 1');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    CREATE INDEX [IX_PRODUTO_INTENCAO_OPERACAO_PASSO_ID_TIPO_OPERACAO] ON [PRODUTO_INTENCAO_OPERACAO_PASSO] ([ID_TIPO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    ALTER TABLE [PRODUTO_INTENCAO_OPERACAO_PASSO] ADD CONSTRAINT [FK_PRODUTO_INTENCAO_OPERACAO_PASSO_TIPO_OPERACAO_ID_TIPO_OPERACAO] FOREIGN KEY ([ID_TIPO_OPERACAO]) REFERENCES [TIPO_OPERACAO] ([ID_TIPO_OPERACAO]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC sp_rename N'[dbo].[PRODUTO_INTENCAO_OPERACAO_PASSO]', N'INTENCAO_OPERACAO_SITUACAO_PASSO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC sp_rename N'[dbo].[INTENCAO_OPERACAO_SITUACAO_PASSO].[ID_PRODUTO_INTENCAO_OPERACAO_PASSO]', N'ID_INTENCAO_OPERACAO_SITUACAO_PASSO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC sp_rename N'[dbo].[INTENCAO_OPERACAO_SITUACAO_PASSO].[ID_SITUACAO_INTENCAO_OPERACAO]', N'ID_INTENCAO_OPERACAO_SITUACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC sp_rename N'[dbo].[SITUACAO_INTENCAO_OPERACAO]', N'INTENCAO_OPERACAO_SITUACAO';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC sp_rename N'[dbo].[INTENCAO_OPERACAO_SITUACAO].[ID_SITUACAO_INTENCAO_OPERACAO]', N'ID_INTENCAO_OPERACAO_SITUACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    EXEC sp_rename N'[dbo].[INTENCAO_OPERACAO_HISTORICO].[ID_SITUACAO_INTENCAO_OPERACAO]', N'ID_INTENCAO_OPERACAO_SITUACAO', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210827174052_AddTipoOperacaoEmPassosSituacaoIntencaoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210830160734_AddObservacoesIntencaoOperacao')
BEGIN
    CREATE TABLE [INTENCAO_OPERACAO_OBSERVACAO] (
        [ID_INTENCAO_OPERACAO_OBSERVACAO] int NOT NULL IDENTITY,
        [ID_INTENCAO_OPERACAO] int NOT NULL,
        [OBSERVACAO] varchar(400) NOT NULL,
        [DATA_INCLUSAO] datetime2 NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_INTENCAO_OPERACAO_OBSERVACAO] PRIMARY KEY ([ID_INTENCAO_OPERACAO_OBSERVACAO]),
        CONSTRAINT [FK_INTENCAO_OPERACAO_OBSERVACAO_INTENCAO_OPERACAO_ID_INTENCAO_OPERACAO] FOREIGN KEY ([ID_INTENCAO_OPERACAO]) REFERENCES [INTENCAO_OPERACAO] ([ID_INTENCAO_OPERACAO]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210830160734_AddObservacoesIntencaoOperacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210830160734_AddObservacoesIntencaoOperacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    EXEC sp_rename N'[REDE_SOCIAL].[ID_TEMPLATE_EMAIL_TIPO]', N'ID_REDE_SOCIAL', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    ALTER TABLE [TELEFONE_CLIENTE] ADD [CONFIRMADO] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    CREATE TABLE [TIPO_SOLICITACAO_CONFIRMACAO] (
        [ID_TIPO_SOLICITACAO_CONFIRMACAO] int NOT NULL,
        [NOME] varchar(100) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TIPO_SOLICITACAO_CONFIRMACAO] PRIMARY KEY ([ID_TIPO_SOLICITACAO_CONFIRMACAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    CREATE TABLE [TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO] (
        [ID_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO] int NOT NULL IDENTITY,
        [ID_TIPO_SOLICITACAO_CONFIRMACAO] int NOT NULL,
        [ID_TELEFONE_CLIENTE] int NOT NULL,
        [TOKEN] nvarchar(4) NOT NULL,
        [ENVIADA] bit NOT NULL,
        [DATA_ENVIO_SOLICITACAO] datetime2 NULL,
        [MENSAGEM_ERRO] varchar(max) NULL,
        [QUANTIDADE_ENVIOS_EFETUADOS] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO] PRIMARY KEY ([ID_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO]),
        CONSTRAINT [FK_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_TELEFONE_CLIENTE_ID_TELEFONE_CLIENTE] FOREIGN KEY ([ID_TELEFONE_CLIENTE]) REFERENCES [TELEFONE_CLIENTE] ([ID_TELEFONE_CLIENTE]),
        CONSTRAINT [FK_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_TIPO_SOLICITACAO_CONFIRMACAO_ID_TIPO_SOLICITACAO_CONFIRMACAO] FOREIGN KEY ([ID_TIPO_SOLICITACAO_CONFIRMACAO]) REFERENCES [TIPO_SOLICITACAO_CONFIRMACAO] ([ID_TIPO_SOLICITACAO_CONFIRMACAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    CREATE INDEX [IX_INTENCAO_OPERACAO_OBSERVACAO_ID_INTENCAO_OPERACAO] ON [INTENCAO_OPERACAO_OBSERVACAO] ([ID_INTENCAO_OPERACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    CREATE INDEX [IX_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_ID_TELEFONE_CLIENTE] ON [TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO] ([ID_TELEFONE_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    CREATE INDEX [IX_TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO_ID_TIPO_SOLICITACAO_CONFIRMACAO] ON [TELEFONE_CLIENTE_SOLICITACAO_CONFIRMACAO] ([ID_TIPO_SOLICITACAO_CONFIRMACAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210831210304_AddTelefoneConfirmacao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210831210304_AddTelefoneConfirmacao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901201619_SolicitacaoDocumento')
BEGIN
    CREATE TABLE [SOLICITACAO_DOCUMENTO] (
        [ID_SOLICITACAO_DOCUMENTO] int NOT NULL IDENTITY,
        [ID_TIPO_DOCUMENTO] int NOT NULL,
        [ID_CLIENTE] int NOT NULL,
        [DATA_SOLICITACAO] datetime2 NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SOLICITACAO_DOCUMENTO] PRIMARY KEY ([ID_SOLICITACAO_DOCUMENTO]),
        CONSTRAINT [FK_SOLICITACAO_DOCUMENTO_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]) ON DELETE CASCADE,
        CONSTRAINT [FK_SOLICITACAO_DOCUMENTO_TIPO_DOCUMENTO_ID_TIPO_DOCUMENTO] FOREIGN KEY ([ID_TIPO_DOCUMENTO]) REFERENCES [TIPO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901201619_SolicitacaoDocumento')
BEGIN
    CREATE INDEX [IX_SOLICITACAO_DOCUMENTO_ID_CLIENTE] ON [SOLICITACAO_DOCUMENTO] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901201619_SolicitacaoDocumento')
BEGIN
    CREATE INDEX [IX_SOLICITACAO_DOCUMENTO_ID_TIPO_DOCUMENTO] ON [SOLICITACAO_DOCUMENTO] ([ID_TIPO_DOCUMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901201619_SolicitacaoDocumento')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210901201619_SolicitacaoDocumento', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902175326_SolicitacaoConclusao')
BEGIN
    ALTER TABLE [SOLICITACAO_DOCUMENTO] ADD [CONCLUIDO] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902175326_SolicitacaoConclusao')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210902175326_SolicitacaoConclusao', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902200233_SolicitacaoImportacaoCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [DATA_SOLICITACAO_IMPORTACAO_DADOS] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902200233_SolicitacaoImportacaoCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [IMPORTACAO_DADOS_SOLICITADA] bit NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902200233_SolicitacaoImportacaoCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210902200233_SolicitacaoImportacaoCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [FK_RENDIMENTO_CLIENTE_BANCO_ID_BANCO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [FK_RENDIMENTO_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DECLARE @var119 sysname;
    SELECT @var119 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RENDIMENTO_CLIENTE]') AND [c].[name] = N'AGENCIA');
    IF @var119 IS NOT NULL EXEC(N'ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [' + @var119 + '];');
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP COLUMN [AGENCIA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DECLARE @var120 sysname;
    SELECT @var120 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RENDIMENTO_CLIENTE]') AND [c].[name] = N'CONTA');
    IF @var120 IS NOT NULL EXEC(N'ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [' + @var120 + '];');
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP COLUMN [CONTA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DROP INDEX [IX_RENDIMENTO_CLIENTE_ID_TIPO_CONTA] ON [RENDIMENTO_CLIENTE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DROP INDEX [IX_RENDIMENTO_CLIENTE_ID_BANCO] ON [RENDIMENTO_CLIENTE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DECLARE @var121 sysname;
    SELECT @var121 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RENDIMENTO_CLIENTE]') AND [c].[name] = N'ID_TIPO_CONTA');
    IF @var121 IS NOT NULL EXEC(N'ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [' + @var121 + '];');
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP COLUMN [ID_TIPO_CONTA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DECLARE @var122 sysname;
    SELECT @var122 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RENDIMENTO_CLIENTE]') AND [c].[name] = N'ID_BANCO');
    IF @var122 IS NOT NULL EXEC(N'ALTER TABLE [RENDIMENTO_CLIENTE] DROP CONSTRAINT [' + @var122 + '];');
    ALTER TABLE [RENDIMENTO_CLIENTE] DROP COLUMN [ID_BANCO];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD [ID_CONTA_CLIENTE] int NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD [ID_CONTA_CLIENTE_RECEBIMENTO] int NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CONTA_CLIENTE] ON [RENDIMENTO_CLIENTE] ([ID_CONTA_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_ID_CONTA_CLIENTE_RECEBIMENTO] ON [RENDIMENTO_CLIENTE] ([ID_CONTA_CLIENTE_RECEBIMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [TIPO_CONTA] ADD [ID_TEMP_TIPO_CONTA] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    UPDATE dbo.TIPO_CONTA SET ID_TEMP_TIPO_CONTA = ID_TIPO_CONTA
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [TIPO_CONTA] DROP CONSTRAINT [PK_TIPO_CONTA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    DECLARE @var123 sysname;
    SELECT @var123 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_CONTA]') AND [c].[name] = N'ID_TIPO_CONTA');
    IF @var123 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_CONTA] DROP CONSTRAINT [' + @var123 + '];');
    ALTER TABLE [TIPO_CONTA] DROP COLUMN [ID_TIPO_CONTA];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    EXEC sp_rename N'[TIPO_CONTA].[ID_TEMP_TIPO_CONTA]', N'ID_TIPO_CONTA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [TIPO_CONTA] ADD CONSTRAINT [PK_TIPO_CONTA] PRIMARY KEY ([ID_TIPO_CONTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE TABLE [FORMA_RECEBIMENTO] (
        [ID_FORMA_RECEBIMENTO] int NOT NULL,
        [NOME] varchar(100) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_FORMA_RECEBIMENTO] PRIMARY KEY ([ID_FORMA_RECEBIMENTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE TABLE [CONTA_CLIENTE] (
        [ID_CONTA_CLIENTE] int NOT NULL IDENTITY,
        [ID_CLIENTE] int NOT NULL,
        [ID_BANCO] int NOT NULL,
        [ID_TIPO_CONTA] int NOT NULL,
        [AGENCIA] varchar(15) NOT NULL,
        [CONTA] varchar(15) NOT NULL,
        [ID_FORMA_RECEBIMENTO] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_CONTA_CLIENTE] PRIMARY KEY ([ID_CONTA_CLIENTE]),
        CONSTRAINT [FK_CONTA_CLIENTE_BANCO_ID_BANCO] FOREIGN KEY ([ID_BANCO]) REFERENCES [BANCO] ([ID_BANCO]),
        CONSTRAINT [FK_CONTA_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]),
        CONSTRAINT [FK_CONTA_CLIENTE_FORMA_RECEBIMENTO_ID_FORMA_RECEBIMENTO] FOREIGN KEY ([ID_FORMA_RECEBIMENTO]) REFERENCES [FORMA_RECEBIMENTO] ([ID_FORMA_RECEBIMENTO]),
        CONSTRAINT [FK_CONTA_CLIENTE_TIPO_CONTA_ID_TIPO_CONTA] FOREIGN KEY ([ID_TIPO_CONTA]) REFERENCES [TIPO_CONTA] ([ID_TIPO_CONTA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE INDEX [IX_CONTA_CLIENTE_ID_BANCO] ON [CONTA_CLIENTE] ([ID_BANCO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE INDEX [IX_CONTA_CLIENTE_ID_CLIENTE] ON [CONTA_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE INDEX [IX_CONTA_CLIENTE_ID_FORMA_RECEBIMENTO] ON [CONTA_CLIENTE] ([ID_FORMA_RECEBIMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    CREATE INDEX [IX_CONTA_CLIENTE_ID_TIPO_CONTA] ON [CONTA_CLIENTE] ([ID_TIPO_CONTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONTA_CLIENTE_ID_CONTA_CLIENTE] FOREIGN KEY ([ID_CONTA_CLIENTE]) REFERENCES [CONTA_CLIENTE] ([ID_CONTA_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    ALTER TABLE [RENDIMENTO_CLIENTE] ADD CONSTRAINT [FK_RENDIMENTO_CLIENTE_CONTA_CLIENTE_ID_CONTA_CLIENTE_RECEBIMENTO] FOREIGN KEY ([ID_CONTA_CLIENTE_RECEBIMENTO]) REFERENCES [CONTA_CLIENTE] ([ID_CONTA_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210908142349_ContasClienteRefactor')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210908142349_ContasClienteRefactor', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910213300_SolicitacaoAnexoMotivo')
BEGIN
    ALTER TABLE [SOLICITACAO_DOCUMENTO] ADD [MOTIVO] varchar(500) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910213300_SolicitacaoAnexoMotivo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210910213300_SolicitacaoAnexoMotivo', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210915170002_AddBeneficioInssMensagemDePara')
BEGIN
    CREATE TABLE [BENEFICIO_INSS_MENSAGEM_DE_PARA] (
        [ID_BENEFICIO_INSS_MENSAGEM_DE_PARA] int NOT NULL IDENTITY,
        [CODIGO_ORIGINAL] varchar(20) NOT NULL,
        [MENSAGEM_ORIGINAL] varchar(4000) NOT NULL,
        [MENSAGEM_TRATADA] varchar(4000) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_BENEFICIO_INSS_MENSAGEM_DE_PARA] PRIMARY KEY ([ID_BENEFICIO_INSS_MENSAGEM_DE_PARA])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210915170002_AddBeneficioInssMensagemDePara')
BEGIN
    CREATE UNIQUE INDEX [IX_BENEFICIO_INSS_MENSAGEM_DE_PARA_CODIGO_ORIGINAL] ON [BENEFICIO_INSS_MENSAGEM_DE_PARA] ([CODIGO_ORIGINAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210915170002_AddBeneficioInssMensagemDePara')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210915170002_AddBeneficioInssMensagemDePara', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    DECLARE @var124 sysname;
    SELECT @var124 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TELEFONE_CLIENTE]') AND [c].[name] = N'PRINCIPAL');
    IF @var124 IS NOT NULL EXEC(N'ALTER TABLE [TELEFONE_CLIENTE] DROP CONSTRAINT [' + @var124 + '];');
    ALTER TABLE [TELEFONE_CLIENTE] DROP COLUMN [PRINCIPAL];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    DECLARE @var125 sysname;
    SELECT @var125 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CLIENTE]') AND [c].[name] = N'EMAIL');
    IF @var125 IS NOT NULL EXEC(N'ALTER TABLE [CLIENTE] DROP CONSTRAINT [' + @var125 + '];');
    ALTER TABLE [CLIENTE] DROP COLUMN [EMAIL];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [USUARIO] ADD [EMAIL_CONFIRMADO] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    DECLARE @var126 sysname;
    SELECT @var126 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TIPO_DOCUMENTO]') AND [c].[name] = N'CODIGO');
    IF @var126 IS NOT NULL EXEC(N'ALTER TABLE [TIPO_DOCUMENTO] DROP CONSTRAINT [' + @var126 + '];');
    ALTER TABLE [TIPO_DOCUMENTO] ALTER COLUMN [CODIGO] varchar(30) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [TIPO_DOCUMENTO] ADD [TIPO_DOCUMENTO_IDENTIFICACAO_PESSOAL] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [REGISTRO_EMAIL] ADD [CODIGO_ORIGEM] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CONTA_CLIENTE] ADD [Deletado] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_CONJUGE] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_ENDERECO_PRINCIPAL] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_ENDERECO_SECUNDARIO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_TELEFONE_PRINCIPAL] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_TELEFONE_SECUNDARIO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [BIOMETRIA_SITUACAO] (
        [ID] int NOT NULL,
        [DESCRICAO] nvarchar(max) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_BIOMETRIA_SITUACAO] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [REGISTRO_BIOMETRIA_UNICO] (
        [ID_REGISTRO_BIOMETRIA_UNICO] int NOT NULL IDENTITY,
        [ID_CLIENTE] int NOT NULL,
        [CODIGO] nvarchar(max) NULL,
        [DATA_ENVIO] datetime2 NOT NULL,
        [DATA_RETORNO] datetime2 NOT NULL,
        [SCORE] int NOT NULL,
        [LIVENESS] bit NOT NULL,
        [FACE_MATCH] bit NOT NULL,
        [POSSUI_BIOMETRIA] bit NOT NULL,
        [CODIGO_SITUACAO_BIOMETRIA] int NOT NULL,
        [CODIGO_ERRO] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_REGISTRO_BIOMETRIA_UNICO] PRIMARY KEY ([ID_REGISTRO_BIOMETRIA_UNICO]),
        CONSTRAINT [FK_REGISTRO_BIOMETRIA_UNICO_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PARENTESCO] (
        [ID_SEGURO_PARENTESCO] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(8000) NULL,
        [CODIGO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PARENTESCO] PRIMARY KEY ([ID_SEGURO_PARENTESCO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PRODUTO] (
        [ID_SEGURO_PRODUTO] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(max) NOT NULL,
        [NOME] varchar(10) NOT NULL,
        [DATA_INICIO_VIGENCIA] date NOT NULL,
        [DATA_FIM_VIGENCIA] date NOT NULL,
        [VALOR_PREMIO] decimal(18,2) NOT NULL,
        [ID_PRODUTO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PRODUTO] PRIMARY KEY ([ID_SEGURO_PRODUTO]),
        CONSTRAINT [FK_SEGURO_PRODUTO_PRODUTO_ID_PRODUTO] FOREIGN KEY ([ID_PRODUTO]) REFERENCES [PRODUTO] ([ID_PRODUTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PROFISSAO] (
        [ID_SEGURO_PROFISSAO] int NOT NULL IDENTITY,
        [CODIGO] int NOT NULL,
        [DESCRICAO] varchar(8000) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PROFISSAO] PRIMARY KEY ([ID_SEGURO_PROFISSAO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [TEMPLATE_SMS] (
        [ID_TEMPLATE_SMS] int NOT NULL,
        [DESCRICAO] varchar(50) NOT NULL,
        [CONTEUDO] varchar(160) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TEMPLATE_SMS] PRIMARY KEY ([ID_TEMPLATE_SMS])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [TEMPLATE_TORPEDO_VOZ] (
        [ID_TEMPLATE_TORPEDO_VOZ] int NOT NULL,
        [DESCRICAO] varchar(50) NOT NULL,
        [CONTEUDO] varchar(500) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TEMPLATE_TORPEDO_VOZ] PRIMARY KEY ([ID_TEMPLATE_TORPEDO_VOZ])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [TEMPLATE_WHATSAPP] (
        [ID_TEMPLATE_WHATSAPP] int NOT NULL,
        [DESCRICAO] varchar(50) NOT NULL,
        [GUID] varchar(20) NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TEMPLATE_WHATSAPP] PRIMARY KEY ([ID_TEMPLATE_WHATSAPP])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [TIPO_REGIME_CASAMENTO] (
        [ID_TIPO_REGIME_CASAMENTO] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(150) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TIPO_REGIME_CASAMENTO] PRIMARY KEY ([ID_TIPO_REGIME_CASAMENTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [USUARIO_CONFIRMACAO_EMAIL] (
        [ID_USUARIO_CONFIRMACAO_EMAIL] int NOT NULL IDENTITY,
        [TOKEN] varchar(100) NOT NULL,
        [DATA_REQUISICAO] datetime2 NOT NULL,
        [ID_USUARIO] int NOT NULL,
        [VALIDO] bit NOT NULL DEFAULT CAST(1 AS bit),
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_USUARIO_CONFIRMACAO_EMAIL] PRIMARY KEY ([ID_USUARIO_CONFIRMACAO_EMAIL]),
        CONSTRAINT [FK_USUARIO_CONFIRMACAO_EMAIL_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [BIOMETRIA_CLIENTE] (
        [ID_BIOMETRIA_CLIENTE] int NOT NULL IDENTITY,
        [ID_CLIENTE] int NOT NULL,
        [DATA_ENVIO] datetime2 NOT NULL,
        [SCORE] int NOT NULL,
        [VALIDO] bit NOT NULL,
        [ID_BIOMETRIA_SITUACAO] int NOT NULL,
        [DATA_RETORNO] datetime2 NOT NULL,
        [IdRegistroBiometriaUnico] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_BIOMETRIA_CLIENTE] PRIMARY KEY ([ID_BIOMETRIA_CLIENTE]),
        CONSTRAINT [FK_BIOMETRIA_CLIENTE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]),
        CONSTRAINT [FK_BIOMETRIA_CLIENTE_REGISTRO_BIOMETRIA_UNICO_IdRegistroBiometriaUnico] FOREIGN KEY ([IdRegistroBiometriaUnico]) REFERENCES [REGISTRO_BIOMETRIA_UNICO] ([ID_REGISTRO_BIOMETRIA_UNICO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PARENTESCO_ICATU] (
        [ID_SEGURO_PARENTESCO_ICATU] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(8000) NULL,
        [CODIGO] int NOT NULL,
        [ID_SEGURO_PARENTESCO_BEM] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PARENTESCO_ICATU] PRIMARY KEY ([ID_SEGURO_PARENTESCO_ICATU]),
        CONSTRAINT [FK_SEGURO_PARENTESCO_ICATU_SEGURO_PARENTESCO_ID_SEGURO_PARENTESCO_BEM] FOREIGN KEY ([ID_SEGURO_PARENTESCO_BEM]) REFERENCES [SEGURO_PARENTESCO] ([ID_SEGURO_PARENTESCO]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_COBERTURA] (
        [ID_SEGURO_COBERTURA] int NOT NULL IDENTITY,
        [CODIGO_COBERTURA] int NOT NULL,
        [TIPO] varchar(1) NOT NULL,
        [VALOR_CAPITAL] decimal(18,2) NOT NULL,
        [VALOR_PREMIO] decimal(18,2) NOT NULL,
        [TIPO_PROPONENTE] varchar(1) NOT NULL,
        [ID_SEGURO_PRODUTO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_COBERTURA] PRIMARY KEY ([ID_SEGURO_COBERTURA]),
        CONSTRAINT [FK_SEGURO_COBERTURA_SEGURO_PRODUTO_ID_SEGURO_PRODUTO] FOREIGN KEY ([ID_SEGURO_PRODUTO]) REFERENCES [SEGURO_PRODUTO] ([ID_SEGURO_PRODUTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PRODUTO_ICATU] (
        [ID_SEGURO_PRODUTO_ICATU] int NOT NULL IDENTITY,
        [GRUPO_APOLICE] int NOT NULL,
        [DATA_INICIO_VIGENCIA] date NOT NULL,
        [DATA_FIM_VIGENCIA] date NOT NULL,
        [MODULO] int NOT NULL,
        [ID_SEGURO_PRODUTO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PRODUTO_ICATU] PRIMARY KEY ([ID_SEGURO_PRODUTO_ICATU]),
        CONSTRAINT [FK_SEGURO_PRODUTO_ICATU_SEGURO_PRODUTO_ID_SEGURO_PRODUTO] FOREIGN KEY ([ID_SEGURO_PRODUTO]) REFERENCES [SEGURO_PRODUTO] ([ID_SEGURO_PRODUTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PROPOSTA] (
        [ID_SEGURO_PROPOSTA] int NOT NULL IDENTITY,
        [DATA_ASSINATURA] date NOT NULL,
        [DATA_INICIO_VIGENCIA] date NOT NULL,
        [DATA_FIM_VIGENCIA] date NOT NULL,
        [DATA_PROTOCOLO] date NOT NULL,
        [DATA_VENCIMENTO] date NOT NULL,
        [VALOR_PREMIO_TOTAL] decimal(18,2) NOT NULL,
        [VALOR_PRIMEIRO_PREMIO_PAGO] bit NOT NULL,
        [NUMERO_PROPOSTA_ICATU] bigint NOT NULL,
        [ID_SEGURO_PRODUTO] int NOT NULL,
        [ID_CLIENTE] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PROPOSTA] PRIMARY KEY ([ID_SEGURO_PROPOSTA]),
        CONSTRAINT [FK_SEGURO_PROPOSTA_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]) ON DELETE CASCADE,
        CONSTRAINT [FK_SEGURO_PROPOSTA_SEGURO_PRODUTO_ID_SEGURO_PRODUTO] FOREIGN KEY ([ID_SEGURO_PRODUTO]) REFERENCES [SEGURO_PRODUTO] ([ID_SEGURO_PRODUTO]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PROFISSAO_ICATU] (
        [ID_SEGURO_PROFISSAO_ICATU] int NOT NULL IDENTITY,
        [CODIGO] int NOT NULL,
        [DESCRICAO] varchar(8000) NULL,
        [ID_SEGURO_PROFISSAO_BEM] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PROFISSAO_ICATU] PRIMARY KEY ([ID_SEGURO_PROFISSAO_ICATU]),
        CONSTRAINT [FK_SEGURO_PROFISSAO_ICATU_SEGURO_PROFISSAO_ID_SEGURO_PROFISSAO_BEM] FOREIGN KEY ([ID_SEGURO_PROFISSAO_BEM]) REFERENCES [SEGURO_PROFISSAO] ([ID_SEGURO_PROFISSAO]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [REGISTRO_SMS] (
        [ID_REGISTRO_SMS] int NOT NULL IDENTITY,
        [ID_TEMPLATE_SMS] int NOT NULL,
        [NUMERO_TELEFONE] varchar(15) NOT NULL,
        [MENSAGEM] varchar(200) NOT NULL,
        [ID_USUARIO] int NULL,
        [CODIGO_ORIGEM] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_REGISTRO_SMS] PRIMARY KEY ([ID_REGISTRO_SMS]),
        CONSTRAINT [FK_REGISTRO_SMS_TEMPLATE_SMS_ID_TEMPLATE_SMS] FOREIGN KEY ([ID_TEMPLATE_SMS]) REFERENCES [TEMPLATE_SMS] ([ID_TEMPLATE_SMS]),
        CONSTRAINT [FK_REGISTRO_SMS_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [REGISTRO_TORPEDO_VOZ] (
        [ID_REGISTRO_TORPEDO_VOZ] int NOT NULL IDENTITY,
        [NUMERO_TELEFONE] varchar(15) NOT NULL,
        [MENSAGEM] varchar(500) NOT NULL,
        [ID_TEMPLATE_TORPEDO_VOZ] int NOT NULL,
        [ID_USUARIO] int NULL,
        [CODIGO_ORIGEM] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_REGISTRO_TORPEDO_VOZ] PRIMARY KEY ([ID_REGISTRO_TORPEDO_VOZ]),
        CONSTRAINT [FK_REGISTRO_TORPEDO_VOZ_TEMPLATE_TORPEDO_VOZ_ID_TEMPLATE_TORPEDO_VOZ] FOREIGN KEY ([ID_TEMPLATE_TORPEDO_VOZ]) REFERENCES [TEMPLATE_TORPEDO_VOZ] ([ID_TEMPLATE_TORPEDO_VOZ]),
        CONSTRAINT [FK_REGISTRO_TORPEDO_VOZ_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [REGISTRO_WHATSAPP] (
        [ID_REGISTRO_WHATSAPP] int NOT NULL IDENTITY,
        [ID_TEMPLATE_WHATSAPP] int NOT NULL,
        [NUMERO_TELEFONE] varchar(15) NOT NULL,
        [PARAMETROS_MENSAGEM] varchar(400) NOT NULL,
        [ID_USUARIO] int NULL,
        [CODIGO_ORIGEM] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_REGISTRO_WHATSAPP] PRIMARY KEY ([ID_REGISTRO_WHATSAPP]),
        CONSTRAINT [FK_REGISTRO_WHATSAPP_TEMPLATE_WHATSAPP_ID_TEMPLATE_WHATSAPP] FOREIGN KEY ([ID_TEMPLATE_WHATSAPP]) REFERENCES [TEMPLATE_WHATSAPP] ([ID_TEMPLATE_WHATSAPP]),
        CONSTRAINT [FK_REGISTRO_WHATSAPP_USUARIO_ID_USUARIO] FOREIGN KEY ([ID_USUARIO]) REFERENCES [USUARIO] ([ID_USUARIO]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [CONJUGE] (
        [ID_CONJUGE] int NOT NULL IDENTITY,
        [CPF] varchar(50) NULL,
        [NOME] varchar(100) NULL,
        [DATA_NASCIMENTO] date NULL,
        [ID_CLIENTE] int NULL,
        [ID_GENERO] int NULL,
        [ID_TIPO_REGIME_CASAMENTO] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_CONJUGE] PRIMARY KEY ([ID_CONJUGE]),
        CONSTRAINT [FK_CONJUGE_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CONJUGE_GENERO_ID_GENERO] FOREIGN KEY ([ID_GENERO]) REFERENCES [GENERO] ([ID_GENERO]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CONJUGE_TIPO_REGIME_CASAMENTO_ID_TIPO_REGIME_CASAMENTO] FOREIGN KEY ([ID_TIPO_REGIME_CASAMENTO]) REFERENCES [TIPO_REGIME_CASAMENTO] ([ID_TIPO_REGIME_CASAMENTO]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [TIPO_REGIME_CASAMENTO_BEM] (
        [ID_TIPO_REGIME_CASAMENTO_BEM] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(150) NOT NULL,
        [ID_TIPO_REGIME_CASAMENTO] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_TIPO_REGIME_CASAMENTO_BEM] PRIMARY KEY ([ID_TIPO_REGIME_CASAMENTO_BEM]),
        CONSTRAINT [FK_TIPO_REGIME_CASAMENTO_BEM_TIPO_REGIME_CASAMENTO_ID_TIPO_REGIME_CASAMENTO] FOREIGN KEY ([ID_TIPO_REGIME_CASAMENTO]) REFERENCES [TIPO_REGIME_CASAMENTO] ([ID_TIPO_REGIME_CASAMENTO]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_COBERTURA_ICATU] (
        [ID_SEGURO_COBERTURA_ICATU] int NOT NULL IDENTITY,
        [CODIGO_COBERTURA] int NOT NULL,
        [TIPO] varchar(1) NOT NULL,
        [VALOR_CAPITAL] decimal(18,2) NOT NULL,
        [VALOR_PREMIO] decimal(18,2) NOT NULL,
        [TIPO_PROPONENTE] varchar(1) NOT NULL,
        [ID_SEGURO_PRODUTO_ICATU] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_COBERTURA_ICATU] PRIMARY KEY ([ID_SEGURO_COBERTURA_ICATU]),
        CONSTRAINT [FK_SEGURO_COBERTURA_ICATU_SEGURO_PRODUTO_ICATU_ID_SEGURO_PRODUTO_ICATU] FOREIGN KEY ([ID_SEGURO_PRODUTO_ICATU]) REFERENCES [SEGURO_PRODUTO_ICATU] ([ID_SEGURO_PRODUTO_ICATU])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_CLIENTE_ICATU] (
        [ID_SEGURO_CLIENTE_ICATU] int NOT NULL IDENTITY,
        [DATA_NASCIMENTO] date NOT NULL,
        [Email] varchar(100) NULL,
        [NOME] varchar(255) NULL,
        [NACIONALIDADE] varchar(100) NOT NULL,
        [PPE] varchar(1) NOT NULL,
        [RENDA_MENSAL] decimal(18,2) NOT NULL,
        [RESIDENTE_PAIS] bit NOT NULL,
        [RELACIONAMENTO_ELETRONICO] bit NOT NULL,
        [APOSENTADO] bit NOT NULL,
        [ID_CLIENTE] int NULL,
        [ID_ESTADO_CIVIL] int NULL,
        [ID_GENERO] int NULL,
        [ID_PROFISSAO_ICATU] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_CLIENTE_ICATU] PRIMARY KEY ([ID_SEGURO_CLIENTE_ICATU]),
        CONSTRAINT [FK_SEGURO_CLIENTE_ICATU_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_CLIENTE_ICATU_ESTADO_CIVIL_ID_ESTADO_CIVIL] FOREIGN KEY ([ID_ESTADO_CIVIL]) REFERENCES [ESTADO_CIVIL] ([ID_ESTADO_CIVIL]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_CLIENTE_ICATU_GENERO_ID_GENERO] FOREIGN KEY ([ID_GENERO]) REFERENCES [GENERO] ([ID_GENERO]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_CLIENTE_ICATU_SEGURO_PROFISSAO_ICATU_ID_PROFISSAO_ICATU] FOREIGN KEY ([ID_PROFISSAO_ICATU]) REFERENCES [SEGURO_PROFISSAO_ICATU] ([ID_SEGURO_PROFISSAO_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_BENEFICIARIO] (
        [ID_SEGURO_BENEFICIARIO] int NOT NULL IDENTITY,
        [NOME] varchar(255) NULL,
        [CPF] varchar(50) NULL,
        [DATA_NASCIMENTO] date NOT NULL,
        [VALOR_PERCENTUAL] decimal(18,2) NOT NULL,
        [ID_SEGURO_PROPOSTA] int NULL,
        [ID_SEGURO_CLIENTE] int NULL,
        [ID_SEGURO_PARENTESCO] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_BENEFICIARIO] PRIMARY KEY ([ID_SEGURO_BENEFICIARIO]),
        CONSTRAINT [FK_SEGURO_BENEFICIARIO_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE] FOREIGN KEY ([ID_SEGURO_CLIENTE]) REFERENCES [SEGURO_CLIENTE_ICATU] ([ID_SEGURO_CLIENTE_ICATU]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_BENEFICIARIO_SEGURO_PARENTESCO_ID_SEGURO_PARENTESCO] FOREIGN KEY ([ID_SEGURO_PARENTESCO]) REFERENCES [SEGURO_PARENTESCO] ([ID_SEGURO_PARENTESCO]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_BENEFICIARIO_SEGURO_PROPOSTA_ID_SEGURO_PROPOSTA] FOREIGN KEY ([ID_SEGURO_PROPOSTA]) REFERENCES [SEGURO_PROPOSTA] ([ID_SEGURO_PROPOSTA]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_CLIENTE_TELEFONE] (
        [ID_SEGURO_CLIENTE_TELEFONE] int NOT NULL IDENTITY,
        [DDD] varchar(3) NULL,
        [FONE] varchar(9) NULL,
        [DELETADO] bit NOT NULL,
        [PRINCIPAL] bit NOT NULL,
        [ID_CLIENTE] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_CLIENTE_TELEFONE] PRIMARY KEY ([ID_SEGURO_CLIENTE_TELEFONE]),
        CONSTRAINT [FK_SEGURO_CLIENTE_TELEFONE_SEGURO_CLIENTE_ICATU_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [SEGURO_CLIENTE_ICATU] ([ID_SEGURO_CLIENTE_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_ENDERECO_CLIENTE] (
        [ID_SEGURO_ENDERECO_CLIENTE] int NOT NULL IDENTITY,
        [BAIRRO] varchar(100) NULL,
        [CEP] varchar(8) NULL,
        [COMPLEMENTO] varchar(100) NULL,
        [LOGRADOURO] varchar(255) NULL,
        [Principal] bit NOT NULL,
        [NUMERO] int NOT NULL,
        [ID_MUNICIPIO] int NULL,
        [ID_SEGURO_CLIENTE_ICATU] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_ENDERECO_CLIENTE] PRIMARY KEY ([ID_SEGURO_ENDERECO_CLIENTE]),
        CONSTRAINT [FK_SEGURO_ENDERECO_CLIENTE_MUNICIPIO_ID_MUNICIPIO] FOREIGN KEY ([ID_MUNICIPIO]) REFERENCES [MUNICIPIO] ([ID_MUNICIPIO]),
        CONSTRAINT [FK_SEGURO_ENDERECO_CLIENTE_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU] FOREIGN KEY ([ID_SEGURO_CLIENTE_ICATU]) REFERENCES [SEGURO_CLIENTE_ICATU] ([ID_SEGURO_CLIENTE_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_PROPOSTA_ICATU] (
        [ID_SEGURO_PROPOSTA_ICATU] int NOT NULL IDENTITY,
        [DATA_ASSINATURA] date NOT NULL,
        [DATA_INICIO_VIGENCIA] date NOT NULL,
        [DATA_FIM_VIGENCIA] date NOT NULL,
        [DATA_PROTOCOLO] date NOT NULL,
        [VALOR_PREMIO_TOTAL] decimal(18,2) NOT NULL,
        [VALOR_PRIMEIRO_PREMIO_PAGO] bit NOT NULL,
        [NUMERO_PROPOSTA_ICATU] bigint NOT NULL,
        [ID_SEGURO_PROPOSTA] int NULL,
        [ID_SEGURO_CLIENTE_ICATU] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_PROPOSTA_ICATU] PRIMARY KEY ([ID_SEGURO_PROPOSTA_ICATU]),
        CONSTRAINT [FK_SEGURO_PROPOSTA_ICATU_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU] FOREIGN KEY ([ID_SEGURO_CLIENTE_ICATU]) REFERENCES [SEGURO_CLIENTE_ICATU] ([ID_SEGURO_CLIENTE_ICATU]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_CLIENTE_ICATU] FOREIGN KEY ([ID_SEGURO_CLIENTE_ICATU]) REFERENCES [SEGURO_PROPOSTA] ([ID_SEGURO_PROPOSTA]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_BENEFICIARIO_ICATU] (
        [ID_SEGURO_BENEFICIARIO_ICATU] int NOT NULL IDENTITY,
        [NOME] varchar(255) NULL,
        [CPF] varchar(50) NULL,
        [DATA_NASCIMENTO] date NOT NULL,
        [VALOR_PERCENTUAL] decimal(18,2) NOT NULL,
        [ID_SEGURO_PROPOSTA] int NULL,
        [ID_SEGURO_CLIENTE_ICATU] int NULL,
        [ID_SEGURO_PARENTESCO_ICATU] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_BENEFICIARIO_ICATU] PRIMARY KEY ([ID_SEGURO_BENEFICIARIO_ICATU]),
        CONSTRAINT [FK_SEGURO_BENEFICIARIO_ICATU_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU] FOREIGN KEY ([ID_SEGURO_CLIENTE_ICATU]) REFERENCES [SEGURO_CLIENTE_ICATU] ([ID_SEGURO_CLIENTE_ICATU]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_BENEFICIARIO_ICATU_SEGURO_PARENTESCO_ICATU_ID_SEGURO_PARENTESCO_ICATU] FOREIGN KEY ([ID_SEGURO_PARENTESCO_ICATU]) REFERENCES [SEGURO_PARENTESCO_ICATU] ([ID_SEGURO_PARENTESCO_ICATU]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SEGURO_BENEFICIARIO_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA] FOREIGN KEY ([ID_SEGURO_PROPOSTA]) REFERENCES [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_PROPOSTA_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_COBRANCA_PROPOSTA_ICATU] (
        [ID_SEGURO_COBRANCA_PROPOSTA_ICATU] int NOT NULL IDENTITY,
        [DATA_VENCIMENTO] date NOT NULL,
        [ID_CONVENIO] int NOT NULL,
        [LINK_PAGAMENTO] varchar(255) NULL,
        [ID_PEDIDO_PAGAMENTO] varchar(255) NULL,
        [ID_LINK_PAGAMENTO] varchar(255) NULL,
        [ID_COBRANCA] varchar(255) NULL,
        [IdSeguroPropostaIcatu] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_COBRANCA_PROPOSTA_ICATU] PRIMARY KEY ([ID_SEGURO_COBRANCA_PROPOSTA_ICATU]),
        CONSTRAINT [FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_IdSeguroPropostaIcatu] FOREIGN KEY ([IdSeguroPropostaIcatu]) REFERENCES [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_PROPOSTA_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_SITUACAO_ICATU] (
        [ID_SEGURO_SITUACAO_ICATU] int NOT NULL IDENTITY,
        [STATUS] varchar(4000) NULL,
        [ID_SEGURO_PROPOSTA_ICATU] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_SITUACAO_ICATU] PRIMARY KEY ([ID_SEGURO_SITUACAO_ICATU]),
        CONSTRAINT [FK_SEGURO_SITUACAO_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU] FOREIGN KEY ([ID_SEGURO_PROPOSTA_ICATU]) REFERENCES [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_PROPOSTA_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] (
        [ID_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] int NOT NULL IDENTITY,
        [ID_CARTAO] nvarchar(4000) NULL,
        [ID_COBRANCA] nvarchar(4000) NULL,
        [QUATRO_ULTIMOS_DIGITOS_CARTAO] varchar(4) NULL,
        [ID_SEGURO_COBRANCA_PROPOSTA_ICATU] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] PRIMARY KEY ([ID_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU]),
        CONSTRAINT [FK_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU_SEGURO_COBRANCA_PROPOSTA_ICATU_ID_SEGURO_COBRANCA_PROPOSTA_ICATU] FOREIGN KEY ([ID_SEGURO_COBRANCA_PROPOSTA_ICATU]) REFERENCES [SEGURO_COBRANCA_PROPOSTA_ICATU] ([ID_SEGURO_COBRANCA_PROPOSTA_ICATU]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_ENDERECO_PRINCIPAL] ON [CLIENTE] ([ID_ENDERECO_PRINCIPAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_ENDERECO_SECUNDARIO] ON [CLIENTE] ([ID_ENDERECO_SECUNDARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_TELEFONE_PRINCIPAL] ON [CLIENTE] ([ID_TELEFONE_PRINCIPAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_TELEFONE_SECUNDARIO] ON [CLIENTE] ([ID_TELEFONE_SECUNDARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE UNIQUE INDEX [IX_BIOMETRIA_CLIENTE_ID_CLIENTE] ON [BIOMETRIA_CLIENTE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE UNIQUE INDEX [IX_BIOMETRIA_CLIENTE_IdRegistroBiometriaUnico] ON [BIOMETRIA_CLIENTE] ([IdRegistroBiometriaUnico]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CONJUGE_ID_CLIENTE] ON [CONJUGE] ([ID_CLIENTE]) WHERE [ID_CLIENTE] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_CONJUGE_ID_GENERO] ON [CONJUGE] ([ID_GENERO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_CONJUGE_ID_TIPO_REGIME_CASAMENTO] ON [CONJUGE] ([ID_TIPO_REGIME_CASAMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_BIOMETRIA_UNICO_ID_CLIENTE] ON [REGISTRO_BIOMETRIA_UNICO] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_SMS_ID_TEMPLATE_SMS] ON [REGISTRO_SMS] ([ID_TEMPLATE_SMS]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_SMS_ID_USUARIO] ON [REGISTRO_SMS] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_TORPEDO_VOZ_ID_TEMPLATE_TORPEDO_VOZ] ON [REGISTRO_TORPEDO_VOZ] ([ID_TEMPLATE_TORPEDO_VOZ]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_TORPEDO_VOZ_ID_USUARIO] ON [REGISTRO_TORPEDO_VOZ] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_WHATSAPP_ID_TEMPLATE_WHATSAPP] ON [REGISTRO_WHATSAPP] ([ID_TEMPLATE_WHATSAPP]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_REGISTRO_WHATSAPP_ID_USUARIO] ON [REGISTRO_WHATSAPP] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_BENEFICIARIO_ID_SEGURO_CLIENTE] ON [SEGURO_BENEFICIARIO] ([ID_SEGURO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_BENEFICIARIO_ID_SEGURO_PARENTESCO] ON [SEGURO_BENEFICIARIO] ([ID_SEGURO_PARENTESCO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_BENEFICIARIO_ID_SEGURO_PROPOSTA] ON [SEGURO_BENEFICIARIO] ([ID_SEGURO_PROPOSTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_BENEFICIARIO_ICATU_ID_SEGURO_CLIENTE_ICATU] ON [SEGURO_BENEFICIARIO_ICATU] ([ID_SEGURO_CLIENTE_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_BENEFICIARIO_ICATU_ID_SEGURO_PARENTESCO_ICATU] ON [SEGURO_BENEFICIARIO_ICATU] ([ID_SEGURO_PARENTESCO_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_BENEFICIARIO_ICATU_ID_SEGURO_PROPOSTA] ON [SEGURO_BENEFICIARIO_ICATU] ([ID_SEGURO_PROPOSTA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_SEGURO_CLIENTE_ICATU_ID_CLIENTE] ON [SEGURO_CLIENTE_ICATU] ([ID_CLIENTE]) WHERE [ID_CLIENTE] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_CLIENTE_ICATU_ID_ESTADO_CIVIL] ON [SEGURO_CLIENTE_ICATU] ([ID_ESTADO_CIVIL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_CLIENTE_ICATU_ID_GENERO] ON [SEGURO_CLIENTE_ICATU] ([ID_GENERO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_CLIENTE_ICATU_ID_PROFISSAO_ICATU] ON [SEGURO_CLIENTE_ICATU] ([ID_PROFISSAO_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_CLIENTE_TELEFONE_ID_CLIENTE] ON [SEGURO_CLIENTE_TELEFONE] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_COBERTURA_ID_SEGURO_PRODUTO] ON [SEGURO_COBERTURA] ([ID_SEGURO_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_COBERTURA_ICATU_ID_SEGURO_PRODUTO_ICATU] ON [SEGURO_COBERTURA_ICATU] ([ID_SEGURO_PRODUTO_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU_ID_SEGURO_COBRANCA_PROPOSTA_ICATU] ON [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] ([ID_SEGURO_COBRANCA_PROPOSTA_ICATU]) WHERE [ID_SEGURO_COBRANCA_PROPOSTA_ICATU] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_COBRANCA_PROPOSTA_ICATU_IdSeguroPropostaIcatu] ON [SEGURO_COBRANCA_PROPOSTA_ICATU] ([IdSeguroPropostaIcatu]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_ENDERECO_CLIENTE_ID_MUNICIPIO] ON [SEGURO_ENDERECO_CLIENTE] ([ID_MUNICIPIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_ENDERECO_CLIENTE_ID_SEGURO_CLIENTE_ICATU] ON [SEGURO_ENDERECO_CLIENTE] ([ID_SEGURO_CLIENTE_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE UNIQUE INDEX [IX_SEGURO_PARENTESCO_ICATU_ID_SEGURO_PARENTESCO_BEM] ON [SEGURO_PARENTESCO_ICATU] ([ID_SEGURO_PARENTESCO_BEM]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_PRODUTO_ID_PRODUTO] ON [SEGURO_PRODUTO] ([ID_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_PRODUTO_ICATU_ID_SEGURO_PRODUTO] ON [SEGURO_PRODUTO_ICATU] ([ID_SEGURO_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE UNIQUE INDEX [IX_SEGURO_PROFISSAO_ICATU_ID_SEGURO_PROFISSAO_BEM] ON [SEGURO_PROFISSAO_ICATU] ([ID_SEGURO_PROFISSAO_BEM]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE UNIQUE INDEX [IX_SEGURO_PROPOSTA_ID_CLIENTE] ON [SEGURO_PROPOSTA] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_PROPOSTA_ID_SEGURO_PRODUTO] ON [SEGURO_PROPOSTA] ([ID_SEGURO_PRODUTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU] ON [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_CLIENTE_ICATU]) WHERE [ID_SEGURO_CLIENTE_ICATU] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_SEGURO_SITUACAO_ICATU_ID_SEGURO_PROPOSTA_ICATU] ON [SEGURO_SITUACAO_ICATU] ([ID_SEGURO_PROPOSTA_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_TIPO_REGIME_CASAMENTO_BEM_ID_TIPO_REGIME_CASAMENTO] ON [TIPO_REGIME_CASAMENTO_BEM] ([ID_TIPO_REGIME_CASAMENTO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE INDEX [IX_USUARIO_CONFIRMACAO_EMAIL_ID_USUARIO] ON [USUARIO_CONFIRMACAO_EMAIL] ([ID_USUARIO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    CREATE UNIQUE INDEX [IX_USUARIO_CONFIRMACAO_EMAIL_TOKEN] ON [USUARIO_CONFIRMACAO_EMAIL] ([TOKEN]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_ENDERECO_CLIENTE_ID_ENDERECO_PRINCIPAL] FOREIGN KEY ([ID_ENDERECO_PRINCIPAL]) REFERENCES [ENDERECO_CLIENTE] ([ID_ENDERECO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_ENDERECO_CLIENTE_ID_ENDERECO_SECUNDARIO] FOREIGN KEY ([ID_ENDERECO_SECUNDARIO]) REFERENCES [ENDERECO_CLIENTE] ([ID_ENDERECO_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_TELEFONE_CLIENTE_ID_TELEFONE_PRINCIPAL] FOREIGN KEY ([ID_TELEFONE_PRINCIPAL]) REFERENCES [TELEFONE_CLIENTE] ([ID_TELEFONE_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_TELEFONE_CLIENTE_ID_TELEFONE_SECUNDARIO] FOREIGN KEY ([ID_TELEFONE_SECUNDARIO]) REFERENCES [TELEFONE_CLIENTE] ([ID_TELEFONE_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220328152745_AddSeguroAndNovasMensagensAndBiometriaUpdates', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_ICATU] DROP CONSTRAINT [FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_IdSeguroPropostaIcatu];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_ENDERECO_CLIENTE] DROP CONSTRAINT [FK_SEGURO_ENDERECO_CLIENTE_SEGURO_CLIENTE_ICATU_ID_SEGURO_CLIENTE_ICATU];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DROP INDEX [IX_SEGURO_ENDERECO_CLIENTE_ID_SEGURO_CLIENTE_ICATU] ON [SEGURO_ENDERECO_CLIENTE];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DECLARE @var127 sysname;
    SELECT @var127 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_ENDERECO_CLIENTE]') AND [c].[name] = N'ID_SEGURO_CLIENTE_ICATU');
    IF @var127 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_ENDERECO_CLIENTE] DROP CONSTRAINT [' + @var127 + '];');
    ALTER TABLE [SEGURO_ENDERECO_CLIENTE] DROP COLUMN [ID_SEGURO_CLIENTE_ICATU];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DECLARE @var128 sysname;
    SELECT @var128 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_ENDERECO_CLIENTE]') AND [c].[name] = N'Principal');
    IF @var128 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_ENDERECO_CLIENTE] DROP CONSTRAINT [' + @var128 + '];');
    ALTER TABLE [SEGURO_ENDERECO_CLIENTE] DROP COLUMN [Principal];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    EXEC sp_rename N'[SEGURO_COBRANCA_PROPOSTA_ICATU].[IdSeguroPropostaIcatu]', N'ID_SEGURO_PROPOSTA_ICATU', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    EXEC sp_rename N'[SEGURO_COBRANCA_PROPOSTA_ICATU].[IX_SEGURO_COBRANCA_PROPOSTA_ICATU_IdSeguroPropostaIcatu]', N'IX_SEGURO_COBRANCA_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DECLARE @var129 sysname;
    SELECT @var129 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_ENDERECO_CLIENTE]') AND [c].[name] = N'NUMERO');
    IF @var129 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_ENDERECO_CLIENTE] DROP CONSTRAINT [' + @var129 + '];');
    ALTER TABLE [SEGURO_ENDERECO_CLIENTE] ALTER COLUMN [NUMERO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DECLARE @var130 sysname;
    SELECT @var130 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU]') AND [c].[name] = N'ID_COBRANCA');
    IF @var130 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] DROP CONSTRAINT [' + @var130 + '];');
    ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] ALTER COLUMN [ID_COBRANCA] varchar(4000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DECLARE @var131 sysname;
    SELECT @var131 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU]') AND [c].[name] = N'ID_CARTAO');
    IF @var131 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] DROP CONSTRAINT [' + @var131 + '];');
    ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] ALTER COLUMN [ID_CARTAO] varchar(4000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] ADD [CPF_TITULAR] varchar(14) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_CARTAO_ICATU] ADD [TITULAR] varchar(255) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    DECLARE @var132 sysname;
    SELECT @var132 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_CLIENTE_ICATU]') AND [c].[name] = N'DATA_NASCIMENTO');
    IF @var132 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_CLIENTE_ICATU] DROP CONSTRAINT [' + @var132 + '];');
    ALTER TABLE [SEGURO_CLIENTE_ICATU] ALTER COLUMN [DATA_NASCIMENTO] date NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_CLIENTE_ICATU] ADD [ID_ENDERECO_COBRANCA] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_CLIENTE_ICATU] ADD [ID_ENDERECO_PRINCIPAL] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [CLIENTE] ADD [ID_PROFISSAO] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    CREATE INDEX [IX_SEGURO_CLIENTE_ICATU_ID_ENDERECO_COBRANCA] ON [SEGURO_CLIENTE_ICATU] ([ID_ENDERECO_COBRANCA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    CREATE INDEX [IX_SEGURO_CLIENTE_ICATU_ID_ENDERECO_PRINCIPAL] ON [SEGURO_CLIENTE_ICATU] ([ID_ENDERECO_PRINCIPAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    CREATE INDEX [IX_CLIENTE_ID_PROFISSAO] ON [CLIENTE] ([ID_PROFISSAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_SEGURO_PROFISSAO_ID_PROFISSAO] FOREIGN KEY ([ID_PROFISSAO]) REFERENCES [SEGURO_PROFISSAO] ([ID_SEGURO_PROFISSAO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_CLIENTE_ICATU] ADD CONSTRAINT [FK_SEGURO_CLIENTE_ICATU_SEGURO_ENDERECO_CLIENTE_ID_ENDERECO_COBRANCA] FOREIGN KEY ([ID_ENDERECO_COBRANCA]) REFERENCES [SEGURO_ENDERECO_CLIENTE] ([ID_SEGURO_ENDERECO_CLIENTE]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_CLIENTE_ICATU] ADD CONSTRAINT [FK_SEGURO_CLIENTE_ICATU_SEGURO_ENDERECO_CLIENTE_ID_ENDERECO_PRINCIPAL] FOREIGN KEY ([ID_ENDERECO_PRINCIPAL]) REFERENCES [SEGURO_ENDERECO_CLIENTE] ([ID_SEGURO_ENDERECO_CLIENTE]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    ALTER TABLE [SEGURO_COBRANCA_PROPOSTA_ICATU] ADD CONSTRAINT [FK_SEGURO_COBRANCA_PROPOSTA_ICATU_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA_ICATU] FOREIGN KEY ([ID_SEGURO_PROPOSTA_ICATU]) REFERENCES [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_PROPOSTA_ICATU]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220331145741_UpdateSeguroPropostaEntities')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220331145741_UpdateSeguroPropostaEntities', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407204941_AddRegistroClubeBeneficioUpdateCliente')
BEGIN
    ALTER TABLE [CLIENTE] ADD [OPERACAO_ATIVA] bit NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407204941_AddRegistroClubeBeneficioUpdateCliente')
BEGIN
    CREATE TABLE [REGISTRO_CLUBE_BENEFICIOS] (
        [ID_REGISTRO_CLUBE_BENEFICIOS] int NOT NULL IDENTITY,
        [ID_CLIENTE] int NOT NULL,
        [DATA_ACESSO] datetime2 NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_REGISTRO_CLUBE_BENEFICIOS] PRIMARY KEY ([ID_REGISTRO_CLUBE_BENEFICIOS]),
        CONSTRAINT [FK_REGISTRO_CLUBE_BENEFICIOS_CLIENTE_ID_CLIENTE] FOREIGN KEY ([ID_CLIENTE]) REFERENCES [CLIENTE] ([ID_CLIENTE])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407204941_AddRegistroClubeBeneficioUpdateCliente')
BEGIN
    CREATE UNIQUE INDEX [IX_REGISTRO_CLUBE_BENEFICIOS_ID_CLIENTE] ON [REGISTRO_CLUBE_BENEFICIOS] ([ID_CLIENTE]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220407204941_AddRegistroClubeBeneficioUpdateCliente')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220407204941_AddRegistroClubeBeneficioUpdateCliente', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411200425_fixed idSeguroProposta')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA_ICATU] DROP CONSTRAINT [FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_CLIENTE_ICATU];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411200425_fixed idSeguroProposta')
BEGIN
    DROP INDEX [IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU] ON [SEGURO_PROPOSTA_ICATU];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411200425_fixed idSeguroProposta')
BEGIN
    CREATE INDEX [IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_CLIENTE_ICATU] ON [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_CLIENTE_ICATU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411200425_fixed idSeguroProposta')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_SEGURO_PROPOSTA_ICATU_ID_SEGURO_PROPOSTA] ON [SEGURO_PROPOSTA_ICATU] ([ID_SEGURO_PROPOSTA]) WHERE [ID_SEGURO_PROPOSTA] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411200425_fixed idSeguroProposta')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA_ICATU] ADD CONSTRAINT [FK_SEGURO_PROPOSTA_ICATU_SEGURO_PROPOSTA_ID_SEGURO_PROPOSTA] FOREIGN KEY ([ID_SEGURO_PROPOSTA]) REFERENCES [SEGURO_PROPOSTA] ([ID_SEGURO_PROPOSTA]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220411200425_fixed idSeguroProposta')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220411200425_fixed idSeguroProposta', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220413052007_UpdateSeguroPrpoposta')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA] ADD [URL_PDF_CONTRATO] varchar(255) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220413052007_UpdateSeguroPrpoposta')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220413052007_UpdateSeguroPrpoposta', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220414210313_UpdateSeguroPropostaAssinaturaProps')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA] ADD [IP_ORIGEM] varchar(19) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220414210313_UpdateSeguroPropostaAssinaturaProps')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA] ADD [LATITUDE] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220414210313_UpdateSeguroPropostaAssinaturaProps')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA] ADD [LONGITUDE] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220414210313_UpdateSeguroPropostaAssinaturaProps')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220414210313_UpdateSeguroPropostaAssinaturaProps', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220420195231_UpdateSeguroProdutoIcatu')
BEGIN
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ADD [CODIGO_PONTO_DE_VENDA] varchar(2000) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220420195231_UpdateSeguroProdutoIcatu')
BEGIN
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ADD [ID_PARCERIA] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220420195231_UpdateSeguroProdutoIcatu')
BEGIN
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ADD [SUBESTIPULANTE] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220420195231_UpdateSeguroProdutoIcatu')
BEGIN
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ADD [TIPO_NUMERACAO_PROPOSTA] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220420195231_UpdateSeguroProdutoIcatu')
BEGIN
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ADD [VALOR_PREMIO] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220420195231_UpdateSeguroProdutoIcatu')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220420195231_UpdateSeguroProdutoIcatu', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426135848_correcaoCamposIcatuSeguro')
BEGIN
    EXEC sp_rename N'[SEGURO_PRODUTO_ICATU].[CODIGO_PONTO_DE_VENDA]', N'CODIGO_PONTO_VENDA', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426135848_correcaoCamposIcatuSeguro')
BEGIN
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ADD [CODIGO_GRUPO_APOLICE] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426135848_correcaoCamposIcatuSeguro')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220426135848_correcaoCamposIcatuSeguro', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426163524_correcaoTipoValorPremio')
BEGIN
    DECLARE @var133 sysname;
    SELECT @var133 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SEGURO_PRODUTO_ICATU]') AND [c].[name] = N'VALOR_PREMIO');
    IF @var133 IS NOT NULL EXEC(N'ALTER TABLE [SEGURO_PRODUTO_ICATU] DROP CONSTRAINT [' + @var133 + '];');
    ALTER TABLE [SEGURO_PRODUTO_ICATU] ALTER COLUMN [VALOR_PREMIO] decimal(18,2) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426163524_correcaoTipoValorPremio')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220426163524_correcaoTipoValorPremio', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428150958_AddTipoFuncionaCargoMarinhaAeronautica')
BEGIN
    CREATE TABLE [AERONAUTICA_CARGO] (
        [ID_AERONAUTICA_CARGO] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(60) NOT NULL,
        [SIGLA] varchar(10) NOT NULL,
        [CODIGO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_AERONAUTICA_CARGO] PRIMARY KEY ([ID_AERONAUTICA_CARGO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428150958_AddTipoFuncionaCargoMarinhaAeronautica')
BEGIN
    CREATE TABLE [AERONAUTICA_TIPO_FUNCIONAL] (
        [ID_AERONAUTICA_TIPO_FUNCIONAL] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(100) NOT NULL,
        [SIGLA] varchar(1) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_AERONAUTICA_TIPO_FUNCIONAL] PRIMARY KEY ([ID_AERONAUTICA_TIPO_FUNCIONAL])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428150958_AddTipoFuncionaCargoMarinhaAeronautica')
BEGIN
    CREATE TABLE [MARINHA_CARGO] (
        [ID_MARINHA_CARGO] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(60) NOT NULL,
        [SIGLA] varchar(10) NOT NULL,
        [CODIGO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_MARINHA_CARGO] PRIMARY KEY ([ID_MARINHA_CARGO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428150958_AddTipoFuncionaCargoMarinhaAeronautica')
BEGIN
    CREATE TABLE [MARINHA_TIPO_FUNCIONAL] (
        [ID_MARINHA_TIPO_FUNCIONAL] int NOT NULL IDENTITY,
        [DESCRICAO] varchar(100) NOT NULL,
        [SIGLA] varchar(1) NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_MARINHA_TIPO_FUNCIONAL] PRIMARY KEY ([ID_MARINHA_TIPO_FUNCIONAL])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428150958_AddTipoFuncionaCargoMarinhaAeronautica')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220428150958_AddTipoFuncionaCargoMarinhaAeronautica', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE_AERONAUTICA] (
        [ID_RENDIMENTO_CLIENTE] int NOT NULL,
        [ID_AERONAUTICA_TIPO_FUNCIONAL] int NOT NULL,
        [ID_AERONAUTICA_CARGO] int NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE_AERONAUTICA] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_AERONAUTICA_AERONAUTICA_CARGO_ID_AERONAUTICA_CARGO] FOREIGN KEY ([ID_AERONAUTICA_CARGO]) REFERENCES [AERONAUTICA_CARGO] ([ID_AERONAUTICA_CARGO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_AERONAUTICA_AERONAUTICA_TIPO_FUNCIONAL_ID_AERONAUTICA_TIPO_FUNCIONAL] FOREIGN KEY ([ID_AERONAUTICA_TIPO_FUNCIONAL]) REFERENCES [AERONAUTICA_TIPO_FUNCIONAL] ([ID_AERONAUTICA_TIPO_FUNCIONAL]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_AERONAUTICA_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    CREATE TABLE [RENDIMENTO_CLIENTE_MARINHA] (
        [ID_RENDIMENTO_CLIENTE] int NOT NULL,
        [ID_MARINHA_TIPO_FUNCIONAL] int NOT NULL,
        [ID_MARINHA_CARGO] int NULL,
        CONSTRAINT [PK_RENDIMENTO_CLIENTE_MARINHA] PRIMARY KEY ([ID_RENDIMENTO_CLIENTE]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_MARINHA_MARINHA_CARGO_ID_MARINHA_CARGO] FOREIGN KEY ([ID_MARINHA_CARGO]) REFERENCES [MARINHA_CARGO] ([ID_MARINHA_CARGO]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_MARINHA_MARINHA_TIPO_FUNCIONAL_ID_MARINHA_TIPO_FUNCIONAL] FOREIGN KEY ([ID_MARINHA_TIPO_FUNCIONAL]) REFERENCES [MARINHA_TIPO_FUNCIONAL] ([ID_MARINHA_TIPO_FUNCIONAL]),
        CONSTRAINT [FK_RENDIMENTO_CLIENTE_MARINHA_RENDIMENTO_CLIENTE_ID_RENDIMENTO_CLIENTE] FOREIGN KEY ([ID_RENDIMENTO_CLIENTE]) REFERENCES [RENDIMENTO_CLIENTE] ([ID_RENDIMENTO_CLIENTE]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_AERONAUTICA_ID_AERONAUTICA_CARGO] ON [RENDIMENTO_CLIENTE_AERONAUTICA] ([ID_AERONAUTICA_CARGO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_AERONAUTICA_ID_AERONAUTICA_TIPO_FUNCIONAL] ON [RENDIMENTO_CLIENTE_AERONAUTICA] ([ID_AERONAUTICA_TIPO_FUNCIONAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_MARINHA_ID_MARINHA_CARGO] ON [RENDIMENTO_CLIENTE_MARINHA] ([ID_MARINHA_CARGO]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    CREATE INDEX [IX_RENDIMENTO_CLIENTE_MARINHA_ID_MARINHA_TIPO_FUNCIONAL] ON [RENDIMENTO_CLIENTE_MARINHA] ([ID_MARINHA_TIPO_FUNCIONAL]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220502165824_AddRendimentoClienteMarinhaEAeronautica')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220502165824_AddRendimentoClienteMarinhaEAeronautica', N'5.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    ALTER TABLE [SEGURO_PROPOSTA] ADD [IdMeioPagamento] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    ALTER TABLE [CLIENTE] ADD [IdContaBancaria] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    CREATE TABLE [CONTA_BANCARIA] (
        [ID_CONTA_BANCARIA] int NOT NULL IDENTITY,
        [AGENCIA] varchar(20) NULL,
        [NUMERO_CONTA] varchar(255) NULL,
        [DIGITO_VERIFICADOR_AGENCIA] int NOT NULL,
        [IdBanco] int NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_CONTA_BANCARIA] PRIMARY KEY ([ID_CONTA_BANCARIA]),
        CONSTRAINT [FK_CONTA_BANCARIA_BANCO_IdBanco] FOREIGN KEY ([IdBanco]) REFERENCES [BANCO] ([ID_BANCO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    CREATE TABLE [MEIO_PAGAMENTO_SEGURO] (
        [ID_MEIO_PAGAMENTO_SEGURO] int NOT NULL IDENTITY,
        [IdProduto] int NOT NULL,
        [ID_MEIO_PAGAMENTO] int NOT NULL,
        [USUARIO_ATUALIZACAO] varchar(10) NULL,
        [DATA_ATUALIZACAO] datetime2 NOT NULL,
        CONSTRAINT [PK_MEIO_PAGAMENTO_SEGURO] PRIMARY KEY ([ID_MEIO_PAGAMENTO_SEGURO]),
        CONSTRAINT [FK_MEIO_PAGAMENTO_SEGURO_PRODUTO_IdProduto] FOREIGN KEY ([IdProduto]) REFERENCES [PRODUTO] ([ID_PRODUTO])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    CREATE INDEX [IX_CLIENTE_IdContaBancaria] ON [CLIENTE] ([IdContaBancaria]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    CREATE INDEX [IX_CONTA_BANCARIA_IdBanco] ON [CONTA_BANCARIA] ([IdBanco]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    CREATE INDEX [IX_MEIO_PAGAMENTO_SEGURO_IdProduto] ON [MEIO_PAGAMENTO_SEGURO] ([IdProduto]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    ALTER TABLE [CLIENTE] ADD CONSTRAINT [FK_CLIENTE_CONTA_BANCARIA_IdContaBancaria] FOREIGN KEY ([IdContaBancaria]) REFERENCES [CONTA_BANCARIA] ([ID_CONTA_BANCARIA]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220504134236_AddContaBancaria')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220504134236_AddContaBancaria', N'5.0.3');
END;
GO

COMMIT;
GO

