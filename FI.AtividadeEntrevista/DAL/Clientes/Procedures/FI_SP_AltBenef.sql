﻿CREATE PROC FI_SP_AltBenef
    @NOME          VARCHAR (50),
	@CPF           VARCHAR (14),
	@ID			   BIGINT,
	@IDCLIENTE	   BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		CPF = @CPF,
		NOME = @NOME,
		IDCLIENTE = @IDCLIENTE
	WHERE ID = @ID
END