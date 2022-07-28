using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cuentaimpuessto
/// </summary>
public class CuentaImpuesto
{
	public CuentaImpuesto()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    //Se declaran los atributos de cuenta impuestos

    public int iIdDescripcionCuentaConcor { get; set; }
    public string sIdDescripcionCuentaConcor { get; set; }
    public string sDescripcion { get; set; }
    public string sCuentaContable { get; set; }
    public int iIdProveedorCuentasBancarias { get; set; }
    public int iIdAduana { get; set; }	
    public int iIdCuentaConcor { get; set; }
    public int iIdPatente { get; set; }
    public string sCveCuentaConcor { get; set; }

	//Se declaran atributos generales
	public int iResultado { get; set; }
	public string sMensaje { get; set; }
	public string sContenido { get; set; }
	public int iIdUsuario { get; set; }
 


    //Funcion para eliminar cuenta impuesto
    /// <param name="objCuentaImpuesto",objeto </param>>
    public void fn_EliminarCuentaImpuesto(CuentaImpuesto objCuentaImpuesto)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "DELETE FROM cDescripcionCuentaConcor WHERE  idDescripcionCuentaConcor='" + objCuentaImpuesto.iIdDescripcionCuentaConcor + "'";
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objCuentaImpuesto.iResultado = 1;
            //Se retorna mensaje de éxito
            objCuentaImpuesto.sMensaje = "Cuenta eliminada con exito.";
        }
        else
        {
            //Se retorna el sResultado 
            objCuentaImpuesto.iResultado = 0;
            //Se retorna mensaje de error
            objCuentaImpuesto.sMensaje = sRes;
        }
    }

    //Funcion para eliminar cuenta concor
    /// <param name="objCuentaImpuesto", </param>>
    public void fn_EliminarCuentaConcor(CuentaImpuesto objCuentaImpuesto)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "DELETE FROM tCuentaConcor WHERE  idCuentaConcor='" + objCuentaImpuesto.iIdCuentaConcor + "'";
        string sRes = objConexion.ejecutarComando(sQuery);
        //Se verifica el resultado
        if (sRes == "1")
        {
            //Se retorna el sResultado 
            objCuentaImpuesto.iResultado = 1;
            //Se retorna mensaje de éxito
            objCuentaImpuesto.sMensaje = "Cuenta eliminada con exito.";
        }
        else
        {
            //Se retorna el sResultado 
            objCuentaImpuesto.iResultado = 0;
            //Se retorna mensaje de error
            objCuentaImpuesto.sMensaje = sRes;
        }
    }

    /*
    //Funcion para llenar combo patente
    /// <param name="objCuentaImpuesto", </param>>
    public void fn_LlenarComboPatente(CuentaImpuesto objCuentaImpuesto)
    {
        //Consulta para obtener datos
        string sQuery = "select patente from cPatente";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "Patente");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["Patente"].Rows.Count > 0)
        {
            //Se crea option vacío
            objCuentaImpuesto.sContenido = "<option value=''></option>";
            //Se recorren los registros
            foreach (DataRow registro in dsDatos.Tables["Patente"].Rows)
            {
                objCuentaImpuesto.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
    }

    //Funcion para llenar combo aduana.
    /// <param name="objCuentaImpuesto", </param>>
    public void fn_LlenarComboAduana(CuentaImpuesto objCuentaImpuesto)
    {
        //Consulta para obtener datos
        string sQuery = "select idAduana,aduana +' - '+ Denominacion as sAduana from cAduana";
        //Se crea la variable para almacenar datos
        DataSet dsDatos;
        //Se instancia la clase conexión
        Conexion objConexion = new Conexion();
        //Se ejecuta la consulta para obtener los datos
        dsDatos = objConexion.ejecutarConsultaRegistroMultiplesDataSet(sQuery, "Aduana");
        //Se verifica que se tengan datos
        if (dsDatos.Tables["Aduana"].Rows.Count > 0)
        {
            //Se crea option vacío
            objCuentaImpuesto.sContenido = "<option value=''></option>";
            //Se recorren los registros
            foreach (DataRow registro in dsDatos.Tables["Aduana"].Rows)
            {
                objCuentaImpuesto.sContenido += "<option value='" + registro[0] + "'>" + registro[1] + "</option>";
            }
        }
    }
    */
    //Funcion para validar que la cuenta concor no exista
    public void fn_ValidarCuentaConcorExiste(CuentaImpuesto objCuentaImpuesto)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery para validar embalajes
        string sQuery = "SELECT COUNT(*) FROM tCuentaConcor tcc WHERE  tcc.cveCuentaConcor='"+ objCuentaImpuesto.sCveCuentaConcor + "' and tcc.idPatente=" + objCuentaImpuesto.iIdPatente +" and tcc.idAduana=" +iIdAduana+"";

        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        //Se retorna el sResultado 
        objCuentaImpuesto.iResultado = int.Parse(sRes[1]);
        //Se retorna mensaje en caso de que el ciente ya este asignado
        objCuentaImpuesto.sMensaje = "La descripcion ya esta asignada a una cuenta.";
    }

    //

   //Funcion para guardar cuenta concor
    public void fn_GuardarCuentaConcor(CuentaImpuesto objCuentaImpuesto)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarCuentaConcor", 0);
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@cveCuentaConcor", SqlDbType.VarChar, objCuentaImpuesto.sCveCuentaConcor);
                objConexion.agregarParametroSP("@idDescripcionCuentaConcor", SqlDbType.Int, objCuentaImpuesto.iIdDescripcionCuentaConcor.ToString());
                objConexion.agregarParametroSP("@idPatente", SqlDbType.Int, objCuentaImpuesto.iIdPatente.ToString());
                objConexion.agregarParametroSP("@idaduana", SqlDbType.Int, objCuentaImpuesto.iIdAduana.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objCuentaImpuesto.iResultado = 1;
                    objCuentaImpuesto.sMensaje = "Cuenta guardada con éxito";
                }
                else
                {
                    //Se retorna el mensaje de error
                    objCuentaImpuesto.iResultado = 0;
                    objCuentaImpuesto.sMensaje = sRes;
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objCuentaImpuesto.iResultado = 0;
                objCuentaImpuesto.sMensaje = ex.Message;
            }
        }
    }

    //

    /// <summary>
    /// Metodo para guardar cuenta impuesto
    /// </summary>
    /// <param name="objCuentaImpuesto"></param>
    public void fn_GuardarCuentaImpuesto(CuentaImpuesto objCuentaImpuesto)
    {
        //Se instancia la clase conexion 
        Conexion objConexion = new Conexion();
        // se verifica existencia del SP 
        string sRes = objConexion.generarSP("pa_GuardarCuentaImpuesto", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP 
                objConexion.agregarParametroSP("@iIdDescripcionCuentaConcor", SqlDbType.Int, objCuentaImpuesto.sIdDescripcionCuentaConcor.ToString());
                objConexion.agregarParametroSP("@sDescripcion", SqlDbType.VarChar, objCuentaImpuesto.sDescripcion);
                objConexion.agregarParametroSP("@iCuentaContable", SqlDbType.VarChar, objCuentaImpuesto.sCuentaContable);
                objConexion.agregarParametroSP("@iIdProveedorCuentasBancarias", SqlDbType.Int, objCuentaImpuesto.iIdProveedorCuentasBancarias.ToString());
                objConexion.agregarParametroSP("@iIdAduana", SqlDbType.Int, objCuentaImpuesto.iIdAduana.ToString());
                //Se ejecuta el SP 

                // sRes = objConexion.ejecutarProc();
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de exito 
                    objCuentaImpuesto.iResultado = 1;
                    objCuentaImpuesto.sMensaje = "Cuenta guardada con éxito.";
                    objCuentaImpuesto.sIdDescripcionCuentaConcor = sResOut[1];
                }

                else
                {
                    //Se retorna el mensaje de error
                    objCuentaImpuesto.iResultado = 0;
                    objCuentaImpuesto.sMensaje = sRes;
                }
                }
            catch (Exception ex)
            {
                //Se guarda el mensaje error
                objCuentaImpuesto.iResultado = 0;
                objCuentaImpuesto.sMensaje = ex.Message;
            }
        }
    }

    public void fn_ObtenerDatosCuentasImpuestos(CuentaImpuesto objCuentaImpuesto)
    {
        //Se instancia la conexión 
        Conexion objConexion = new Conexion();
        //Se crea el arreglo de atributos
        string[] arrAtributos = { "sDescripcion", "sCuentaContable", "iIdProveedorCuentasBancarias", "iIdAduana" };
        //Se crea la consulta
        string sQuery = "select Descripcion as sDescripcion, CuentaContable as sCuentaContable,idProveedorCuentasBancarias as iIdProveedorCuentasBancarias, idAduana as iIdAduana from cDescripcionCuentaConcor where cDescripcionCuentaConcor.idDescripcionCuentaConcor =" + objCuentaImpuesto.iIdDescripcionCuentaConcor;
        //Se ejecuta el método para obtener datos
        objConexion.ejecutaRecuperaObjeto<CuentaImpuesto>(sQuery, arrAtributos, objCuentaImpuesto);
        //Se asigna el resultado
        objCuentaImpuesto.iResultado = 1;
    }


    ///<summary>
    ///Método para validar que la cuenta no exista
    /// </summary>
    /// <param name="objCuentaImpuesto"></param>
    public void fn_ValidarCuentaExistente(CuentaImpuesto objCuentaImpuesto)
    {
        if (objCuentaImpuesto.iIdDescripcionCuentaConcor == 0)
        {
            //se instancia la clase conexion
            Conexion objConexion = new Conexion();
            //SQuery
            string sQuery = "SELECT COUNT (*) FROM cDescripcionCuentaConcor dc WHERE dc.Descripcion = '" + objCuentaImpuesto.sDescripcion + "'";
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            //se retorna el iResultado
            objCuentaImpuesto.iResultado = int.Parse(sRes[1]);
            //se retorna mensaje en caso de que exista la cuenta
            objCuentaImpuesto.sMensaje = "La descripcion ya existe.";
        }
        else
        {
            //se retorna el iResultado
            objCuentaImpuesto.iResultado = 0;
        }
    }

  

}

