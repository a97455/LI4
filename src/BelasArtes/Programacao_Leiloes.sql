-- Criar o procedimento armazenado, se não existir
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'VerificarAtualizarLeilao')
BEGIN
    EXEC('CREATE PROCEDURE VerificarAtualizarLeilao AS
    BEGIN
        UPDATE Leilao
        SET CodEstado = 2
        WHERE DataInicio < GETDATE() AND CodEstado = 1;

        UPDATE Leilao
        SET CodEstado = 3
        WHERE DataFim < GETDATE() AND CodEstado = 2;
    END;');
END;

-- Criar um job no SQL Server Agent
USE msdb;
GO

DECLARE @jobId UNIQUEIDENTIFIER;
DECLARE @jobName NVARCHAR(128);

SET @jobName = 'AtualizarLeilaoJob';

-- Verificar se o job já existe
IF NOT EXISTS (SELECT job_id FROM msdb.dbo.sysjobs WHERE name = @jobName)
BEGIN
    -- Criar o job
    EXEC msdb.dbo.sp_add_job
        @job_name = @jobName,
        @enabled = 1,
        @notify_level_eventlog = 0,
        @notify_level_email = 0;

    -- Obter o ID do job
    SET @jobId = (SELECT job_id FROM msdb.dbo.sysjobs WHERE name = @jobName);

    -- Adicionar uma etapa ao job
    EXEC msdb.dbo.sp_add_jobstep
        @job_id = @jobId,
        @step_id = 1,
        @step_name = 'Executar Procedimento',
        @subsystem = 'TSQL',
        @command = 'EXEC VerificarAtualizarLeilao;',
        @on_success_action = 3; -- Encerrar o job em caso de sucesso

    -- Agendar o job para ser executado a cada minuto
    EXEC msdb.dbo.sp_add_jobschedule
        @job_id = @jobId,
        @name = 'MinutelySchedule',
        @freq_type = 4,  -- Frequência: Minutely
        @freq_interval = 1; -- A cada 1 minuto
END;
