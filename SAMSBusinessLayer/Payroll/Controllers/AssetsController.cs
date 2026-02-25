using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Company Related Tasks
    /// <remarks>
    /// <list type="bullet">
    /// <item>Insert Company</item>
    /// <item>Get Company</item>
    /// </list>
    /// </remarks>
    /// </summary>
    public class AssetsController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public AssetsController()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Asset Category

        public DataTable SelectAssetCategory()
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertAssetTypes loan = new spInsertAssetTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                DataTable dt = loan.ExecuteTableForAssetCategory();
                return dt;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw ex;
            }
        }
        public bool InsertAssetCategory(spInsertAssetTypes atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                atm.Transaction = mTranscation;
                atm.Connection = mConnection;
                atm.IS_DELETED = atm.IS_DELETED == true ? false : true;
                atm.ExecuteQueryForAssetCategory();
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion
        public DataTable SelectAssetType()
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertAssetTypes loan = new spInsertAssetTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                DataTable dt = loan.ExecuteTable();
                return dt;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw ex;
            }
        }


        public bool InsertAssetsType(AssetsTypeModel atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertAssetTypes loan = new spInsertAssetTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                loan.CompanyID = atm.CompanyID;
                loan.AssetTypeName = atm.AssetTypeName;
                loan.Description = atm.Description;
                loan.Brand = atm.Brand;
                loan.Model = atm.Model;
                loan.Capacity = atm.Capacity;
                loan.AssetCategoryID = atm.AssetCategoryID;
                loan.IS_DELETED = atm.IS_DELETED == true ? false : true;
                loan.User_ID = atm.User_ID;
                loan.IsSerialNoBased = atm.IsSerialNoBased;
                loan.ExecuteQuery();            
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public bool UpdateAssetsType(AssetsTypeModel atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spUpdateAssetTypes loan = new spUpdateAssetTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                loan.AssetTypeID = atm.AssetTypeID;                
                loan.AssetTypeName = atm.AssetTypeName;
                loan.User_ID = atm.User_ID;
                loan.IS_ACTIVE = atm.IS_ACTIVE;
                loan.Description = atm.Description;
                loan.Brand = atm.Brand;
                loan.Model = atm.Model;
                loan.Capacity = atm.Capacity;
                loan.AssetCategoryID = atm.AssetCategoryID;
                loan.IS_DELETED = atm.IS_DELETED == true ? false : true;
                loan.IsSerialNoBased = atm.IsSerialNoBased;
                loan.ExecuteQuery();
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable SelectSupplier()
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spSupplierCRUD loan = new spSupplierCRUD();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                DataTable dt = loan.ExecuteTable();
                return dt;
            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                throw ex;
            }
        }
        public bool InsertSupplier(spSupplierCRUD atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spSupplierCRUD supplier = new spSupplierCRUD();
                supplier = atm;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                supplier.ExecuteQuery();
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public bool UpdateSupplier(spSupplierCRUD atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                atm.Transaction = mTranscation;
                atm.Connection = mConnection;
                atm.ExecuteQueryForUpdate();
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectPurchaseDocumentNo(DateTime p_Document_Date, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetPurchaseMaster mPurchaseMaster = new spAssetPurchaseMaster();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_Document_Date;
                mPurchaseMaster.Asset_Purchase_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.User_ID = p_User_Id;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectAssetPurchaseDetail(long p_AssetPurchaseID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetPurchaseDetail mPurchaseMaster = new spAssetPurchaseDetail();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Asset_Purchase_ID = p_AssetPurchaseID;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectAssetNo(long p_AssetNoMarking_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetPurchaseDetail mPurchaseMaster = new spAssetPurchaseDetail();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Asset_Purchase_ID = p_AssetNoMarking_ID;
                DataTable dt = mPurchaseMaster.ExecuteTableForAssetNo();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectSerialNo1()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetPurchaseDetail mPurchaseMaster = new spAssetPurchaseDetail();
                mPurchaseMaster.Connection = mConnection;
                DataTable dt = mPurchaseMaster.ExecuteTableForSerialNo();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectCustomerForAsset(string p_locationIds)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetPurchaseDetail mPurchaseMaster = new spAssetPurchaseDetail();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.SerialNo1 = p_locationIds;
                DataTable dt = mPurchaseMaster.ExecuteTableForCustomer();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectPurchasedAssetsForAssetNoMarking(int p_distributor_ID, bool p_WithCreatedAssetNo)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetNoMarkingCRUD mPurchaseMaster = new spAssetNoMarkingCRUD();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_distributor_ID;
                mPurchaseMaster.WithCreatedAssetNo = p_WithCreatedAssetNo;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public long InsertAssetPurchase(spAssetPurchaseMaster apm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spAssetPurchaseMaster supplier = new spAssetPurchaseMaster();
                supplier = apm;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                var result = supplier.ExecuteQuery();
                mTranscation.Commit();
                return Convert.ToInt64(result.Rows[0]["Asset_Purchase_ID"].ToString());
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public long UpdateAssetPurchase(spAssetPurchaseMaster apm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spAssetPurchaseMaster supplier = new spAssetPurchaseMaster();
                supplier = apm;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                var result = supplier.ExecuteQueryForUpdate();
                mTranscation.Commit();
                return supplier.Asset_Purchase_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public long InsertAssetPurchaseDetail(spAssetPurchaseDetail apd)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spAssetPurchaseDetail supplier = new spAssetPurchaseDetail();
                supplier = apd;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                var result = supplier.ExecuteQuery();
                mTranscation.Commit();
                return Convert.ToInt64(result.Rows[0]["Asset_PurchaseDetail_ID"].ToString());
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public long InsertOrUpdateAssetMarkingNo(spAssetNoMarkingCRUD apd)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spAssetNoMarkingCRUD supplier = new spAssetNoMarkingCRUD();
                supplier = apd;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                var result = supplier.ExecuteQuery();
                mTranscation.Commit();
                return Convert.ToInt64(result.Rows[0]["Asset_Marking_ID"].ToString());
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #region Asset Transfer Out
        public DataTable SelectTransferOutDocumentNo(DateTime p_Document_Date, int p_distributorId, long p_Asset_Stock_ID, int p_User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.FROM_LOCATION = p_distributorId;
                mPurchaseMaster.DOCUMENT_DATE = p_Document_Date;
                mPurchaseMaster.ASSET_STOCK_ID = p_Asset_Stock_ID;
                mPurchaseMaster.User_ID = p_User_Id;
                DataTable dt = mPurchaseMaster.ExecuteTableForTransferOutDocNo();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectTransferOutAssetsByID(long p_Asset_Stock_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.ASSET_STOCK_ID = p_Asset_Stock_ID;
                DataTable dt = mPurchaseMaster.ExecuteTableForTransferOutDetail();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable SelectAssetDetailByAssetStockID(long p_Asset_Stock_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.ASSET_STOCK_ID = p_Asset_Stock_ID;
                DataTable dt = mPurchaseMaster.ExecuteTableForAssetDetail();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public DataTable SelectAssetsFromAssetNoMarking(int p_distributor_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_distributor_ID;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public long InsertAssetStock(spAssetTransferOut apd)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spAssetTransferOut supplier = new spAssetTransferOut();
                supplier = apd;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                var result = supplier.ExecuteQuery();
                mTranscation.Commit();
                return Convert.ToInt64(result.Rows[0]["ID"].ToString());
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public long InsertOrUpdateStockDetail(spAssetTransferOutDetail apd)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spAssetTransferOutDetail supplier = new spAssetTransferOutDetail();
                supplier = apd;
                supplier.Transaction = mTranscation;
                supplier.Connection = mConnection;
                var result = supplier.ExecuteQuery();
                mTranscation.Commit();
                return Convert.ToInt64(result.Rows[0]["ID"].ToString());
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion

        #region Asset Transfer In
        public DataTable SelectTransferOutAssets(DateTime p_workingDate, int p_userId, long p_stockId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.User_ID = p_userId;
                mPurchaseMaster.DOCUMENT_DATE = p_workingDate;
                mPurchaseMaster.ASSET_STOCK_ID = p_stockId;
                DataTable dt = mPurchaseMaster.ExecuteTableForTransferIn();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion

        #region Damage
        public DataTable SelectDamageType()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetDamage mPurchaseMaster = new spAssetDamage();
                mPurchaseMaster.Connection = mConnection;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectClosingStock(int p_DISTRIBUTOR_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetDamage mPurchaseMaster = new spAssetDamage();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                DataTable dt = mPurchaseMaster.ExecuteTableForClosingStock();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectAssetDamage(DateTime p_Document_Date, int p_distributorId, long p_Asset_Stock_ID, int p_User_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetDamage mPurchaseMaster = new spAssetDamage();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.FROM_LOCATION = p_distributorId;
                mPurchaseMaster.DOCUMENT_DATE = p_Document_Date;
                mPurchaseMaster.ASSET_STOCK_ID = p_Asset_Stock_ID;
                mPurchaseMaster.User_ID = p_User_Id;
                DataTable dt = mPurchaseMaster.ExecuteTableForDamageDocNo();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion

        #region Customer Asset Return
        public DataTable SelectTransferOutCustomerAsset(int p_toLocation, long p_customerId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.TO_LOCATION = p_toLocation;
                mPurchaseMaster.CUSTOMER_ID = p_customerId;
                DataTable dt = mPurchaseMaster.ExecuteTableForTransferOutCustomerAsset();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        #endregion
        public DataTable SelectLocationWiseAssetDetail(int p_toLocation, DateTime p_Date, int p_typeId, string p_disIds)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spAssetTransferOut mPurchaseMaster = new spAssetTransferOut();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_toLocation;
                mPurchaseMaster.DOCUMENT_DATE = p_Date;
                mPurchaseMaster.TYPE_ID = p_typeId;
                mPurchaseMaster.Remarks = p_disIds;
                DataTable dt = mPurchaseMaster.ExecuteTableForAssetDetailByLocation();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectAssetDetailRpt(string p_AssetTypeIds, string p_locationIds, string p_assetNo, 
            string p_TransactionType, string p_SerialNo, string customerIds, DateTime p_fromDate, DateTime p_toDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                RptAsset mPurchaseMaster = new RptAsset();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_IDs = p_locationIds;
                mPurchaseMaster.AssetType_IDs = p_AssetTypeIds;
                mPurchaseMaster.AssetNo = p_assetNo;
                mPurchaseMaster.Transaction_Type = p_TransactionType;
                mPurchaseMaster.SerialNo1 = p_SerialNo;
                mPurchaseMaster.Customer_IDs = customerIds;
                mPurchaseMaster.FROM_DATE = p_fromDate;
                mPurchaseMaster.TO_DATE = p_toDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
    }
}
