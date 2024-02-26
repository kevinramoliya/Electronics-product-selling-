using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace ECommerce.DAL.Order
{
    public class OrderDALBase : DALHelper
    {
        #region Method : Order SelectAll (Pending)
        public DataTable PendingOrderSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Pending_Order_SelectAll");
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
    
    #region Method : Order SelectAll (Completed)
    public DataTable CompletedOrderSelectAll()
    {
        try
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Completed_Order_SelectAll");
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

    #region Method : Order SelectByPK
    public DataTable OrderSelectByPK(int OrderID)
    {
        try
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Order_SelectAllByPK");
            sqlDatabase.AddInParameter(dbCommand, "@OrderID", DbType.Int32, OrderID);
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

    #region Method : Order Complete
    public bool OrderComplete(int OrderID)
    {
        try
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Complete_Order");
            sqlDatabase.AddInParameter(dbCommand, "@OrderID", DbType.Int32, OrderID);
            bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
            return isSuccess;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
		#endregion

		#region Method : Order Insert
		public bool OrderInsert(int UserID, int[] ProductIDs, int AddressID)
		{
			SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
			try
			{
				DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Order_Insert");
				sqlDatabase.AddInParameter(dbCommand, "@AddressID", DbType.Int32, AddressID);
				sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
				DataTable productIdsTable = new DataTable();
				productIdsTable.Columns.Add("ProductID", typeof(int));
				foreach (var productId in ProductIDs)
				{
					productIdsTable.Rows.Add(productId);
				}
				// Convert DataTable to a comma-separated string of ProductIDs
				string productIdsString = string.Join(",", productIdsTable.AsEnumerable().Select(row => row.Field<int>("ProductID")));

				// Pass the string as a parameter
				sqlDatabase.AddInParameter(dbCommand, "@ProductIDs", DbType.String, productIdsString);
				sqlDatabase.AddInParameter(dbCommand, "@isCompleted", DbType.Boolean, DBNull.Value);
				sqlDatabase.AddInParameter(dbCommand, "@OrderStatus", DbType.String, DBNull.Value);
				sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
				sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
				sqlDatabase.AddInParameter(dbCommand, "@Completed", DbType.DateTime, DBNull.Value);
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
