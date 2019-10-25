using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using TableToEntity.Entity;

namespace TableToEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("No", typeof(Int32));
            dataTable.Columns.Add("Name");
            dataTable.Rows.Add(123, "test1");
            dataTable.Rows.Add(456, "test2");
            List<User> users=new List<User>();
            users=ConvertDataTable<User>(dataTable);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    try {
                          var s=   pro.CustomAttributes.FirstOrDefault(x=>x.AttributeType==typeof(ColumnAttribute)).ConstructorArguments.FirstOrDefault().Value.ToString();
                    if (s == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                    }
                    catch(Exception e)
                    {

                    }
                   
                }
            }
            return obj;
        }
    }
}
