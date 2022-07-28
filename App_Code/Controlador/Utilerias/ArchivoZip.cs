using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;
using Ionic.Zip;
using UnRar;

public class ArchivoZip
{
    /// <summary>
    /// Método utilizado para comprimir archivos
    /// </summary>
    /// <param name="Ruta">Ruta del folder que se desea comprimir. (Server.MapPath(...))
    /// <param name="Nombre">el nombre con el que se desea que se guarde el archivo generado. Sin la extension
    /// <returns></returns>
    public Boolean fn_ComprimirCarpeta(string Ruta, string Nombre)
    {
        try
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Ruta);
                zip.Save(HttpContext.Current.Server.MapPath("temp/" + Nombre + ".zip"));
                zip.Dispose();
            }
            return false;
        }
        catch
        {
            return true;
        }
    }

    /// <summary>
    /// Método utilizado para descomprimir los archivos de un zip file
    /// </summary>
    /// <param name="ArchivoZip">Ruta donde se encuentra el archivo ZIP
    /// <param name="RutaGuardar">Ruta donde se guardaran los archivos extraídos del ZIP
    /// <returns></returns>
    public string fn_DescromprimirZip(string ArchivoZip, string RutaGuardar)
    {
        try
        {
            //decide si descomprime zip o rar
            if (System.IO.Path.GetExtension(ArchivoZip).ToLower() == ".zip")
            {
                //funciona para descomprimir .zip
                using (ZipFile zip = ZipFile.Read(ArchivoZip))
                {
                    //se pasa la ruta de destino del zip
                    zip.ExtractAll(RutaGuardar);
                    //se libera el objeto zip
                    zip.Dispose();
                }

                return fn_AsignarPermisosCarpeta(RutaGuardar);
            }
            else if (System.IO.Path.GetExtension(ArchivoZip).ToLower() == ".rar")
            {
                //funciona para descomprimir .rar
                Unrar rar = new Unrar();
                //se pasa la ruta del .rar
                rar.Open(ArchivoZip, Unrar.OpenMode.Extract);
                //se pasa la ruta destino
                rar.DestinationPath = RutaGuardar;
                //se extrae cada fichero dentro del rar
                while (rar.ReadHeader())
                { rar.Extract(); }
                //se libera el objeto rar
                rar.Dispose();

                return fn_AsignarPermisosCarpeta(RutaGuardar);
            }
            else
            {
                //retorna error, no se descomprime si no es zip o rar
                return "El archivo está dañado  o no tiene una extensión válida";
            }
        }
        catch (Exception ex)
        {
            return "Exception al descomprimir el archivo: " + ex.Message.ToString();
        }
    }

    public Boolean fn_ComprimirCarpetaZIP(string Ruta, string Nombre, string NombreCarpeta)
    {
        try
        {

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(Ruta);
                zip.Save(HttpContext.Current.Server.MapPath("../../Documentos/" + NombreCarpeta + "/" + Nombre + ".zip"));
                zip.Dispose();


            }
            return false;
        }
        catch
        {
            return true;
        }
    }

    /// <summary>
    /// Método para descomprimir recursivamente los zip dentro de un directorio 
    /// </summary>
    /// <param name="ruta"></param>
    /// <returns></returns>
    private string fn_DescomprimeZIP(string ruta)
    {
        try
        {
            DirectoryInfo objDir = new DirectoryInfo(ruta);
            //se descomprimen todos los zip que existan en los subdirectorios
            foreach (var objFile in objDir.GetFiles("*", SearchOption.AllDirectories))
            {
                if (System.IO.Path.GetExtension(objFile.Name).ToLower() == ".zip" ||
                    System.IO.Path.GetExtension(objFile.Name).ToLower() == ".rar")
                {
                    //Descomprime archivo zip
                    ArchivoZip obj = new ArchivoZip();
                    obj.fn_DescromprimirZip(objFile.DirectoryName + "\\" + objFile.Name,
                                         objFile.DirectoryName + "\\" + System.IO.Path.GetFileNameWithoutExtension(objFile.Name));
                    //realiza proceso de busqueda de archivos
                    fn_DescomprimeZIP(objFile.DirectoryName + "\\" + System.IO.Path.GetFileNameWithoutExtension(objFile.Name));
                }
            }
            return "1";
        }
        catch (Exception ex)
        {
            //se controla que no se detenga la aplicación
            return "Error al descomprimir el zip: " + ex.Message.ToString();
        }
    }


    /// <summary>
    /// Método para dar permisos a archivos dentro de una carpeta
    /// </summary>
    /// <param name="sRutaCarpeta"></param>
    /// <returns></returns>
    public string fn_AsignarPermisosCarpeta(string sRutaCarpeta)
    {
        try
        {
            //recorre cada uno de los archivos dentro de la carpeta
            foreach (var sRutaArchivo in Directory.GetFiles(sRutaCarpeta, "*.*"))
            {
                //asigna todos los permisos al archivo
                File.SetAttributes(sRutaArchivo, FileAttributes.Normal);
            }
            return "1";
        }
        catch (Exception ex)
        {
            return "Exception al dar permisos a los archivos: " + ex.Message.ToString();
        }
    }



}
