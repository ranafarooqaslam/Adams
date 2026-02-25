using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Purchase, TranferOut, Purchase Return, TranferIn And Damage Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// <term>
    /// Update Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </term>
    /// <item>
    /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PurchaseController
    {
        #region Constructor

        /// <summary>
        /// Constructor for PurchaseController
        /// </summary>
        public PurchaseController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

        #region Private Variables
        
        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Public Methods

        #region Select

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPrivousePurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = PConnection;
                mPurchaseDetail.Transaction = PTransaction;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.POSTING = p_Posting;
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
                        
        /// <summary>
        /// Gets Purchase Order Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase Order Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseOrderDocumentNo(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_DOCUMENT_DATE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
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

        public DataTable SelectPurchaseOrderDocumentNo2(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int P_USER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.USER_ID = P_USER_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
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
        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseOrderDetail(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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
        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="P_DocumentDate">Date</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, DateTime P_DocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = P_DocumentDate;
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

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting, int p_SOLD_TO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
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

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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
        public DataTable SelectPurchaseDetail(long p_PURCHASE_MASTER_ID,int p_DISTRIBUTOR_ID,int p_DISTRIBUTOR_ID2)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL2 mPurchaseDetail = new spSelectPURCHASE_DETAIL2();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.DISTRIBUTOR_ID2 = p_DISTRIBUTOR_ID2;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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

        public DataTable GetMaxVehicleReading(int p_VEHICLE_ID, DateTime p_DATE, int p_TYPE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMaxVehicleReading mVehicleReading = new uspGetMaxVehicleReading();
                mVehicleReading.Connection = mConnection;
                mVehicleReading.VEHICLE_ID = p_VEHICLE_ID;
                mVehicleReading.DATE = p_DATE;
                mVehicleReading.TYPE = p_TYPE;
                DataTable dt = mVehicleReading.ExecuteTable();
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

        #region Insert, Update

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID,string p_ORDER_NUMBER,int p_TYPE_ID,DateTime p_DOCUMENT_DATE,int p_SOLD_TO,int p_SOLD_FROM,decimal p_TOTAL_AMOUNT,bool p_IS_DELETE,DataTable dtPurchaseDetail,int p_Posting,string p_BuiltyNo,int p_UserId,int p_PrincipalId) 
		{
			try
			{                 
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;  
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;   
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;
               
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();
                    
                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId; 
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;   
                    mStockUpdate.ExecuteQuery();   
                }
                mTransaction.Commit();
                return true; 
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();  
				return false;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}
       
        public long InsertPurchaseDocumentID(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
               return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return Constants.LongNullValue;
        }
    
        
        
        public bool InsertPurchaseDocument2(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, 
            int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting,
            string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(Adams Head Office)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(Adams Head Office)";
                                }
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction ,mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                      bool IsInsert=  LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection);
                      if (!IsInsert)
                      {
                          return false;
                      }
                    }
                    else if(p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public long InsertPurchaseDocument2ID(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(Adams Head Office)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(Adams Head Office)";
                                }
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return Constants.LongNullValue;
        }
        public long InsertPurchaseDocument2ID(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId,out string Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out || p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(Adams Head Office)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(Adams Head Office)";
                                }
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                Voucher_No = null;
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
                
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return Constants.LongNullValue;
        }      
        
        public bool InsertPurchaseDocument2(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Transfer Out(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction ,mConnection );
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                      bool isinsert=  LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                      if (!isinsert)
                      {
                          return false;
                      }

                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                      bool isinsert=  LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                      if (!isinsert)
                      {
                          return false;
                      }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public long InsertPurchaseDocument2ID(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out || p_TYPE_ID == Constants.Document_Returnable_Stock)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out || p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                                {
                                    drStock["REMARKS"] = "Returnable Replace Send(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(" + p_ACCOUNT_NAME + ")";
                                }
                                else if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(" + p_ACCOUNT_NAME + ")";
                                }
                                else if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                                {
                                    drDiscount["REMARKS"] = "Returnable Replace Send(" + p_ACCOUNT_NAME + ")";
                                }                                
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }

                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Replace Send Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                }

                mTransaction.Commit();
              return  mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return Constants.LongNullValue;
        }
        public long InsertPurchaseDocument2ID(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME , out string Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out || p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Transfer Out(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Stock Received(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Stock Received(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = 0;
                                drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                Voucher_No=null;
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                        

                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    Voucher_No=MaxDocumentId;
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No=null;
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return Constants.LongNullValue;
        }
        
        public bool InsertPurchaseDocument3(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();


                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Opening Stock(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Short(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Short(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Excess(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Excess(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = 0;
                                drAccountHead["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Damage(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Damage(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction ,mConnection );
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                       bool IsInsert= LController.Add_Voucher2Opening(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Opening Stock Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Opening, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                       if (!IsInsert)
                       {
                           return false;
                       }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Short Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Short, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Excess Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Acess, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Damage Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Damaged, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public long InsertPurchaseDocument3(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out String Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();


                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Opening Stock(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Short(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Short(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Excess(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Excess(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = 0;
                                drAccountHead["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Damage(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Damage(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                }

                 Voucher_No = null;
                if (dtVoucher.Rows.Count > 0)
                {
                    var lController = new LedgerController();
                    string maxDocumentId = lController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                        bool isInsert = lController.Add_Voucher2Opening(p_DISTRIBUTOR_ID, 0, maxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Opening Stock Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Opening, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isInsert)
                        {
                            return -2;
                        }
                        Voucher_No = maxDocumentId;
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        bool isInsert = lController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, maxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Short Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Short, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isInsert)
                        {
                            return -2;
                        }
                        Voucher_No = maxDocumentId;
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        bool isInsert = lController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, maxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Excess Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Acess, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isInsert)
                        {
                            return -2;
                        }
                        Voucher_No = maxDocumentId;
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        bool isInsert = lController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, maxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Damage Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Damaged, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isInsert)
                        {
                            return -2;
                        }
                        Voucher_No = maxDocumentId;
                    }
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
           
        }

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail,
            int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, long p_PO_ID, string p_REF_NO
            , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME, string p_VEHICLE_NO, string p_TEMP)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = p_PO_ID;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                  

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool InsertPurchaseDocument2(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail,
            int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, long p_PO_ID, string p_REF_NO
            , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME, string p_VEHICLE_NO, string p_TEMP)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                
                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = p_PO_ID;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    mPurchaseDetail.Document_date = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                    if (dtAccount.Rows.Count > 0)
                    {
                        if (foundRows.Length > 0)
                        {
                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Purchase(Stock In Hand)";
                            drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drStock["CREDIT"] = 0;
                            drStock["Principal_Id"] = p_PrincipalId;
                            dtVoucher.Rows.Add(drStock);
                         
                            DataRow drDiscount = dtVoucher.NewRow();
                            drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                            drDiscount["REMARKS"] = "Purchase(Adams Head Office)";
                            drDiscount["DEBIT"] = 0;
                            drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drDiscount["Principal_Id"] = p_PrincipalId;
                            dtVoucher.Rows.Add(drDiscount);
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction ,mConnection );

                    LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public long InsertPurchaseDocument2(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail,
            int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, long p_PO_ID, string p_REF_NO
            , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME,
            string p_VEHICLE_NO, string p_TEMP, out String Voucher_No, long? p_SALE_INVOICE_ID = null, bool? p_isSynced = null)
        {
            try
            {
                SkuController skuCtl = new SkuController();
                DataTable dtAccount = skuCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = p_PO_ID;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;
                mPurchaseMaster.SALE_INVOICE_ID = p_SALE_INVOICE_ID;
                mPurchaseMaster.IS_SYNCED = p_isSynced;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                decimal totalTax = 0;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString()); 
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    mPurchaseDetail.Document_date = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    totalTax += decimal.Parse(dr["AMOUNT"].ToString()) * 
                        (decimal.Parse(dr["TAX_PERCENT"].ToString()) / 100);

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                    if (dtAccount.Rows.Count > 0)
                    {
                        if (foundRows.Length > 0)
                        {
                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Purchase(Stock In Hand)";
                            drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drStock["CREDIT"] = 0;
                            drStock["Principal_Id"] = p_PrincipalId;
                            dtVoucher.Rows.Add(drStock);

                            DataRow drDiscount = dtVoucher.NewRow();
                            drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                            drDiscount["REMARKS"] = "Purchase(Adams Head Office)";
                            drDiscount["DEBIT"] = 0;
                            drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drDiscount["Principal_Id"] = p_PrincipalId;
                            dtVoucher.Rows.Add(drDiscount);
                        }
                    }
                }

                if (dtAccount.Rows.Count > 0)
                {
                        DataRow drStock = dtVoucher.NewRow();
                        drStock["ACCOUNT_HEAD_ID"] = 725;
                        drStock["REMARKS"] = "Sales Tax Payable";
                        drStock["DEBIT"] = totalTax;
                        drStock["CREDIT"] = 0;
                        drStock["Principal_Id"] = p_PrincipalId;
                        dtVoucher.Rows.Add(drStock);

                        DataRow drDiscount = dtVoucher.NewRow();
                        drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                        drDiscount["REMARKS"] = "Sales Tax Payable (Adams Head Office)";
                        drDiscount["DEBIT"] = 0;
                        drDiscount["CREDIT"] = totalTax;
                        drDiscount["Principal_Id"] = p_PrincipalId;
                        dtVoucher.Rows.Add(drDiscount);
                }

                LedgerController LController = new LedgerController();
                Voucher_No = null;
                string maxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                
                if (dtVoucher.Rows.Count > 0)
                {
                   
                 bool isinsert= LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, maxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                    if(!isinsert)
                    {
                        return -2;
                    }
                    Voucher_No = maxDocumentId;
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

       
        /// <summary>
        /// Updates Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId,int p_Principal)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER(); 
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;  
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;   
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;  
                mPurchaseMaster.ExecuteQuery();
               
                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID,mConnection,mTransaction);
                
                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID; 
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                   
                    mPurchaseStock.ExecuteQuery();
                }
                                
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    
                   //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;  
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal; 
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseDocumentTransfer_In(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());

                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
    
        public bool UpdatePurchaseDocumentTransfor_Out_Shop(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal)
        
        {

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());

                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public bool UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(Adams Head Office)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(Adams Head Office)";
                                }
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction ,mConnection );
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                      bool IsInsert=  LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                      if (!IsInsert)
                      {
                          return false;
                      }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, out String Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(Adams Head Office)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(Adams Head Office)";
                                }
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                Voucher_No = null;
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseDocument2PurchaseReutn(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, out String Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drStock["REMARKS"] = "Purchase Return(Stock In Hand)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                }
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                                if (p_TYPE_ID == Constants.Document_Purchase_Return)
                                {
                                    drDiscount["REMARKS"] = "Purchase Return(Adams Head Office)";
                                }
                                else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                                {
                                    drDiscount["REMARKS"] = "Transfer Out(Adams Head Office)";
                                }
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                Voucher_No = null;
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool IsInsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!IsInsert)
                        {
                            return false;
                        }
                    }
                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }        
        
        public bool UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Transfer Out(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction,mConnection);
                   
                   
                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                       bool isinsert= LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                       if (!isinsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out String Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Transfer Out(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                Voucher_No = null;
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);


                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public bool UpdatePurchaseDocument2Transfer_Out_Branch(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out String Voucher_No)
        {

            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                {
                    dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                }

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Purchase_Return || p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Transfer Out(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Transfer Out(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Stock Received(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Stock Received(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = 0;
                                drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }
                Voucher_No = null;
                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);


                    if (p_TYPE_ID == Constants.Document_Purchase_Return)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Return Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase_Return, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Transfer_Out)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Transfer Out Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Transfer_Out, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
             
        public bool UpdatePurchaseDocument3(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Opening Stock(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Short(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Short(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Excess(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Excess(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = 0;
                                drAccountHead["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Damage(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Excess(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Replace Send(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Returnable Replace Send(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = 0;
                                drAccountHead["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction ,mConnection );
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                      bool isinsert=  LController.Add_Voucher2Opening(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Opening Stock Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Opening, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                      if (!isinsert)
                      {
                          return false;
                      }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Short Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Short, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Excess Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Acess, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Damage Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Damaged, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Replace Send Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseDocumentStockAdjustment(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME)
        {

            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Opening Stock(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Short(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Short(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Excess(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Excess(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = 0;
                                drAccountHead["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Damage(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Excess(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Opening)
                    {
                        bool isinsert = LController.Add_Voucher2Opening(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Opening Stock Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Opening, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Short)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Short Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Short, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Acess)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Excess Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Acess, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Damaged)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Damage Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Damaged, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, string p_REF_NO
            , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME, string p_VEHICLE_NO, string p_TEMP)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                //DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                //foreach (DataRow dr in dt.Rows)
                //{
                //    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                //    mPurchaseStock.Connection = mConnection;
                //    mPurchaseStock.Transaction = mTransaction;

                //    mPurchaseStock.TYPEID = p_TYPE_ID;
                //    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                //    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                //    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;

                //    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                //    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                //    mPurchaseStock.ExecuteQuery();
                //}

                UspUpdatePurchaseDetailStock2 mPurchaseDetail = new UspUpdatePurchaseDetailStock2();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    //  mPurchaseDetail.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());

                    if (dr["TIME_STAMP"] != null)
                    {
                        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    }
                    else
                    {
                        mPurchaseDetail.TIME_STAMP = Constants.DateNullValue;
                    }
                    if (dr["MFG_DATE"] != null)
                    {
                        mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    }
                    else
                    {
                        mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    }
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = "N/A";// mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, string p_REF_NO
            , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME, string p_VEHICLE_NO, string p_TEMP)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;
                mPurchaseMaster.ExecuteQuery();


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.document_date = p_DOCUMENT_DATE;
                    mPurchaseStock.ExecuteQuery();
                }

                #region insert Purchase Detail
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    mPurchaseDetail.Document_date = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();



                    ////
                


                #endregion
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




                    #region Update Purchase Detail by hasan 


                    //UspUpdatePurchaseDetailStock2 mPurchaseDetail = new UspUpdatePurchaseDetailStock2();
                //mPurchaseDetail.Connection = mConnection;
                //mPurchaseDetail.Transaction = mTransaction;

                //foreach (DataRow dr in dtPurchaseDetail.Rows)
                //{

                //    //update stock;
                //    //  mPurchaseDetail.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                //    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                //    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                //    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                //    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());

                //    if (dr["TIME_STAMP"] != null)
                //    {
                //        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                //    }
                //    else
                //    {
                //        mPurchaseDetail.TIME_STAMP = Constants.DateNullValue;
                //    }
                //    if (dr["MFG_DATE"] != null)
                //    {
                //        mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                //    }
                //    else
                //    {
                //        mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                //    }
                    //    mPurchaseDetail.ExecuteQuery();
                    #endregion


                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                    if (dtAccount.Rows.Count > 0)
                    {
                        if (foundRows.Length > 0)
                        {
                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Purchase(Stock In Hand)";
                            drStock["DEBIT"] = decimal.Parse(dr["FREE_SKU"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drStock["CREDIT"] = 0;
                            drStock["Principal_Id"] = p_Principal;
                            dtVoucher.Rows.Add(drStock);

                            DataRow drDiscount = dtVoucher.NewRow();
                            drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                            drDiscount["REMARKS"] = "Purchase(Adams Head Office)";
                            drDiscount["DEBIT"] = 0;
                            drDiscount["CREDIT"] = decimal.Parse(dr["FREE_SKU"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drDiscount["Principal_Id"] = p_Principal;
                            dtVoucher.Rows.Add(drDiscount);
                        }
                    }

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = "N/A";// mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                 
                }


                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE,mTransaction,mConnection);

                  bool isinsert=  LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                  
                  if (!isinsert)
                  {
                      return false;
                  }
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public long UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, string p_REF_NO
          , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME, string p_VEHICLE_NO, string p_TEMP, out String Voucher_No)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;
                mPurchaseMaster.ExecuteQuery();


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.document_date = p_DOCUMENT_DATE;
                    mPurchaseStock.ExecuteQuery();
                }

                #region insert Purchase Detail
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                decimal totalTax = 0;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString()); 
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    mPurchaseDetail.Document_date = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    totalTax += decimal.Parse(dr["AMOUNT"].ToString()) *
                        (decimal.Parse(dr["TAX_PERCENT"].ToString()) / 100);

                    ////



                    #endregion
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




                    #region Update Purchase Detail by hasan


                    //UspUpdatePurchaseDetailStock2 mPurchaseDetail = new UspUpdatePurchaseDetailStock2();
                    //mPurchaseDetail.Connection = mConnection;
                    //mPurchaseDetail.Transaction = mTransaction;

                    //foreach (DataRow dr in dtPurchaseDetail.Rows)
                    //{

                    //    //update stock;
                    //    //  mPurchaseDetail.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    //    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    //    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    //    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    //    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());

                    //    if (dr["TIME_STAMP"] != null)
                    //    {
                    //        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    //    }
                    //    else
                    //    {
                    //        mPurchaseDetail.TIME_STAMP = Constants.DateNullValue;
                    //    }
                    //    if (dr["MFG_DATE"] != null)
                    //    {
                    //        mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    //    }
                    //    else
                    //    {
                    //        mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    //    }
                    //    mPurchaseDetail.ExecuteQuery();
                    #endregion


                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                    if (dtAccount.Rows.Count > 0)
                    {
                        if (foundRows.Length > 0)
                        {
                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Purchase(Stock In Hand)";
                            drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drStock["CREDIT"] = 0;
                            drStock["Principal_Id"] = p_Principal;
                            dtVoucher.Rows.Add(drStock);

                            DataRow drDiscount = dtVoucher.NewRow();
                            drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                            drDiscount["REMARKS"] = "Purchase(Adams Head Office)";
                            drDiscount["DEBIT"] = 0;
                            drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drDiscount["Principal_Id"] = p_Principal;
                            dtVoucher.Rows.Add(drDiscount);
                        }
                    }

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = "N/A";// mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();


                }

                if (dtAccount.Rows.Count > 0)
                {
                    DataRow drStock = dtVoucher.NewRow();
                    drStock["ACCOUNT_HEAD_ID"] = 725;
                    drStock["REMARKS"] = "Sales Tax Payable";
                    drStock["DEBIT"] = totalTax;
                    drStock["CREDIT"] = 0;
                    drStock["Principal_Id"] = p_Principal;
                    dtVoucher.Rows.Add(drStock);

                    DataRow drDiscount = dtVoucher.NewRow();
                    drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                    drDiscount["REMARKS"] = "Sales Tax Payable (Adams Head Office)";
                    drDiscount["DEBIT"] = 0;
                    drDiscount["CREDIT"] = totalTax;
                    drDiscount["Principal_Id"] = p_Principal;
                    dtVoucher.Rows.Add(drDiscount);
                }

                LedgerController LController = new LedgerController();
                Voucher_No = null;
                string maxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);

                if (dtVoucher.Rows.Count > 0)
                {
                   
                    bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, maxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);

                    if (!isinsert)
                    {
                        return -2;
                    }
                    Voucher_No = maxDocumentId;
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseDocument2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, string p_REF_NO
            , DateTime p_DESPATCH_DATE, string p_DESPATCH_NO, string p_SEAL_NO, string p_GATEPASS_NO, string p_DRIVER_NAME, string p_VEHICLE_NO, string p_TEMP, decimal p_CurrentRcdQty)
        {
            try
            {
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.DESPATCH_DATE = p_DESPATCH_DATE;
                mPurchaseMaster.DESPATCH_NO = p_DESPATCH_NO;
                mPurchaseMaster.SEAL_NO = p_SEAL_NO;
                mPurchaseMaster.GATEPASS_NO = p_GATEPASS_NO;
                mPurchaseMaster.DRIVER_NAME = p_DRIVER_NAME;
                mPurchaseMaster.VEHICLE_NO = p_VEHICLE_NO;
                mPurchaseMaster.TEMP = p_TEMP;
                mPurchaseMaster.ExecuteQuery();


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.document_date = p_DOCUMENT_DATE;
                    mPurchaseStock.ExecuteQuery();
                }

                #region insert Purchase Detail
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = 0;
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    mPurchaseDetail.Document_date = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();



                    ////



                #endregion
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




                    #region Update Purchase Detail by hasan


                    //UspUpdatePurchaseDetailStock2 mPurchaseDetail = new UspUpdatePurchaseDetailStock2();
                    //mPurchaseDetail.Connection = mConnection;
                    //mPurchaseDetail.Transaction = mTransaction;

                    //foreach (DataRow dr in dtPurchaseDetail.Rows)
                    //{

                    //    //update stock;
                    //    //  mPurchaseDetail.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    //    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    //    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    //    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    //    mPurchaseDetail.QUANTITY = int.Parse(dr["FREE_SKU"].ToString());

                    //    if (dr["TIME_STAMP"] != null)
                    //    {
                    //        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    //    }
                    //    else
                    //    {
                    //        mPurchaseDetail.TIME_STAMP = Constants.DateNullValue;
                    //    }
                    //    if (dr["MFG_DATE"] != null)
                    //    {
                    //        mPurchaseDetail.MFG_DATE = Convert.ToDateTime(dr["MFG_DATE"]);
                    //    }
                    //    else
                    //    {
                    //        mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    //    }
                    //    mPurchaseDetail.ExecuteQuery();
                    #endregion


                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                    if (dtAccount.Rows.Count > 0)
                    {
                        if (foundRows.Length > 0)
                        {
                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Purchase(Stock In Hand)";
                            drStock["DEBIT"] = decimal.Parse(dr["FREE_SKU"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drStock["CREDIT"] = 0;
                            drStock["Principal_Id"] = p_Principal;
                            dtVoucher.Rows.Add(drStock);

                            DataRow drDiscount = dtVoucher.NewRow();
                            drDiscount["ACCOUNT_HEAD_ID"] = 85;//Adams Head Office
                            drDiscount["REMARKS"] = "Purchase(Adams Head Office)";
                            drDiscount["DEBIT"] = 0;
                            drDiscount["CREDIT"] = decimal.Parse(dr["FREE_SKU"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                            drDiscount["Principal_Id"] = p_Principal;
                            dtVoucher.Rows.Add(drDiscount);
                        }
                    }

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = 0;
                    mStockUpdate.BATCHNO = "N/A";// mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();


                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE);

                    LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Purchase Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Purchase, dtVoucher, p_UserId, null, Constants.DateNullValue);
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }


        public long InsertPurchaseDocumentReturnable(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out string Voucher_No)
        {
            try
            {
                Voucher_No = null;
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);                
                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Replace Send(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Replace Send(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Stock Received(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Stock Received(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = 0;
                                drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {                    
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Replace Send Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Stock Received Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock_Received, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }

                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public long InsertPurchaseDocumentReturnableStock(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out string Voucher_No)
        {
            try
            {
                Voucher_No = null;
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = new DataTable();

                dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = 0;
                mPurchaseMaster.DESPATCH_DATE = Constants.DateNullValue;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.RECEIVED_QUANTITY = int.Parse(dr["Received_Quantity"].ToString());
                    mPurchaseDetail.REJECTED_QUANTITY = int.Parse(dr["Rejected_Quantity"].ToString());
                    mPurchaseDetail.BATCH_FAULT_QUANTITY = int.Parse(dr["Batch_Fault_Quantity"].ToString());
                    mPurchaseDetail.FAULT_ID = int.Parse(dr["FAULT_ID"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.MFG_DATE = Constants.DateNullValue;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();

                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Replace Send(Stock In Hand)";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Replace Send(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Stock Received(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Stock Received(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = 0;
                                drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["Principal_Id"] = p_PrincipalId;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Replace Send Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Stock Received Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock_Received, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                    }

                    Voucher_No = MaxDocumentId;
                }

                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_MASTER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return Constants.LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdatePurchaseDocumentReturnableStock(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out string Voucher_No)
        {
            try
            {
                Voucher_No = null;
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.RECEIVED_QUANTITY = int.Parse(dr["Received_Quantity"].ToString());
                    mPurchaseDetail.REJECTED_QUANTITY = int.Parse(dr["Rejected_Quantity"].ToString());
                    mPurchaseDetail.BATCH_FAULT_QUANTITY = int.Parse(dr["Batch_Fault_Quantity"].ToString());
                    mPurchaseDetail.FAULT_ID = int.Parse(dr["FAULT_ID"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Replace Send(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Returnable Replace Send(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["DEBIT"] = 0;
                                drAccountHead["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);
                            }
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                       
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Stock Received(Stock In Hand)";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drDiscount["REMARKS"] = "Returnable Stock Received(" + p_ACCOUNT_NAME + ")";
                                drDiscount["DEBIT"] = 0;
                                drDiscount["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drDiscount["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Replace Send Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        Voucher_No = MaxDocumentId;
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                    else if (p_TYPE_ID == Constants.Document_Returnable_Stock_Received)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Stock Received Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        Voucher_No = MaxDocumentId;
                        if (!isinsert)
                        {
                            return false;
                        }

                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseDocumentReturnable(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_ACCOUNT_HEAD_ID, string p_ACCOUNT_NAME, out string Voucher_No)
        {
            try
            {
                Voucher_No = null;
                SkuController SKUCtl = new SkuController();
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();
                
                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStockTransforOut mPurchaseStock = new UspUpdatePurchaseDetailStockTransforOut();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ORDER_QUANTITY = 0;//int.Parse(dr["ORDER_QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Returnable Replace Send(Stock In Hand)";
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drStock["DEBIT"] = 0;
                                drStock["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drStock);

                                DataRow drAccountHead = dtVoucher.NewRow();
                                drAccountHead["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                                drAccountHead["REMARKS"] = "Returnable Replace Send(" + p_ACCOUNT_NAME + ")";
                                drAccountHead["CREDIT"] = 0;
                                drAccountHead["DEBIT"] = decimal.Parse(dr["QUANTITY"].ToString()) * decimal.Parse(dr["PRICE"].ToString());
                                drAccountHead["Principal_Id"] = p_Principal;
                                dtVoucher.Rows.Add(drAccountHead);

                               
                            }
                        }
                    }
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    LedgerController LController = new LedgerController();
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE, mTransaction, mConnection);
                    if (p_TYPE_ID == Constants.Document_Returnable_Stock)
                    {
                        bool isinsert = LController.Add_Voucher2(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, "N/A", "Default Returnable Replace Send Voucher", Constants.DateNullValue, null, mPurchaseMaster.PURCHASE_MASTER_ID, Constants.Document_Returnable_Stock, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        Voucher_No = MaxDocumentId;
                        if (!isinsert)
                        {
                            return false;
                        }
                    }
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                Voucher_No = null;
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }


        /// <summary>
        /// Posts Pending Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool PostPendingDocument(long p_PURCHASE_MASTER_ID,int p_Type_Id,int p_Distributor_Id,int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.TYPE_ID = p_Type_Id;  
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public long InsertPurchaseOrder(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, string p_REF_NO, string  p_BUILTY_NO, DateTime p_DOCUMENT_DATE, decimal p_TOTAL_AMOUNT, int p_USER_ID,
            DataTable dtPurchaseDetail, DateTime p_TimeStamp)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_ORDER mPurchaseMaster = new spInsertPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.BUILTY_NO = p_BUILTY_NO;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = p_TimeStamp;  // as document date
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TIME_STAMP = DateTime.Now;
                    mPurchaseDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_ORDER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
           
        }
       


        public long UpdatePurchaseOrderDetail(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, string p_REF_NO, string p_BUILTY_NO, decimal p_TOTAL_AMOUNT, int p_USER_ID, DataTable dtPurchaseDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spDeletePURCHASE_ORDER_DETAIL mDelete = new spDeletePURCHASE_ORDER_DETAIL();
                mDelete.Connection = mConnection;
                mDelete.Transaction = mTransaction;
                mDelete.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDelete.ExecuteQuery();

               
                spUpdatePURCHASE_ORDER mPurchaseMaster = new spUpdatePURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.BUILTY_NO = p_BUILTY_NO;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TIME_STAMP = DateTime.Now;
                    mPurchaseDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return mPurchaseMaster.PURCHASE_ORDER_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return -2;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseOrderDetail2(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL2 mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL2();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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

        public DataTable SelectPurchaseOrderDetail2(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, DateTime p_Document_date)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL2 mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL2();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.Document_date = p_Document_date;
                DataTable dt = mPurchaseDetail.ExecuteTable();
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

        #region Insert / Update Fuel Purchase


        public long InsertFuelPurchaseDocument(int p_Distributor_ID,int p_principal_ID, int p_Fuel_TYPE_ID, decimal p_Quentity, decimal p_Price, decimal p_TOTAL_AMOUNT, DateTime p_DOCUMENT_DATE ,int p_UserId,int p_Credit_To, int p_Credit_From)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertFUEL_PURCHASE mPurchaseMaster = new spInsertFUEL_PURCHASE();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.FUEL_TYPE = p_Fuel_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_principal_ID;
                mPurchaseMaster.QUANTITY = p_Quentity;
                mPurchaseMaster.PRICE = p_Price;
                mPurchaseMaster.AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster .CREDIT_FROM =p_Credit_From;
                mPurchaseMaster .CREDIT_TO =p_Credit_To;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.USER_ID = p_UserId ;
               
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster .IS_DELETED =false;
              long x =  mPurchaseMaster.ExecuteQuery();

                
                mTransaction.Commit();
                return x;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return Constants .LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
       
        public bool UpdateFuelPurchaseDocument(int p_Distributor_ID, int p_Principal_ID ,long p_Fuel_purchase_ID ,int p_Fuel_TYPE_ID, decimal p_Quentity, decimal p_Price, decimal p_TOTAL_AMOUNT, DateTime p_DOCUMENT_DATE, int p_UserId, int p_Credit_To, int p_Credit_From)
        {

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateFUEL_PURCHASE mPurchaseMaster = new spUpdateFUEL_PURCHASE();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.FUEL_TYPE = p_Fuel_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_ID;
                mPurchaseMaster.QUANTITY = p_Quentity;
                mPurchaseMaster.PRICE = p_Price;
                mPurchaseMaster.AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.CREDIT_FROM = p_Credit_From;
                mPurchaseMaster.CREDIT_TO = p_Credit_To;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.FUEL_PURCHASE_ID = p_Fuel_purchase_ID;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.IS_DELETED = false;
                mPurchaseMaster.ExecuteQuery();


                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SelectFuelPurchaseDocumentNo(int p_Distributor_id, int p_principal_ID, int p_TYPE_ID, long p_User_Id, DateTime p_Date, long p_Document_No)
        {

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectFUEL_PURCHASE mPurchaseMaster = new spSelectFUEL_PURCHASE();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.FUEL_TYPE = p_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_id;
                mPurchaseMaster.PRINCIPAL_ID = p_principal_ID;
                mPurchaseMaster.FUEL_PURCHASE_ID = p_Document_No;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.DOCUMENT_DATE = p_Date;
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
        public DataTable SelectFuelPurchaseDetail(int p_Distributor_id, int p_Principal_id,int p_TYPE_ID, long p_User_Id, DateTime p_Date, long p_Document_No)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectFUEL_PURCHASE_DETAIL mPurchaseMaster = new spSelectFUEL_PURCHASE_DETAIL();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.FUEL_TYPE = p_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_id;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_id;
                mPurchaseMaster.FUEL_PURCHASE_ID = p_Document_No;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.DOCUMENT_DATE = p_Date;
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
        public long InsertVehicleMaintenance(int p_Distributor_ID, int p_Principal_ID, int p_Fuel_TYPE_ID, decimal p_TOTAL_AMOUNT, DateTime p_DOCUMENT_DATE, int p_UserId, int p_Credit_To, int p_Credit_From, long p_VehicleID, long p_Sale_Person, string p_Vehicle_Reading, string p_Remarks, int p_Driver_ID, int p_Loader_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
               
                spInsertVEHICLE_MAINTENANCE mPurchaseMaster = new spInsertVEHICLE_MAINTENANCE();
                mPurchaseMaster.Connection = mConnection;
               
                mPurchaseMaster.MAINTENANCE_TYPE  = p_Fuel_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_ID;
                mPurchaseMaster.VEHICLE_ID = p_VehicleID;
                mPurchaseMaster.VEHICLE_READING = p_Vehicle_Reading;
                mPurchaseMaster.SALE_PERSON_ID = p_Sale_Person;
                mPurchaseMaster.AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.REMARKS = p_Remarks;
                mPurchaseMaster.CREDIT_FROM = p_Credit_From;
                mPurchaseMaster.CREDIT_TO = p_Credit_To;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.IS_DELETED = false;
                mPurchaseMaster.DRIVER_ID = p_Driver_ID;
                mPurchaseMaster.LOADER_ID = p_Loader_ID;
               long Vehicle_Maintenance_ID= mPurchaseMaster.ExecuteQuery();


               return Vehicle_Maintenance_ID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
              
                return Constants .LongNullValue ;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateVehicleMaintenance(int p_Distributor_Id, int p_Principal_ID, long p_Vehicle_maintenance_Id ,int p_Fuel_TYPE_ID, decimal p_TOTAL_AMOUNT, DateTime p_DOCUMENT_DATE, int p_UserId, int p_Credit_To, int p_Credit_From, long p_VehicleID, long p_Sale_Person, string p_Vehicle_Reading, string p_Remarks, bool Is_deleated, int p_Driver_ID, int p_Loader_ID)
        {

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                
                spUpdateVEHICLE_MAINTENANCE mPurchaseMaster = new spUpdateVEHICLE_MAINTENANCE();
                mPurchaseMaster.Connection = mConnection;
               
                mPurchaseMaster.VEHICLE_MAINTENANCE_ID = p_Vehicle_maintenance_Id;
                mPurchaseMaster.MAINTENANCE_TYPE = p_Fuel_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_ID;
                mPurchaseMaster.VEHICLE_ID = p_VehicleID;
                mPurchaseMaster.VEHICLE_READING = p_Vehicle_Reading;
                mPurchaseMaster.SALE_PERSON_ID = p_Sale_Person;
                mPurchaseMaster.AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.REMARKS = p_Remarks;
                mPurchaseMaster.CREDIT_FROM = p_Credit_From;
                mPurchaseMaster.CREDIT_TO = p_Credit_To;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.IS_DELETED = Is_deleated;
                mPurchaseMaster.DRIVER_ID = p_Driver_ID;
                mPurchaseMaster.LOADER_ID = p_Loader_ID;
                mPurchaseMaster.ExecuteQuery();


                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
              
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
       
        
        
        public DataTable SelectVehicleMaintenance(int p_distributor_id, int p_principal_ID, int p_TYPE_ID, long p_User_Id, DateTime p_Date, long p_Document_No)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectVEHICLE_MAINTENANCE mPurchaseMaster = new spSelectVEHICLE_MAINTENANCE();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.MAINTENANCE_TYPE = p_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_distributor_id;
                mPurchaseMaster.PRINCIPAL_ID = p_principal_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.DOCUMENT_DATE = p_Date;
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


        public DataTable Selectfuelissuance(int p_distributor_ID, int p_Principal_ID, int p_TYPE_ID, long p_User_Id, DateTime p_Date, long p_Document_No)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectFUEL_ISSUANCE mPurchaseMaster = new spSelectFUEL_ISSUANCE();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.FUEL_TYPE = p_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_distributor_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.DOCUMENT_DATE = p_Date;
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










        public long InsertFuelIssuanceDocument(int p_Distributor_ID, int p_Principal_ID, int p_Fuel_TYPE_ID,long p_Vehicle_Id,long p_Sale_person_Id, string p_Vehicle_reading, decimal p_Price,  decimal p_Quantity, decimal p_TOTAL_AMOUNT, DateTime p_DOCUMENT_DATE, int p_UserId, int p_Credit_To, int p_Credit_From)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                
                spInsertFUEL_ISSUANCE mPurchaseMaster = new spInsertFUEL_ISSUANCE();
                mPurchaseMaster.Connection = mConnection;
                
                mPurchaseMaster.FUEL_TYPE = p_Fuel_TYPE_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_ID;
                mPurchaseMaster.QUANTITY = p_Quantity;
                mPurchaseMaster.PRICE = p_Price;
                mPurchaseMaster.AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.CREDIT_FROM = p_Credit_From;
                mPurchaseMaster.CREDIT_TO = p_Credit_To;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.VEHICLE_READING = p_Vehicle_reading;
                mPurchaseMaster.VEHICLE_ID = p_Vehicle_Id;
                mPurchaseMaster.SALE_PERSON_ID = p_Sale_person_Id;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.IS_DELETED = false;
               long P_Fuel_IssuanceID =mPurchaseMaster.ExecuteQuery();

                return P_Fuel_IssuanceID;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
            
                return Constants .LongNullValue;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateFuelIssuanceDocument(int p_Distributor_ID, int p_Principal_ID, long p_Fuel_Issuance_ID, int p_Fuel_TYPE_ID, long p_Vehicle_Id, long p_Sale_person_Id, string p_Vehicle_reading, decimal p_Price, decimal p_Quantity, decimal p_TOTAL_AMOUNT, DateTime p_DOCUMENT_DATE, int p_UserId, int p_Credit_To, int p_Credit_From, bool p_Is_deleted)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
               
                spUpdateFUEL_ISSUANCE mPurchaseMaster = new spUpdateFUEL_ISSUANCE();
                mPurchaseMaster.Connection = mConnection;
               
                mPurchaseMaster.FUEL_TYPE = p_Fuel_TYPE_ID;
                mPurchaseMaster.FUEL_ISSUANCE_ID = p_Fuel_Issuance_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_Principal_ID;
                mPurchaseMaster.QUANTITY = p_Quantity;
                mPurchaseMaster.PRICE = p_Price;
                mPurchaseMaster.AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.CREDIT_FROM = p_Credit_From;
                mPurchaseMaster.CREDIT_TO = p_Credit_To;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.FUEL_ISSUANCE_ID  = p_Fuel_Issuance_ID;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.IS_DELETED = p_Is_deleted;
                mPurchaseMaster.VEHICLE_READING = p_Vehicle_reading;
                mPurchaseMaster.VEHICLE_ID = p_Vehicle_Id;
                mPurchaseMaster.SALE_PERSON_ID = p_Sale_person_Id;
                mPurchaseMaster.ExecuteQuery();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
               
                return false;
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
        
        #endregion
    }
}
