using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using ECommerce.Areas.Address.Models;

namespace ECommerce.DAL.Checkout
{
    public class CheckoutDALBase : DALHelper
    {
        #region Method : Checkout
        public DataTable Checkout(int UserID)
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

        #region Method : Checkout
        public DataTable Address_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Address_SelectAll");
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

        #region Method : Address Insert
        public bool AddressInsert(AddressModel addressModel, int UserID)
            {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Address_Insert");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                sqlDatabase.AddInParameter(dbCommand, "@Address", DbType.String, addressModel.Address);
                sqlDatabase.AddInParameter(dbCommand, "@Country", DbType.String, addressModel.Country);
                sqlDatabase.AddInParameter(dbCommand, "@State", DbType.String, addressModel.State);
                sqlDatabase.AddInParameter(dbCommand, "@City", DbType.String, addressModel.City);
                sqlDatabase.AddInParameter(dbCommand, "@Postal", DbType.String, addressModel.Postal);
                sqlDatabase.AddInParameter(dbCommand, "@isDefault", DbType.Boolean, DBNull.Value);
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
    }
}
