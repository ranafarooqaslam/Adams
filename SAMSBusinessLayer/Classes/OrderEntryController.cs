using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDatabaseLayer.Classes;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using SAMSDataAccessLayer.Classes;
using CORNDatabaseLayer.Classes;
using CORNDataAccessLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Order/Invoice/Sale Return Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Order/Invoice/Sale Return
    /// </item>
    /// <term>
    /// Update Order/Invoice/Sale Return
    /// </term>
    /// <item>
    /// Get Order/Invoice/Sale Return
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class OrderEntryController
    {
        #region Constructor

        /// <summary>
        /// Constructor for OrderEntryController
        /// </summary>
        public OrderEntryController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
        #endregion

        #region Select

        public DataTable GetPromotion(int p_Distributor_Id, DateTime p_Working_Date, int CUSTOMER_PROMOTION_CLASS_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetPromotionDetail mPromotionDetail = new uspGetPromotionDetail();
                mPromotionDetail.Connection = mConnection;
                mPromotionDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mPromotionDetail.WORKING_DATE = p_Working_Date;
                mPromotionDetail.CUSTOMER_PROMOTION_CLASS_ID = CUSTOMER_PROMOTION_CLASS_ID;
                DataTable dt = mPromotionDetail.ExecuteTable();
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
        public static string GetMaxInvoiceId(int Distributor_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectMaxInvoiceNODist mInvoiceDetail = new uspSelectMaxInvoiceNODist();

                mInvoiceDetail.Connection = mConnection;
                mInvoiceDetail.DISTRIBUTOR_ID = Distributor_id;
                string MaxId;
                return MaxId = mInvoiceDetail.ExecuteScalar();

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
        public DataTable SelectDamageReplacmentDetail(int p_Distributor_Id, long p_SaleOrder_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDemageReplacmentDetail mOrderDetail = new spSelectDemageReplacmentDetail();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrderDetail.SALE_ORDER_ID = p_SaleOrder_Id;
                mOrderDetail.IS_DELETED = false;
                DataTable dt = mOrderDetail.ExecuteTable();
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
        public DataTable SelectDamageReplacment(int p_Distributor_Id, int p_Area_Id, int p_Principal_Id, int p_Order_Booker, int p_DeliveryMan_Id, int p_OrderStatus, int p_Ordertype, int p_UserId, DateTime p_DOCUMENT_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectDamageAndReplacment mOrder = new UspSelectDamageAndReplacment();
                mOrder.Connection = mConnection;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.AREA_ID = p_Area_Id;
                mOrder.ORDERBOOKER_ID = p_Order_Booker;
                mOrder.DELIVERYMAN_ID = p_DeliveryMan_Id;
                mOrder.USER_ID = p_UserId;
                mOrder.PRINCIPAL_ID = p_Principal_Id;
                mOrder.STATUS_ID = p_OrderStatus;
                mOrder.ORDER_TYPE_ID = p_Ordertype;
                mOrder.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mOrder.ExecuteTable();
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
        /// Gets Promotions
        /// </summary>
        /// <remarks>
        /// Returns Promotions as PromotionCollections_Controller
        /// </remarks>
        /// <param name="p_DistId">Location</param>
        /// <param name="Princpal_Id">Principal</param>
        /// <param name="pCurrentDate">Date</param>
        /// <returns>Promotions as PromotionCollections_Controller</returns>
        public PromotionCollections_Controller LoadSchemes(int p_DistId, int Princpal_Id, DateTime pCurrentDate)
        {
            IDbConnection m_Connection = null;
            DataControl dc = new DataControl();
            PromotionCollections_Controller pcc = new PromotionCollections_Controller();
            DataTable dt = null, dt2 = null, dt3 = null, dt4 = null, dt5 = null;

            try
            {

                m_Connection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                m_Connection.Open();

                #region Load Scheme Promotion c0llection

                uspSelectPROMOTIONS mSP = new uspSelectPROMOTIONS();
                mSP.Connection = m_Connection;
                mSP.DISTRIBUTOR_ID = p_DistId;
                mSP.PROMOTION_TYPE = Princpal_Id;
                mSP.IS_ACTIVE = true;
                mSP.START_DATE = DateTime.Parse(pCurrentDate.ToShortDateString() + " 00:00:00");
                mSP.END_DATE = DateTime.Parse(pCurrentDate.ToShortDateString() + " 23:59:59");//System.DateTime.Now ;
                dt2 = mSP.ExecuteTable();

                for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                {	// this loop will get all active promotions against the scheme id and distributor id
                    Promotion_Collection pc = new Promotion_Collection();
                    pc.ObjBasketCol_Cntrl = new BasketCollection_Controller();
                    pc.ObjPromotionCustTypeCol_Cntrl = new PromotionCustTypeColl_Controller();
                    pc.ObjPromotionForCol_Cntrl = new PromotionForCollection_Controller();
                    pc.ObjPromotionVolClassCol_Cntrl = new PromotionCustVolclassColl_Controller();

                    pc.Dist_ID = int.Parse(dt2.Rows[j]["Distributor_ID"].ToString());
                    pc.Promotion_Code = dt2.Rows[j]["PROMOTION_CODE"].ToString();
                    pc.Promotion_Date = DateTime.Parse(dt2.Rows[j]["Promo_Date"].ToString());
                    pc.Promotion_Desc = dt2.Rows[j]["Promotion_Description"].ToString();
                    pc.Promotion_ID = long.Parse(dt2.Rows[j]["Promotion_ID"].ToString());
                    if (dt2.Rows[j]["Promotion_Selection"].ToString() != "")
                    { pc.Promotion_Selection = int.Parse(dt2.Rows[j]["Promotion_Selection"].ToString()); }
                    else
                    { pc.Promotion_Selection = -1; }
                    if (dt2.Rows[j]["Promotion_Type"].ToString() != "")
                    { pc.Promotion_Type = int.Parse(dt2.Rows[j]["Promotion_Type"].ToString()); }
                    else
                    { pc.Promotion_Type = -1; }
                    pc.Scheme_ID = int.Parse(dt2.Rows[j]["Scheme_ID"].ToString());
                    pc.Start_Date = DateTime.Parse(dt2.Rows[j]["Start_Date"].ToString());
                    pc.End_Date = DateTime.Parse(dt2.Rows[j]["End_Date"].ToString());
                    pc.Claimable = bool.Parse(dt2.Rows[j]["Claimable"].ToString());
                    pc.Is_Scheme = bool.Parse(dt2.Rows[j]["IS_SCHEME"].ToString());
                    // this loop will get basket data against the scheme id,PromotionID and distributor id

                    #region Basket Collection

                    uspSelectBASKET_MASTER_dt mBM = new uspSelectBASKET_MASTER_dt();
                    mBM.Connection = m_Connection;
                    mBM.DISTRIBUTOR_ID = pc.Dist_ID; //Configuration.DistributorId ;//System .Convert .ToInt32 (sc.Dist_ID) ;
                    mBM.SCHEME_ID = System.Convert.ToInt32(pc.Scheme_ID);
                    mBM.PROMOTION_ID = System.Convert.ToInt32(pc.Promotion_ID);
                    dt3 = mBM.ExecuteTable();

                    for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                    {
                        Basket_Collection bc = new Basket_Collection();
                        bc.ObjBasketDtlCol_Cntrlr = new BasketDetailCollection_Controller();


                        bc.Basket_ID = long.Parse(dt3.Rows[k]["Basket_ID"].ToString());
                        bc.Basket_On = int.Parse(dt3.Rows[k]["Basket_On"].ToString());
                        if (dt3.Rows[k]["Basket_Selection"].ToString() != "")
                        { bc.Basket_Selection = int.Parse(dt3.Rows[k]["Basket_Selection"].ToString()); }
                        else
                        { bc.Basket_Selection = 0; }
                        bc.Dist_ID = int.Parse(dt3.Rows[k]["Distributor_ID"].ToString());
                        bc.Is_And = bool.Parse(dt3.Rows[k]["IS_AND"].ToString());
                        bc.Is_Basket = bool.Parse(dt3.Rows[k]["IS_Basket"].ToString());
                        bc.Is_Multiple = bool.Parse(dt3.Rows[k]["IS_Multiple"].ToString());
                        bc.Promotion_ID = long.Parse(dt3.Rows[k]["Promotion_ID"].ToString());
                        bc.Scheme_ID = int.Parse(dt2.Rows[j]["Scheme_ID"].ToString());
                        // this loop will get basketDetail data against the BasketID, scheme id,PromotionID and distributor id

                        #region Basket Detail
                        uspSelectBASKET_DETAIL_dt mBD = new uspSelectBASKET_DETAIL_dt();
                        mBD.Connection = m_Connection;
                        mBD.BASKET_ID = System.Convert.ToInt32(bc.Basket_ID);
                        mBD.DISTRIBUTOR_ID = bc.Dist_ID; ///Configuration.DistributorId ;//System .Convert .ToInt32 (sc.Dist_ID) ;
                        mBD.SCHEME_ID = System.Convert.ToInt32(pc.Scheme_ID);
                        mBD.PROMOTION_ID = System.Convert.ToInt32(pc.Promotion_ID);
                        dt4 = mBD.ExecuteTable();

                        for (int l = 0; l <= dt4.Rows.Count - 1; l++)
                        {
                            Basket_Detail_Collection bdc = new Basket_Detail_Collection();
                            bdc.ObjPromotionOfferCol_Cntrl = new PromotionOfferColl_Controller();

                            bdc.Basket_ID = long.Parse(dt4.Rows[l]["Basket_ID"].ToString());
                            bdc.BasketDetail_ID = long.Parse(dt4.Rows[l]["Basket_Detail_ID"].ToString());
                            bdc.Dist_ID = int.Parse(dt4.Rows[l]["Distributor_ID"].ToString());
                            bdc.Max_Val = decimal.Parse(dt4.Rows[l]["Max_Val"].ToString());
                            bdc.Min_Val = decimal.Parse(dt4.Rows[l]["Min_Val"].ToString());
                            bdc.Multiple_Of = int.Parse(dc.chkNull(dt4.Rows[l]["Multiple_of"].ToString()));
                            bdc.Promotion_ID = long.Parse(dt4.Rows[l]["Promotion_ID"].ToString());
                            bdc.Scheme_ID = int.Parse(dt4.Rows[l]["Scheme_ID"].ToString());
                            bdc.SKU_ID = int.Parse(dc.chkNull(dt4.Rows[l]["SKU_ID"].ToString()));
                            bdc.SKUBrand_ID = int.Parse(dc.chkNull(dt4.Rows[l]["Brand_ID"].ToString()));
                            bdc.SKUCatg_ID = int.Parse(dc.chkNull(dt4.Rows[l]["Category_ID"].ToString()));
                            bdc.SKUDiv_ID = int.Parse(dc.chkNull(dt4.Rows[l]["Division_ID"].ToString()));
                            bdc.SKUGroup_ID = int.Parse(dc.chkNull(dt4.Rows[l]["SKU_Group_ID"].ToString()));
                            bdc.SKUProductLine_ID = int.Parse(dc.chkNull(dt4.Rows[l]["Variant_ID"].ToString()));
                            bdc.UOM_ID = int.Parse(dc.chkNull(dt4.Rows[l]["UOM_ID"].ToString()));
                            bdc.SKUCompany_ID = int.Parse(dc.chkNull(dt4.Rows[l]["Company_ID"].ToString()));
                            bc.ObjBasketDtlCol_Cntrlr.Add(bdc);

                            #region Basket Promotion Offer Collection
                            /////////////////////////////////////////////
                            ///
                            // This loop will get Promotion Offer data against the BasketID, scheme id,
                            // PromotionID,basketDetail ID and distributor id
                            uspSelectPROMOTION_OFFER_dt mPO = new uspSelectPROMOTION_OFFER_dt();

                            mPO.Connection = m_Connection;
                            mPO.BASKET_ID = System.Convert.ToInt32(bc.Basket_ID);
                            mPO.DISTRIBUTOR_ID = bc.Dist_ID;//System .Convert .ToInt32 (sc.Dist_ID) ;
                            mPO.SCHEME_ID = System.Convert.ToInt32(pc.Scheme_ID);
                            mPO.PROMOTION_ID = System.Convert.ToInt32(pc.Promotion_ID);
                            mPO.BASKET_DETAIL_ID = System.Convert.ToInt32(bdc.BasketDetail_ID);
                            dt5 = null;		// no need to keep old values in dt4
                            dt5 = mPO.ExecuteTable();
                            for (int m = 0; m <= dt5.Rows.Count - 1; m++)
                            {
                                #region to avoid null values
                                string mDiscount = dt5.Rows[m]["Discount"].ToString();
                                string mOfferValue = dt5.Rows[m]["Offer_Value"].ToString();
                                string mQty = dt5.Rows[m]["Quantity"].ToString();
                                if ((mDiscount == "") || (mDiscount == null))
                                { mDiscount = "0"; }
                                if ((mOfferValue == "") || (mOfferValue == null))
                                { mOfferValue = "0"; }
                                if ((mQty == "") || (mQty == null))
                                { mQty = "0"; }
                                #endregion

                                PromotionOffer_Collection poc = new PromotionOffer_Collection();

                                poc.Basket_ID = long.Parse(dt5.Rows[m]["Basket_ID"].ToString());
                                poc.BasketDetail_ID = long.Parse(dt5.Rows[m]["Basket_Detail_ID"].ToString());
                                poc.Discount = float.Parse(mDiscount);
                                poc.Dist_ID = int.Parse(dt5.Rows[m]["Distributor_ID"].ToString());
                                poc.Is_And = bool.Parse(dt5.Rows[m]["Is_And"].ToString());
                                poc.Offer_Value = decimal.Parse(mOfferValue);
                                poc.Promotion_ID = long.Parse(dt5.Rows[m]["Promotion_ID"].ToString());
                                poc.Promotion_Offer_ID = long.Parse(dt5.Rows[m]["Promotion_Offer_ID"].ToString());
                                poc.Quantity = int.Parse(mQty);
                                poc.Scheme_ID = int.Parse(dt5.Rows[m]["Scheme_ID"].ToString());
                                poc.SKU_ID = int.Parse(dc.chkNull(dt5.Rows[m]["SKU_ID"].ToString()));
                                poc.UOM_ID = int.Parse(dc.chkNull(dt5.Rows[m]["UOM_ID"].ToString()));

                                bdc.ObjPromotionOfferCol_Cntrl.Add(poc);
                            }
                            #endregion

                        }
                        #endregion
                        pc.ObjBasketCol_Cntrl.Add(bc);
                    }
                    #endregion
                    ////////////////////////////////////////	
                    // this loop will get Promotion_For data against the scheme id,PromotionID and distributor id					
                    #region Promotion_For Collection
                    dt3 = null;
                    uspSelectPROMOTION_FOR_dt mPF = new uspSelectPROMOTION_FOR_dt();
                    mPF.Connection = m_Connection;
                    mPF.PROMOTION_FOR_ID = Constants.LongNullValue;
                    mPF.DISTRIBUTOR_ID = pc.Dist_ID;//System .Convert .ToInt32 (sc.Dist_ID) ;
                    mPF.ASSIGNED_DISTRIBUTOR_ID = Configuration.DistributorId;
                    mPF.SCHEME_ID = System.Convert.ToInt32(pc.Scheme_ID);
                    mPF.PROMOTION_ID = System.Convert.ToInt32(pc.Promotion_ID);
                    dt3 = mPF.ExecuteTable();

                    for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                    {
                        PromotionFor_Collection pfc = new PromotionFor_Collection();
                        pfc.Dist_ID = int.Parse(dt3.Rows[k]["Distributor_ID"].ToString());
                        pfc.Assigned_Dist_ID = int.Parse(dt3.Rows[k]["Assigned_Distributor_ID"].ToString());
                        pfc.Promotion_For_ID = long.Parse(dt3.Rows[k]["Promotion_For_ID"].ToString());
                        pfc.Promotion_ID = long.Parse(dt3.Rows[k]["Promotion_ID"].ToString());
                        pfc.Scheme_ID = int.Parse(dt3.Rows[k]["Scheme_ID"].ToString());
                        pc.ObjPromotionForCol_Cntrl.Add(pfc);
                    }

                    #endregion
                    ////////////////////////////////////////
                    ///// this loop will get Promotion_For_Customer_VolClass data against the scheme id,PromotionID and distributor id					
                    #region Promotion_For_VOLUMECLASS Collection
                    dt3 = null;
                    spSelectPROMOTION_CUSTOMER_VOLUMECLASS mPFC = new spSelectPROMOTION_CUSTOMER_VOLUMECLASS();
                    mPFC.Connection = m_Connection;
                    mPFC.DISTRIBUTOR_ID = pc.Dist_ID;
                    mPFC.SCHEME_ID = System.Convert.ToInt32(pc.Scheme_ID);
                    mPFC.PROMOTION_ID = System.Convert.ToInt32(pc.Promotion_ID);
                    dt3 = mPFC.ExecuteTable();

                    for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                    {
                        PromotionCustomerVolClass_Collection pfcc = new PromotionCustomerVolClass_Collection();
                        pfcc.Dist_ID = int.Parse(dt3.Rows[k]["Distributor_ID"].ToString());
                        pfcc.Promotion_ID = long.Parse(dt3.Rows[k]["Promotion_ID"].ToString());
                        pfcc.Scheme_ID = int.Parse(dt3.Rows[k]["Scheme_ID"].ToString());
                        pfcc.Customer_VolClass_ID = int.Parse(dt3.Rows[k]["CUSTOMER_VOLUMECLASS_ID"].ToString());
                        pc.ObjPromotionVolClassCol_Cntrl.Add(pfcc);
                    }

                    #endregion
                    ////////////////////////////////////////
                    //////// this loop will get Promotion_Customer_type data against the scheme id,PromotionID and distributor id					
                    #region Promotion_Customer_Type Collection
                    dt3 = null;
                    uspSelectPROMOTION_CUSTOMER_TYPE_dt mPCT = new uspSelectPROMOTION_CUSTOMER_TYPE_dt();
                    mPCT.Connection = m_Connection;
                    mPCT.DISTRIBUTOR_ID = pc.Dist_ID; // Configuration.DistributorId ;//System .Convert .ToInt32 (sc.Dist_ID) ;
                    mPCT.SCHEME_ID = System.Convert.ToInt32(pc.Scheme_ID);
                    mPCT.PROMOTION_ID = System.Convert.ToInt32(pc.Promotion_ID);
                    dt3 = mPCT.ExecuteTable();

                    for (int k = 0; k <= dt3.Rows.Count - 1; k++)
                    {
                        PromotionCustomerType_Collection pctc = new PromotionCustomerType_Collection();
                        pctc.Dist_ID = int.Parse(dt3.Rows[k]["Distributor_ID"].ToString());
                        pctc.Customer_Type_ID = int.Parse(dt3.Rows[k]["Customer_Type_ID"].ToString());
                        pctc.Promotion_Cust_Type_ID = long.Parse(dt3.Rows[k]["Promotion_Customer_Type_ID"].ToString());
                        pctc.Promotion_ID = long.Parse(dt3.Rows[k]["Promotion_ID"].ToString());
                        pctc.Scheme_ID = int.Parse(dt3.Rows[k]["Scheme_ID"].ToString());
                        pc.ObjPromotionCustTypeCol_Cntrl.Add(pctc);
                    }

                    #endregion
                    ////////////////////////////////////////
                    pcc.Add_PCol(pc);

                }
                #endregion

                return pcc;


            }
            catch (Exception ex)
            {
                ExceptionPublisher.PublishException(ex);
                return null;
            }
            finally
            {
                if (m_Connection != null && m_Connection.State != ConnectionState.Open)
                {
                    m_Connection.Close();
                }
            }
        }
        
        /// <summary>
        /// Gets Promotion Offers
        /// </summary>
        /// <remarks>
        /// Returns Promotion Offers as ArrayList
        /// </remarks>
        /// <param name="pc">PromotionCollections_Controller</param>
        /// <param name="p_CustomerVoldClass">PromotionClass</param>
        /// <param name="p_CustomerId">Customer</param>
        /// <param name="p_OrderDetail">OrderDetailDatatable</param>
        /// <param name="p_IsScheme">IsScheme</param>
        /// <returns>Promotion Offers as ArrayList</returns>
        public ArrayList GetPromotionOffers(PromotionCollections_Controller pc, int p_CustomerVoldClass, int p_CustomerId, DataTable p_OrderDetail, bool p_IsScheme)
        {

            int PromotionIdx = 0;
            DataRow drOrderDetail = null;

            PromoOffersCol_Controller POffersCol_Cntrlr = new PromoOffersCol_Controller();
            SKUGroupController GroupCtl = new SKUGroupController();

            ArrayList arrPromotionOffers = new ArrayList();



            while (PromotionIdx < pc.Count)
            {


                bool IsValidCustomerTypeId = false;
                bool IsValidCustomerVolCla = false;

                DataTable dtGroup = new DataTable();
                dtGroup.Columns.Add("GroupId", typeof(long));

                for (int j = 0; j < pc.Get_PCol(PromotionIdx).ObjPromotionCustTypeCol_Cntrl.Count; j++)
                {
                    if (pc.Get_PCol(PromotionIdx).ObjPromotionCustTypeCol_Cntrl.Get(j).Customer_Type_ID == p_CustomerId)
                    {
                        IsValidCustomerTypeId = true;
                        break;
                    }
                }

                for (int j = 0; j < pc.Get_PCol(PromotionIdx).ObjPromotionVolClassCol_Cntrl.Count; j++)
                {
                    if (pc.Get_PCol(PromotionIdx).ObjPromotionVolClassCol_Cntrl.Get(j).Customer_VolClass_ID == p_CustomerVoldClass)
                    {
                        IsValidCustomerVolCla = true;
                        break;
                    }
                }
                if (IsValidCustomerTypeId == true && IsValidCustomerVolCla == true)
                {

                    for (int idxOrderDetail = 0; idxOrderDetail < p_OrderDetail.Rows.Count; idxOrderDetail++)
                    {


                        drOrderDetail = p_OrderDetail.Rows[idxOrderDetail];


                        #region Slab

                       BasketCollection_Controller ObjBasket = pc.Get_PCol(PromotionIdx).ObjBasketCol_Cntrl;

                        for (int i = 0; i < ObjBasket.Count; i++)
                        {
                            bool Applygroup = false;
                            decimal dValueToCompare = 0;
                            int dMultipalGroupItem = 0;

                            BasketDetailCollection_Controller objBasketDetail = ObjBasket.Get(i).ObjBasketDtlCol_Cntrlr;

                            for (int j = 0; j < objBasketDetail.Count; j++)
                            {

                                if (objBasketDetail.Get(j).SKU_ID > 0)
                                {
                                    #region Single SKU
                                    //If SLAB applied at single SKU

                                    if (objBasketDetail.Get(j).SKU_ID == int.Parse(drOrderDetail["SKU_ID"].ToString()))
                                    {
                                        //Check at Amount or Quantity SLAB is Applied
                                        if (ObjBasket.Get(i).Basket_On == Constants.Basket_On_Amount)
                                        {
                                            dValueToCompare = decimal.Parse(drOrderDetail["Amount"].ToString());
                                        }
                                        else if (ObjBasket.Get(i).Basket_On == Constants.Basket_On_Quantity)
                                        {
                                            dValueToCompare = decimal.Parse(drOrderDetail["QUANTITY_UNIT"].ToString());
                                        }

                                        //Check if SLAB is applicable

                                        if (dValueToCompare >= objBasketDetail.Get(j).Min_Val && (dValueToCompare <= objBasketDetail.Get(j).Max_Val || objBasketDetail.Get(j).Max_Val == 0))
                                        {

                                            //Add applied Promotion offer in array										

                                            PromotionOfferColl_Controller objPromotionOffer = objBasketDetail.Get(j).ObjPromotionOfferCol_Cntrl;
                                            PromoOffers_Collection AppProCol = new PromoOffers_Collection();

                                            if (dValueToCompare > objBasketDetail.Get(j).Multiple_Of && objBasketDetail.Get(j).Multiple_Of > 0)
                                            {
                                                int iMultiply = Convert.ToInt32(Math.Floor(Convert.ToDouble(dValueToCompare / objBasketDetail.Get(j).Multiple_Of)));
                                                AppProCol.Quantity = objPromotionOffer.Get(j).Quantity * iMultiply;
                                                AppProCol.Offer_Value = objPromotionOffer.Get(j).Offer_Value * iMultiply;

                                            }
                                            else
                                            {
                                                AppProCol.Quantity = objPromotionOffer.Get(j).Quantity;
                                                AppProCol.Offer_Value = objPromotionOffer.Get(j).Offer_Value;

                                            }

                                            AppProCol.SKU_ID = int.Parse(drOrderDetail["SKU_ID"].ToString());
                                            AppProCol.Group_ID = Constants.IntNullValue;
                                            AppProCol.Promotion_ID = int.Parse(objPromotionOffer.Get(j).Promotion_ID.ToString());
                                            AppProCol.Scheme_ID = objPromotionOffer.Get(j).Scheme_ID;
                                            AppProCol.Basket_ID = objPromotionOffer.Get(j).Basket_ID;
                                            AppProCol.BasketDetail_ID = objPromotionOffer.Get(j).BasketDetail_ID;
                                            AppProCol.Free_SKU_ID = objPromotionOffer.Get(j).SKU_ID;
                                            AppProCol.Discount = objPromotionOffer.Get(j).Discount;
                                            AppProCol.Is_And = pc.Get_PCol(PromotionIdx).Is_Scheme;
                                            AppProCol.Is_Claimable = pc.Get_PCol(PromotionIdx).Claimable;

                                            arrPromotionOffers.Add(AppProCol);

                                        }


                                    }
                                    #endregion
                                }
                                else if (objBasketDetail.Get(j).SKUGroup_ID > 0)
                                {
                                    SKUGroupController mGroup = new SKUGroupController();

                                    //check if Already Apply Group then return 

                                    foreach (DataRow drGroup in dtGroup.Rows)
                                    {
                                        if (drGroup[0].ToString() == objBasketDetail.Get(j).SKUGroup_ID.ToString())
                                        {
                                            Applygroup = true;
                                        }
                                    }
                                        #region Group
                                    if (Applygroup == false)
                                    {
                                        dValueToCompare = 0;
                                        dMultipalGroupItem = 0;

                                        if (ObjBasket.Get(i).Basket_On == Constants.Basket_On_Amount)
                                        {
                                            foreach (DataRow dg in p_OrderDetail.Rows)
                                            {
                                                if (GroupCtl.ExistsInGroup(Constants.IntNullValue, objBasketDetail.Get(j).SKUGroup_ID, int.Parse(dg["SKU_ID"].ToString())))
                                                {
                                                    dValueToCompare += decimal.Parse(dg["AMOUNT"].ToString());
                                                    dMultipalGroupItem += 1;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (DataRow dg in p_OrderDetail.Rows)
                                            {
                                                if (GroupCtl.ExistsInGroup(Constants.IntNullValue, objBasketDetail.Get(j).SKUGroup_ID, int.Parse(dg["SKU_ID"].ToString())))
                                                {
                                                    dValueToCompare += decimal.Parse(dg["QUANTITY_UNIT"].ToString());
                                                    dMultipalGroupItem += 1;
                                                }
                                            }
                                        }

                                        if (dValueToCompare >= objBasketDetail.Get(j).Min_Val && (dValueToCompare <= objBasketDetail.Get(j).Max_Val || objBasketDetail.Get(j).Max_Val == 0))
                                        {

                                            //Add applied Promotion offer in array										

                                            PromotionOfferColl_Controller objPromotionOffer = objBasketDetail.Get(j).ObjPromotionOfferCol_Cntrl;
                                            PromoOffers_Collection AppProCol = new PromoOffers_Collection();

                                            if (dValueToCompare > objBasketDetail.Get(j).Multiple_Of && objBasketDetail.Get(j).Multiple_Of > 0)
                                            {
                                                int iMultiply = Convert.ToInt32(Math.Floor(Convert.ToDouble(dValueToCompare / objBasketDetail.Get(j).Multiple_Of)));
                                                AppProCol.Quantity = (objPromotionOffer.Get(j).Quantity * iMultiply);
                                                AppProCol.Offer_Value = (objPromotionOffer.Get(j).Offer_Value * iMultiply) / dMultipalGroupItem;

                                            }
                                            else
                                            {
                                                AppProCol.Quantity = objPromotionOffer.Get(j).Quantity;
                                                AppProCol.Offer_Value = objPromotionOffer.Get(j).Offer_Value / dMultipalGroupItem;

                                            }

                                            AppProCol.SKU_ID = objBasketDetail.Get(j).SKU_ID;
                                            AppProCol.Group_ID = objBasketDetail.Get(j).SKUGroup_ID;
                                            AppProCol.Promotion_ID = int.Parse(objPromotionOffer.Get(j).Promotion_ID.ToString());
                                            AppProCol.Scheme_ID = objPromotionOffer.Get(j).Scheme_ID;
                                            AppProCol.Basket_ID = objPromotionOffer.Get(j).Basket_ID;
                                            AppProCol.BasketDetail_ID = objPromotionOffer.Get(j).BasketDetail_ID;
                                            AppProCol.Free_SKU_ID = objPromotionOffer.Get(j).SKU_ID;
                                            AppProCol.Discount = objPromotionOffer.Get(j).Discount;
                                            AppProCol.Is_And = pc.Get_PCol(PromotionIdx).Is_Scheme;
                                            AppProCol.Is_Claimable = pc.Get_PCol(PromotionIdx).Claimable;
                                            arrPromotionOffers.Add(AppProCol);


                                            DataRow drNewGroup = dtGroup.NewRow();
                                            drNewGroup[0] = AppProCol.Group_ID.ToString();
                                            dtGroup.Rows.Add(drNewGroup);

                                        }
                                    #endregion
                                    }
                                }

                            }
                        }
                        #endregion
                    }
                }
                PromotionIdx++;
            }
            return arrPromotionOffers;
        }
        
        /// <summary>
        /// Gets Pending Orders
        /// </summary>
        /// <param name="p_Distributor_Id">Loation</param>
        /// <param name="p_Area_Id">Route</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_Order_Booker">OrderBooker</param>
        /// <param name="p_DeliveryMan_Id">DeliveryMan</param>
        /// <param name="p_OrderStatus">Status</param>
        /// <param name="p_Ordertype">Type</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <returns>Pending Orders as Datatable</returns>
        /// 
        public DataTable GetDocumentNo2(int p_Distributor_Id, int p_Area_Id, int p_Principal_Id, int p_DeliveryMan_Id, DateTime p_DOCUMENT_DATE, int p_USER_ID, int p_TYPE_ID, int p_OrderBookerId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetDocumentNo mDoc = new uspGetDocumentNo();
                mDoc.Connection = mConnection;

                mDoc.DISTRIBUTOR_ID = p_Distributor_Id;
                mDoc.AREA_ID = p_Area_Id;
                mDoc.DELIVERYMAN_ID = p_DeliveryMan_Id;
                mDoc.PRINCIPAL_ID = p_Principal_Id;

                mDoc.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mDoc.USER_ID = p_USER_ID;
                mDoc.TYPE_ID = p_TYPE_ID;
                mDoc.ORDERBOOKERID = p_OrderBookerId;
                DataTable dt = mDoc.ExecuteTable();
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
        
        public DataTable SelectPendingOrder(int p_Distributor_Id, int p_Area_Id, int p_Principal_Id, int p_Order_Booker, int p_DeliveryMan_Id, int p_OrderStatus, int p_Ordertype, int p_UserId, DateTime p_DOCUMENT_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingOrder mOrder = new UspSelectPendingOrder();
                mOrder.Connection = mConnection;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.AREA_ID = p_Area_Id;
                mOrder.ORDERBOOKER_ID = p_Order_Booker;
                mOrder.DELIVERYMAN_ID = p_DeliveryMan_Id;
                mOrder.USER_ID = p_UserId;
                mOrder.PRINCIPAL_ID = p_Principal_Id;
                mOrder.STATUS_ID = p_OrderStatus;
                mOrder.ORDER_TYPE_ID = p_Ordertype;
                mOrder.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mOrder.ExecuteTable();
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
        /// Gets Order Detail
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_SaleOrder_Id">Order</param>
        /// <returns>Order Detail as Datatable</returns>
        public DataTable SelectOrderDetail(int p_Distributor_Id, long p_SaleOrder_Id, int p_DistributorIdGRN)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSALE_ORDER_DETAIL mOrderDetail = new spSelectSALE_ORDER_DETAIL();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrderDetail.SALE_ORDER_ID = p_SaleOrder_Id;
                mOrderDetail.DISTRIBUTOR_ID_GRN = p_DistributorIdGRN;
                mOrderDetail.IS_DELETED = false;
              
                DataTable dt = mOrderDetail.ExecuteTable();
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
        /// Gets Promotions Of Order
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_SaleOrder_Id">Order</param>
        /// <returns>Promotion Of Order as Datatable</returns>
        public DataTable SelectOrderPromotion(int p_Distributor_Id, long p_SaleOrder_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSALE_ORDER_PROMOTION mOrderDetail = new spSelectSALE_ORDER_PROMOTION();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrderDetail.SALE_ORDER_ID = p_SaleOrder_Id;
                DataTable dt = mOrderDetail.ExecuteTable();
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
        /// Gets Promotions Of Invoice
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_SaleInvoice_Id">Invoice</param>
        /// <returns>Promotion Of Invoice as Datatable</returns>
        public DataTable SelectInvoicePromotion(int p_Distributor_Id, long p_SaleInvoice_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSALE_INVOICE_PROMOTION mOrderDetail = new spSelectSALE_INVOICE_PROMOTION();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrderDetail.SALE_INVOICE_ID = p_SaleInvoice_Id;
                DataTable dt = mOrderDetail.ExecuteTable();
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
        /// Gets Legend
        /// </summary>
        /// <returns>Legend as Datatable</returns>
        public DataTable SelectLegend()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLEGEND mOrderDetail = new spSelectLEGEND();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.LEGEND_ID = Constants.IntNullValue;
                mOrderDetail.LEGEND_TYPE_ID = Constants.IntNullValue;
                mOrderDetail.TIMESTAMP = Constants.DateNullValue;
                mOrderDetail.LAST_UPDATE_DATE = Constants.DateNullValue;
                mOrderDetail.LEGEND_DESCRIPTION = null;
                mOrderDetail.LEGEND_NAME = null;
                mOrderDetail.IS_ACTIVE = true;
                mOrderDetail.USER_ID = Constants.IntNullValue;
                DataTable dt = mOrderDetail.ExecuteTable();
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
        /// Gets Tranporter
        /// </summary>
        /// <returns>Transporter as Datatable</returns>
        public DataTable SelectTranspoter()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectTRANSPOTER mTranspoter = new spSelectTRANSPOTER();
                mTranspoter.Connection = mConnection;
                DataTable dt = mTranspoter.ExecuteTable();
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
        /// Gets Transporter Invoices
        /// </summary>
        /// <param name="p_Distributor_id">Location</param>
        /// <param name="p_CustomerId">Customer</param>
        /// <param name="p_type">Type</param>
        /// <returns>Transporter Invoices as Datatable</returns>
        public DataTable SelectTranspoterInvoice(int p_Distributor_id, long p_CustomerId, int p_type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectTranspoterInvoice mInvoice = new UspSelectTranspoterInvoice();
                mInvoice.Connection = mConnection;
                mInvoice.DISTRIBUTOR_ID = p_Distributor_id;
                mInvoice.CUSTOMER_ID = p_CustomerId;
                mInvoice.TypeId = p_type;
                DataTable dt = mInvoice.ExecuteTable();
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
        /// Converts Orders To Invoices
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_MANUAL_ORDER_ID">ManualInvocie</param>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_SaleOrder_Id">Order</param>
        /// <param name="p_Document_Date">Date</param>
        /// <param name="p_GrossSale">Sale</param>
        /// <param name="p_Discount">Discount</param>
        /// <param name="p_scheme">Scheme</param>
        /// <param name="p_GstAmt">GST</param>
        /// <param name="p_Net_Amount">NetAmount</param>
        /// <param name="p_OrderStatus">Status</param>
        /// <param name="p_OrderTypeId">Type</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PayeesName">Payee</param>
        /// <returns></returns>
        public DataTable ConvertOrder_to_Invoice(int p_Distributor_Id, string p_MANUAL_ORDER_ID, long p_Customer_Id, int p_Principal_Id,
            long p_SaleOrder_Id, DateTime p_Document_Date, decimal p_GrossSale, decimal p_Discount, decimal p_scheme,
            decimal p_GstAmt, decimal p_Net_Amount, int p_OrderStatus, int p_OrderTypeId, 
            int p_UserId, string p_PayeesName, int p_ChannelType_ID)
        {
            #region variables
            IDbTransaction mTransaction = null;
            IDbConnection mConnection = null;
            #endregion

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspConvertOrdertoInvoice mOrder = new UspConvertOrdertoInvoice();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                mOrder.Connection = mConnection;
                mOrder.Transaction = mTransaction;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.PRINCIPAL_ID = p_Principal_Id;
                mOrder.CUSTOMER_ID = p_Customer_Id;
                mOrder.DOCUMENT_DATE = p_Document_Date;
                mOrder.SALE_ORDER_ID = p_SaleOrder_Id;

                if (p_OrderTypeId == Constants.Credit_Order_Id)
                {
                    mOrder.NET_AMOUNT = p_Net_Amount;
                }
                else
                {
                    mOrder.NET_AMOUNT = 0;
                }
                mOrder.ORDER_STATUS = p_OrderStatus;
                DataTable dt = mOrder.ExecuteTable();
                if (dt.Columns.Count > 1)
                {
                    mTransaction.Rollback();
                    return dt;
                }

                #region Account Posting

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_Id);

                if (p_OrderTypeId == Constants.Advance_PaymentOrder_id)
                {
                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_Id, 0, p_Net_Amount, p_Document_Date, "Net Sale Value", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_PayeesName);
                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_Id, p_Net_Amount, 0, p_Document_Date, "Recevieable from Customer", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_PayeesName);

                }
                else if (p_OrderTypeId == Constants.Credit_Order_Id)
                {

                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_Id, p_Net_Amount, 0, p_Document_Date, "Credit Sale Default", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_PayeesName);
                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_Id, 0, p_Net_Amount, p_Document_Date, "Credit Sale Default", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_PayeesName);
                }

                #endregion

                SkuController SKUCtl = new SkuController();
                CustomerDataController CustCtl = new CustomerDataController();

                DataTable PurchaseSKU = SelectOrderDetail(p_Distributor_Id, p_SaleOrder_Id, Constants.IntNullValue);
                DataTable dtFreeSKU = SelectOrderPromotion(p_Distributor_Id, p_SaleOrder_Id);

                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, p_ChannelType_ID);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));
                foreach (DataRow dr in PurchaseSKU.Rows)
                {                    
                    if (dtAccount.Rows.Count > 0)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drConsumtion = dtVoucher.NewRow();
                            drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                            drConsumtion["REMARKS"] = "Consumtion";
                            drConsumtion["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                            drConsumtion["CREDIT"] = 0;
                            drConsumtion["Principal_Id"] = p_Principal_Id;
                            dtVoucher.Rows.Add(drConsumtion);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Stock In Hand";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                            drStock["Principal_Id"] = p_Principal_Id;
                            dtVoucher.Rows.Add(drStock);

                            DataRow drDiscount = dtVoucher.NewRow();
                            drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                            drDiscount["REMARKS"] = "Discount Allowed";
                            drDiscount["DEBIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                            drDiscount["CREDIT"] = 0;
                            drDiscount["Principal_Id"] = p_Principal_Id;
                            dtVoucher.Rows.Add(drDiscount);

                            if (dtChannel.Rows.Count > 0)
                            {
                                DataRow drParty = dtVoucher.NewRow();
                                if (p_OrderTypeId == Constants.Credit_Order_Id)
                                {
                                    drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                    drParty["REMARKS"] = "Channel Credit";
                                }
                                else
                                {
                                    drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                    drParty["REMARKS"] = "Channel Cash";
                                }
                                drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()));
                                drParty["CREDIT"] = 0;
                                drParty["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drParty);

                                DataRow drSale = dtVoucher.NewRow();
                                drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                drSale["REMARKS"] = "Gross Sale";
                                drSale["DEBIT"] = 0;
                                drSale["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                drSale["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drSale);
                            }
                        }
                    }
                }

                foreach (DataRow df in dtFreeSKU.Rows)
                {                    
                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + df["SKU_ID"].ToString() + "'");

                    DataRow drScheme = dtVoucher.NewRow();
                    drScheme["ACCOUNT_HEAD_ID"] = foundRows[0]["SCHEME_ID"];
                    drScheme["REMARKS"] = "Scheme";
                    drScheme["DEBIT"] = decimal.Parse(df["QUANTITY"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                    drScheme["CREDIT"] = 0;
                    drScheme["Principal_Id"] = p_Principal_Id;
                    dtVoucher.Rows.Add(drScheme);

                    DataRow drStock = dtVoucher.NewRow();
                    drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                    drStock["REMARKS"] = "Stock In Hand";
                    drStock["DEBIT"] = 0;
                    drStock["CREDIT"] = decimal.Parse(df["QUANTITY"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                    drStock["Principal_Id"] = p_Principal_Id;
                    dtVoucher.Rows.Add(drStock);
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_Id, p_Document_Date);

                    LController.Add_Voucher2(p_Distributor_Id, 0, MaxDocumentId, Constants.Journal_Voucher, p_Document_Date, Constants.CashPayment, "N/A", "Default Sales Voucher", Constants.DateNullValue, null, Convert.ToInt64(dt.Rows[0][0]), Constants.Document_SaleInvoice, dtVoucher, p_UserId, null, Constants.DateNullValue);
                }

                mTransaction.Commit();
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



           public DataTable ConvertOrder_to_Invoice(int p_Distributor_Id, string p_MANUAL_ORDER_ID, long p_Customer_Id, int p_Principal_Id,
            long p_SaleOrder_Id, DateTime p_Document_Date, decimal p_GrossSale, decimal p_Discount, decimal p_scheme,
            decimal p_GstAmt, decimal p_Net_Amount, int p_OrderStatus, int p_OrderTypeId, int p_UserId, 
            string p_PayeesName, int p_ChannelType_ID, string p_Customer_Name, string p_SalePerson, 
            decimal p_advanceTax)
        {
            #region variables
            IDbTransaction mTransaction = null;
            IDbConnection mConnection = null;
            #endregion

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspConvertOrdertoInvoice mOrder = new UspConvertOrdertoInvoice();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                mOrder.Connection = mConnection;
                mOrder.Transaction = mTransaction;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.PRINCIPAL_ID = p_Principal_Id;
                mOrder.CUSTOMER_ID = p_Customer_Id;
                mOrder.DOCUMENT_DATE = p_Document_Date;
                mOrder.SALE_ORDER_ID = p_SaleOrder_Id;

                if (p_OrderTypeId == Constants.Credit_Order_Id)
                {
                    mOrder.NET_AMOUNT = p_Net_Amount;
                }
                else
                {
                    mOrder.NET_AMOUNT = 0;
                }
                mOrder.ORDER_STATUS = p_OrderStatus;
                DataTable dt = mOrder.ExecuteTable();
                if (dt.Columns.Count > 1)
                {
                    mTransaction.Rollback();
                    return dt;
                }

                #region Account Posting

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_Id);

                if (p_OrderTypeId == Constants.Advance_PaymentOrder_id)
                {
                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_Id, 0, p_Net_Amount, p_Document_Date, "Net Sale Value", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_PayeesName);
                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_Id, p_Net_Amount, 0, p_Document_Date, "Recevieable from Customer", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_PayeesName);

                }
                else if (p_OrderTypeId == Constants.Credit_Order_Id)
                {

                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_Id, p_Net_Amount, 0, p_Document_Date, "Credit Sale Default", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_PayeesName);
                    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_Id, 0, p_Net_Amount, p_Document_Date, "Credit Sale Default", DateTime.Now, p_Principal_Id, int.Parse(p_Customer_Id.ToString()), long.Parse(dt.Rows[0][0].ToString()), p_MANUAL_ORDER_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_PayeesName);
                }

                #endregion

                SkuController SKUCtl = new SkuController();
                CustomerDataController CustCtl = new CustomerDataController();

                DataTable PurchaseSKU = SelectOrderDetail(p_Distributor_Id, p_SaleOrder_Id, Constants.IntNullValue);
                DataTable dtFreeSKU = SelectOrderPromotion(p_Distributor_Id, p_SaleOrder_Id);

                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, p_ChannelType_ID);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));
                foreach (DataRow dr in PurchaseSKU.Rows)
                {                    
                    if (dtAccount.Rows.Count > 0)
                    {
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            if (decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString()) > 0)
                            {
                                DataRow drConsumtion = dtVoucher.NewRow();
                                drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                                drConsumtion["REMARKS"] = "Consumtion";
                                drConsumtion["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drConsumtion["CREDIT"] = 0;
                                drConsumtion["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drConsumtion);
                            }

                            if (decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString()) > 0)
                            {
                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Stock In Hand";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drStock["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drStock);
                            }

                            if (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) > 0)
                            {
                                DataRow drDiscount = dtVoucher.NewRow();
                                drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                                drDiscount["REMARKS"] = "Discount Allowed";
                                drDiscount["DEBIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                                drDiscount["CREDIT"] = 0;
                                drDiscount["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drDiscount);
                            }
                            if (dtChannel.Rows.Count > 0)
                            {
                                DataRow drParty = dtVoucher.NewRow();
                                if (p_OrderTypeId == Constants.Credit_Order_Id || p_OrderTypeId == Constants.Advance_PaymentOrder_id)
                                {
                                    drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                    drParty["REMARKS"] = "Channel Credit";
                                    //GST
                                    if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                    {
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_Principal_Id;
                                            dtVoucher.Rows.Add(drGST);
                                        }

                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_Principal_Id;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }
                                }
                                else
                                {
                                    drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                    drParty["REMARKS"] = "Channel Cash";

                                    //GST
                                    if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                    {
                                        //Credit
                                        DataRow drGST = dtVoucher.NewRow();
                                        drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                        drGST["REMARKS"] = "Sales Tax";
                                        drGST["DEBIT"] = 0;
                                        drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                        drGST["Principal_Id"] = p_Principal_Id;
                                        dtVoucher.Rows.Add(drGST);

                                        //Debit
                                        DataRow drGST2 = dtVoucher.NewRow();
                                        drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                        drGST2["REMARKS"] = "Channel Debit";
                                        drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                        drGST2["CREDIT"] = 0;
                                        drGST2["Principal_Id"] = p_Principal_Id;
                                        dtVoucher.Rows.Add(drGST2);
                                    }
                                }
                                drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) -
                                    (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) +
                                    decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) +
                                    decimal.Parse(dr["ADVANCE_TAX"].ToString());

                                drParty["CREDIT"] = 0;
                                drParty["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drParty);

                                DataRow drSale = dtVoucher.NewRow();
                                drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                drSale["REMARKS"] = "Gross Sale";
                                drSale["DEBIT"] = 0;
                                drSale["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                drSale["Principal_Id"] = p_Principal_Id;
                                dtVoucher.Rows.Add(drSale);
                            }
                        }
                    }
                }

                //Advance Tax
                if (p_advanceTax > 0)
                {
                    //Credit
                    DataRow drGST = dtVoucher.NewRow();
                    drGST["ACCOUNT_HEAD_ID"] = 1306;
                    drGST["REMARKS"] = "Parties W.Tax Deducted US 236 G and H";
                    drGST["DEBIT"] = 0;
                    drGST["CREDIT"] = p_advanceTax;
                    drGST["Principal_Id"] = p_Principal_Id;
                    dtVoucher.Rows.Add(drGST);
                }

                foreach (DataRow df in dtFreeSKU.Rows)
                {                    
                    DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + df["SKU_ID"].ToString() + "'");

                    DataRow drScheme = dtVoucher.NewRow();
                    drScheme["ACCOUNT_HEAD_ID"] = foundRows[0]["SCHEME_ID"];
                    drScheme["REMARKS"] = "Scheme";
                    drScheme["DEBIT"] = decimal.Parse(df["QUANTITY"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                    drScheme["CREDIT"] = 0;
                    drScheme["Principal_Id"] = p_Principal_Id;
                    dtVoucher.Rows.Add(drScheme);

                    DataRow drStock = dtVoucher.NewRow();
                    drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                    drStock["REMARKS"] = "Stock In Hand";
                    drStock["DEBIT"] = 0;
                    drStock["CREDIT"] = decimal.Parse(df["QUANTITY"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                    drStock["Principal_Id"] = p_Principal_Id;
                    dtVoucher.Rows.Add(drStock);
                }

                if (dtVoucher.Rows.Count > 0)
                {
                    string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_Id, p_Document_Date);

                    LController.Add_Voucher2(p_Distributor_Id, 0, MaxDocumentId, Constants.Journal_Voucher, p_Document_Date, Constants.CashPayment, "N/A", "Default Sales Voucher ," + p_SalePerson + " , " + p_Customer_Name + " , " + Convert.ToInt64(dt.Rows[0][0]) + "", Constants.DateNullValue, null, Convert.ToInt64(dt.Rows[0][0]), Constants.Document_SaleInvoice, dtVoucher, p_UserId, null, Constants.DateNullValue);
                }

                mTransaction.Commit();
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
        /// Gets Orders And Invoices Summary
        /// </summary>
        /// <param name="p_Distributor_ID">Location</param>
        /// <param name="p_Areaid">Market</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="FromDocNo">DateFrom</param>
        /// <param name="ToDocNo">DateTo</param>
        /// <param name="DocumentTypeId">Type</param>
        /// <param name="p_IS_REGISTERED">IsRegistered</param>
        /// <param name="p_DELIVERYMAN_ID">Deliveryman</param>
        /// <returns>Orders And Invoices Summary as Datatable</returns>
        public DataTable SelectDocumentforView(int p_Distributor_ID, int p_Areaid, int p_Principal_Id, DateTime FromDocNo, DateTime ToDocNo, int DocumentTypeId, int p_IS_REGISTERED, int p_DELIVERYMAN_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspDocumentView ObjPrint = new UspDocumentView();
                SAMSBusinessLayer.Reports.DsReport ds = new SAMSBusinessLayer.Reports.DsReport();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBTOR_ID = p_Distributor_ID;
                ObjPrint.AREA_ID = p_Areaid;
                ObjPrint.PRINCIPAL_ID = p_Principal_Id;
                ObjPrint.FROM_DATE = FromDocNo;
                ObjPrint.TO_DATE = ToDocNo;
                ObjPrint.TYPE_ID = DocumentTypeId;
                ObjPrint.IS_REGISTERED = p_IS_REGISTERED;
                ObjPrint.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                DataTable dt = ObjPrint.ExecuteTable();
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
        public DataTable GetDocumentDetail(long p_DocID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetDocumentDetail mOrderDetail = new uspGetDocumentDetail();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DocID = p_DocID;
                mOrderDetail.TYPE_ID = p_TYPE_ID;
                DataTable dt = mOrderDetail.ExecuteTable();
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
        
        public DataTable SelectPreviousSaleDetail(int p_DISTRIBUTOR_ID, long p_SALE_INVOICE_ID, IDbConnection PConnection, IDbTransaction PTransaction)
          {
              try
              {
                  spSelectSALE_DETAIL mSaleDetail = new spSelectSALE_DETAIL();
                  mSaleDetail.Connection = PConnection;
                  mSaleDetail.Transaction = PTransaction;
                  mSaleDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                  mSaleDetail.SALE_INVOICE_ID = p_SALE_INVOICE_ID;
                  DataTable dt = mSaleDetail.ExecuteTable();
                  return dt;
              }
              catch (Exception exp)
              {
                  ExceptionPublisher.PublishException(exp);
                  return null;
              }
          }
       

        #region Rollback

        /// <summary>
        /// Gets Rollback Data For Order, Invoice And Sale Return
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_Order_Booker">OrderBooker</param>
        /// <param name="p_TypeId">Type</param>
        /// <param name="p_DocumentDate">Date</param>
        /// <returns>Rollback Data For Order, Invoice And Sale Return as Datatable</returns>
        public DataTable SelectRollBackDocument(int p_Distributor_Id, int p_Principal_Id, int p_Order_Booker, int p_TypeId, DateTime p_DocumentDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectRollBackDocument mOrder = new UspSelectRollBackDocument();
                mOrder.Connection = mConnection;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.DOCUMENT_TYPE = p_TypeId;
                mOrder.PRINCIPAL_ID = p_Principal_Id;
                mOrder.ORDERBOOKER_ID = p_Order_Booker;
                mOrder.DOCUMENT_DATE = p_DocumentDate;
                DataTable dt = mOrder.ExecuteTable();
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

        #region Added By Hazrat Ali

        /// <summary>
        /// Checks Manual Order No And Manual Invoice No
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_MANUAL_ORDER_ID">ManualOrder</param>
        /// <param name="p_TYPE">Type</param>
        /// <returns>Manual Order No And Manual Invoice No as Datatable</returns>
        public DataTable SelectBillBookNo(int p_Distributor_Id, string p_MANUAL_ORDER_ID, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspIsBillBookNoExist mBillBookNo = new uspIsBillBookNoExist();
                mBillBookNo.Connection = mConnection;
                mBillBookNo.DISTRIBUTOR_ID = p_Distributor_Id;
                mBillBookNo.MANUAL_ID = p_MANUAL_ORDER_ID;
                mBillBookNo.TYPE = p_TYPE;
                DataTable dt = mBillBookNo.ExecuteTable();
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

        public DataTable GetOrderClosingStock(int p_Distributor_Id, int p_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetOrderClosingStock mStock = new uspGetOrderClosingStock();
                mStock.Connection = mConnection;
                mStock.DISTRIBUTOR_ID = p_Distributor_Id;
                mStock.SKU_ID = p_SKU_ID;
                DataTable dt = mStock.ExecuteTable();
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


        public DataTable GetOrderClosingStock2(int p_Distributor_Id, int p_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetOrderClosingStock2 mStock = new uspGetOrderClosingStock2();
                mStock.Connection = mConnection;
                mStock.DISTRIBUTOR_ID = p_Distributor_Id;
                mStock.SKU_ID = p_SKU_ID;
                DataTable dt = mStock.ExecuteTable();
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

        #endregion

        #region Insert, Update, Deleted
        
        public bool Update_Invoice2(long p_SALE_INVOICE_ID, DateTime p_DocumentDate, int p_Distributor_id, string p_MANUAL_INVOICE_ID, long p_SOLD_TO, int p_DELIVERYMAN_ID,
        decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, 
        decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId,
        DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, decimal p_CashReceived,
        decimal p_TSTAmount, decimal p_SEDAmount, int p_DCNumber, int p_RefNumber, DateTime p_PODate,
        int p_PRINCIPAL_ID, int p_ChannelTypeID, string p_Customer_Name, string p_SalePerson_NAme,
        DateTime p_PO_DATE, string p_DC_PO_NO, decimal p_advanceTaxPercent, decimal p_advanceTaxAmount)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                SkuController SKUCtl = new SkuController();
                CustomerDataController CustCtl = new CustomerDataController();

                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, p_ChannelTypeID);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspUpdateInvoiceMaster2 mISom = new uspUpdateInvoiceMaster2();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                    mISom.SALE_INVOICE_ID = p_SALE_INVOICE_ID;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;

                    if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                    }
                    else
                    {
                        mISom.CREDIT_AMOUNT = 0;
                        mISom.CURRENT_CREDIT_AMOUNT = 0;
                    }
                    mISom.USER_ID = p_UserId;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.PO_DATE = p_PO_DATE;
                    mISom.DC_PO_NO = p_DC_PO_NO;
                    mISom.ADVANCE_TAX_PERCENT = p_advanceTaxPercent;
                    mISom.ADVANCE_TAX = p_advanceTaxAmount;
                    mISom.ExecuteQuery();

                    DataTable dt = SelectPreviousSaleDetail(p_Distributor_id, p_SALE_INVOICE_ID, mConnection, mTransaction);

                    foreach (DataRow dr in dt.Rows)
                    {
                        uspDeleteInvoiceDetail mSale = new uspDeleteInvoiceDetail();
                        mSale.Connection = mConnection;
                        mSale.Transaction = mTransaction;
                        mSale.SALE_INVOICE_ID = p_SALE_INVOICE_ID;
                        mSale.DOCUMENT_DATE = p_DocumentDate;
                        mSale.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                        mSale.ExecuteQuery();
                    }
                    //----------------Insert into sale order detail-------------
                    spInsertSALE_INVOICE_DETAIL mSaleOrderDetail = new spInsertSALE_INVOICE_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.SALE_INVOICE_ID = p_SALE_INVOICE_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.DISTRIBUTOR_PRICE = decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["GST_AMOUNT2"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = 0;
                        mSaleOrderDetail.ADVANCE_TAX = decimal.Parse(dr["ADVANCE_TAX"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX_PERCENT = decimal.Parse(dr["ADVANCE_TAX_PERCENT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.IS_DELETED = false;
                        mSaleOrderDetail.TIME_STAMP = p_DocumentDate;

                        mSaleOrderDetail.ExecuteQuery();

                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drConsumtion = dtVoucher.NewRow();
                                drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                                drConsumtion["REMARKS"] = "Consumtion";
                                drConsumtion["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drConsumtion["CREDIT"] = 0;
                                drConsumtion["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drConsumtion);

                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Stock In Hand";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drStock["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drStock);
                                if (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) > 0)
                                {
                                    DataRow drDiscount = dtVoucher.NewRow();
                                    drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                                    drDiscount["REMARKS"] = "Discount Allowed";
                                    drDiscount["DEBIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                                    drDiscount["CREDIT"] = 0;
                                    drDiscount["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drDiscount);
                                }

                                if (dtChannel.Rows.Count > 0)
                                {
                                    DataRow drParty = dtVoucher.NewRow();
                                    if (InvoiceTypeId == Constants.Credit_Order_Id || InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                                    {
                                        drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                        drParty["REMARKS"] = "Channel Credit";
                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }
                                    else
                                    {
                                        drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                        drParty["REMARKS"] = "Channel Cash";
                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }
                                    drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - 
                                        (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) +
                                        decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) +
                                        decimal.Parse(dr["ADVANCE_TAX"].ToString());
                                    drParty["CREDIT"] = 0;
                                    drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drParty);

                                    DataRow drSale = dtVoucher.NewRow();
                                    drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                    drSale["REMARKS"] = "Gross Sale";
                                    drSale["DEBIT"] = 0;
                                    drSale["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                    drSale["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drSale);  
                                }
                            }
                        }

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                        mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                        mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                        mStockUpdate.FREE_QTY = 0;
                        mStockUpdate.ExecuteQuery();
                    }

                    //Advance Tax
                    if (p_advanceTaxAmount > 0)
                    {
                        //Credit
                        DataRow drGST = dtVoucher.NewRow();
                        drGST["ACCOUNT_HEAD_ID"] = 1306;
                        drGST["REMARKS"] = "Parties W.Tax Deducted US 236 G and H";
                        drGST["DEBIT"] = 0;
                        drGST["CREDIT"] = p_advanceTaxAmount;
                        drGST["Principal_Id"] = p_PRINCIPAL_ID;
                        dtVoucher.Rows.Add(drGST);
                    }

                    foreach (DataRow df in dtFreeSKU.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertSALE_INVOICE_PROMOTION mSaleOrderPromo = new spInsertSALE_INVOICE_PROMOTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                        mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                        mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                        mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                        mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                        mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                        mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                        mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_PRICE = decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                        mSaleOrderPromo.SED_AMOUNT = 0;
                        mSaleOrderPromo.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                        mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                        mSaleOrderPromo.ExecuteQuery();

                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + df["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drScheme = dtVoucher.NewRow();
                            drScheme["ACCOUNT_HEAD_ID"] = foundRows[0]["SCHEME_ID"];
                            drScheme["REMARKS"] = "Scheme";
                            drScheme["DEBIT"] = decimal.Parse(df["Quantity"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                            drScheme["CREDIT"] = 0;
                            drScheme["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drScheme);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Stock In Hand";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(df["Quantity"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                            drStock["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drStock);
                        }

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderPromo.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A";
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mSaleOrderPromo.QUANTITY;
                        mStockUpdate.ExecuteQuery();
                    }

                    #region Account Posting
                    LedgerController LController = new LedgerController();
                    Configuration.GetAccountHead();
                    DistributorController Dcontroller = new DistributorController();


                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id,mTransaction ,mConnection );

                    if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                    {
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_AMOUNT, p_DocumentDate, "Gross Sale Value", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                        if (p_STANDARD_DISCOUNT_AMOUNT > 0)
                        {
                            LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleDiscount), p_Distributor_id, p_STANDARD_DISCOUNT_AMOUNT, 0, p_DocumentDate, "Commision/Discount", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                        }
                        if (p_EXTRA_DISCOUNT_AMOUNT > 0)
                        {
                            LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleScheme), p_Distributor_id, p_EXTRA_DISCOUNT_AMOUNT, 0, p_DocumentDate, "Extra Discount", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                        }
                        if (p_GST_AMOUNT + p_TSTAmount > 0)
                        {
                            LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.GSTAccount), p_Distributor_id, 0, p_GST_AMOUNT + p_TSTAmount, p_DocumentDate, "Sales Tax", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                        }
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, p_DocumentDate, "Credit Sale Default", DateTime.Now, 0, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                    }
                    else if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                                    
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT - p_CashReceived, 0, p_DocumentDate, "Credit Sale Default", DateTime.Now,p_PRINCIPAL_ID , int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT - p_CashReceived, p_DocumentDate, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());

                    }
                    #endregion

                    if (dtVoucher.Rows.Count > 0)
                    {
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate,mTransaction ,mConnection );

                      bool Isinsert=  LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Sales Voucher " + p_SalePerson_NAme + " , " + p_Customer_Name  + " , " + p_SALE_INVOICE_ID + "", Constants.DateNullValue, null, mISom.SALE_INVOICE_ID, Constants.Document_SaleInvoice, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                    if (!Isinsert)
                    {
                        throw new ArgumentNullException();
                    }
                    }

                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
       
        public bool UpdateDamage(long p_SALES_RETURN_ID, int p_Distributor_id, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
      decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, DateTime p_DocumentDate, decimal p_TstAmount, decimal p_SEDAmount)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                foreach (DataRow dr in dtOrderDetail.Rows)
                {
                    uspDeleteDamageDetail mOrder = new uspDeleteDamageDetail();
                    mOrder.Connection = mConnection;
                    mOrder.SALES_RETURN_ID = p_SALES_RETURN_ID;
                    mOrder.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                    mOrder.QUANTITY = Convert.ToInt32(dr["QUANTITY_UNIT"]);
                    mOrder.DISTRIBUTOR_ID = p_Distributor_id;
                    mOrder.STOCK_DATE = p_DocumentDate;
                    mOrder.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mOrder.ExecuteQuery();
                }

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspUpdateDamageMaster mISom = new uspUpdateDamageMaster();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Return Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.SALES_RETURN_ID = p_SALES_RETURN_ID;
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = int.Parse(p_AREA_ID.ToString());
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.CUSTOMER_ID = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.TST_AMOUNT = p_TstAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0;
                    mISom.ExecuteQuery();

                    //----------------Insert into sales return detail-------------
                    spInsertSALES_RETURN_DETAIL mSaleOrderDetail = new spInsertSALES_RETURN_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.SALES_RETURN_ID = mISom.SALES_RETURN_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["TST_AMOUNT"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.TIME_STAMP = Convert.ToDateTime(dr["ExpDate"]); ;
                        mSaleOrderDetail.ExecuteQuery();

                        if (p_PRINCIPAL_ID == 0)
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = 10;
                            mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                            mStockUpdate.STOCK_DATE = p_DocumentDate;
                            mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                            mStockUpdate.BATCHNO = "N/A";
                            mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }
                        else if (p_PRINCIPAL_ID == 2)
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = 11;
                            mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                            mStockUpdate.STOCK_DATE = p_DocumentDate;
                            mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                            mStockUpdate.BATCHNO = "N/A";
                            mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }

                    }

                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool Add_Damage(int p_Distributor_id, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
     decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, DateTime p_DocumentDate, decimal p_TstAmount, decimal p_SEDAmount)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALES_RETURN_MASTER mISom = new spInsertSALES_RETURN_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Return Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = int.Parse(p_AREA_ID.ToString());
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.CUSTOMER_ID = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.TST_AMOUNT = p_TstAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0;
                    mISom.ExecuteQuery();

                    //----------------Insert into sales return detail-------------
                    spInsertSALES_RETURN_DETAIL mSaleOrderDetail = new spInsertSALES_RETURN_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.SALES_RETURN_ID = mISom.SALES_RETURN_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["TST_AMOUNT"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.TIME_STAMP = Convert.ToDateTime(dr["ExpDate"]); ;
                        mSaleOrderDetail.ExecuteQuery();

                        if (p_PRINCIPAL_ID == 0)
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = 10;
                            mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                            mStockUpdate.STOCK_DATE = p_DocumentDate;
                            mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                            mStockUpdate.BATCHNO = "N/A";
                            mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }
                        else if (p_PRINCIPAL_ID == 2)
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = 11;
                            mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                            mStockUpdate.STOCK_DATE = p_DocumentDate;
                            mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                            mStockUpdate.BATCHNO = "N/A";
                            mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }

                    }


                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }


      
        public bool Add_Order(int p_Distributor_id,string p_MANUAL_ORDER_ID,int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID,int p_OrderTypeId,
            decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT,
            decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int p_STATUS_ID, 
            DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, DateTime p_Document_Date,
            decimal p_SEDAmount, decimal p_TSTAmount, long p_Vehicle_NO, DateTime p_PO_DATE, 
            string p_DC_PO_NO, decimal p_advanceTaxPercent, decimal p_advanceTaxAmount)
        {
           
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALE_ORDER_MASTER mISom = new spInsertSALE_ORDER_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)  
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.MANUAL_ORDER_ID = p_MANUAL_ORDER_ID;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;  
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_Document_Date;
                    mISom.SHIP_TO = p_SHIP_TO;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;  
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;  
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.STATUS_ID = p_STATUS_ID;
                    mISom.USER_ID = p_UserId;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;  
                    mISom.ORDER_TYPE_ID = p_OrderTypeId;  
                    mISom.TIME_STAMP = DateTime.Now;   
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.VEHICLE_NO = p_Vehicle_NO;
                    mISom.PO_DATE = p_PO_DATE;
                    mISom.DC_PO_NO = p_DC_PO_NO;
                    mISom.ADVANCE_TAX_PERCENT = p_advanceTaxPercent;
                    mISom.ADVANCE_TAX = p_advanceTaxAmount;
                    mISom.ExecuteQuery();                    

                    //----------------Insert into sale order detail-------------
                    spInsertSALE_ORDER_DETAIL mSaleOrderDetail = new spInsertSALE_ORDER_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach(DataRow dr in dtOrderDetail.Rows)   
                    {
                        mSaleOrderDetail.SALE_ORDER_ID = mISom.SALE_ORDER_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID  = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO  = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.DISTRIBUTOR_PRICE = decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["GST_AMOUNT2"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX = decimal.Parse(dr["ADVANCE_TAX"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX_PERCENT = decimal.Parse(dr["ADVANCE_TAX_PERCENT"].ToString());
                        mSaleOrderDetail.IS_DELETED = false;
                        mSaleOrderDetail.TIME_STAMP = p_Document_Date;                        
                        mSaleOrderDetail.ExecuteQuery();
                    }
                    
                    foreach (DataRow df in dtFreeSKU.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertSALE_ORDER_PROMOTION mSaleOrderPromo = new spInsertSALE_ORDER_PROMOTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                        mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                        mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                        mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                        mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                        mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                        mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                        mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_PRICE = decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderPromo.SALE_ORDER_ID = mISom.SALE_ORDER_ID;
                        mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                        mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                        mSaleOrderPromo.SED_AMOUNT = 0;
                        mSaleOrderPromo.ExecuteQuery();
                    }
                     
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }


        public bool Update_Order(long p_Sale_order_ID,int p_Distributor_id, string p_MANUAL_ORDER_ID, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, int p_OrderTypeId,
          decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT,
          decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int p_STATUS_ID,
          DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, DateTime p_Document_Date,
          decimal p_SEDAmount, decimal p_TSTAmount, long p_Vehicle_NO, DateTime p_PO_DATE,
          string p_DC_PO_NO, decimal p_advanceTaxPercent, decimal p_advanceTaxAmount)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdateSALE_ORDER_MASTER_detail mISom = new spUpdateSALE_ORDER_MASTER_detail();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.SALE_ORDER_ID = p_Sale_order_ID;
                    mISom.MANUAL_ORDER_ID = p_MANUAL_ORDER_ID;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_Document_Date;
                    mISom.SHIP_TO = p_SHIP_TO;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.STATUS_ID = p_STATUS_ID;
                    mISom.USER_ID = p_UserId;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.ORDER_TYPE_ID = p_OrderTypeId;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.VEHICLE_NO = p_Vehicle_NO;
                    mISom.PO_DATE = p_PO_DATE;
                    mISom.DC_PO_NO = p_DC_PO_NO;
                    mISom.ADVANCE_TAX_PERCENT = p_advanceTaxPercent;
                    mISom.ADVANCE_TAX = p_advanceTaxAmount;
                    mISom.ExecuteQuery();

                    //----------------Insert into sale order detail-------------
                    spInsertSALE_ORDER_DETAIL mSaleOrderDetail = new spInsertSALE_ORDER_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.SALE_ORDER_ID = mISom.SALE_ORDER_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.DISTRIBUTOR_PRICE = decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["GST_AMOUNT2"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX = decimal.Parse(dr["ADVANCE_TAX"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX_PERCENT = decimal.Parse(dr["ADVANCE_TAX_PERCENT"].ToString());
                        mSaleOrderDetail.IS_DELETED = false;
                        mSaleOrderDetail.TIME_STAMP = p_Document_Date;
                        mSaleOrderDetail.ExecuteQuery();
                    }

                    foreach (DataRow df in dtFreeSKU.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertSALE_ORDER_PROMOTION mSaleOrderPromo = new spInsertSALE_ORDER_PROMOTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                        mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                        mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                        mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                        mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                        mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                        mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                        mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_PRICE = decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderPromo.SALE_ORDER_ID = mISom.SALE_ORDER_ID;
                        mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                        mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                        mSaleOrderPromo.SED_AMOUNT = 0;
                        mSaleOrderPromo.ExecuteQuery();
                    }

                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }




        /// <summary>
        /// Inserts Invoice
        /// </summary>
        /// <param name="p_Distributor_id">Location</param>
        /// <param name="p_MANUAL_INVOICE_ID">ManualInvoice</param>
        /// <param name="p_TOWN_ID">Town</param>
        /// <param name="p_AREA_ID">Route</param>
        /// <param name="p_PRINCIPAL_ID">Principal</param>
        /// <param name="p_SOLD_TO">Customer</param>
        /// <param name="p_SHIP_TO">ShipTo</param>
        /// <param name="p_ORDERBOOKER_ID">OrderBooker</param>
        /// <param name="p_DELIVERYMAN_ID">Deliveryman</param>
        /// <param name="p_Orderid">Order</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_EXTRA_DISCOUNT_AMOUNT">ExtraDiscount</param>
        /// <param name="p_STANDARD_DISCOUNT_AMOUNT">Discount</param>
        /// <param name="p_GST_AMOUNT">GST</param>
        /// <param name="p_TOTAL_NET_AMOUNT">NetAmount</param>
        /// <param name="p_SCHEME_AMOUNT">SchemeAmount</param>
        /// <param name="InvoiceTypeId">Type</param>
        /// <param name="dtOrderDetail">OrderDetailDatatable</param>
        /// <param name="dtFreeSKU">FreeSKUDatatable</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_CashReceived">Cash</param>
        /// <param name="p_DocumentDate">Date</param>
        /// <param name="p_TSTAmount">TSTAmount</param>
        /// <param name="p_SEDAmount">SEDAmount</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool Add_Invoice(int p_Distributor_id,string p_MANUAL_INVOICE_ID, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long  p_Orderid,
         decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId, DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId,decimal p_CashReceived,DateTime p_DocumentDate,decimal p_TSTAmount,decimal p_SEDAmount,long p_Vehicle_NO)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            
            decimal TotalAmt = 0, DiscountAmount = 0, ExtraDiscount = 0, GSTAmount = 0, TotalNetAmt = 0;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALE_INVOICE_MASTER mISom = new spInsertSALE_INVOICE_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                    mISom.IS_DELETED = false;  
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                    }
                    else
                    {
                        mISom.CREDIT_AMOUNT = 0;
                        mISom.CURRENT_CREDIT_AMOUNT = 0;
                    }
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.USER_ID = p_UserId;
                    mISom.SALE_ORDER_ID  = p_Orderid;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;  
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.VEHICLE_NO = p_Vehicle_NO;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0; 
                    mISom.ExecuteQuery();

                    //----------------Insert into sale order detail-------------
                    spInsertSALE_INVOICE_DETAIL mSaleOrderDetail = new spInsertSALE_INVOICE_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        //SaleOrderDetail_Collection mSod_Col=new SaleOrderDetail_Collection ();
                        mSaleOrderDetail.SALE_INVOICE_ID  = mISom.SALE_INVOICE_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO  = dr["BATCH_NO"].ToString(); 
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["TST_AMOUNT"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString()); 
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.IS_DELETED = false;
                        mSaleOrderDetail.TIME_STAMP = p_DocumentDate;
                        TotalAmt += decimal.Parse(dr["AMOUNT"].ToString());
                        ExtraDiscount += decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        DiscountAmount += decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        GSTAmount += decimal.Parse(dr["GST_AMOUNT"].ToString());
                        TotalNetAmt += decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.ExecuteQuery();
                        
                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                        mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                        mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                        mStockUpdate.FREE_QTY = 0;
                        mStockUpdate.ExecuteQuery(); 
                    }
                    foreach (DataRow df in dtFreeSKU.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertSALE_INVOICE_PROMOTION mSaleOrderPromo = new spInsertSALE_INVOICE_PROMOTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                        mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                        mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                        mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                        mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                        mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                        mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                        mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                        mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                        mSaleOrderPromo.SED_AMOUNT = 0; 
                        mSaleOrderPromo.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                        mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                        mSaleOrderPromo.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderPromo.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A"; 
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mSaleOrderPromo.QUANTITY; 
                        mStockUpdate.ExecuteQuery(); 
                    }

                    #region Account Posting
                    LedgerController LController = new LedgerController();
                    Configuration.GetAccountHead();
                    DistributorController  Dcontroller = new DistributorController();
                   
                    
                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);

                    if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                    {
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT, mISom.DOCUMENT_DATE, "Net Sale Value", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());                        
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                    }
                    else if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT - p_CashReceived, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT - p_CashReceived, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        
                    }
                    #endregion

                    #region Update Pending Order

                    spUpdateSALE_ORDER_MASTER mOrderUpdate = new spUpdateSALE_ORDER_MASTER();
                    mOrderUpdate.Connection = mConnection;
                    mOrderUpdate.Transaction = mTransaction;
                    mOrderUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                    mOrderUpdate.SALE_ORDER_ID = p_Orderid;
                    mOrderUpdate.STATUS_ID = Constants.Order_Posted_Id;
                    mOrderUpdate.ExecuteQuery();
                    
                    #endregion
                   
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public long Add_Invoice2(int p_Distributor_id, string p_MANUAL_INVOICE_ID, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
         decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT,
         decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId,
         DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, decimal p_CashReceived,
         DateTime p_DocumentDate, decimal p_TSTAmount, decimal p_SEDAmount, long p_Vehicle_NO,
         int p_ChannelTypeID, string p_Customer_Name, string p_Sale_Person_Name, DateTime p_PO_DATE,
         string p_DC_PO_NO, decimal p_advanceTaxPercent, decimal p_advanceTaxAmount)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            SkuController SKUCtl = new SkuController();
            CustomerDataController CustCtl = new CustomerDataController();
            bool CashReceived = false;
            DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
            DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue,p_ChannelTypeID);

            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
            dtVoucher.Columns.Add("Debit", typeof(decimal));
            dtVoucher.Columns.Add("Credit", typeof(decimal));
            dtVoucher.Columns.Add("Remarks", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALE_INVOICE_MASTER mISom = new spInsertSALE_INVOICE_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                    mISom.IS_DELETED = false;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                    }
                    else
                    {
                        mISom.CREDIT_AMOUNT = 0;
                        mISom.CURRENT_CREDIT_AMOUNT = 0;
                    }
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.USER_ID = p_UserId;
                    mISom.SALE_ORDER_ID = p_Orderid;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.VEHICLE_NO = p_Vehicle_NO;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0;
                    mISom.PO_DATE = p_PO_DATE;
                    mISom.DC_PO_NO = p_DC_PO_NO;
                    mISom.ADVANCE_TAX_PERCENT = p_advanceTaxPercent;
                    mISom.ADVANCE_TAX = p_advanceTaxAmount;
                    mISom.ExecuteQuery();

                    //----------------Insert into sale order detail-------------
                    spInsertSALE_INVOICE_DETAIL mSaleOrderDetail = new spInsertSALE_INVOICE_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.DISTRIBUTOR_PRICE = decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["GST_AMOUNT2"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX = decimal.Parse(dr["ADVANCE_TAX"].ToString());
                        mSaleOrderDetail.ADVANCE_TAX_PERCENT = decimal.Parse(dr["ADVANCE_TAX_PERCENT"].ToString());
                        mSaleOrderDetail.IS_DELETED = false;
                        mSaleOrderDetail.TIME_STAMP = p_DocumentDate;
                        mSaleOrderDetail.ExecuteQuery();

                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drConsumtion = dtVoucher.NewRow();
                                drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                                drConsumtion["REMARKS"] = "Consumtion";
                                drConsumtion["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drConsumtion["CREDIT"] = 0;
                                drConsumtion["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drConsumtion);

                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Stock In Hand";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drStock["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drStock);
                                if (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) > 0)
                                {
                                    DataRow drDiscount = dtVoucher.NewRow();
                                    drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                                    drDiscount["REMARKS"] = "Discount Allowed";
                                    drDiscount["DEBIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                                    drDiscount["CREDIT"] = 0;
                                    drDiscount["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drDiscount);
                                }

                                if (dtChannel.Rows.Count > 0)
                                {                                    
                                    if (InvoiceTypeId == Constants.Credit_Order_Id || InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                                    {
                                        if (p_CashReceived > 0)
                                        {
                                            if (!CashReceived)
                                            {
                                                DataRow drPartyCash = dtVoucher.NewRow();
                                                drPartyCash["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                                drPartyCash["REMARKS"] = "Channel Cash";
                                                drPartyCash["DEBIT"] = p_CashReceived;
                                                drPartyCash["CREDIT"] = 0;
                                                drPartyCash["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drPartyCash);
                                                CashReceived = true;
                                            }

                                            DataRow drPartyCredit = dtVoucher.NewRow();
                                            drPartyCredit["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                            drPartyCredit["REMARKS"] = "Channel Credit";
                                            drPartyCredit["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - 
                                                (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) +
                                                decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) -
                                                p_CashReceived + decimal.Parse(dr["ADVANCE_TAX"].ToString());
                                            drPartyCredit["CREDIT"] = 0;
                                            drPartyCredit["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drPartyCredit);
                                            
                                        }
                                        else
                                        {
                                            DataRow drParty = dtVoucher.NewRow();
                                            drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                            drParty["REMARKS"] = "Channel Credit";
                                            drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) -
                                                (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + 
                                                decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) +
                                                decimal.Parse(dr["ADVANCE_TAX"].ToString());
                                            drParty["CREDIT"] = 0;
                                            drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drParty);
                                        }

                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"]; ;
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }
                                    else
                                    {
                                        DataRow drParty = dtVoucher.NewRow();
                                        drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                        drParty["REMARKS"] = "Channel Cash";
                                        drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - 
                                            (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) +
                                            decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) 
                                            + decimal.Parse(dr["ADVANCE_TAX"].ToString());
                                        drParty["CREDIT"] = 0;
                                        drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                        dtVoucher.Rows.Add(drParty);

                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"]; ;
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }

                                    DataRow drSale = dtVoucher.NewRow();
                                    drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                    drSale["REMARKS"] = "Gross Sale";
                                    drSale["DEBIT"] = 0;
                                    drSale["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                    drSale["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drSale);
                                }
                            }
                        }

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                        mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                        mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                        mStockUpdate.FREE_QTY = 0;
                        mStockUpdate.ExecuteQuery();
                    }

                    //Advance Tax
                    if (p_advanceTaxAmount > 0)
                    {
                        //Credit
                        DataRow drGST = dtVoucher.NewRow();
                        drGST["ACCOUNT_HEAD_ID"] = 1306;
                        drGST["REMARKS"] = "Parties W.Tax Deducted US 236 G and H";
                        drGST["DEBIT"] = 0;
                        drGST["CREDIT"] = p_advanceTaxAmount;
                        drGST["Principal_Id"] = p_PRINCIPAL_ID;
                        dtVoucher.Rows.Add(drGST);
                    }

                    foreach (DataRow df in dtFreeSKU.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertSALE_INVOICE_PROMOTION mSaleOrderPromo = new spInsertSALE_INVOICE_PROMOTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                        mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                        mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                        mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                        mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                        mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                        mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                        mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_PRICE = decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                        mSaleOrderPromo.SED_AMOUNT = 0;
                        mSaleOrderPromo.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                        mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                        mSaleOrderPromo.ExecuteQuery();

                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + df["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drScheme = dtVoucher.NewRow();
                            drScheme["ACCOUNT_HEAD_ID"] = foundRows[0]["SCHEME_ID"];
                            drScheme["REMARKS"] = "Scheme";
                            drScheme["DEBIT"] = decimal.Parse(df["Quantity"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                            drScheme["CREDIT"] = 0;
                            drScheme["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drScheme);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Stock In Hand";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(df["Quantity"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                            drStock["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drStock);
                        }

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderPromo.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A";
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mSaleOrderPromo.QUANTITY;
                        mStockUpdate.ExecuteQuery();
                    }

                    #region Account Posting
                    LedgerController LController = new LedgerController();
                    Configuration.GetAccountHead();
                    DistributorController Dcontroller = new DistributorController();


                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);
                    bool flag = true;
                    bool isinsert = true;
                    if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                    {
                        flag = LController.PostingInvoiceAccountNew(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT, mISom.DOCUMENT_DATE, "Net Sale Value", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                        if (flag)
                        {
                            flag = LController.PostingInvoiceAccountNew(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                        }

                    }
                    else if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        flag = LController.PostingInvoiceAccountNew(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT - p_CashReceived, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        if (flag)
                        {
                            flag = LController.PostingInvoiceAccountNew(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT - p_CashReceived, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        }

                    }
                    #endregion

                    #region Update Pending Order
                    if (flag)
                    {
                        spUpdateSALE_ORDER_MASTER mOrderUpdate = new spUpdateSALE_ORDER_MASTER();
                        mOrderUpdate.Connection = mConnection;
                        mOrderUpdate.Transaction = mTransaction;
                        mOrderUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mOrderUpdate.SALE_ORDER_ID = p_Orderid;
                        mOrderUpdate.STATUS_ID = Constants.Order_Posted_Id;
                        flag = mOrderUpdate.ExecuteQuery();
                    }
                    #endregion

                    if (dtVoucher.Rows.Count > 0)
                    {
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate,mTransaction ,mConnection);

                      isinsert=  LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Sales Voucher ," + p_Sale_Person_Name + " , " + p_Customer_Name + " , " + mISom.SALE_INVOICE_ID + "", Constants.DateNullValue, null, mISom.SALE_INVOICE_ID, Constants.Document_SaleInvoice, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);                      
                    }
                    if (flag && isinsert)
                    {
                        mTransaction.Commit();
                        return mISom.SALE_INVOICE_ID;
                    }
                    else
                    {
                        mTransaction.Rollback();
                        return 0;
                    }
                }
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();
                ExceptionPublisher.PublishException(exp);
                return 0;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return 0;
        }

        public static long Add_Invoice2(int p_Distributor_id, string p_MANUAL_INVOICE_ID, int p_PRINCIPAL_ID,
            long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
            decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT,
            decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId,
            DataTable dtOrderDetail, int p_UserId, decimal p_CashReceived, DateTime p_DocumentDate,
            decimal p_TSTAmount, decimal p_SEDAmount, string p_AuthorisedBy, string p_InvoiceNumberFBR,
            int p_channel_Type_Id, string p_customerName, string p_salePersonName)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            decimal TotalAmt = 0, GSTAmount = 0, TotalNetAmt = 0;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALE_INVOICE_MASTER2 mISom = new spInsertSALE_INVOICE_MASTER2();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                SkuController SKUCtl = new SkuController();
                CustomerDataController CustCtl = new CustomerDataController();

                bool CashReceived = false;
                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, p_channel_Type_Id);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                //------------Insert into Sale Invoice Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = InvoiceTypeId;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                    mISom.IS_DELETED = false;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;


                    if (InvoiceTypeId == Constants.Credit)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT;
                    }
                    else if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT;
                    }
                    else if (InvoiceTypeId == 230 || InvoiceTypeId == 231)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_STANDARD_DISCOUNT_AMOUNT;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_STANDARD_DISCOUNT_AMOUNT;
                    }
                    else if (InvoiceTypeId == Constants.CashandCredit_Order_Id)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_STANDARD_DISCOUNT_AMOUNT;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_STANDARD_DISCOUNT_AMOUNT;
                    }

                    else
                    {
                        mISom.CREDIT_AMOUNT = 0;
                        mISom.CURRENT_CREDIT_AMOUNT = 0;
                    }
                    mISom.TOWN_ID = 0;
                    mISom.USER_ID = p_UserId;
                    mISom.SALE_ORDER_ID = p_Orderid;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.AUTHORISED_PERSON = p_AuthorisedBy;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0;
                    mISom.InvoiceNumberFBR = p_InvoiceNumberFBR;
                    mISom.ExecuteQuery();

                    //------------------Ledger Posting--------------------------\\



                    //----------------Insert into sale order detail-------------
                    spInsertSALE_INVOICE_DETAILDecimal mSaleOrderDetail = new spInsertSALE_INVOICE_DETAILDecimal();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        //SaleOrderDetail_Collection mSod_Col=new SaleOrderDetail_Collection ();
                        mSaleOrderDetail.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = decimal.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["TST_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.IS_DELETED = false;
                        mSaleOrderDetail.IS_VOID = true;
                        mSaleOrderDetail.TIME_STAMP = p_DocumentDate;

                        if ((dr["CHECK_DELETE"].ToString()) == "0")
                        {
                            TotalAmt += decimal.Parse(dr["AMOUNT"].ToString());
                            GSTAmount += decimal.Parse(dr["GST_AMOUNT"].ToString());
                            TotalNetAmt += decimal.Parse(dr["NET_AMOUNT"].ToString());
                            mSaleOrderDetail.IS_VOID = false;
                        }
                        mSaleOrderDetail.ExecuteQuery();

                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drConsumtion = dtVoucher.NewRow();
                                drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                                drConsumtion["REMARKS"] = "Consumtion";
                                drConsumtion["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                                drConsumtion["CREDIT"] = 0;
                                drConsumtion["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drConsumtion);

                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Stock In Hand";
                                drStock["DEBIT"] = 0;
                                drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                                drStock["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drStock);
                                if (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) > 0)
                                {
                                    DataRow drDiscount = dtVoucher.NewRow();
                                    drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                                    drDiscount["REMARKS"] = "Discount Allowed";
                                    drDiscount["DEBIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                                    drDiscount["CREDIT"] = 0;
                                    drDiscount["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drDiscount);
                                }

                                if (dtChannel.Rows.Count > 0)
                                {
                                    if (InvoiceTypeId == Constants.Credit_Order_Id || InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                                    {
                                        if (p_CashReceived > 0)
                                        {
                                            if (!CashReceived)
                                            {
                                                DataRow drPartyCash = dtVoucher.NewRow();
                                                drPartyCash["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                                drPartyCash["REMARKS"] = "Channel Cash";
                                                drPartyCash["DEBIT"] = p_CashReceived;
                                                drPartyCash["CREDIT"] = 0;
                                                drPartyCash["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drPartyCash);
                                                CashReceived = true;
                                            }

                                            DataRow drPartyCredit = dtVoucher.NewRow();
                                            drPartyCredit["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                            drPartyCredit["REMARKS"] = "Channel Credit";
                                            drPartyCredit["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) - p_CashReceived;
                                            drPartyCredit["CREDIT"] = 0;
                                            drPartyCredit["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drPartyCredit);

                                        }
                                        else
                                        {
                                            DataRow drParty = dtVoucher.NewRow();
                                            drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                            drParty["REMARKS"] = "Channel Credit";
                                            drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()));
                                            drParty["CREDIT"] = 0;
                                            drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drParty);
                                        }

                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"]; ;
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }
                                    else
                                    {
                                        DataRow drParty = dtVoucher.NewRow();
                                        drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                        drParty["REMARKS"] = "Channel Cash";
                                        drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()));
                                        drParty["CREDIT"] = 0;
                                        drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                        dtVoucher.Rows.Add(drParty);

                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = 0;
                                            drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"]; ;
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["CREDIT"] = 0;
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }

                                    DataRow drSale = dtVoucher.NewRow();
                                    drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                    drSale["REMARKS"] = "Gross Sale";
                                    drSale["DEBIT"] = 0;
                                    drSale["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                    drSale["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drSale);
                                }
                            }
                        }


                        if ((dr["CHECK_DELETE"].ToString()) == "0")
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            if (mSaleOrderDetail.QUANTITY_UNIT < 0)
                            {
                                mStockUpdate.TYPE_ID = Constants.Document_Sale_Return;
                                mStockUpdate.STOCK_QTY = Convert.ToInt32(mSaleOrderDetail.QUANTITY_UNIT * (-1));
                            }
                            else
                            {
                                mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                                mStockUpdate.STOCK_QTY = Convert.ToInt32(mSaleOrderDetail.QUANTITY_UNIT);
                            }

                            mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                            mStockUpdate.STOCK_DATE = p_DocumentDate;
                            mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                            mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();

                        }

                    }


                    #region Account Posting

                    LedgerController LController = new LedgerController();
                    Configuration.GetAccountHead();


                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher,
                        p_Distributor_id, 0);

                    //if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                    //{
                    //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_AMOUNT, mISom.DOCUMENT_DATE, "Gross Sale Value", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                    //    if (p_STANDARD_DISCOUNT_AMOUNT > 0)
                    //    {
                    //        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleDiscount), p_Distributor_id, p_STANDARD_DISCOUNT_AMOUNT, 0, mISom.DOCUMENT_DATE, "Commision/Discount", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                    //    }
                    //    if (p_EXTRA_DISCOUNT_AMOUNT > 0)
                    //    {
                    //        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleScheme), p_Distributor_id, p_EXTRA_DISCOUNT_AMOUNT, 0, mISom.DOCUMENT_DATE, "Extra Discount", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                    //    }
                    //    if (p_GST_AMOUNT + p_TSTAmount > 0)
                    //    {
                    //        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.GSTAccount), p_Distributor_id, 0, p_GST_AMOUNT + p_TSTAmount, mISom.DOCUMENT_DATE, "Sales Tax", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                    //    }
                    //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                    //}
                    if (InvoiceTypeId == Constants.Credit)//Credit
                    {
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());

                    }

                    if (dtVoucher.Rows.Count > 0)
                    {
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, 
                            p_DocumentDate, mTransaction, mConnection);

                        bool isinsert = LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher,
                            p_DocumentDate, Constants.CashPayment,
                            "N/A", "Default Sales Voucher ," + p_salePersonName + " , " + p_customerName + " , " + mISom.SALE_INVOICE_ID + "",
                            Constants.DateNullValue, null, mISom.SALE_INVOICE_ID, Constants.Document_SaleInvoice,
                            dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);

                        if (!isinsert)
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    #endregion


                    mTransaction.Commit();

                    return mISom.SALE_INVOICE_ID;
                }
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();
                ExceptionPublisher.PublishException(exp);

                return -2;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return -1;
        }


        //Insertion from PO to Sale Invoice
        public bool AddInvoice(int p_Distributor_id, string p_MANUAL_INVOICE_ID, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID, long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
        decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT, decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId, DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, decimal p_CashReceived, DateTime p_DocumentDate, decimal p_TSTAmount, decimal p_SEDAmount, long p_Vehicle_NO, int p_ChannelTypeID, string p_Customer_Name, string p_Sale_Person_Name, DateTime p_PO_DATE, string p_DC_PO_NO)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            SkuController SKUCtl = new SkuController();
            CustomerDataController CustCtl = new CustomerDataController();
            bool CashReceived = false;
            DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
            DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, p_ChannelTypeID);

            DataTable dtVoucher = new DataTable();
            dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
            dtVoucher.Columns.Add("Debit", typeof(decimal));
            dtVoucher.Columns.Add("Credit", typeof(decimal));
            dtVoucher.Columns.Add("Remarks", typeof(string));
            dtVoucher.Columns.Add("Principal_Id", typeof(string));

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALE_INVOICE_MASTER mISom = new spInsertSALE_INVOICE_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.MANUAL_INVOICE_ID = p_MANUAL_INVOICE_ID;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.SOLD_TO = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.SCHEME_AMOUNT = p_SCHEME_AMOUNT;
                    mISom.IS_DELETED = false;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        mISom.CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                        mISom.CURRENT_CREDIT_AMOUNT = p_TOTAL_NET_AMOUNT - p_CashReceived;
                    }
                    else
                    {
                        mISom.CREDIT_AMOUNT = 0;
                        mISom.CURRENT_CREDIT_AMOUNT = 0;
                    }
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.USER_ID = p_UserId;
                    mISom.SALE_ORDER_ID = p_Orderid;
                    mISom.TST_AMOUNT = p_TSTAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.VEHICLE_NO = p_Vehicle_NO;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0;
                    mISom.PO_DATE = p_PO_DATE;
                    mISom.DC_PO_NO = p_DC_PO_NO;
                    mISom.ExecuteQuery();

                    //----------------Insert into sale order detail-------------
                    spInsertSALE_INVOICE_DETAIL mSaleOrderDetail = new spInsertSALE_INVOICE_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        if (int.Parse(dr["CurrentRcdQty"].ToString()) > 0)
                        {
                            mSaleOrderDetail.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                            mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                            mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                            mSaleOrderDetail.BATCH_NO = "N/A";
                            mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["CurrentRcdQty"].ToString());
                            mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                            mSaleOrderDetail.DISTRIBUTOR_PRICE = decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                            mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                            mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                            mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                            mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                            mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                            mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["GST_AMOUNT2"].ToString());
                            mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                            mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                            mSaleOrderDetail.IS_DELETED = false;
                            mSaleOrderDetail.TIME_STAMP = p_DocumentDate;
                            mSaleOrderDetail.ExecuteQuery();

                            DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                            if (dtAccount.Rows.Count > 0)
                            {
                                if (foundRows.Length > 0)
                                {
                                    DataRow drConsumtion = dtVoucher.NewRow();
                                    drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                                    drConsumtion["REMARKS"] = "Consumtion";
                                    drConsumtion["DEBIT"] = decimal.Parse(dr["CurrentRcdQty"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                    drConsumtion["CREDIT"] = 0;
                                    drConsumtion["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drConsumtion);

                                    DataRow drStock = dtVoucher.NewRow();
                                    drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                    drStock["REMARKS"] = "Stock In Hand";
                                    drStock["DEBIT"] = 0;
                                    drStock["CREDIT"] = decimal.Parse(dr["CurrentRcdQty"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                    drStock["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drStock);
                                    if (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) > 0)
                                    {
                                        DataRow drDiscount = dtVoucher.NewRow();
                                        drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                                        drDiscount["REMARKS"] = "Discount Allowed";
                                        drDiscount["DEBIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                                        drDiscount["CREDIT"] = 0;
                                        drDiscount["Principal_Id"] = p_PRINCIPAL_ID;
                                        dtVoucher.Rows.Add(drDiscount);
                                    }

                                    if (dtChannel.Rows.Count > 0)
                                    {
                                        if (InvoiceTypeId == Constants.Credit_Order_Id)
                                        {
                                            if (p_CashReceived > 0)
                                            {
                                                if (!CashReceived)
                                                {
                                                    DataRow drPartyCash = dtVoucher.NewRow();
                                                    drPartyCash["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                                    drPartyCash["REMARKS"] = "Channel Cash";
                                                    drPartyCash["DEBIT"] = p_CashReceived;
                                                    drPartyCash["CREDIT"] = 0;
                                                    drPartyCash["Principal_Id"] = p_PRINCIPAL_ID;
                                                    dtVoucher.Rows.Add(drPartyCash);
                                                    CashReceived = true;
                                                }

                                                DataRow drPartyCredit = dtVoucher.NewRow();
                                                drPartyCredit["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                                drPartyCredit["REMARKS"] = "Channel Credit";
                                                drPartyCredit["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString())) - p_CashReceived;
                                                drPartyCredit["CREDIT"] = 0;
                                                drPartyCredit["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drPartyCredit);

                                            }
                                            else
                                            {
                                                DataRow drParty = dtVoucher.NewRow();
                                                drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                                drParty["REMARKS"] = "Channel Credit";
                                                drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()));
                                                drParty["CREDIT"] = 0;
                                                drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drParty);
                                            }

                                            //GST
                                            if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                            {
                                                //Credit
                                                DataRow drGST = dtVoucher.NewRow();
                                                drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                                drGST["REMARKS"] = "Sales Tax";
                                                drGST["DEBIT"] = 0;
                                                drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                                drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drGST);

                                                //Debit
                                                DataRow drGST2 = dtVoucher.NewRow();
                                                drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"]; ;
                                                drGST2["REMARKS"] = "Channel Debit";
                                                drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                                drGST2["CREDIT"] = 0;
                                                drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drGST2);
                                            }
                                        }
                                        else
                                        {
                                            DataRow drParty = dtVoucher.NewRow();
                                            drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                            drParty["REMARKS"] = "Channel Cash";
                                            drParty["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()));
                                            drParty["CREDIT"] = 0;
                                            drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drParty);

                                            //GST
                                            if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                            {
                                                //Credit
                                                DataRow drGST = dtVoucher.NewRow();
                                                drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                                drGST["REMARKS"] = "Sales Tax";
                                                drGST["DEBIT"] = 0;
                                                drGST["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                                drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drGST);

                                                //Debit
                                                DataRow drGST2 = dtVoucher.NewRow();
                                                drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"]; ;
                                                drGST2["REMARKS"] = "Channel Debit";
                                                drGST2["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                                drGST2["CREDIT"] = 0;
                                                drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                                dtVoucher.Rows.Add(drGST2);
                                            }
                                        }

                                        DataRow drSale = dtVoucher.NewRow();
                                        drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                        drSale["REMARKS"] = "Gross Sale";
                                        drSale["DEBIT"] = 0;
                                        drSale["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                        drSale["Principal_Id"] = p_PRINCIPAL_ID;
                                        dtVoucher.Rows.Add(drSale);
                                    }
                                }
                            }

                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                            mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                            mStockUpdate.STOCK_DATE = p_DocumentDate;
                            mStockUpdate.SKU_ID = mSaleOrderDetail.SKU_ID;
                            mStockUpdate.BATCHNO = mSaleOrderDetail.BATCH_NO;
                            mStockUpdate.STOCK_QTY = mSaleOrderDetail.QUANTITY_UNIT;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }
                    }
                    foreach (DataRow df in dtFreeSKU.Rows)
                    {
                        //----------------Insert into sale order Promotion-------------
                        spInsertSALE_INVOICE_PROMOTION mSaleOrderPromo = new spInsertSALE_INVOICE_PROMOTION();
                        mSaleOrderPromo.Connection = mConnection;
                        mSaleOrderPromo.Transaction = mTransaction;

                        mSaleOrderPromo.BASKET_DETAIL_ID = int.Parse(df["BASKET_DETAIL_ID"].ToString());
                        mSaleOrderPromo.BASKET_ID = int.Parse(df["BASKET_ID"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderPromo.GST_AMOUNT = decimal.Parse(df["GST_AMOUNT"].ToString());
                        mSaleOrderPromo.GST_RATE = float.Parse(df["GST_RATE"].ToString());
                        mSaleOrderPromo.PROMOTION_ID = int.Parse(df["PROMOTION_ID"].ToString());
                        mSaleOrderPromo.PROMOTION_OFFER_ID = int.Parse(df["PROMOTION_OFFER_ID"].ToString());
                        mSaleOrderPromo.QUANTITY = int.Parse(df["Quantity"].ToString());
                        mSaleOrderPromo.SKU_ID = int.Parse(df["SKU_ID"].ToString());
                        mSaleOrderPromo.UNIT_PRICE = decimal.Parse(df["UNIT_PRICE"].ToString());
                        mSaleOrderPromo.DISTRIBUTOR_PRICE = decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                        mSaleOrderPromo.TST_AMOUNT = decimal.Parse(df["TST_AMOUNT"].ToString());
                        mSaleOrderPromo.SED_AMOUNT = 0;
                        mSaleOrderPromo.SALE_INVOICE_ID = mISom.SALE_INVOICE_ID;
                        mSaleOrderPromo.AMOUNT = decimal.Parse(df["AMOUNT"].ToString());
                        mSaleOrderPromo.ExecuteQuery();

                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + df["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drScheme = dtVoucher.NewRow();
                            drScheme["ACCOUNT_HEAD_ID"] = foundRows[0]["SCHEME_ID"];
                            drScheme["REMARKS"] = "Scheme";
                            drScheme["DEBIT"] = decimal.Parse(df["Quantity"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                            drScheme["CREDIT"] = 0;
                            drScheme["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drScheme);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Stock In Hand";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(df["Quantity"].ToString()) * decimal.Parse(df["DISTRIBUTOR_PRICE"].ToString());
                            drStock["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drStock);
                        }

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mSaleOrderPromo.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A";
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mSaleOrderPromo.QUANTITY;
                        mStockUpdate.ExecuteQuery();
                    }

                    #region Account Posting
                    LedgerController LController = new LedgerController();
                    Configuration.GetAccountHead();
                    DistributorController Dcontroller = new DistributorController();


                    string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);

                    if (InvoiceTypeId == Constants.Advance_PaymentOrder_id)
                    {
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT, mISom.DOCUMENT_DATE, "Net Sale Value", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.Cash_Advance, p_DELIVERYMAN_ID.ToString());

                    }
                    else if (InvoiceTypeId == Constants.Credit_Order_Id)
                    {
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, p_TOTAL_NET_AMOUNT - p_CashReceived, 0, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());
                        LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT - p_CashReceived, mISom.DOCUMENT_DATE, "Credit Sale Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserId, mTransaction, mConnection, Constants.CreditSale, p_DELIVERYMAN_ID.ToString());

                    }
                    #endregion

                    #region Update Pending Order

                    spUpdateSALE_ORDER_MASTER mOrderUpdate = new spUpdateSALE_ORDER_MASTER();
                    mOrderUpdate.Connection = mConnection;
                    mOrderUpdate.Transaction = mTransaction;
                    mOrderUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                    mOrderUpdate.SALE_ORDER_ID = p_Orderid;
                    mOrderUpdate.STATUS_ID = Constants.Order_Posted_Id;
                    mOrderUpdate.ExecuteQuery();

                    #endregion

                    if (dtVoucher.Rows.Count > 0)
                    {
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate, mTransaction, mConnection);

                        bool isinsert = LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Sales Voucher ," + p_Sale_Person_Name + " , " + p_Customer_Name + " , " + mISom.SALE_INVOICE_ID + "", Constants.DateNullValue, null, mISom.SALE_INVOICE_ID, Constants.Document_SaleInvoice, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// Inserts Sale Returns
        /// </summary>
        /// <param name="p_Distributor_id">Location</param>
        /// <param name="p_TOWN_ID">Town</param>
        /// <param name="p_AREA_ID">Market</param>
        /// <param name="p_PRINCIPAL_ID">Principal</param>
        /// <param name="p_SOLD_TO">Customer</param>
        /// <param name="p_SHIP_TO">ShipTo</param>
        /// <param name="p_ORDERBOOKER_ID">OrderBooker</param>
        /// <param name="p_DELIVERYMAN_ID">Deliveryman</param>
        /// <param name="p_Orderid">Order</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_EXTRA_DISCOUNT_AMOUNT">ExtraDiscount</param>
        /// <param name="p_STANDARD_DISCOUNT_AMOUNT">Discount</param>
        /// <param name="p_GST_AMOUNT">GST</param>
        /// <param name="p_TOTAL_NET_AMOUNT">NetAmount</param>
        /// <param name="p_SCHEME_AMOUNT">Scheme</param>
        /// <param name="InvoiceTypeId">Type</param>
        /// <param name="dtOrderDetail">OrderDetailDatatable</param>
        /// <param name="dtFreeSKU">FreeSKUDatatable</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_DocumentDate">Date</param>
        /// <param name="p_TstAmount">TSTAmount</param>
        /// <param name="p_SEDAmount">SEDAmount</param>
        /// <returns>True On Success And False On Failure</returns>
        public long Add_SaleReturn(int p_Distributor_id, int p_TOWN_ID, long p_AREA_ID, int p_PRINCIPAL_ID,
            long p_SOLD_TO, long p_SHIP_TO, int p_ORDERBOOKER_ID, int p_DELIVERYMAN_ID, long p_Orderid,
        decimal p_TOTAL_AMOUNT, decimal p_EXTRA_DISCOUNT_AMOUNT, decimal p_STANDARD_DISCOUNT_AMOUNT,
        decimal p_GST_AMOUNT, decimal p_TOTAL_NET_AMOUNT, decimal p_SCHEME_AMOUNT, int InvoiceTypeId,
        DataTable dtOrderDetail, DataTable dtFreeSKU, int p_UserId, DateTime p_DocumentDate,
        decimal p_TstAmount, decimal p_SEDAmount, long p_Vehicle_NO, int p_ChannelTypeID,
        string p_Customer_Name, string p_Sale_Person_Name, decimal p_advanceTaxPercent, decimal p_advanceTax)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                SkuController SKUCtl = new SkuController();
                CustomerDataController CustCtl = new CustomerDataController();

                DataTable dtAccount = SKUCtl.GetSKUAccountDetail(Constants.IntNullValue);
                DataTable dtChannel = CustCtl.GetChannelAccountDetail(Constants.IntNullValue, p_ChannelTypeID);

                DataTable dtVoucher = new DataTable();
                dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                dtVoucher.Columns.Add("Debit", typeof(decimal));
                dtVoucher.Columns.Add("Credit", typeof(decimal));
                dtVoucher.Columns.Add("Remarks", typeof(string));
                dtVoucher.Columns.Add("Principal_Id", typeof(string));

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSALES_RETURN_MASTER mISom = new spInsertSALES_RETURN_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Return Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = int.Parse(p_AREA_ID.ToString());
                    mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                    mISom.ORDERBOOKER_ID = p_ORDERBOOKER_ID;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.CUSTOMER_ID = p_SOLD_TO;
                    mISom.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                    mISom.EXTRA_DISCOUNT_AMOUNT = p_EXTRA_DISCOUNT_AMOUNT;
                    mISom.STANDARD_DISCOUNT_AMOUNT = p_STANDARD_DISCOUNT_AMOUNT;
                    mISom.GST_AMOUNT = p_GST_AMOUNT;
                    mISom.TOTAL_NET_AMOUNT = p_TOTAL_NET_AMOUNT;
                    mISom.TOWN_ID = p_TOWN_ID;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LASTUPDATE_DATE = System.DateTime.Now;
                    mISom.TST_AMOUNT = p_TstAmount;
                    mISom.SED_AMOUNT = p_SEDAmount;
                    mISom.VEHICLE_NO = p_Vehicle_NO;
                    mISom.IS_DELETED = false;
                    mISom.POSTING = 0;
                    mISom.ADVANCE_TAX_PERCENT = p_advanceTaxPercent;
                    mISom.ADVANCE_TAX = p_advanceTax;
                    mISom.ExecuteQuery();

                    //----------------Insert into sales return detail-------------
                    spInsertSALES_RETURN_DETAIL mSaleOrderDetail = new spInsertSALES_RETURN_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.SALES_RETURN_ID = mISom.SALES_RETURN_ID;
                        mSaleOrderDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                        mSaleOrderDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mSaleOrderDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.GST_RATE = float.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mSaleOrderDetail.EXTRA_DISCOUNT = decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                        mSaleOrderDetail.STANDARD_DISCOUNT = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString());
                        mSaleOrderDetail.GST_AMOUNT = decimal.Parse(dr["GST_AMOUNT"].ToString());
                        mSaleOrderDetail.TST_AMOUNT = decimal.Parse(dr["GST_AMOUNT2"].ToString());
                        mSaleOrderDetail.SED_AMOUNT = decimal.Parse(dr["SED_AMOUNT"].ToString());
                        mSaleOrderDetail.NET_AMOUNT = decimal.Parse(dr["NET_AMOUNT"].ToString());
                        mSaleOrderDetail.TIME_STAMP = p_DocumentDate;
                        mSaleOrderDetail.ADVANCE_TAX_PERCENT = dr["ADVANCE_TAX_PERCENT"] != null ? decimal.Parse(dr["ADVANCE_TAX_PERCENT"].ToString()) : 0;
                        mSaleOrderDetail.ADVANCE_TAX = dr["ADVANCE_TAX"] != null ? decimal.Parse(dr["ADVANCE_TAX"].ToString()) : 0;
                        mSaleOrderDetail.ExecuteQuery();
                        
                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");

                        if (dtAccount.Rows.Count > 0)
                        {
                            if (foundRows.Length > 0)
                            {
                                DataRow drConsumtion = dtVoucher.NewRow();
                                drConsumtion["ACCOUNT_HEAD_ID"] = foundRows[0]["CONSUMPTION_ID"];
                                drConsumtion["REMARKS"] = "Consumtion";
                                drConsumtion["DEBIT"] = 0;
                                drConsumtion["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drConsumtion["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drConsumtion);

                                DataRow drStock = dtVoucher.NewRow();
                                drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                                drStock["REMARKS"] = "Stock In Hand";
                                drStock["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["DISTRIBUTOR_PRICE"].ToString());
                                drStock["CREDIT"] = 0;
                                drStock["Principal_Id"] = p_PRINCIPAL_ID;
                                dtVoucher.Rows.Add(drStock);
                                if (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()) > 0)
                                {
                                    DataRow drDiscount = dtVoucher.NewRow();
                                    drDiscount["ACCOUNT_HEAD_ID"] = foundRows[0]["DISCOUNTALLOW_ID"];
                                    drDiscount["REMARKS"] = "Discount Allowed";
                                    drDiscount["DEBIT"] = 0;
                                    drDiscount["CREDIT"] = decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString());
                                    drDiscount["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drDiscount);
                                }

                                if (dtChannel.Rows.Count > 0)
                                {
                                    DataRow drParty = dtVoucher.NewRow();
                                    if (InvoiceTypeId == Constants.Credit_Order_Id)
                                    {
                                        drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CREDIT_HEAD_ID"];
                                        drParty["REMARKS"] = "Channel Credit";
                                    }
                                    else
                                    {
                                        drParty["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                        drParty["REMARKS"] = "Channel Cash";
                                        //GST
                                        if (decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString()) > 0)
                                        {
                                            //Credit
                                            DataRow drGST = dtVoucher.NewRow();
                                            drGST["ACCOUNT_HEAD_ID"] = foundRows[0]["SALESTAX_ID"];
                                            drGST["REMARKS"] = "Sales Tax";
                                            drGST["DEBIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST["CREDIT"] = 0;
                                            drGST["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST);

                                            //Debit
                                            DataRow drGST2 = dtVoucher.NewRow();
                                            drGST2["ACCOUNT_HEAD_ID"] = dtChannel.Rows[0]["CASH_HEAD_ID"];
                                            drGST2["REMARKS"] = "Channel Debit";
                                            drGST2["DEBIT"] = 0;
                                            drGST2["CREDIT"] = decimal.Parse(dr["GST_AMOUNT"].ToString()) + decimal.Parse(dr["GST_AMOUNT2"].ToString());
                                            drGST2["Principal_Id"] = p_PRINCIPAL_ID;
                                            dtVoucher.Rows.Add(drGST2);
                                        }
                                    }
                                    drParty["DEBIT"] = 0;
                                    drParty["CREDIT"] = decimal.Parse(dr["AMOUNT"].ToString()) - (decimal.Parse(dr["STANDARD_DISCOUNT"].ToString()) + decimal.Parse(dr["EXTRA_DISCOUNT"].ToString()));
                                    drParty["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drParty);

                                    DataRow drSale = dtVoucher.NewRow();
                                    drSale["ACCOUNT_HEAD_ID"] = foundRows[0]["SALE_ID"];
                                    drSale["REMARKS"] = "Gross Sale";
                                    drSale["DEBIT"] = decimal.Parse(dr["AMOUNT"].ToString());
                                    drSale["CREDIT"] = 0;
                                    drSale["Principal_Id"] = p_PRINCIPAL_ID;
                                    dtVoucher.Rows.Add(drSale);
                                }
                            }
                        }
                    }


                    #region Account Posting

                    LedgerController LController = new LedgerController();
                    //Configuration.GetAccountHead();
                    //DistributorController Dcontroller = new DistributorController();
                    //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_Distributor_id);
                    //LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleReturnAccount), p_Distributor_id, p_TOTAL_AMOUNT, 0, mISom.DOCUMENT_DATE, "Sales Return Value", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALES_RETURN_ID, mISom.SALES_RETURN_ID.ToString(), Constants.Document_Sale_Return, p_UserId, mTransaction, mConnection, Constants.CashSaleReturn, p_DELIVERYMAN_ID.ToString());
                    //if (p_STANDARD_DISCOUNT_AMOUNT > 0 || p_EXTRA_DISCOUNT_AMOUNT > 0)
                    //{
                    //LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleReturnDiscount), p_Distributor_id, 0, p_STANDARD_DISCOUNT_AMOUNT + p_EXTRA_DISCOUNT_AMOUNT, mISom.DOCUMENT_DATE, "Sales Return Discount/Extra Discount", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALES_RETURN_ID, mISom.SALES_RETURN_ID.ToString(), Constants.Document_Sale_Return, p_UserId, mTransaction, mConnection, Constants.CashSaleReturn, p_DELIVERYMAN_ID.ToString());
                    //}
                    //if (p_GST_AMOUNT + p_TstAmount > 0)
                    //{
                    //    LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleReturnGST), p_Distributor_id, p_GST_AMOUNT + p_TstAmount, 0, mISom.DOCUMENT_DATE, "Sales Return Tax", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALES_RETURN_ID, mISom.SALES_RETURN_ID.ToString(), Constants.Document_Sale_Return, p_UserId, mTransaction, mConnection, Constants.CashSaleReturn, p_DELIVERYMAN_ID.ToString());
                    //}
                    //LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_Distributor_id, 0, p_TOTAL_NET_AMOUNT, mISom.DOCUMENT_DATE, "Sales Return Default", DateTime.Now, p_PRINCIPAL_ID, int.Parse(p_SOLD_TO.ToString()), mISom.SALES_RETURN_ID, mISom.SALES_RETURN_ID.ToString(), Constants.Document_Sale_Return, p_UserId, mTransaction, mConnection, Constants.CashSaleReturn, p_DELIVERYMAN_ID.ToString());

                    #endregion

                    if (dtVoucher.Rows.Count > 0)
                    {
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate,mTransaction ,mConnection );

                      bool ininsert=  LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Sales Return Voucher , " + p_Sale_Person_Name + " , " + p_Customer_Name + " , " + mISom.SALES_RETURN_ID, Constants.DateNullValue, null, mISom.SALES_RETURN_ID, Constants.Document_SaleReturn, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction,mConnection);
                        if (!ininsert)
                        {
                            throw new ArgumentNullException();
                        }
                    }

                    mTransaction.Commit();
                    return mISom.SALES_RETURN_ID;
                }
            }
            catch (Exception exp)
            {
                mTransaction.Rollback();  
                ExceptionPublisher.PublishException(exp);
                  
                return 0;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return 0;
        }
        
        /// <summary>
        /// Delets Free SKU Form Invoice
        /// </summary>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Invoice_Id">Invoice</param>
        /// <param name="p_SalePromotionId">Promotion</param>
        /// <param name="p_SKU_id">SKU</param>
        /// <param name="p_Qty">Quantity</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool DeleteFreeSKUFromInvoice(int p_Distributor_Id, long  p_Invoice_Id, long p_SalePromotionId, int p_SKU_id,int p_Qty)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspDeleteFreeSKUOut mOrder = new UspDeleteFreeSKUOut();
                mOrder.Connection = mConnection;
                mOrder.Distributor_id = p_Distributor_Id;
                mOrder.InvoiceNo  = p_Invoice_Id;
                mOrder.SALE_INVOICE_PROMOTION_ID  = p_SalePromotionId;
                mOrder.SKU_id = p_SKU_id;
                mOrder.Qty = p_Qty;
                mOrder.ExecuteQuery();
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
        /// Inserts Transport Expenses
        /// </summary>
        /// <param name="p_Distributor_id">Location</param>
        /// <param name="p_SaleInvoiceId">Invoice</param>
        /// <param name="p_Transport_ID">Transport</param>
        /// <param name="p_Bilty_no">Builty</param>
        /// <param name="p_DilveryChallan">DeliveryChalan</param>
        /// <param name="p_Exp">Expenses</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool PostTranspoterExp(int p_Distributor_id,long p_SaleInvoiceId, int p_Transport_ID, string p_Bilty_no,string p_DilveryChallan,decimal  p_Exp)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertDTINVOICE_BILLTY_DETAIL InsertExp = new spInsertDTINVOICE_BILLTY_DETAIL();
                InsertExp.Connection = mConnection;
                
                InsertExp.DISTRIBUTOR_ID = p_Distributor_id;
                InsertExp.SALE_INVOICE_NO = p_SaleInvoiceId;  
                InsertExp.TRANSPOTER_NO = p_Transport_ID;
                InsertExp.BILTY_NO = p_Bilty_no;
                InsertExp.DELIVERY_CHALLAN_NO = p_DilveryChallan;
                InsertExp.TOTAL_EXPENCESS = p_Exp;
                return InsertExp.ExecuteQuery();  
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// Inserts Free SKU
        /// </summary>
        /// <param name="p_Distributor_id">Location</param>
        /// <param name="p_SaleInvoiceId">Invoice</param>
        /// <param name="p_sku_ID">SKU</param>
        /// <param name="p_QUANTITY">Quantitiy</param>
        /// <param name="p_UNIT_PRICE">Price</param>
        /// <param name="p_AMOUNT">Amount</param>
        /// <param name="p_GST_RATE">GSTRate</param>
        /// <param name="p_GST_AMOUNT">GSTAmount</param>
        /// <param name="p_DocumentDate">Date</param>
        /// <param name="p_TstAmount">TSTAmount</param>
        /// <param name="p_SedAmount">SEDAmount</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertFreeSKU(int p_Distributor_id, long p_SaleInvoiceId, int p_sku_ID, int p_QUANTITY, decimal p_UNIT_PRICE, decimal p_AMOUNT, float  p_GST_RATE, decimal p_GST_AMOUNT,DateTime p_DocumentDate,decimal p_TstAmount,decimal p_SedAmount)
        {

            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                //----------------Insert into sale order Promotion-------------
                spInsertManualSALE_INVOICE_PROMOTION mSaleOrderPromo = new spInsertManualSALE_INVOICE_PROMOTION();
                mSaleOrderPromo.Connection = mConnection;
                mSaleOrderPromo.BASKET_DETAIL_ID = -1;
                mSaleOrderPromo.BASKET_ID = -1;
                mSaleOrderPromo.DISTRIBUTOR_ID = p_Distributor_id;
                mSaleOrderPromo.GST_AMOUNT = p_GST_AMOUNT ;
                mSaleOrderPromo.GST_RATE = p_GST_RATE;
                mSaleOrderPromo.PROMOTION_ID = -1;
                mSaleOrderPromo.PROMOTION_OFFER_ID = -1;
                mSaleOrderPromo.QUANTITY = p_QUANTITY ;
                mSaleOrderPromo.SKU_ID = p_sku_ID ;
                mSaleOrderPromo.UNIT_PRICE = p_UNIT_PRICE ;
                mSaleOrderPromo.SALE_INVOICE_ID = p_SaleInvoiceId;
                mSaleOrderPromo.AMOUNT = p_AMOUNT;
                mSaleOrderPromo.SED_AMOUNT = p_SedAmount;
                mSaleOrderPromo.TST_AMOUNT = p_TstAmount;  
                mSaleOrderPromo.ExecuteQuery();

                UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                mStockUpdate.Connection = mConnection;
                mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                mStockUpdate.STOCK_DATE = p_DocumentDate;
                mStockUpdate.SKU_ID = mSaleOrderPromo.SKU_ID;
                mStockUpdate.BATCHNO = "N/A";
                mStockUpdate.STOCK_QTY = 0;
                mStockUpdate.FREE_QTY = p_QUANTITY;
                mStockUpdate.ExecuteQuery();
              
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        #region Rollback

        /// <summary>
        /// Rollbacks Order, Invoice And Sale Return
        /// </summary>
        /// <param name="p_DocumentId">Document</param>
        /// <param name="p_Type_Id">Type</param>
        /// <param name="p_LegendId">Legend</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdateRollBackDocument(long p_DocumentId, int p_Type_Id, int p_LegendId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                // UspRollBackDocumentTransection inside 
                UspRollBackDocumentTransection mOrder = new UspRollBackDocumentTransection();
                mOrder.Connection = mConnection;
                mOrder.DOCUMENT_ID = p_DocumentId;
                mOrder.DOCUMENT_TYPE = p_Type_Id;
                mOrder.LEGEND_ID = p_LegendId;
                mOrder.ExecuteQuery();
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

        #region Added By Hassan

        //public bool InsertLoadPass(long p_LOADPASS_ID2,int p_Distributor_id, int p_PRINCIPAL_ID, long p_AREA_ID, int p_DELIVERYMAN_ID,long p_VEHICLE_ID, DataTable dtOrderDetail, int p_UserId, DateTime p_Document_Date, int p_Customer_ID)
        //{

        //    IDbConnection mConnection = null;
        //    IDbTransaction mTransaction = null;
        //    try
        //    {
        //        mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        mConnection.Open();
        //        mTransaction = ProviderFactory.GetTransaction(mConnection);

        //        spInsertLOADPASS_MASTER mISom = new spInsertLOADPASS_MASTER();
        //        mISom.Connection = mConnection;
        //        mISom.Transaction = mTransaction;

        //        //------------Insert into Sale Order Master----------

        //        if (dtOrderDetail.Rows.Count > 0)
        //        {
        //            mISom.LOADPASS_ID2 =p_LOADPASS_ID2;
        //            mISom.DISTRIBUTOR_ID = p_Distributor_id;
        //            mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
        //            mISom.AREA_ID = p_AREA_ID;
        //            mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
        //            mISom.VEHICLE_ID = p_VEHICLE_ID;
        //            mISom.DOCUMENT_DATE = p_Document_Date;
        //            mISom.USER_ID = p_UserId;
        //            mISom.CUSTOMER_ID = p_Customer_ID;
        //            mISom.ExecuteQuery();
                     

        //            //----------------Insert into sale order detail-------------
        //            spInsertLOADPASS_DETAIL mSaleOrderDetail = new spInsertLOADPASS_DETAIL();
        //            mSaleOrderDetail.Connection = mConnection;
        //            mSaleOrderDetail.Transaction = mTransaction;

        //            foreach (DataRow dr in dtOrderDetail.Rows)
        //            {
        //                 mSaleOrderDetail.LOADPASS_ID = mISom.LOADPASS_ID;
        //                 mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
        //                mSaleOrderDetail.SKU_RATE = decimal.Parse(dr["SKU_RATE"].ToString());
        //                mSaleOrderDetail.DEMAND_QUANTITY = int.Parse(dr["DEMAND_QUANTITY"].ToString());
        //                mSaleOrderDetail.ISSUED_QUANTITY = int.Parse(dr["ISSUED_QUANTITY"].ToString());
        //                mSaleOrderDetail.RETURN_QUANTITY = int.Parse(dr["RETURN_QUANTITY"].ToString());
        //                mSaleOrderDetail.SALE_RETURN_QUANTITY = int.Parse(dr["SALE_RETURN_QUANTITY"].ToString());
        //                mSaleOrderDetail.PURCHASE_RETURN_QUANTITY = int.Parse(dr["PURCHASE_RETURN_QUANTITY"].ToString());
        //                mSaleOrderDetail.TIME_STAMP = DateTime.Now;
        //              //  mSaleOrderDetail.IS_PENDING = bool.Parse(dr["IS_PENDING"].ToString());
        //                mSaleOrderDetail.DOCUMENT_DATE = DateTime.Parse(dr["DOCUMENT_DATE"].ToString());
        //                mSaleOrderDetail.GST_RATE_TP = decimal.Parse(dr["GST_RATE_TP"].ToString());
        //                mSaleOrderDetail.ExecuteQuery();

        //            }
        //            mTransaction.Commit();
        //            return true;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);

        //        return false;// exp.Message;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }
        //    return true;
        //}

        public bool UpdateLoadPass(long p_LOADPASS_ID, int p_Distributor_id, int p_PRINCIPAL_ID, long p_AREA_ID, int p_DELIVERYMAN_ID, long p_VEHICLE_ID, DataTable dtOrderDetail, int p_UserId, DateTime p_Document_Date, int p_Customer_ID)
        {


            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdateLOADPASS_MASTER mISom = new spUpdateLOADPASS_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.LOADPASS_ID = p_LOADPASS_ID;
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERY_MAN_ID = p_DELIVERYMAN_ID;
                    mISom.VEHICLE_ID = p_VEHICLE_ID;
                    mISom.DOCUMENT_DATE = p_Document_Date;
                    mISom.USER_ID = p_UserId;
                    mISom.CUSTOMER_ID = p_Customer_ID;
                    mISom.LAST_UPDATE = DateTime.Now;
                    mISom.ExecuteQuery();


                    //----------------Insert into sale order detail-------------
                    spInsertLOADPASS_DETAIL mSaleOrderDetail = new spInsertLOADPASS_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.LOADPASS_ID = mISom.LOADPASS_ID;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.SKU_RATE = decimal.Parse(dr["SKU_RATE"].ToString());
                        mSaleOrderDetail.DEMAND_QUANTITY = int.Parse(dr["DEMAND_QUANTITY"].ToString());
                        mSaleOrderDetail.ISSUED_QUANTITY = int.Parse(dr["ISSUED_QUANTITY"].ToString());
                        mSaleOrderDetail.RETURN_QUANTITY = int.Parse(dr["RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.SALE_RETURN_QUANTITY = int.Parse(dr["SALE_RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.PURCHASE_RETURN_QUANTITY = int.Parse(dr["PURCHASE_RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.TIME_STAMP = DateTime.Now;
                        //  mSaleOrderDetail.IS_PENDING = bool.Parse(dr["IS_PENDING"].ToString());
                        mSaleOrderDetail.DOCUMENT_DATE = DateTime.Parse(dr["DOCUMENT_DATE"].ToString());
                        mSaleOrderDetail.GST_RATE_TP = decimal.Parse(dr["GST_RATE_TP"].ToString());
                        mSaleOrderDetail.ExecuteQuery();

                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool InsertLoadPass(int p_Distributor_id, int p_PRINCIPAL_ID, long p_AREA_ID, int p_DELIVERYMAN_ID, long p_VEHICLE_ID, DataTable dtOrderDetail, int p_UserId, DateTime p_Document_Date, int p_Customer_ID)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertLOADPASS_MASTER mISom = new spInsertLOADPASS_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                  
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.AREA_ID = p_AREA_ID;
                    mISom.DELIVERY_MAN_ID = p_DELIVERYMAN_ID;
                    mISom.VEHICLE_ID = p_VEHICLE_ID;
                    mISom.DOCUMENT_DATE = p_Document_Date;
                    mISom.TIME_STAMP = DateTime.Now;
                    mISom.LAST_UPDATE = DateTime.Now;
                    mISom.USER_ID = p_UserId;
                    mISom.IS_ACTIVE = true;
                    mISom.IS_CLOSED = false;
                    mISom.CUSTOMER_ID = p_Customer_ID;
                    mISom.ExecuteQuery();


                    //----------------Insert into sale order detail-------------
                    spInsertLOADPASS_DETAIL mSaleOrderDetail = new spInsertLOADPASS_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.LOADPASS_ID = mISom.LOADPASS_ID;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.SKU_RATE = decimal.Parse(dr["SKU_RATE"].ToString());
                        mSaleOrderDetail.DEMAND_QUANTITY = int.Parse(dr["DEMAND_QUANTITY"].ToString());
                        mSaleOrderDetail.ISSUED_QUANTITY = int.Parse(dr["ISSUED_QUANTITY"].ToString());
                        mSaleOrderDetail.RETURN_QUANTITY = int.Parse(dr["RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.SALE_RETURN_QUANTITY = int.Parse(dr["SALE_RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.PURCHASE_RETURN_QUANTITY = int.Parse(dr["PURCHASE_RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.TIME_STAMP = DateTime.Now;
                        //  mSaleOrderDetail.IS_PENDING = bool.Parse(dr["IS_PENDING"].ToString());
                        mSaleOrderDetail.DOCUMENT_DATE = DateTime.Parse(dr["DOCUMENT_DATE"].ToString());
                        mSaleOrderDetail.GST_RATE_TP = decimal.Parse(dr["GST_RATE_TP"].ToString());
                        mSaleOrderDetail.ExecuteQuery();

                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool UpdateLoadPass(long p_LOADPASS_ID, int p_Distributor_id, int p_PRINCIPAL_ID, long p_AREA_ID, int p_DELIVERYMAN_ID, long p_VEHICLE_ID, DataTable dtOrderDetail, int p_UserId, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUpdateLOADPASS_MASTER mISom = new spUpdateLOADPASS_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Sale Order Master----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.LOADPASS_ID = p_LOADPASS_ID;
                   // mISom.DISTRIBUTOR_ID = p_Distributor_id;
                  //  mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                  //  mISom.AREA_ID = p_AREA_ID;
                 //   mISom.DELIVERYMAN_ID = p_DELIVERYMAN_ID;
                 //   mISom.VEHICLE_ID = p_VEHICLE_ID;
                    mISom.DOCUMENT_DATE = p_Document_Date;
                    mISom.USER_ID = p_UserId;
                    mISom.LAST_UPDATE = DateTime.Now;
                     mISom.ExecuteQuery();


                    //----------------Insert into sale order detail-------------
                    spInsertLOADPASS_DETAIL2 mSaleOrderDetail = new spInsertLOADPASS_DETAIL2();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.LOADPASS_ID = mISom.LOADPASS_ID;
                        mSaleOrderDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.SKU_RATE = decimal.Parse(dr["SKU_RATE"].ToString());
                        mSaleOrderDetail.DEMAND_QUANTITY = int.Parse(dr["DEMAND_QUANTITY"].ToString());
                        mSaleOrderDetail.ISSUED_QUANTITY = int.Parse(dr["ISSUED_QUANTITY"].ToString());
                        mSaleOrderDetail.RETURN_QUANTITY = int.Parse(dr["RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.SALE_RETURN_QUANTITY = int.Parse(dr["SALE_RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.PURCHASE_RETURN_QUANTITY = int.Parse(dr["PURCHASE_RETURN_QUANTITY"].ToString());
                        mSaleOrderDetail.TIME_STAMP = DateTime.Now;
                        mSaleOrderDetail.IS_PENDING = bool.Parse(dr["IS_PENDING"].ToString());
                        mSaleOrderDetail.DOCUMENT_DATE = DateTime.Parse(dr["DOCUMENT_DATE"].ToString());
                        mSaleOrderDetail.ExecuteQuery();

                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
    
        
        public bool InsertFreeSKU2(long p_GIFT_MASTER_ID2, int p_Distributor_id, int p_PRINCIPAL_ID, long p_ACCOUNT_HEAD_ID, string p_REAMRKS,
            DataTable dtGiftDetail, int p_UserId, DateTime p_DocumentDate)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
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

                spInsertGIFT_SKU_MASTER mISom = new spInsertGIFT_SKU_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Gift SKU Master----------

                if (dtGiftDetail.Rows.Count > 0)
                {
                    mISom.GIFT_MASTER_ID2 = p_GIFT_MASTER_ID2;
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.ACCOUNT_HEAD_ID = p_ACCOUNT_HEAD_ID;
                    mISom.REMARKS = p_REAMRKS;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.USER_ID = p_UserId;

                    long GIFT_MASTER_ID2 = mISom.ExecuteQuery();

                    if (p_GIFT_MASTER_ID2 > 0)
                    {
                        DataTable dt = SelectPreviousGiftSKUDetail(p_GIFT_MASTER_ID2, mConnection, mTransaction);

                        foreach (DataRow dr in dt.Rows)
                        {
                            spDeleteGIFT_SKU_DETAIL mSale = new spDeleteGIFT_SKU_DETAIL();
                            mSale.Connection = mConnection;
                            mSale.Transaction = mTransaction;
                            mSale.GIFT_MASTER_ID = p_GIFT_MASTER_ID2;
                            mSale.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                            mSale.ExecuteQuery();
                        }
                    }
                    //----------------Insert into Gift SKU detail-------------

                    spInsertGIFT_SKU_DETAIL mGiftDetail = new spInsertGIFT_SKU_DETAIL();
                    mGiftDetail.Connection = mConnection;
                    mGiftDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtGiftDetail.Rows)
                    {
                        mGiftDetail.GIFT_MASTER_ID = mISom.GIFT_MASTER_ID;
                        mGiftDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mGiftDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mGiftDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mGiftDetail.GST_RATE = decimal.Parse(dr["GST_RATE"].ToString());
                        mGiftDetail.TIME_STAMP = p_DocumentDate;
                        mGiftDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mGiftDetail.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mGiftDetail.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A";
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mGiftDetail.QUANTITY_UNIT;
                        mStockUpdate.ExecuteQuery();


                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drScheme = dtVoucher.NewRow();
                            drScheme["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                            drScheme["REMARKS"] = "Gift SKU";
                            drScheme["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                            drScheme["CREDIT"] = 0;
                            drScheme["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drScheme);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Gift SKU(Stock In Hand)";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                            drStock["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drStock);
                        }
                    }

                    if (dtVoucher.Rows.Count > 0)
                    {
                        LedgerController LController = new LedgerController();
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate,mTransaction ,mConnection );

                       bool isinsert= LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Gift SKU Voucher", Constants.DateNullValue, null, mISom.GIFT_MASTER_ID, Constants.Document_FreeSKU, dtVoucher, p_UserId, null, Constants.DateNullValue,mTransaction ,mConnection );
                       if (!isinsert)
                       {
                           return false;
                       }
                    }

                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
        

        public long InsertFreeSKU3(long p_GIFT_MASTER_ID2, int p_Distributor_id, int p_PRINCIPAL_ID, long p_ACCOUNT_HEAD_ID, string p_REAMRKS,
          DataTable dtGiftDetail, int p_UserId, DateTime p_DocumentDate)
        {


            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
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

                spInsertGIFT_SKU_MASTER mISom = new spInsertGIFT_SKU_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Gift SKU Master----------

                if (dtGiftDetail.Rows.Count > 0)
                {
                    mISom.GIFT_MASTER_ID2 = p_GIFT_MASTER_ID2;
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.ACCOUNT_HEAD_ID = p_ACCOUNT_HEAD_ID;
                    mISom.REMARKS = p_REAMRKS;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.USER_ID = p_UserId;

                    long GIFT_MASTER_ID2 = mISom.ExecuteQuery();

                    if (p_GIFT_MASTER_ID2 > 0)
                    {
                        DataTable dt = SelectPreviousGiftSKUDetail(p_GIFT_MASTER_ID2, mConnection, mTransaction);

                        foreach (DataRow dr in dt.Rows)
                        {
                            spDeleteGIFT_SKU_DETAIL mSale = new spDeleteGIFT_SKU_DETAIL();
                            mSale.Connection = mConnection;
                            mSale.Transaction = mTransaction;
                            mSale.GIFT_MASTER_ID = p_GIFT_MASTER_ID2;
                            mSale.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                            mSale.ExecuteQuery();
                        }
                    }
                    //----------------Insert into Gift SKU detail-------------

                    spInsertGIFT_SKU_DETAIL mGiftDetail = new spInsertGIFT_SKU_DETAIL();
                    mGiftDetail.Connection = mConnection;
                    mGiftDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtGiftDetail.Rows)
                    {
                        mGiftDetail.GIFT_MASTER_ID = mISom.GIFT_MASTER_ID;
                        mGiftDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mGiftDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mGiftDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mGiftDetail.GST_RATE = decimal.Parse(dr["GST_RATE"].ToString());
                        mGiftDetail.TIME_STAMP = p_DocumentDate;
                        mGiftDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mGiftDetail.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mGiftDetail.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A";
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mGiftDetail.QUANTITY_UNIT;
                        mStockUpdate.ExecuteQuery();


                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drScheme = dtVoucher.NewRow();
                            drScheme["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                            drScheme["REMARKS"] = "Gift SKU";
                            drScheme["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                            drScheme["CREDIT"] = 0;
                            drScheme["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drScheme);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Gift SKU(Stock In Hand)";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                            drStock["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drStock);
                        }
                    }

                    if (dtVoucher.Rows.Count > 0)
                    {
                        LedgerController LController = new LedgerController();
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate, mTransaction, mConnection);

                        bool isinsert = LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Gift SKU Voucher", Constants.DateNullValue, null, mISom.GIFT_MASTER_ID, Constants.Document_FreeSKU, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants .LongNullValue ;
                        }
                    }

                    mTransaction.Commit();
                    return mISom.GIFT_MASTER_ID;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);

                return Constants.LongNullValue;// exp.Message;
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

        public long InsertFreeSKU3(long p_GIFT_MASTER_ID2, int p_Distributor_id, int p_PRINCIPAL_ID, long p_ACCOUNT_HEAD_ID, string p_REAMRKS,
            DataTable dtGiftDetail, int p_UserId, DateTime p_DocumentDate,out String Voucher_No)
        {


            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
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

                spInsertGIFT_SKU_MASTER mISom = new spInsertGIFT_SKU_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;

                //------------Insert into Gift SKU Master----------

                if (dtGiftDetail.Rows.Count > 0)
                {
                    mISom.GIFT_MASTER_ID2 = p_GIFT_MASTER_ID2;
                    mISom.DISTRIBUTOR_ID = p_Distributor_id;
                    mISom.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mISom.ACCOUNT_HEAD_ID = p_ACCOUNT_HEAD_ID;
                    mISom.REMARKS = p_REAMRKS;
                    mISom.DOCUMENT_DATE = p_DocumentDate;
                    mISom.USER_ID = p_UserId;

                    long GIFT_MASTER_ID2 = mISom.ExecuteQuery();

                    if (p_GIFT_MASTER_ID2 > 0)
                    {
                        DataTable dt = SelectPreviousGiftSKUDetail(p_GIFT_MASTER_ID2, mConnection, mTransaction);

                        foreach (DataRow dr in dt.Rows)
                        {
                            spDeleteGIFT_SKU_DETAIL mSale = new spDeleteGIFT_SKU_DETAIL();
                            mSale.Connection = mConnection;
                            mSale.Transaction = mTransaction;
                            mSale.GIFT_MASTER_ID = p_GIFT_MASTER_ID2;
                            mSale.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                            mSale.ExecuteQuery();
                        }
                    }
                    //----------------Insert into Gift SKU detail-------------

                    spInsertGIFT_SKU_DETAIL mGiftDetail = new spInsertGIFT_SKU_DETAIL();
                    mGiftDetail.Connection = mConnection;
                    mGiftDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtGiftDetail.Rows)
                    {
                        mGiftDetail.GIFT_MASTER_ID = mISom.GIFT_MASTER_ID;
                        mGiftDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mGiftDetail.UNIT_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mGiftDetail.QUANTITY_UNIT = int.Parse(dr["QUANTITY_UNIT"].ToString());
                        mGiftDetail.GST_RATE = decimal.Parse(dr["GST_RATE"].ToString());
                        mGiftDetail.TIME_STAMP = p_DocumentDate;
                        mGiftDetail.DISTRIBUTOR_ID = p_Distributor_id;
                        mGiftDetail.ExecuteQuery();

                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.TYPE_ID = Constants.Document_Invoice;
                        mStockUpdate.DISTRIBUTOR_ID = p_Distributor_id;
                        mStockUpdate.STOCK_DATE = p_DocumentDate;
                        mStockUpdate.SKU_ID = mGiftDetail.SKU_ID;
                        mStockUpdate.BATCHNO = "N/A";
                        mStockUpdate.STOCK_QTY = 0;
                        mStockUpdate.FREE_QTY = mGiftDetail.QUANTITY_UNIT;
                        mStockUpdate.ExecuteQuery();


                        DataRow[] foundRows = dtAccount.Select("SKU_ID  = '" + dr["SKU_ID"].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            DataRow drScheme = dtVoucher.NewRow();
                            drScheme["ACCOUNT_HEAD_ID"] = p_ACCOUNT_HEAD_ID;
                            drScheme["REMARKS"] = "Gift SKU";
                            drScheme["DEBIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                            drScheme["CREDIT"] = 0;
                            drScheme["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drScheme);

                            DataRow drStock = dtVoucher.NewRow();
                            drStock["ACCOUNT_HEAD_ID"] = foundRows[0]["STOCKINHAND_ID"];
                            drStock["REMARKS"] = "Gift SKU(Stock In Hand)";
                            drStock["DEBIT"] = 0;
                            drStock["CREDIT"] = decimal.Parse(dr["QUANTITY_UNIT"].ToString()) * decimal.Parse(dr["UNIT_PRICE"].ToString());
                            drStock["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(drStock);
                        }
                    }
                    Voucher_No = null;
                    if (dtVoucher.Rows.Count > 0)
                    {
                        LedgerController LController = new LedgerController();
                        string MaxDocumentId = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_Distributor_id, p_DocumentDate, mTransaction, mConnection);

                        bool isinsert = LController.Add_Voucher2(p_Distributor_id, 0, MaxDocumentId, Constants.Journal_Voucher, p_DocumentDate, Constants.CashPayment, "N/A", "Default Gift SKU Voucher", Constants.DateNullValue, null, mISom.GIFT_MASTER_ID, Constants.Document_FreeSKU, dtVoucher, p_UserId, null, Constants.DateNullValue, mTransaction, mConnection);
                        if (!isinsert)
                        {
                            return Constants.LongNullValue;
                        }
                        Voucher_No = MaxDocumentId;
                    }

                    mTransaction.Commit();
                    return mISom.GIFT_MASTER_ID;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                Voucher_No = null;
                return Constants.LongNullValue;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            Voucher_No = null;
            return Constants.LongNullValue;
            
        }
    
        public DataTable SelectLoadPassDetail(long p_LoadPass_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLOADPASS_DETAIL mOrderDetail = new spSelectLOADPASS_DETAIL();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.LOADPASS_ID = p_LoadPass_Id;

                DataTable dt = mOrderDetail.ExecuteTable();
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
      
        //public DataTable GetDocumentDetail2(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_AREA_ID, int p_DELIVERY_MAN_ID, DateTime p_FROM_DATE, DateTime p_TO_DATE)
        //{
        //    IDbConnection mConnection = null;
        //    try
        //    {
        //        //mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
        //        //mConnection.Open();
        //        //uspGetDocumentDetail2 mOrderDetail = new uspGetDocumentDetail2();
        //        //mOrderDetail.Connection = mConnection;
        //        //mOrderDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
        //        //mOrderDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
        //        //mOrderDetail.AREA_ID = p_AREA_ID;
        //        //mOrderDetail.DELIVERY_MAN_ID = p_DELIVERY_MAN_ID;
        //        //mOrderDetail.FROM_DATE = p_FROM_DATE;
        //        //mOrderDetail.TO_DATE = p_TO_DATE;

        //        //DataTable dt = mOrderDetail.ExecuteTable();
        //        //return dt;

        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionPublisher.PublishException(exp);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (mConnection != null && mConnection.State == ConnectionState.Open)
        //        {
        //            mConnection.Close();
        //        }
        //    }

        //}

        /// <summary>
        /// Gets Pending Orders
        /// </summary>
        /// <param name="p_Distributor_Id">Loation</param>
        /// <param name="p_Area_Id">Route</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <param name="p_Order_Booker">OrderBooker</param>
        /// <param name="p_DeliveryMan_Id">DeliveryMan</param>
        /// <param name="p_OrderStatus">Status</param>
        /// <param name="p_Ordertype">Type</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <returns>Pending Orders as Datatable</returns>
        public DataTable SelectPendingLoadPass(DateTime p_DOCUMENT_DATE,int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingLOADPASS mOrder = new UspSelectPendingLOADPASS();
                mOrder.Connection = mConnection;
                mOrder.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mOrder.USER_ID = p_USER_ID;
                DataTable dt = mOrder.ExecuteTable();
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
        public DataTable SelectPendingLoadPass(long p_Loadpass_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingLOADPASS_byID mOrder = new UspSelectPendingLOADPASS_byID();
                mOrder.Connection = mConnection;
                mOrder.LOADPASS_ID = p_Loadpass_Id;
                DataTable dt = mOrder.ExecuteTable();
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
        public DataTable SelectPendingLoadPass_ByDate(DateTime p_DOCUMENT_DATE,int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingLOADPASS_Bydate mOrder = new UspSelectPendingLOADPASS_Bydate();
                mOrder.Connection = mConnection;
                mOrder.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mOrder.USER_ID = p_USER_ID;
                DataTable dt = mOrder.ExecuteTable();
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

        public DataTable SelectPendingGiftSKU(DateTime p_DOCUMENT_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingGIFT_SKU_MASTER mOrder = new UspSelectPendingGIFT_SKU_MASTER();
                mOrder.Connection = mConnection;
                mOrder.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mOrder.ExecuteTable();
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
        public DataTable SelectGiftSKUDetail(long p_GIFT_MASTER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectGIFT_SKU_DETAIL mOrderDetail = new spSelectGIFT_SKU_DETAIL();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.GIFT_MASTER_ID = p_GIFT_MASTER_ID;

                DataTable dt = mOrderDetail.ExecuteTable();
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
        public DataTable SelectPreviousGiftSKUDetail(long p_GIFT_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectGIFT_SKU_DETAIL mOrderDetail = new spSelectGIFT_SKU_DETAIL();
                mOrderDetail.Connection = PConnection;
                mOrderDetail.Transaction = PTransaction;

                mOrderDetail.GIFT_MASTER_ID = p_GIFT_MASTER_ID;

                DataTable dt = mOrderDetail.ExecuteTable();
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

        //on Delivery Order form
        public DataTable SelectPendingOrder2(long pOrderId, int p_Ordertype)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingOrder2 mOrder = new UspSelectPendingOrder2();
                mOrder.Connection = mConnection;
                mOrder.documentId = pOrderId;
                mOrder.ORDER_TYPE_ID = p_Ordertype;
                DataTable dt = mOrder.ExecuteTable();
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

       //On delivery View
        public DataTable SelectPendingOrder2(int p_Distributor_Id, int p_Area_Id, int p_Principal_Id, int p_Order_Booker, int p_DeliveryMan_Id, int p_OrderStatus, int p_Ordertype, int p_UserId, DateTime p_DOCUMENT_DATE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPendingOrder2 mOrder = new UspSelectPendingOrder2();
                mOrder.Connection = mConnection;
                mOrder.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrder.AREA_ID = p_Area_Id;

                mOrder.USER_ID = p_UserId;

                mOrder.STATUS_ID = p_OrderStatus;
                mOrder.ORDER_TYPE_ID = p_Ordertype;
                mOrder.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mOrder.ExecuteTable();
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
        public DataTable SelectOrderDetail(int p_Distributor_Id, long pSaleInvoiceId, long p_SaleOrder_Id, DateTime ptimeStamp, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSALE_ORDER_DETAIL mOrderDetail = new spSelectSALE_ORDER_DETAIL();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrderDetail.SALE_ORDER_ID = p_SaleOrder_Id;
                mOrderDetail.SALE_ORDER_DETAIL_ID = pSaleInvoiceId;
                mOrderDetail.IS_DELETED = false;
                mOrderDetail.TYPE_ID = p_TYPE_ID;
                mOrderDetail.TIME_STAMP = ptimeStamp;

                DataTable dt = mOrderDetail.ExecuteTable();
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

        public DataTable SelectOrderDetail(int p_Distributor_Id, long p_SaleOrder_Id, DateTime ptimeStamp, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSALE_ORDER_DETAIL mOrderDetail = new spSelectSALE_ORDER_DETAIL();
                mOrderDetail.Connection = mConnection;
                mOrderDetail.DISTRIBUTOR_ID = p_Distributor_Id;
                mOrderDetail.SALE_ORDER_ID = p_SaleOrder_Id;
                mOrderDetail.IS_DELETED = false;
                mOrderDetail.TYPE_ID = p_TYPE_ID;
                mOrderDetail.TIME_STAMP = ptimeStamp;

                DataTable dt = mOrderDetail.ExecuteTable();
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

        public bool Add_CustomerWisePrice(DataTable dtOrderDetail, int p_UserId, DateTime p_DocumentDate, string p_groupName, DateTime p_DATE_EFFECTED, bool p_IsActive)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsert_CUST_PRICE_GROUP_MASTER mISom = new spInsert_CUST_PRICE_GROUP_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;
                mISom.TIME_STAMP = p_DocumentDate;
                mISom.GROUP_NAME = p_groupName;
                mISom.DATE_EFFECTED = p_DATE_EFFECTED;
                mISom.GROUP_DESCRIPTION = "";
                mISom.is_Deleted = p_IsActive;
                mISom.USER_ID = 1;

                //------------Insert into CUST_WISE_PRICE_GROUP_MASTER----------

                if (dtOrderDetail.Rows.Count > 0)
                {
                    mISom.GROUP_NAME = p_groupName;
                    mISom.TIME_STAMP = p_DocumentDate;
                    mISom.GROUP_DESCRIPTION = "";
                    mISom.USER_ID = p_UserId;


                    mISom.ExecuteQuery();

                    //----------------Insert into CUST_WISE_PRICE_GROUP_detail-------------
                    spInsert_CUST_PRICE_GROUP_DETAIL mSaleOrderDetail = new spInsert_CUST_PRICE_GROUP_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;
                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.CUST_WISE_GROUP_MASTER_ID = Convert.ToInt32(mISom.CUST_WISE_GROUP_MASTER_ID);

                        mSaleOrderDetail.SKU_CODE_ID = int.Parse(dr["SKU_ID"].ToString());
                        mSaleOrderDetail.SKU_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.SKU_ARTICAL_NO = int.Parse(dr["SKU_ARTICAL_NO"].ToString());
                        mSaleOrderDetail.GST_RATE = decimal.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.DATE_EFFECTED = DateTime.Parse(dr["DATE_EFFECTED"].ToString());
                        mSaleOrderDetail.ExecuteQuery();
                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool Update_CustomerWisePrice(DataTable dtOrderDetail, int p_UserId, DateTime p_DocumentDate, string p_groupName, int GroupMasterId, DateTime p_DATE_EFFECTED, bool p_IsActive)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spUPDATE_CUST_PRICE_GROUP_MASTER mISom = new spUPDATE_CUST_PRICE_GROUP_MASTER();
                mISom.Connection = mConnection;
                mISom.Transaction = mTransaction;
                mISom.GROUP_NAME = p_groupName;
                mISom.TIME_STAMP = p_DocumentDate;
                mISom.GROUP_DESCRIPTION = "";
                mISom.USER_ID = p_UserId;
                mISom.DATE_EFFECTED = DateTime.Now;
                mISom.CUST_WISE_GROUP_MASTER_ID = GroupMasterId;
                mISom.IS_DELETED = p_IsActive;
                mISom.ExecuteQuery();
                //------------Insert into CUST_WISE_PRICE_GROUP_MASTER----------

                if (dtOrderDetail.Rows.Count > 0)
                {

                    //----------------Insert into CUST_WISE_PRICE_GROUP_detail-------------
                    spInsert_CUST_PRICE_GROUP_DETAIL mSaleOrderDetail = new spInsert_CUST_PRICE_GROUP_DETAIL();
                    mSaleOrderDetail.Connection = mConnection;
                    mSaleOrderDetail.Transaction = mTransaction;
                    foreach (DataRow dr in dtOrderDetail.Rows)
                    {
                        mSaleOrderDetail.CUST_WISE_GROUP_MASTER_ID = Convert.ToInt32(mISom.CUST_WISE_GROUP_MASTER_ID);

                        mSaleOrderDetail.SKU_CODE_ID = int.Parse(dr["SKU_ID"].ToString());

                        mSaleOrderDetail.SKU_PRICE = decimal.Parse(dr["UNIT_PRICE"].ToString());
                        mSaleOrderDetail.SKU_ARTICAL_NO = int.Parse(dr["SKU_ARTICAL_NO"].ToString());
                        if (dr["GST_RATE"].ToString() == "")
                        {
                            dr["GST_RATE"] = "0";
                        }
                        if (dr["DATE_EFFECTED"].ToString() == "")
                        {
                            dr["DATE_EFFECTED"] = DateTime.Now;
                        }
                        mSaleOrderDetail.GST_RATE = decimal.Parse(dr["GST_RATE"].ToString());
                        mSaleOrderDetail.DATE_EFFECTED = Convert.ToDateTime(dr["DATE_EFFECTED"]);
                        mSaleOrderDetail.ExecuteQuery();
                    }

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }
    }	
 }