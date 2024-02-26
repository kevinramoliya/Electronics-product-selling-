using ECommerce.Areas.Product.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace ECommerce.DAL.Product
{
    public class ProductDALBase : DALHelper
    {
        #region Method : Product SelectAll (Active)
        public DataTable ProductSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_SelectAll");
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

        #region Method : Product SelectAll (In Active)
        public DataTable ProductDeletedSelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_Deleted");
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

        #region Method : Shopping Product By ID
        public DataTable ShoppingProductByID(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
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


        #region Method : Product Insert & Update
        public bool ProductSave(ProductModel productModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (productModel.ProductID == 0)
                {
                    if (productModel.File != null)
                    {
                        string FilePath = "wwwroot\\Upload";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, productModel.File.FileName);

                        productModel.DisplayImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + productModel.File.FileName;

                        using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            productModel.File.CopyTo(fileStream);
                        }
                    }
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productModel.ProductName);
                    sqlDatabase.AddInParameter(dbCommand, "@Discription", DbType.String, productModel.Discription);
                    sqlDatabase.AddInParameter(dbCommand, "@Price", DbType.Int32, productModel.Price);
                    sqlDatabase.AddInParameter(dbCommand, "@Code", DbType.String, productModel.Code);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@isActive", DbType.Boolean, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@DisplayImage", DbType.String, productModel.DisplayImage);
                    sqlDatabase.AddInParameter(dbCommand, "@Discount", DbType.Int32, productModel.Discount);
                    sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.Int32, productModel.Rating);
                    sqlDatabase.AddInParameter(dbCommand, "@Created", DbType.DateTime, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand, "@Modified", DbType.DateTime, DBNull.Value);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    if (productModel.File != null)
                    {
                        string FilePath = "wwwroot\\Upload";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, productModel.File.FileName);

                        productModel.DisplayImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + productModel.File.FileName;

                        using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            productModel.File.CopyTo(fileStream);
                        }
                    }

                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_Update");
                    sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, productModel.ProductID);
                    sqlDatabase.AddInParameter(dbCommand, "@ProductName", DbType.String, productModel.ProductName);
                    sqlDatabase.AddInParameter(dbCommand, "@Discription", DbType.String, productModel.Discription);
                    sqlDatabase.AddInParameter(dbCommand, "@Price", DbType.Int32, productModel.Price);
                    sqlDatabase.AddInParameter(dbCommand, "@Code", DbType.String, productModel.Code);
                    sqlDatabase.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, productModel.CategoryID);
                    sqlDatabase.AddInParameter(dbCommand, "@DisplayImage", DbType.String, productModel.DisplayImage);
                    sqlDatabase.AddInParameter(dbCommand, "@Discount", DbType.Int32, productModel.Discount);
                    sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.Int32, productModel.Rating);
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

        #region Method : Product By ID
        public ProductModel ProductByID(int ProductID)
        {
            ProductModel productModel = new ProductModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@ProductID", DbType.Int32, ProductID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    productModel.ProductID = Convert.ToInt32(dataRow["ProductID"]);
                    productModel.ProductName = dataRow["ProductName"].ToString();
                    productModel.Discription = dataRow["Discription"].ToString();
                    productModel.Price = Convert.ToDouble(dataRow["Price"]);
                    productModel.Code = dataRow["Code"].ToString();
                    productModel.DisplayImage = dataRow["DisplayImage"].ToString();
                    productModel.CategoryID = Convert.ToInt32(dataRow["CategoryID"]);
                    productModel.isActive = Convert.ToBoolean(dataRow["isActive"]);
                    productModel.Discount = Convert.ToInt32(dataRow["Discount"].ToString());
                    productModel.Rating = Convert.ToInt32(dataRow["Rating"].ToString());
                    productModel.Created = Convert.ToDateTime(dataRow["Created"]);
                    productModel.Modified = Convert.ToDateTime(dataRow["Modified"]);
                }
                return productModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method : Product Delete
        public bool ProductDelete(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_Delete");
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

        #region Method : Product Retrive
        public bool ProductRetrive(int ProductID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("Product_Retrive");
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
    }
}
