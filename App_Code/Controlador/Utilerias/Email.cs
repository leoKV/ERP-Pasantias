using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Descripción breve de Email
/// </summary>
public class Email
{
    public String sAsunto { get; set; }
    public String sCorreoPerfil { get; set; } //--variable que recupera el correo que se utilizara para mandar el correo
    public String sNombrePerfil { get; set; } //--variable que recupera el nombre del perfil que se mostrara en el nombre del correo
    public String sUsuarioPerfil { get; set; } //--variable que recupera el password del correo
    public String sPassPerfil { get; set; } //--variable que recupera el password del correo
    public String sHostPerfil { get; set; } //--variable que recupera el servidor de correo saliente
    public String iPuertoPerfil { get; set; } //--variable que recupera el numero de puerto por el cual sale el correo
    public bool bSslPerfil { get; set; } //--variable que recupera si aplica conexion ssl
    

    Conexion con;
    MailMessage _Correo;
    SmtpClient _Smtp;
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="html">es html: true/false</param>
    /// <param name="prioridad">prioridad, ej: System.Net.Mail.MailPriority.High/Low/Normal</param>
    /// <param name="tipoAlerta">tipo alerta configurada en base de datos</param>
    public Email(String tipoAlerta){

        con = new Conexion();
        //obtiene el perfil del correo
        string[] arrAtr = new string[] { "sAsunto", "sUsuarioPerfil", "sPassPerfil", "sHostPerfil",
                                         "iPuertoPerfil", "bSslPerfil", "sCorreoPerfil", "sNombrePerfil"};
        string sQuery = "select t_ca.asunto sAsunto,t_cp.usuario sUsuarioPerfil,"+
                        " dbo.fn_DecodeB64(t_cp.contrasenia) sPassPerfil,t_cp.servidorSalida sHostPerfil,"+
                        " convert(varchar,t_cp.puertoSalida) iPuertoPerfil, enableSsl bSslPerfil,"+
                        " t_cp.correo sCorreoPerfil,t_ca.nombreAlerta sNombrePerfil"+
                        " from tCorreoAlerta t_ca"+
                        " join tCorreoPerfil t_cp on t_ca.idPerfilCorreo = t_cp.idPerfilCorreo"+
                        " where t_ca.nombreAlerta = '" + tipoAlerta + "'";
        con.ejecutaRecuperaObjeto<Email>(sQuery, arrAtr, this);
    }

    /// <summary>
    /// Método que establece la configuración del correo
    /// </summary>
    /// <param name="html"></param>
    /// <param name="prioridad"></param>
    /// <param name="?"></param>
    public void fn_IniciarConfiguracionCorreo(bool html,MailPriority prioridad) {
        //instancia a objeto Correo
        _Correo = new MailMessage();
        _Correo.IsBodyHtml = html;
        _Correo.Priority = prioridad;
        _Correo.Subject = this.sAsunto;
        //pasa parametros de salida del correo
        recuperaInfoCorreo();
    }
    /// <summary>
    /// Método para ingresar lista destinos de correos separados por ;
    /// </summary>
    /// <param name="destinos"></param>
    public void _AddTo(String destinos)
    {
        ///se recupera lista de correos en arreglo
        string[] correos = destinos.Split(';');
        //--si es mayor se recorreo el arreglo
        if (correos.Length > 0)
        {
            ///se recorre lista de correos para agregar el correo
            for (int i = 0; i < correos.Length; i++)
            {
                _Correo.To.Add(new MailAddress(correos[i].ToString()));
            }
        }
        
    }

    /// <summary>
    /// Método para ingresar destinos en copia separados por ;
    /// </summary>
    /// <param name="destinos"></param>
    public void _AddCC(String destinos)
    {
        ///se recupera lista de correos en arreglo
        string[] correos = destinos.Split(';');
        //--si es mayor se recorreo el arreglo
        if (correos.Length > 0)
        {
            ///se recorre lista de correos para agregar el correo
            for (int i = 0; i < correos.Length; i++)
            {
                _Correo.CC.Add(new MailAddress(correos[i].ToString()));
            }
        }
    }

    /// <summary>
    /// Método para ingresar destinos con Copia Oculta separados por ;
    /// </summary>
    /// <param name="destinos"></param>
    public void _AddBCC(String destinos)
    {
        ///se recupera lista de correos en arreglo
        string[] correos = destinos.Split(';');
        //--si es mayor se recorreo el arreglo
        if (correos.Length > 0)
        {
            ///se recorre lista de correos para agregar el correo
            for (int i = 0; i < correos.Length; i++)
            {
                _Correo.Bcc.Add(new MailAddress(correos[i].ToString()));
            }
        }
    }

    /// <summary>
    /// Método para ingresar el cuerpo del correo
    /// </summary>
    /// <param name="cuerpoCorreo"></param>
    public void _AddBody(String cuerpoCorreo)
    {
        _Correo.Body = cuerpoCorreo;
    }

    /// <summary>
    /// Método agregar adjunto
    /// </summary>
    /// <param name="adjunto"></param>
    public void _AddAttachment(String adjunto)
    {
        Attachment att = new Attachment(adjunto);
        _Correo.Attachments.Add(att);
    }

    /// <summary>
    /// Método para hacer el envio del correo
    /// </summary>
    /// <returns></returns>
    public string sendMail() { 
        try{
            
            _Smtp.Send(_Correo);
            _Correo.Dispose();
            return "1";
        }
        catch (Exception ex)
        {
            return "Error enviando correo electrónico: " + ex.Message;
        }   
    }


    /// <summary>
    /// recupera informacion del correo
    /// </summary>
    private void recuperaInfoCorreo()
    {
        //correo del remitente
        _Correo.From = new MailAddress(this.sCorreoPerfil,this.sNombrePerfil);
        //configuración del smtp
        _Smtp = new SmtpClient();
        _Smtp.Credentials = new NetworkCredential(this.sUsuarioPerfil, sPassPerfil);
        _Smtp.Host = this.sHostPerfil;
        _Smtp.Port = int.Parse(this.iPuertoPerfil);
        _Smtp.EnableSsl = this.bSslPerfil;
    }
        
    
}