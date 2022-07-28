using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CodigoAnalisis
/// </summary>
public class CodigoAnalisis
{
    public CodigoAnalisis()
    {
       
    }
    //Se declaran los atributos de metodo pago
    public int iIdCodigoAnalisis { get; set; }
    public string sIdCodigoAnalisis { get; set; }
    public string sCodigoAnalisis { get; set; }
    public string sDenominacion { get; set; }
    public int iIdEstatus { get; set; }
    
    public string iIdCliente { get; set; }
    public int iIdProveedor { get; set; }
    public string sNumCuenta { get; set; }
    public int iIdTipoProveedor { get; set; }
    public int iIdComitente { get; set; }


   //
    public int iIdFactorCondicionPago { get; set; }
    public string sRfc { get; set; }
    public int iIdServicio { get; set; }
    // public int iIdCliente { get; set; }
    public int iIdOficina { get; set; }
    public float sMayorA { get; set; }
    public float sMenorA { get; set; }
    // public string sMayorA { get; set; }
    // public string sMenorA { get; set; }
    public int iIdMoneda { get; set; }
    //public int iIdComitente { get; set; }
    public int sSegundoServicio { get; set; }
    // public int iIdTipoProveedor  { get; set; }
    public int iIdCondicionPagoProv { get; set; }
    //Se declaran atributos generales
    public int iResultado { get; set; }
    public string sMensaje { get; set; }
    public string sContenido { get; set; }
    public int iIdUsuario { get; set; }
    public int iIdBanco { get; set; }

