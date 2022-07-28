
USE DBNADSI



SELECT * FROM tLogImpuestoConcor where refOperativa in(
'NL1-FCA-67736',
'NL1-FCA-67727',
'NL1-FCA-67695',
'NL1-FCA-67691',
'NL1-FCA-67687',
'NL1-FCA-67773',
'NL1-FCA-67770',
'NL1-FCA-67759',
'NL1-FCA-67758',
'NL1-FCA-67900',
'NL1-FCA-67762',
'NL1-FCA-67756',
'NL1-FCA-67755',
'NL1-FCA-67651'
)

SELECT * FROM tImpuesto where idSubreferencia in(SELECT idSubreferencia FROM tSubReferencia where refOperativa in(
'PN1-NPN-380709R'
)

)


UPDATE tImpuesto
SET idCliente = 3685
WHERE idImpuesto = 1287003



SELECT * FROM tSubReferencia WHERE idSubreferencia = 556852
SELECT * FROM [dbo].[tLogCargaReferencia] WHERE refOperativa = 'Q015F3/22'

SELECT * FROM [dbo].[tLogCargaReferencia] WHERE RFCClienteSecundario = 'CEXTR920901TS4'

UPDATE tSubReferencia
SET idClienteContable = 3685 , idClienteOperativo = 3685
WHERE refOperativa = 'Q015F3/22'


SELECT * FROM tCliente WHERE idCliente = 3685


--Se declara contador
declare @icont int
declare @tabla as table(Instancia varchar(3),
		Cliente varchar (30),TipoOperacion varchar (30),RefOperativa varchar (20),
		Pedimento varchar (20),Efectivo float,Banco varchar (50),DesCuenta varchar (20),
		Servicio varchar (20),Importe float,FechaPago varchar(10),FormaPago varchar(50))
insert into @tabla(Instancia,Cliente,Pedimento,TipoOperacion,RefOperativa,Banco,Servicio,FormaPago,DesCuenta,FechaPago,Efectivo,Importe)
select distinct 'CC',rfcCliente,aduana+patente+pedimento,tipoOperacion,refOperativa,idBanco,servicio,
formaPagoPedimento,cuentaBancaria,fechaPago,efectivo,importe from tLogImpuestoConcor 
where refOperativa in(
'PN1-NPN-380709R'
)

select distinct RefOperativa from @tabla

--Se ajusta contador
set @icont=(select count(*) from @tabla)

 WHILE @icont >0
 BEGIN
    DECLARE @sInstancia varchar(3),
			 @sCliente varchar (30),
			 @sTipoOperacion varchar (30),
			 @sRefOperativa varchar (20),
			 @sPedimento varchar (20),
			 @fEfectivo float,
			 @sBanco varchar (50),
			 @sDesCuenta varchar (20),
			 @sServicio varchar (20),
			 @fImporte float,
			 @sFechaPago varchar(10),
			 @sFormaPago varchar(50),
			 @sRes VARCHAR(100)


	SELECT top(1) @sInstancia=instancia, @sCliente=Cliente, @sTipoOperacion=tipoOperacion, @sRefOperativa=refOperativa,
		   @sPedimento=pedimento, @fEfectivo=efectivo, @sBanco=Banco, @sDesCuenta=DesCuenta, @sServicio=servicio,
		   @fImporte=importe, @sFechaPago=fechaPago, @sFormaPago=FormaPago
	FROM @tabla 

	BEGIN TRY
	BEGIN TRANSACTION reenviarImpuesto
		EXEC pa_GuardarImpuestoConcor @sInstancia,@sCliente,@sTipoOperacion,@sRefOperativa,@sPedimento,@fEfectivo
			,@sBanco,@sDesCuenta,@sServicio,@fImporte,@sFechaPago,@sFormaPago,@sRes OUT
										
	COMMIT TRANSACTION reenviarImpuesto
	END TRY
	BEGIN CATCH
			ROLLBACK TRANSACTION reenviarImpuesto
			print ERROR_MESSAGE()+' error'
	END CATCH

	--Se elimina el registro
	delete top(1) @tabla
	--Se ajusta contador
	set @icont=(select count(*) from @tabla)
 END
 GO


-- --//Reactivar IMPUESTOS
 update tImpuesto set idPoliza=null,Estatus=1,Seleccionado=0,idSubreferenciaN=null,iRefMatriz=null
,SubreferenciaSeleccion=null where idImpuesto in(

select idImpuesto from tImpuesto where idSubreferencia in(SELECT idSubreferencia FROM tSubReferencia where refOperativa in(
'K041930/22'

)
)

)



SELECT * FROM tFactura WHERE noFactura = '1268424'

SELECT * FROM [dbo].[tFacturaDirectaCompras_ION] WHERE UUID = '0686D78C-5F88-49FB-B6FE-3DFCA94E971A'
SELECT * FROM tCliente WHERE idCliente = 783
'69378'