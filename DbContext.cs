using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using PetaPoco;

namespace Prognose.Web.Models
{
    
    public class DbContext<T>
    {
        private static Database DBInstance()
        {
            return new Database("PrognoseConnectionString");
        }

        #region Database SQL Builders for Select/Where

        public static IEnumerable<T> GetAll()
        {
            var sql = "SELECT * FROM " + TableName();
            return DBInstance().Fetch<T>(sql);
        }

        public static T Get(object o)
        {
            var sql = "SELECT * FROM " + TableName() + " WHERE " + PrimaryKey() + " = @0";
            return DBInstance().FirstOrDefault<T>(sql, o);
        }

        public static IEnumerable<T> Where(string column, object value)
        {
            return Where(column, "=", value);
        }

        public static IEnumerable<T> Where(string column, string Operator, object value)
        {
            var sql = "SELECT * FROM " + TableName() + " WHERE " + column + " " + Operator + " @0";
            return DBInstance().Fetch<T>(sql, value);
        }

        #endregion

        #region Insert/Upadate/Save Operations for Primary Key

        public object Insert()
        {
            return DBInstance().Insert(this);
        }

        public int Update()
        {
            return DBInstance().Update(this);
        }

        public void Save()
        {
            DBInstance().Save(this);
        }

        public void Delete()
        {
            DBInstance().Delete(this);
        }

        #endregion

        #region Get Attributes

        protected static string TableName()
        {

            object[] attrs = typeof(T).GetCustomAttributes(true);

            foreach (object attr in attrs)
            {
                var tableNameAttr = attr as PetaPoco.TableNameAttribute;

                if (tableNameAttr != null)
                {
                    return tableNameAttr.Value;
                }
            }


            return MethodBase.GetCurrentMethod().DeclaringType.Name;
        }

        protected static string PrimaryKey()
        {

            object[] attrs = typeof(T).GetCustomAttributes(true);

            foreach (object attr in attrs)
            {
                var PrimaryKeyAttribute = attr as PetaPoco.PrimaryKeyAttribute;

                if (PrimaryKeyAttribute != null)
                {
                    return PrimaryKeyAttribute.Value;
                }
            }

            return "Id";
        }

        #endregion
    }

    public static class DbContextEnumerables
    {
        public static void SaveAll<T>(this IEnumerable<DbContext<T>> context)
        {
            foreach( var item in context)
               item.Save();
        }

        public static void DeleteAll<T>(this IEnumerable<DbContext<T>> context)
        {
            foreach (var item in context)
                item.Delete();
        }

        public static void InsertAll<T>(this IEnumerable<DbContext<T>> context)
        {
            foreach (var item in context)
                item.Insert();
        }

        public static void UpdateAll<T>(this IEnumerable<DbContext<T>> context)
        {
            foreach (var item in context)
                item.Update();
        }

    }

}

