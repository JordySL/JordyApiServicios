# JordyApiServicios
Tabla:
CREATE TABLE [dbo].[Cliente] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [nombre]          VARCHAR (100) NULL,
    [ApellidoPaterno] VARCHAR (100) NULL,
    [ApellidoMaterno] VARCHAR (100) NULL,
    [Direccion]       VARCHAR (100) NULL,
    [Foto]            VARCHAR (250) NULL,
    [Doc]             VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

Procedimiento:

CREATE PROCEDURE [dbo].[GetClienteByParams]
	@offset int,
	@perpage int
AS
	SELECT c.*, COUNT(*) OVER() Total
	from dbo.Cliente c
	order by 1 
	OFFSET @perpage * (@offset -1) ROWS
	FETCH NEXT @perpage ROWS ONLY;
RETURN 0


CREATE PROCEDURE dbo.UpdateCliente
	@id int,
	@Nombre varchar(100)=0,
	@ApellidoPaterno varchar(100)=0,
	@ApellidoMaterno varchar(100)=0,
	@Direccion varchar(100)=0
AS
	update Cliente
	set Nombre = @Nombre,
	ApellidoPaterno = @ApellidoPaterno,
	ApellidoMaterno = @ApellidoMaterno,
	Direccion = @Direccion
	where Id = @id
RETURN 0
