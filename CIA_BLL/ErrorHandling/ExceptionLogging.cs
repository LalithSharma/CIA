using CIA_BLL.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using context = System.Web.HttpContext;

namespace CIA_BLL.ErrorHandling
{
    public class ExceptionLogging
    {
        private static String ErrorlineNo, errormsg, extype, exurl, errorClass;

        public static void LogException(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;

            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            errormsg = ex.Message.ToString();
            extype = ex.GetType().ToString();
            exurl = context.Current.Request.Url.ToString();
            errorClass = ex.GetType().Name.ToString();

            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
            string loggedUserFullName = SessionObj.UsrName ?? "Unknown";
            string loggedUSerName = SessionObj.LoginName ?? "Unknown";
            string loggedRole = SessionObj.UsrRole ?? "Unknown";
            var exceptionLocation = trace.GetFrame(0).GetMethod().ReflectedType.FullName;
            var exception = ex.ToString();


            try
            {
                string filepath = context.Current.Server.MapPath("~/ErrorLog/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {


                    File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Logged User Full Name :" + " " + loggedUserFullName + line + "Logged Username :" + " " + loggedUSerName + line + "User Role :" + " " + loggedRole + line + "Error Location :" + " " + exceptionLocation + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Class:" + " " + errorClass + line + "Exception Type:" + " " + extype + line + "Error Message :" + " " + errormsg + line + "Error Page Url:" + " " + exurl + line + "Exception :" + " " + exception + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

        public DataTable ListToDataTable<T>(List<T> data)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public DataTable ConvertToTranspose(DataTable dt)
        {
            DataTable dt2 = new DataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int c = 0; c <= dt.Columns.Count - 2; c++)
                {
                    string colname = dt.Rows[i][c].ToString();
                    int colnum = 1;
                    while (dt2.Columns.Contains(colname))
                    {
                        colname = string.Format("{0}_{1}", "Empty", ++colnum);
                    }
                    dt2.Columns.Add(colname);
                }
            }
            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                dt2.Rows.Add();
                //dt2.Rows[i][0] = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dt2.Rows[i][j] = dt.Rows[j][i + 1];
                }
            }

            return dt2;
        }

        public DataTable MergeTablesByIndex(DataTable t1, DataTable t2)
        {
            if (t1 == null || t2 == null) throw new ArgumentNullException("t1 or t2", "Both tables must not be null");

            DataTable t3 = t1.Clone();  // first add columns from table1

            foreach (DataColumn col in t2.Columns)
            {
                string newColumnName = col.ColumnName;
                int colNum = 1;
                while (t3.Columns.Contains(newColumnName))
                {
                    newColumnName = string.Format("{0}_{1}", col.ColumnName, ++colNum);
                }
                t3.Columns.Add(newColumnName, col.DataType);
            }

            var mergedRows = t1.AsEnumerable().Zip(t2.AsEnumerable(),
                (r1, r2) => r1.ItemArray.Concat(r2.ItemArray).ToArray());
            foreach (object[] rowFields in mergedRows)
                t3.Rows.Add(rowFields);

            //t3.Merge(t1);
            return t3;
        }


        public void Logcreation(string msg)
        {
            string path_name = "~\\Log\\Log_" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MMM-yyyy") + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
            string root_path = System.Web.HttpContext.Current.Server.MapPath(path_name);
            string file_name = "Log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (!Directory.Exists(root_path))
            {
                Directory.CreateDirectory(root_path);
            }
            FileInfo info = new FileInfo(Path.Combine(root_path, file_name));
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(" " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  :  " + msg + "\n");
            if (!info.Exists)
            {
                using (StreamWriter writer = info.CreateText())
                {
                    writer.WriteLine(sb.ToString());
                    writer.Close();
                }
            }
            else
            {
                using (StreamWriter writer = info.AppendText())
                {
                    writer.WriteLine(sb.ToString());
                    writer.Close();
                }
            }
        }
    }
}