    /// <summary>
    /// Método para validar que la metodo pago no exista
    /// </summary>
    /// <param name="objCodigoAnalisis"></param>
    public void fn_ValidarCodigoContado(CodigoAnalisis objCodigoAnalisis)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = "select COUNT(*) from tCodigoAnalisisProv where numCuenta ='" +this.sNumCuenta +"'"+
                    " and cveComitente = ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente +") and idtipoProveedor = "+ this.iIdTipoProveedor+
                        " and idProveedor ="+ this.iIdProveedor;
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
        
        
        if (sRes[1] == "0")
        {
            // se valida el numero de cuenta si ya existe
            sQuery = "select count (*) from tProveedorCuentasBancarias where numCuenta = '"+this.sNumCuenta+"' and idProveedor="+this.iIdProveedor;
            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            if (sRes[1] != "0")
            {
                // ya existe todo bien
                //Se retorna el iResultado 
                objCodigoAnalisis.iResultado = 1;

            }
            else {
                // no existe cuenta manda mensaje para decirle si la inserta
                objCodigoAnalisis.iResultado = 2;
                objCodigoAnalisis.sMensaje = "El numero de cuenta: " + this.sNumCuenta + " no se encuentra registrada, ingresa el banco correspondiente " +
                                                        "para guardar la información";
            }
           
        }
        else
        {
            // se obtiene el codigo que ya existe
             sQuery = "select top 1 codigoAnalisis from tCodigoAnalisisProv where numCuenta ='" + this.sNumCuenta + "'" +
                    " and cveComitente = ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idtipoProveedor = " + this.iIdTipoProveedor +
                        " and idProveedor =" + this.iIdProveedor;
            sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);
            //Se retorna mensaje en caso de que exista la metodo pago
            objCodigoAnalisis.sMensaje = "La infomación seleccionada ya se encuentra guardada para el codigo de analisis: "+ sRes[1]+ ", ¿Deseas reemplazarlo?";
            //Se retorna el iResultado 
            objCodigoAnalisis.iResultado = 3;
        }
    }

    /// <summary>
    /// Método para guardar metodo pago
    /// </summary>
    /// <param name="objCodigoAnalisis"></param>
    public void fn_GuardarCodigoAnalisisContado(CodigoAnalisis objCodigoAnalisis)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarCodigoAnalisisContado", 0);
        string[] sResOut = new string[2];
        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objCodigoAnalisis.iIdProveedor.ToString());
                objConexion.agregarParametroSP("@sCodigoAnalisis", SqlDbType.VarChar, objCodigoAnalisis.sCodigoAnalisis);
                objConexion.agregarParametroSP("@sNumCuenta", SqlDbType.VarChar, objCodigoAnalisis.sNumCuenta);
                objConexion.agregarParametroSP("@iIdTipoProveedor", SqlDbType.Int, objCodigoAnalisis.iIdTipoProveedor.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objCodigoAnalisis.iIdComitente.ToString());
                objConexion.agregarParametroSP("@iIdBanco", SqlDbType.Int, objCodigoAnalisis.iIdBanco.ToString());
                //Se ejecuta el SP
                sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sResOut[0] == "1")
                {
                    //Se retorna el mensaje de éxito
                    objCodigoAnalisis.iResultado = 1;
                    objCodigoAnalisis.sMensaje = "Codigo Analisis guardada con éxito.";
                    objCodigoAnalisis.iIdCodigoAnalisis = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objCodigoAnalisis.iResultado = 0;
                    objCodigoAnalisis.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objCodigoAnalisis.iResultado = 0;
                objCodigoAnalisis.sMensaje = ex.Message;
            }
        }
    }

    /// <summary>
    /// Método para eliminar metodo pago
    /// </summary>
    /// <param name="objCodigoAnalisis"></param>
    public void fn_EliminarCodigoAnalisis(CodigoAnalisis objCodigoAnalisis)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        // se hace sentencia
        string sQuery = "delete from tCodigoAnalisisProv where idCodigoAnalisisProv ="+this.iIdCodigoAnalisis;
        string sResOut = objConexion.ejecutarComando(sQuery);

        if (sResOut == "1")
        {
            // todo salio bien
            this.iResultado = 1;
            this.sMensaje = "Codigo analisis eliminado con exito";
        }
        else {
            this.iResultado = 0;
            this.sMensaje = sResOut;
        }

    }


    public void fn_EliminarCodigocredito(CodigoAnalisis objCodigoAnalisis)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        // se hace sentencia
        string sQuery = "delete from tCondicionPagoProv where idCondicionPagoProv =" + this.iIdCondicionPagoProv;
        string sResOut = objConexion.ejecutarComando(sQuery);

        if (sResOut == "1")
        {
            // todo salio bien
            this.iResultado = 1;
            this.sMensaje = "Codigo analisis credito eliminado con exito";
        }
        else
        {
            this.iResultado = 0;
            this.sMensaje = sResOut;
        }

    }

    public void fn_GuardarCodigoAnalisisCredito(CodigoAnalisis objCodigoAnalisis)
    {
        //Se instancia la clase conexión 
        Conexion objConexion = new Conexion();
        //Se verifica existencia del SP
        string sRes = objConexion.generarSP("pa_GuardarCodigoAnalisisCredito", 0);
        //string[] sResOut = new string[2];

        if (sRes == "1")
        {
            try
            {
                //Se pasan los parametros del SP
                objConexion.agregarParametroSP("@iIdProveedor", SqlDbType.Int, objCodigoAnalisis.iIdProveedor.ToString());
                objConexion.agregarParametroSP("@iIdFactorCondicionPago", SqlDbType.Int, objCodigoAnalisis.iIdFactorCondicionPago.ToString());
                objConexion.agregarParametroSP("@sRfc", SqlDbType.VarChar, objCodigoAnalisis.sRfc);
                objConexion.agregarParametroSP("@iIdServicio", SqlDbType.Int, objCodigoAnalisis.iIdServicio.ToString());
                objConexion.agregarParametroSP("@iIdCliente", SqlDbType.VarChar, objCodigoAnalisis.iIdCliente);

                objConexion.agregarParametroSP("@iIdMayorA", SqlDbType.Float, objCodigoAnalisis.sMayorA.ToString());
                objConexion.agregarParametroSP("@iIdMenorA", SqlDbType.Float, objCodigoAnalisis.sMenorA.ToString());
                objConexion.agregarParametroSP("@iIdOficina", SqlDbType.Int, objCodigoAnalisis.iIdOficina.ToString());
                objConexion.agregarParametroSP("@sMoneda", SqlDbType.Int, objCodigoAnalisis.iIdMoneda.ToString());
                objConexion.agregarParametroSP("@iIdComitente", SqlDbType.Int, objCodigoAnalisis.iIdComitente.ToString());
                objConexion.agregarParametroSP("@sSegundoServicio", SqlDbType.Int, objCodigoAnalisis.sSegundoServicio.ToString());
                objConexion.agregarParametroSP("@iIdTipoProveedor", SqlDbType.Int, objCodigoAnalisis.iIdTipoProveedor.ToString());
                //Se ejecuta el SP
                sRes = objConexion.ejecutarProc();
                //sResOut = objConexion.ejecutarProcOUTPUT_INT("@iResOut");
                if (sRes == "1")
                {
                    //Se retorna el mensaje de éxito
                    objCodigoAnalisis.iResultado = 1;
                    objCodigoAnalisis.sMensaje = "Codigo Analisis guardada con éxito.";
                    //objCodigoAnalisis.i = int.Parse(sResOut[1]);
                }
                else
                {
                    //Se retorna el mensaje de error
                    objCodigoAnalisis.iResultado = 0;
                    objCodigoAnalisis.sMensaje = sRes;
                    // objCodigoAnalisis.sMensaje = sResOut[0];
                }
            }
            catch (Exception ex)
            {
                //Se guarda el mensaje de error
                objCodigoAnalisis.iResultado = 0;
                objCodigoAnalisis.sMensaje = ex.Message;
            }
        }
    }

    public void fn_ValidarFactorPago(CodigoAnalisis objcodigoAnalisis)
    {
        Conexion objConexion = new Conexion();
        //sQuery
        string sQuery = @"if EXISTS(select idProveedor from tCondicionPagoProv where idProveedor = " + objcodigoAnalisis.iIdProveedor + @")
             select idFactorCondicionPago from tCondicionPagoProv where idProveedor = " + objcodigoAnalisis.iIdProveedor + @" else select 0 ";
        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

        if (sRes[0] == "1")
        {

            objcodigoAnalisis.iResultado = 1;
            objcodigoAnalisis.iIdFactorCondicionPago = int.Parse(sRes[1]);

        }

        else
        {
            objcodigoAnalisis.iResultado = 0;
            objcodigoAnalisis.sMensaje = sRes[0];
        }



    }


    //validar configuracion credito
    public void fn_ValidarCodigoCredito(CodigoAnalisis objCodigoAnalisis)
    {


        //sQuery


        if (objCodigoAnalisis.iIdFactorCondicionPago == 1)
        {  //servicio 
            Conexion objConexion = new Conexion();
            //SQuery
            string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idServicio = " + this.iIdServicio + " and cveComitente  = ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor = " + this.iIdProveedor;
            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

            //se retorna el iResultado
            objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
            //se retorna mensaje en caso de que exista la cuenta
            objCodigoAnalisis.sMensaje = "La configuracion de pago ya existe.";

        }
        else
        {
            if (objCodigoAnalisis.iIdFactorCondicionPago == 2)
            { //cliente
                Conexion objConexion = new Conexion();
                //SQuery
                string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and Cliente = " + this.iIdCliente + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor + "";
                string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                //se retorna el iResultado
                objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                //se retorna mensaje en caso de que exista la cuenta
                objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
            }
            else
            {

                if (objCodigoAnalisis.iIdFactorCondicionPago == 3)
                { // por montontos
                    Conexion objConexion = new Conexion();
                    //SQuery
                    string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + "and mayorA =" + this.sMayorA + " and menorA = " + this.sMenorA + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor;
                    string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                    //se retorna el iResultado
                    objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                    //se retorna mensaje en caso de que exista la cuenta
                    objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                }
                else
                {

                    if (objCodigoAnalisis.iIdFactorCondicionPago == 4)
                    {//servicio-cliente
                        Conexion objConexion = new Conexion();
                        //SQuery
                        string sQuery = "select count(*) from tCondicionPagoProv where  idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and cliente = " + this.iIdCliente + " and idServicio = " + this.iIdServicio + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor;
                        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                        //se retorna el iResultado
                        objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                        //se retorna mensaje en caso de que exista la cuenta
                        objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                    }
                    else
                    {

                        if (objCodigoAnalisis.iIdFactorCondicionPago == 5)
                        { //oficina
                            Conexion objConexion = new Conexion();
                            //SQuery
                            string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idOficina = " + this.iIdOficina + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor;
                            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                            //se retorna el iResultado
                            objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                            //se retorna mensaje en caso de que exista la cuenta
                            objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                        }
                        else
                        {

                            if (objCodigoAnalisis.iIdFactorCondicionPago == 7)
                            { //sevicio monto
                                Conexion objConexion = new Conexion();
                                //SQuery
                                string sQuery = "select count(*) from tCondicionPagoProv where  idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idServicio = " + this.iIdServicio + "  and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ")  and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor + "";
                                string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                                //se retorna el iResultado
                                objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                                //se retorna mensaje en caso de que exista la cuenta
                                objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                            }

                            else
                            {

                                if (objCodigoAnalisis.iIdFactorCondicionPago == 9)
                                { //por oficina -servicio
                                    Conexion objConexion = new Conexion();
                                    //SQuery
                                    string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idOficina = " + this.iIdOficina + " and idServicio = " + this.iIdServicio + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor;
                                    string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                                    //se retorna el iResultado
                                    objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                                    //se retorna mensaje en caso de que exista la cuenta
                                    objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                                }
                                else
                                {

                                    if (objCodigoAnalisis.iIdFactorCondicionPago == 12)
                                    {// por moneda
                                        Conexion objConexion = new Conexion();
                                        //SQuery
                                        string sQuery = "select count(*) from tCondicionPagoProv where  idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and Moneda = (select cveMoneda from cMoneda where idMoneda = " + this.iIdMoneda + ") and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor;
                                        string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                                        //se retorna el iResultado
                                        objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                                        //se retorna mensaje en caso de que exista la cuenta
                                        objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                                    }
                                    else
                                    {

                                        if (objCodigoAnalisis.iIdFactorCondicionPago == 13)
                                        { //por oficina y segundo servicio
                                            Conexion objConexion = new Conexion();
                                            //SQuery
                                            string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idServicio = " + this.iIdServicio + " and idOficina = " + this.iIdOficina + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and  idServicioSegundo = " + this.sSegundoServicio + "and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor + "";
                                            string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                                            //se retorna el iResultado
                                            objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                                            //se retorna mensaje en caso de que exista la cuenta
                                            objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                                        }
                                        else
                                        {

                                            if (objCodigoAnalisis.iIdFactorCondicionPago == 14)
                                            {// por validacion montos y servicio
                                                Conexion objConexion = new Conexion();
                                                //SQuery
                                                string sQuery = "select count(*) from tCondicionPagoProv where idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idServicio = " + this.iIdServicio + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor + "";
                                                string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                                                //se retorna el iResultado
                                                objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                                                //se retorna mensaje en caso de que exista la cuenta
                                                objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                                            }

                                            else
                                            {

                                                if (objCodigoAnalisis.iIdFactorCondicionPago == 15)
                                                {
                                                    Conexion objConexion = new Conexion();
                                                    //SQuery
                                                    string sQuery = "select count(*) from tCondicionPagoProv where  idFactorCondicionPago = " + this.iIdFactorCondicionPago + " and idServicioSegundo =" + this.sSegundoServicio + " and cveComitente =  ( select cveComitente from tComitente where idcomitente = " + this.iIdComitente + ") and idTipoProveedor = " + this.iIdTipoProveedor + " and idProveedor =" + this.iIdProveedor;
                                                    string[] sRes = objConexion.ejecutarConsultaRegistroSimple(sQuery);

                                                    //se retorna el iResultado
                                                    objCodigoAnalisis.iResultado = int.Parse(sRes[1]);
                                                    //se retorna mensaje en caso de que exista la cuenta
                                                    objCodigoAnalisis.sMensaje = "La Configuracion de pago ya existe.";
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }



    }
}