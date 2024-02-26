using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using ECommerce.Areas.Cart.Models;
using ECommerce.BAL;
using ECommerce.Areas.Category.Models;
using Microsoft.CodeAnalysis;

namespace ECommerce.DAL.Cart
{
    public class CartDALBase : DALHelper
    {
        #region Method : Cart SelectAll
        public DataTable CartSelectAll(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Cart_SelectAll");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, Convert.ToInt32(UserID));
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

        #region Method : Cart Insert

        public bool CartInsert(CartModel cartModel, int ProductID, int UserID)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Cart_Insert");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                sqlDatabase.AddInParameter(dbCommand, "@Quantity", DbType.Int32, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@isOrderDone", DbType.Boolean, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : Increment Quantity
        public bool Increment_Quantity(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Increment_Quantity");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Method : Decrement Quantity
        public bool Decrement_Quantity(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Decrement_Quantity");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region Method : Remove Cart Product
        public bool Remove_Cart_Product(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Remove_Cart_Product");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Method : Cart Count
        public DataTable CartCount(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("CartCount");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
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

        #region Method : Update Order Status
        public bool Update_Order_Status(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Update_Order_Status");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
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
