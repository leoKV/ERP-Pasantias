using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel;
using System.IO;
using System.Data;
using System.Reflection;

public class ExcelData
{
    private string _ruta;

    public ExcelData(string ruta)
    {
        _ruta = ruta;
    }

    public IExcelDataReader getExcelReader()
    {
        //Se necesita un Filestream para no utilizar ACE o Interop
        FileStream stream = File.Open(_ruta, FileMode.Open, FileAccess.Read);

        IExcelDataReader reader = null;
        try
        {
            if (_ruta.EndsWith(".xls") || _ruta.EndsWith(".XLS"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            if (_ruta.EndsWith(".xlsx") || _ruta.EndsWith(".XLSX"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            //Retornamos el dataReader
            return reader;
        }
        catch (Exception)
        {
            throw;
        }
    }

    //Retorna una lista IEnumerable con los nombres de las hojas.
    public IEnumerable<string> getWorksheetNames()
    {
        var reader = this.getExcelReader();
        var workbook = reader.AsDataSet();
        var sheets = from DataTable sheet in workbook.Tables select sheet.TableName;
        return sheets;
    }

    //Retorna una lista de rows, recibe el nombre de la hoja.
    public IEnumerable<DataRow> getData(bool firstRowIsColumnNames = true)
    {
        using (IExcelDataReader reader = this.getExcelReader())
        {
            reader.IsFirstRowAsColumnNames = firstRowIsColumnNames;
            var workSheet = reader.AsDataSet().Tables[0];
            var rows = from DataRow a in workSheet.Rows select a;
            //reader.Close();
            return rows;
        }
    }

    public List<T> excelAListaObjetos<T>(string[] columnas) where T : new()
    {
        List<T> Lista = new List<T>();
        IExcelDataReader reader = getExcelReader();
        reader.Read();
        while (reader.Read())
        {
            var Data = new T();
            PropertyInfo[] Properties = Data.GetType().GetProperties();
            //int i = 0;
            foreach (var p in Properties)
            {
                int index = Array.IndexOf(columnas, p.Name);
                p.SetValue(Data, reader.GetString(index), null);
            }
            Lista.Add(Data);
        }
        reader.Close();
        return Lista;
    }

    #region getObjectList
    public List<T> excelAListaObjetos<T>(List<string> columnas) where T : new()
    {
        List<T> Lista = new List<T>();
        IExcelDataReader reader = this.getExcelReader();
        reader.Read();
        while (reader.Read())
        {
            var Data = new T();
            PropertyInfo[] Properties = Data.GetType().GetProperties();

            foreach (var p in Properties)
            {
                int pos = columnas.IndexOf(p.Name);
                p.SetValue(Data, reader.GetString(pos), null);
            }
            Lista.Add(Data);
        }
        reader.Close();
        return Lista;
    }
    #endregion

}
