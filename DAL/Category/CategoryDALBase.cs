using ECommerce.Areas.Category.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace ECommerce.DAL.Category
{
    public class CategoryDALBase : DALHelper
    {
        #region Method : Category SelectAll
        public DataTable CategorySelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Category_SelectAll");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : Category Insert and Update
        public bool CategorySave(CategoryModel categoryModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (categoryModel.CategoryID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Category_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryName", DbType.String, categoryModel.CategoryName);
                    sqlDatabase.AddInParameter(dbCommand, "@Discription", DbType.String, categoryModel.Description);
                    sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Category_Update");
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, categoryModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryName", DbType.String, categoryModel.CategoryName);
                    sqlDatabase.AddInParameter(dbCommand, "@Discription", DbType.String, categoryModel.Description);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : Category By ID
        public CategoryModel CategoryByID(int CategoryID)
        {
            CategoryModel categoryModel = new CategoryModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Category_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    categoryModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    categoryModel.CategoryName = dataRow["CategoryName"].ToString();
                    categoryModel.Description = dataRow["Discription"].ToString();
                    categoryModel.Created = Convert.ToDateTime(dataRow["Created"]);
                    categoryModel.Modified = Convert.ToDateTime(dataRow["Modified"]);
                }
                return categoryModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : Category Delete
        public bool CategoryDelete(int CategoryID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Category_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
