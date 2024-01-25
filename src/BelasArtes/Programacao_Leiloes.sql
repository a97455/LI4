CREATE PROCEDURE VerificarAtualizarLeilao
AS
BEGIN
    UPDATE Leilao
    SET CodEstado = 2
    WHERE DataInicio > GETDATE() AND CodEstado = 1;
    UPDATE Leilao
    SET CodEstado = 3
    WHERE DataFim > GETDATE() AND CodEstado = 2;
END;

USE msdb;
GO

EXEC sp_add_job
    @job_name = N'AtualizarLeiloesJob';

EXEC sp_add_jobstep
    @job_name = N'AtualizarLeiloesJob',
    @step_name = N'ExecutarProcedimento',
    @subsystem = N'TSQL',
    @command = N'EXEC VerificarAtualizarLeilao;',
    @retry_attempts = 0,
    @retry_interval = 1;

EXEC sp_add_schedule
    @job_name = N'AtualizarLeiloesJob',
    @name = N'MinuteSchedule',
    @freq_type = 4,
    @freq_interval = 1;

EXEC sp_attach_schedule
    @job_name = N'AtualizarLeiloesJob',
    @schedule_name = N'MinuteSchedule';

EXEC sp_add_jobserver
    @job_name = N'AtualizarLeiloesJob';

