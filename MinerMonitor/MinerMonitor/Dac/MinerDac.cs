using Miner.Helper;
using System.Data.Common;
using System.Data;

namespace Miner.Dac
{

//    public class MinerDac : SqlHelper
//    {
//        public MinerDac()
//            : base(Miner.Library.DBConnType.Service_Miner)
//        {
//        }

//        #region ================= MM =================
//        public DataTable Insert_PurchaseOrder_Item_History(string PurchaseOrderNo, string PurchaseOrderItemNo, string InterfaceDate, string InterfaceTime, string DeletionFlag,
//            string Plant, string MaterialCode, string MaterialDescription, double Quantity, string UoM, double NetPrice, double Per, string Currency, string DeliveryDate)
//        {
//            string strSql = @"
// Insert Into BD.dbo.T_Collaboration_PurchaseOrder_Item_History
//Values (@PurchaseOrderNo, @PurchaseOrderItemNo, @InterfaceDate, @InterfaceTime, NULLIF(@DeletionFlag, ''), @Plant, @MaterialCode, @MaterialDescription, @Quantity, @UoM, @NetPrice, @Per, @Currency, null, @DeliveryDate, 'N')
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderNo", DbType.AnsiString, PurchaseOrderNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderItemNo", DbType.AnsiString, PurchaseOrderItemNo);
//            this.DbAccess.AddInParameter(dbCommand, "@InterfaceDate", DbType.AnsiString, InterfaceDate);
//            this.DbAccess.AddInParameter(dbCommand, "@InterfaceTime", DbType.AnsiString, InterfaceTime);
//            this.DbAccess.AddInParameter(dbCommand, "@DeletionFlag", DbType.AnsiString, DeletionFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialDescription", DbType.AnsiString, MaterialDescription);
//            this.DbAccess.AddInParameter(dbCommand, "@Quantity", DbType.Decimal, Quantity);
//            this.DbAccess.AddInParameter(dbCommand, "@UoM", DbType.AnsiString, UoM);
//            this.DbAccess.AddInParameter(dbCommand, "@NetPrice", DbType.Decimal, NetPrice);
//            this.DbAccess.AddInParameter(dbCommand, "@Per", DbType.Decimal, Per);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@DeliveryDate", DbType.AnsiString, DeliveryDate);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapPurchaseOrder(string PurchaseOrderNo, string PurchaseOrderItemNo, string PostingDate,
//            string MovementType, string MaterialDoc, string MaterialDocItem, string PurchasingOrg, string MaterialCode, string Plant, double Quantity, double NetPrice, string Currency, double PriceUnit)
//        {
//            string spName = "WMDM.dbo.up_SapPurchaseOrder_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderNo", DbType.AnsiString, PurchaseOrderNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderItemNo", DbType.AnsiString, PurchaseOrderItemNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingDate", DbType.AnsiString, PostingDate);
//            this.DbAccess.AddInParameter(dbCommand, "@MovementType", DbType.AnsiString, MovementType);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialDoc", DbType.AnsiString, MaterialDoc);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialDocItem", DbType.AnsiString, MaterialDocItem);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingOrg", DbType.AnsiString, PurchasingOrg);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@Quantity", DbType.Decimal, Quantity);
//            this.DbAccess.AddInParameter(dbCommand, "@NetPrice", DbType.Decimal, NetPrice);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@PriceUnit", DbType.Decimal, PriceUnit);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapCustomerCompany(string CustomerNo, string CompanyCode, string ReconAccount, string PaymentMethods, string TermsOfPaymentKey, string CreateDate, string PostingBlock
//            , string DeleteFlag)
//        {
//            string spName = "WMDM.dbo.up_SapCustomerCompany_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CustomerNo", DbType.AnsiString, CustomerNo);
//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@ReconAccount", DbType.AnsiString, ReconAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@PaymentMethod", DbType.AnsiString, PaymentMethods);
//            this.DbAccess.AddInParameter(dbCommand, "@TermsOfPaymentKey", DbType.AnsiString, TermsOfPaymentKey);
//            this.DbAccess.AddInParameter(dbCommand, "@CreateDate", DbType.AnsiString, CreateDate);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingBlock", DbType.AnsiString, PostingBlock);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteFlag", DbType.AnsiString, DeleteFlag);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapCodes(string CodeType, string Code, string CodeName)
//        {
//            string spName = "WMDM.dbo.up_SapCodes_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, "A150");
//            this.DbAccess.AddInParameter(dbCommand, "@CodeType", DbType.AnsiString, CodeType);
//            this.DbAccess.AddInParameter(dbCommand, "@Code", DbType.AnsiString, Code);
//            this.DbAccess.AddInParameter(dbCommand, "@CodeName", DbType.AnsiString, CodeName);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_Materials(string MaterialCode, string MaterialName, string MaterialType, string MaterialGroup, string MaterialGroupName)
//        {
//            string strSql = @"
// Insert Into WMDM.dbo.Materials
//Values (@MaterialCode, @MaterialName, @MaterialType, @MaterialGroup, @MaterialGroupName, '', 'N', CONVERT(VARCHAR(8), GETDATE(), 112), replace(Convert (varchar(8),GetDate(), 108),':',''))
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialName", DbType.AnsiString, MaterialName);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialType", DbType.AnsiString, MaterialType);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialGroup", DbType.AnsiString, MaterialGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialGroupName", DbType.AnsiString, MaterialGroupName);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapCompanyPlant(string Plant, string PlantName, string CompanyCode)
//        {
//            string spName = "WMDM.dbo.up_SapCompanyPlant_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@PlantName", DbType.AnsiString, PlantName);
//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPMaterials(string MaterialCode, string MaterialName, string MaterialType, string MaterialGroup, string MaterialGroupName, string BaseUnit, string EANCode,
//                                              string ProductHierarchy, string Division, string LaboratoryOffice, string XPlantMaterialStatus, string Manufacture, string ManufactureName, string ManufacturePartNo,
//                                                  string DeleteFlag, string Remark)
//        {
//            string spName = "WMDM.dbo.up_SapMaterial_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialName", DbType.AnsiString, MaterialName);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialType", DbType.AnsiString, MaterialType);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialGroup", DbType.AnsiString, MaterialGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialGroupName", DbType.AnsiString, MaterialGroupName);
//            this.DbAccess.AddInParameter(dbCommand, "@BaseUnit", DbType.AnsiString, BaseUnit);
//            this.DbAccess.AddInParameter(dbCommand, "@EANCode", DbType.AnsiString, EANCode);
//            this.DbAccess.AddInParameter(dbCommand, "@ProductHierarchy", DbType.AnsiString, ProductHierarchy);
//            this.DbAccess.AddInParameter(dbCommand, "@Division", DbType.AnsiString, Division);
//            this.DbAccess.AddInParameter(dbCommand, "@LaboratoryOffice", DbType.AnsiString, LaboratoryOffice);
//            this.DbAccess.AddInParameter(dbCommand, "@XPlantMaterialStatus", DbType.AnsiString, XPlantMaterialStatus);
//            this.DbAccess.AddInParameter(dbCommand, "@Manufacture", DbType.AnsiString, Manufacture);
//            this.DbAccess.AddInParameter(dbCommand, "@ManufactureName", DbType.AnsiString, ManufactureName);
//            this.DbAccess.AddInParameter(dbCommand, "@ManufacturePartNo", DbType.AnsiString, ManufacturePartNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteFlag", DbType.AnsiString, DeleteFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@Remark", DbType.AnsiString, Remark);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPPlantMaterials(string Plant, string MaterialCode, string ProdStorageLoc, string DiscontIndicator, string FUMaterial, string PlannedDelivTime, string SchedMarginKey, string GRProcessTime,
//                                              string PurchaingGroup, string BackflushIndicator, string RoundingValue, string MRPGroup, string ComponentScrap)
//        {
//            string spName = "WMDM.dbo.up_SapPlantMaterial_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@ProdStorageLoc", DbType.AnsiString, ProdStorageLoc);
//            this.DbAccess.AddInParameter(dbCommand, "@DiscontIndicator", DbType.AnsiString, DiscontIndicator);
//            this.DbAccess.AddInParameter(dbCommand, "@FUMaterial", DbType.AnsiString, FUMaterial);
//            this.DbAccess.AddInParameter(dbCommand, "@PlannedDelivTime", DbType.Decimal, PlannedDelivTime);
//            this.DbAccess.AddInParameter(dbCommand, "@SchedMarginKey", DbType.AnsiString, SchedMarginKey);
//            this.DbAccess.AddInParameter(dbCommand, "@GRProcessTime", DbType.Decimal, GRProcessTime);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchaingGroup", DbType.AnsiString, PurchaingGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@BackflushIndicator", DbType.AnsiString, BackflushIndicator);
//            this.DbAccess.AddInParameter(dbCommand, "@RoundingValue", DbType.Decimal, RoundingValue);
//            this.DbAccess.AddInParameter(dbCommand, "@MRPGroup", DbType.AnsiString, MRPGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@ComponentScrap", DbType.Decimal, ComponentScrap);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapCustomerGeneral(string CustomerNo, string CustomerName, string CountryCode, string CorpRegNumber, string VatNo, string Street, string City, string Region, string RegionDesc
//            , string PostalCode, string TelephoneNo, string FaxNo, string DeleteBlock, string NameOfRepresentative, string TypeOfBusiness, string TypeOfIndustry, string Email)
//        {
//            string spName = "WMDM.dbo.up_SapCustomerGeneral_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CustomerNo", DbType.AnsiString, CustomerNo);
//            this.DbAccess.AddInParameter(dbCommand, "@CustomerName", DbType.AnsiString, CustomerName);
//            this.DbAccess.AddInParameter(dbCommand, "@CountryCode", DbType.AnsiString, CountryCode);
//            this.DbAccess.AddInParameter(dbCommand, "@CorpRegNumber", DbType.AnsiString, CorpRegNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@VatNo", DbType.AnsiString, VatNo);
//            this.DbAccess.AddInParameter(dbCommand, "@Street", DbType.AnsiString, Street);
//            this.DbAccess.AddInParameter(dbCommand, "@City", DbType.AnsiString, City);
//            this.DbAccess.AddInParameter(dbCommand, "@Region", DbType.AnsiString, Region);
//            this.DbAccess.AddInParameter(dbCommand, "@RegionDesc", DbType.AnsiString, RegionDesc);
//            this.DbAccess.AddInParameter(dbCommand, "@PostalCode", DbType.AnsiString, PostalCode);
//            this.DbAccess.AddInParameter(dbCommand, "@TelephoneNo", DbType.AnsiString, TelephoneNo);
//            this.DbAccess.AddInParameter(dbCommand, "@FaxNo", DbType.AnsiString, FaxNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteBlock", DbType.AnsiString, DeleteBlock);
//            this.DbAccess.AddInParameter(dbCommand, "@NameOfRepresentative", DbType.AnsiString, NameOfRepresentative);
//            this.DbAccess.AddInParameter(dbCommand, "@TypeOfBusiness", DbType.AnsiString, TypeOfBusiness);
//            this.DbAccess.AddInParameter(dbCommand, "@TypeOfIndustry", DbType.AnsiString, TypeOfIndustry);
//            this.DbAccess.AddInParameter(dbCommand, "@Email", DbType.AnsiString, Email);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapCustomerSalesOrg(string CustomerNo, string SalesOrg, string DistributionChannel, string Division, string DeliveringPlant, string TermsOfPaymentKey, string Currency)
//        {
//            string spName = "WMDM.dbo.up_SapCustomerSalesOrg_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CustomerNo", DbType.AnsiString, CustomerNo);
//            this.DbAccess.AddInParameter(dbCommand, "@SalesOrg", DbType.AnsiString, SalesOrg);
//            this.DbAccess.AddInParameter(dbCommand, "@DistributionChannel", DbType.AnsiString, DistributionChannel);
//            this.DbAccess.AddInParameter(dbCommand, "@Division", DbType.AnsiString, Division);
//            this.DbAccess.AddInParameter(dbCommand, "@DeliveringPlant", DbType.AnsiString, DeliveringPlant);
//            this.DbAccess.AddInParameter(dbCommand, "@TermsOfPaymentKey", DbType.AnsiString, TermsOfPaymentKey);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_GLAccount(string CompanyCode, string GLAccount, string LanguageKey, string GLAccountName, string GLType)
//        {
//            string spName = "WMDM.dbo.up_GLAccount_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@LanguageKey", DbType.AnsiString, LanguageKey);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccountName", DbType.AnsiString, GLAccountName);
//            this.DbAccess.AddInParameter(dbCommand, "@GLType", DbType.AnsiString, GLType);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_GLAccountTaxCode(string CompanyCode, string GLAccount, string TaxCategory, string Indicator)
//        {
//            string strSql = @"
// IF EXISTS(SELECT 'W' FROM WebDev.dbo.Budget_GLAccountTaxCode WHERE CompanyCode = @CompanyCode AND GLAccount = @GLAccount) BEGIN  
//	UPDATE WebDev.dbo.Budget_GLAccountTaxCode SET TaxCategory = @TaxCategory, Indicator = @Indicator, InterfaceDate = CONVERT(CHAR(8), GETDATE(), 112), InterfaceTime = Replace(CONVERT(VARCHAR(8),GETDATE(),108),':','') WHERE CompanyCode = @CompanyCode and GLAccount = @GLAccount
//END
//ELSE BEGIN
//INSERT INTO WebDev.dbo.Budget_GLAccountTaxCode (CompanyCode, GLAccount, TaxCategory, Indicator, InterfaceFlag, InterfaceDate, InterfaceTime)
//	VALUES (@CompanyCode, @GLAccount, @TaxCategory, @Indicator, 'N', CONVERT(CHAR(8), GETDATE(), 112), Replace(CONVERT(VARCHAR(8),GETDATE(),108),':',''))
//END
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxCategory", DbType.AnsiString, TaxCategory);
//            this.DbAccess.AddInParameter(dbCommand, "@Indicator", DbType.AnsiString, Indicator);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_Cost_ExtraSAPData(string CompanyCode, string FiscalYear, string AccountDoc, string ItemIdx, string IsDebitCredit, string DocCurrency, double DocAmount, double LocAmount
//            , string GLAccount, string CostCenter, string CostCenterName, string Customer, string Vendor, string Note, string PostingDate, string INTERFACE_FLAG, string INTERFACE_DATE, string INTERFACE_TIME)
//        {
//            string strSql = @"
// Insert Into WebDev.dbo.IB_Cost_ExtraSAPData
//Values (@CompanyCode, @FiscalYear, @AccountDoc, @ItemIdx, @IsDebitCredit, @DocCurrency, @DocAmount, @LocAmount, @GLAccount, @CostCenter, @CostCenterName, @Customer, @Vendor
//, @Note, @PostingDate, @INTERFACE_FLAG, @INTERFACE_DATE, @INTERFACE_TIME)";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@FiscalYear", DbType.AnsiString, FiscalYear);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountDoc", DbType.AnsiString, AccountDoc);
//            this.DbAccess.AddInParameter(dbCommand, "@ItemIdx", DbType.AnsiString, ItemIdx);
//            this.DbAccess.AddInParameter(dbCommand, "@IsDebitCredit", DbType.AnsiString, IsDebitCredit);
//            this.DbAccess.AddInParameter(dbCommand, "@DocCurrency", DbType.AnsiString, DocCurrency);
//            this.DbAccess.AddInParameter(dbCommand, "@DocAmount", DbType.Decimal, DocAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@LocAmount", DbType.Decimal, LocAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@CostCenter", DbType.AnsiString, CostCenter);
//            this.DbAccess.AddInParameter(dbCommand, "@CostCenterName", DbType.AnsiString, CostCenterName);
//            this.DbAccess.AddInParameter(dbCommand, "@Customer", DbType.AnsiString, Customer);
//            this.DbAccess.AddInParameter(dbCommand, "@Vendor", DbType.AnsiString, Vendor);
//            this.DbAccess.AddInParameter(dbCommand, "@Note", DbType.AnsiString, Note);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingDate", DbType.AnsiString, PostingDate);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_FLAG", DbType.AnsiString, INTERFACE_FLAG);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_TIME", DbType.AnsiString, INTERFACE_TIME);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }


//        public DataTable Insert_SAPVendor_General(string VendorNo, string CountryKey, string VendorName1, string VendorName2, string City, string District, string PostalCode, string Region, string Street, string AddrNo, string Title
//            , string CreateDate, string VendorAccGroup, string CustomerNo, string DeleteFlag, string PostingBlock, string PurchasingBlock, string TaxNo1, string TaxNo2, string TeleboxNo, string TelNo1, string TelNo2, string FaxNo
//            , string TradingPartner, string VatNo, string Plant, string TaxJurisdiction, string NameOfRepresentative, string TypeOfBusiness, string TypeOfIndustry, string VendorTypeDesc, string DeleteBlock)
//        {
//            string spName = "WMDM.dbo.up_SapVendorGeneral_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@CountryKey", DbType.AnsiString, CountryKey);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorName1", DbType.AnsiString, VendorName1);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorName2", DbType.AnsiString, VendorName2);
//            this.DbAccess.AddInParameter(dbCommand, "@City", DbType.AnsiString, City);
//            this.DbAccess.AddInParameter(dbCommand, "@District", DbType.AnsiString, District);
//            this.DbAccess.AddInParameter(dbCommand, "@PostalCode", DbType.AnsiString, PostalCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Region", DbType.AnsiString, Region);
//            this.DbAccess.AddInParameter(dbCommand, "@Street", DbType.AnsiString, Street);
//            this.DbAccess.AddInParameter(dbCommand, "@AddrNo", DbType.AnsiString, AddrNo);
//            this.DbAccess.AddInParameter(dbCommand, "@Title", DbType.AnsiString, Title);
//            this.DbAccess.AddInParameter(dbCommand, "@CreateDate", DbType.AnsiString, CreateDate);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorAccGroup", DbType.AnsiString, VendorAccGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@CustomerNo", DbType.AnsiString, CustomerNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteFlag", DbType.AnsiString, DeleteFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingBlock", DbType.AnsiString, PostingBlock);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingBlock", DbType.AnsiString, PurchasingBlock);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxNo1", DbType.AnsiString, TaxNo1);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxNo2", DbType.AnsiString, TaxNo2);
//            this.DbAccess.AddInParameter(dbCommand, "@TeleboxNo", DbType.AnsiString, TeleboxNo);
//            this.DbAccess.AddInParameter(dbCommand, "@TelNo1", DbType.AnsiString, TelNo1);
//            this.DbAccess.AddInParameter(dbCommand, "@TelNo2", DbType.AnsiString, TelNo2);
//            this.DbAccess.AddInParameter(dbCommand, "@FaxNo", DbType.AnsiString, FaxNo);
//            this.DbAccess.AddInParameter(dbCommand, "@TradingPartner", DbType.AnsiString, TradingPartner);
//            this.DbAccess.AddInParameter(dbCommand, "@VatNo", DbType.AnsiString, VatNo);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxJurisdiction", DbType.AnsiString, TaxJurisdiction);
//            this.DbAccess.AddInParameter(dbCommand, "@NameOfRepresentative", DbType.AnsiString, NameOfRepresentative);
//            this.DbAccess.AddInParameter(dbCommand, "@TypeOfBusiness", DbType.AnsiString, TypeOfBusiness);
//            this.DbAccess.AddInParameter(dbCommand, "@TypeOfIndustry", DbType.AnsiString, TypeOfIndustry);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorTypeDesc", DbType.AnsiString, VendorTypeDesc);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteBlock", DbType.AnsiString, DeleteBlock);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPVendor_Company(string VendorNo, string CompanyCode, string CreateDate, string PostingBlock, string DeleteFlag, string KeyForSorting, string ReconAccount, string PaymentMethod, string ClearingIndicator, string BlockKeyForPayment
//            , string TermsOfPaymentKey, string HeadOfficeAccountNo, string AccountNoOfAltPayee, string ShortKeyForHouseBank, string WithholdingTaxCode, string TimeOfLastChangeConf, string DeleteBlock)
//        {
//            string spName = "WMDM.dbo.up_SapVendorCompany_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@CreateDate", DbType.AnsiString, CreateDate);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingBlock", DbType.AnsiString, PostingBlock);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteFlag", DbType.AnsiString, DeleteFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@KeyForSorting", DbType.AnsiString, KeyForSorting);
//            this.DbAccess.AddInParameter(dbCommand, "@ReconAccount", DbType.AnsiString, ReconAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@PaymentMethod", DbType.AnsiString, PaymentMethod);
//            this.DbAccess.AddInParameter(dbCommand, "@ClearingIndicator", DbType.AnsiString, ClearingIndicator);
//            this.DbAccess.AddInParameter(dbCommand, "@BlockKeyForPayment", DbType.AnsiString, BlockKeyForPayment);
//            this.DbAccess.AddInParameter(dbCommand, "@TermsOfPaymentKey", DbType.AnsiString, TermsOfPaymentKey);
//            this.DbAccess.AddInParameter(dbCommand, "@HeadOfficeAccountNo", DbType.AnsiString, HeadOfficeAccountNo);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountNoOfAltPayee", DbType.AnsiString, AccountNoOfAltPayee);
//            this.DbAccess.AddInParameter(dbCommand, "@ShortKeyForHouseBank", DbType.AnsiString, ShortKeyForHouseBank);
//            this.DbAccess.AddInParameter(dbCommand, "@WithholdingTaxCode", DbType.AnsiString, WithholdingTaxCode);
//            this.DbAccess.AddInParameter(dbCommand, "@TimeOfLastChangeConf", DbType.AnsiString, TimeOfLastChangeConf);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteBlock", DbType.AnsiString, DeleteBlock);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPVendor_Purchasing(string VendorNo, string PurchasingOrg, string CreateDate, string PurchasingBlock, string DeleteFlag, string AbcIndicator, string PoCurrency, string TermsOfPaymentKey, string Incoterms1, string Incoterms2
//           , string GrBasedIvIndicator, string CustomsOffice, string PurchasingGroup, double PlannedDeliveryTime, string ConfControlKey)
//        {
//            string spName = "WMDM.dbo.up_SapVendorPurchasing_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingOrg", DbType.AnsiString, PurchasingOrg);
//            this.DbAccess.AddInParameter(dbCommand, "@CreateDate", DbType.AnsiString, CreateDate);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingBlock", DbType.AnsiString, PurchasingBlock);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteFlag", DbType.AnsiString, DeleteFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@AbcIndicator", DbType.AnsiString, AbcIndicator);
//            this.DbAccess.AddInParameter(dbCommand, "@PoCurrency", DbType.AnsiString, PoCurrency);
//            this.DbAccess.AddInParameter(dbCommand, "@TermsOfPaymentKey", DbType.AnsiString, TermsOfPaymentKey);
//            this.DbAccess.AddInParameter(dbCommand, "@Incoterms1", DbType.AnsiString, Incoterms1);
//            this.DbAccess.AddInParameter(dbCommand, "@Incoterms2", DbType.AnsiString, Incoterms2);
//            this.DbAccess.AddInParameter(dbCommand, "@GrBasedIvIndicator", DbType.AnsiString, GrBasedIvIndicator);
//            this.DbAccess.AddInParameter(dbCommand, "@CustomsOffice", DbType.AnsiString, CustomsOffice);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingGroup", DbType.AnsiString, PurchasingGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@PlannedDeliveryTime", DbType.Decimal, PlannedDeliveryTime);
//            this.DbAccess.AddInParameter(dbCommand, "@ConfControlKey", DbType.AnsiString, ConfControlKey);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPVendor_Bank(string VendorNo, string BankCountryKey, string BankKey, string BankAccountNo, string BankDescription, string AccountHolder, string PartnerBankType, string InterfaceDate)
//        {
//            string spName = "WMDM.dbo.up_SapVendorBank_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@BankCountryKey", DbType.AnsiString, BankCountryKey);
//            this.DbAccess.AddInParameter(dbCommand, "@BankKey", DbType.AnsiString, BankKey);
//            this.DbAccess.AddInParameter(dbCommand, "@BankAccountNo", DbType.AnsiString, BankAccountNo);
//            this.DbAccess.AddInParameter(dbCommand, "@BankDescription", DbType.AnsiString, BankDescription);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountHolder", DbType.AnsiString, AccountHolder);
//            this.DbAccess.AddInParameter(dbCommand, "@PartnerBankType", DbType.AnsiString, PartnerBankType);
//            this.DbAccess.AddInParameter(dbCommand, "@InterfaceDate", DbType.AnsiString, InterfaceDate);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPVendor_Email(string VendorNo, string AddressNumber, int SequenceNo, string IsDefault, string Email, string IsDelete)
//        {
//            string spName = "WMDM.dbo.up_SapVendorEmail_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@AddressNumber", DbType.AnsiString, AddressNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@SequenceNo", DbType.Int32, SequenceNo);
//            this.DbAccess.AddInParameter(dbCommand, "@IsDefault", DbType.AnsiString, IsDefault);
//            this.DbAccess.AddInParameter(dbCommand, "@Email", DbType.AnsiString, Email);
//            this.DbAccess.AddInParameter(dbCommand, "@IsDelete", DbType.AnsiString, IsDelete);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPPurchasingGroup(string PurchasingGroup, string PurchasingGroupName, string PurchasingGroupMail)
//        {
//            string spName = "WMDM.dbo.up_SapPurchasingGroup_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingGroup", DbType.AnsiString, PurchasingGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingGroupName", DbType.AnsiString, PurchasingGroupName);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingGroupMail", DbType.AnsiString, PurchasingGroupMail);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPInfoRecordGeneral(string InfoRecordNumber, string VendorNo, string MaterialCode, string DeletionFlag)
//        {

//            string spName = "WMDM.dbo.up_SAPInfoRecordGeneral_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@InfoRecordNumber", DbType.AnsiString, InfoRecordNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@DeletionFlag", DbType.AnsiString, DeletionFlag);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPInfoRecordPurchasing(string InfoRecordNumber, string Plant, string PurchasingOrg, string PurchasingCategory, string ValidFrom, string ValidTo, string DeleteFlag, string Currency, string LastOrderDate
//            , double NetPrice, double PriceUnit)
//        {

//            string spName = "WMDM.dbo.up_SapInfoRecordPurchasing_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@InfoRecordNumber", DbType.AnsiString, InfoRecordNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingOrg", DbType.AnsiString, PurchasingOrg);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingCategory", DbType.AnsiString, PurchasingCategory);
//            this.DbAccess.AddInParameter(dbCommand, "@ValidFrom", DbType.AnsiString, ValidFrom);
//            this.DbAccess.AddInParameter(dbCommand, "@ValidTo", DbType.AnsiString, ValidTo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeleteFlag", DbType.AnsiString, DeleteFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@LastOrderDate", DbType.AnsiString, LastOrderDate);
//            this.DbAccess.AddInParameter(dbCommand, "@NetPrice", DbType.Decimal, NetPrice);
//            this.DbAccess.AddInParameter(dbCommand, "@PriceUnit", DbType.Decimal, PriceUnit);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAP_Vat_To_Web(int ApprovalItemIdx, string SAP_FiscalYear, string SAP_AccountDoc, string SAP_PostingDate, double SAP_SupplyAmount, double SAP_VAT, string SAP_TaxOffice
//            , string SAP_SalesVATCode, string SAP_Status)
//        {
//            string spName = "CardDB.dbo.UP_IF_G_SAP_VAT_TO_WEB";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);


//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalItemIdx", DbType.Int32, ApprovalItemIdx);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_FiscalYear", DbType.AnsiString, SAP_FiscalYear);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_AccountDoc", DbType.AnsiString, SAP_AccountDoc);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_PostingDate", DbType.AnsiString, SAP_PostingDate);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_SupplyAmount", DbType.Decimal, SAP_SupplyAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_VAT", DbType.Decimal, SAP_VAT);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_TaxOffice", DbType.AnsiString, SAP_TaxOffice);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_SalesVATCode", DbType.AnsiString, SAP_SalesVATCode);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_Status", DbType.AnsiString, SAP_Status);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAP_To_Web(int ApprovalItemIdx, string ReceiptDate, string SAP_Status)
//        {
//            string spName = "CardDB.dbo.UP_IF_G_SAP_TO_WEB";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);


//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalItemIdx", DbType.Int32, ApprovalItemIdx);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiptDate", DbType.AnsiString, ReceiptDate);
//            this.DbAccess.AddInParameter(dbCommand, "@SAP_Status", DbType.AnsiString, SAP_Status);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SAPMaterialSourceList(string MaterialCode, string Plant, string SourceListSeq, string CreateDate, string VendorNo, string FromDate, string ToDate, string PurchasingOrg, string FixedVendor
//    , string BlockIndicator, string MRPIndicator)
//        {

//            string spName = "WMDM.dbo.up_SapMaterialSourceList_Insert ";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@SourceListSeq", DbType.AnsiString, SourceListSeq);
//            this.DbAccess.AddInParameter(dbCommand, "@CreateDate", DbType.AnsiString, CreateDate);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorNo", DbType.AnsiString, VendorNo);
//            this.DbAccess.AddInParameter(dbCommand, "@FromDate", DbType.AnsiString, FromDate);
//            this.DbAccess.AddInParameter(dbCommand, "@ToDate", DbType.AnsiString, ToDate);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingOrg", DbType.AnsiString, PurchasingOrg);
//            this.DbAccess.AddInParameter(dbCommand, "@FixedVendor", DbType.AnsiString, FixedVendor);
//            this.DbAccess.AddInParameter(dbCommand, "@BlockIndicator", DbType.AnsiString, BlockIndicator);
//            this.DbAccess.AddInParameter(dbCommand, "@MRPIndicator", DbType.AnsiString, MRPIndicator);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_T_Collaboration_Delivery_Header(string DeleveryNo, string DeliveryComplete, string GRPostingDate, string InterfaceDate)
//        {

//            string strSql = @"
//Update BD.dbo.T_Collaboration_Delivery_Header
//set DeliveryComplete = NULLIF(@DeliveryComplete, ''), GRPostingDate = NULLIF(@GRPostingDate, ''), UpdatedDate = @UpdatedDate
//Where DeliveryNo = @DeliveryNo
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@DeliveryNo", DbType.AnsiString, DeleveryNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeliveryComplete", DbType.AnsiString, DeliveryComplete);
//            this.DbAccess.AddInParameter(dbCommand, "@GRPostingDate", DbType.AnsiString, GRPostingDate);
//            this.DbAccess.AddInParameter(dbCommand, "@UpdatedDate", DbType.AnsiString, InterfaceDate);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_BudgetDepreciation(string SAPCompanyCode, string Year, string CostCenter, string GLAccount, double Month01, double Month02, double Month03, double Month04
//            , double Month05, double Month06, double Month07, double Month08, double Month09, double Month10, double Month11, double Month12, string Currency)
//        {

//            string strSql = @"
// Insert Into WebDev.dbo.I_Budget_Depreciation
//Values (@SAPCompanyCode, @Year, @CostCenter, @GLAccount, @Month01, @Month02, @Month03, @Month04, @Month05, @Month06, @Month07, @Month08, @Month09, @Month10, @Month11, @Month12
//, @Currency,'N', CONVERT(VARCHAR(8), GETDATE(), 112), replace(Convert (varchar(8),GetDate(), 108),':',''))
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@SAPCompanyCode", DbType.AnsiString, SAPCompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Year", DbType.AnsiString, Year);
//            this.DbAccess.AddInParameter(dbCommand, "@CostCenter", DbType.AnsiString, CostCenter);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@Month01", DbType.Decimal, Month01);
//            this.DbAccess.AddInParameter(dbCommand, "@Month02", DbType.Decimal, Month02);
//            this.DbAccess.AddInParameter(dbCommand, "@Month03", DbType.Decimal, Month03);
//            this.DbAccess.AddInParameter(dbCommand, "@Month04", DbType.Decimal, Month04);
//            this.DbAccess.AddInParameter(dbCommand, "@Month05", DbType.Decimal, Month05);
//            this.DbAccess.AddInParameter(dbCommand, "@Month06", DbType.Decimal, Month06);
//            this.DbAccess.AddInParameter(dbCommand, "@Month07", DbType.Decimal, Month07);
//            this.DbAccess.AddInParameter(dbCommand, "@Month08", DbType.Decimal, Month08);
//            this.DbAccess.AddInParameter(dbCommand, "@Month09", DbType.Decimal, Month09);
//            this.DbAccess.AddInParameter(dbCommand, "@Month10", DbType.Decimal, Month10);
//            this.DbAccess.AddInParameter(dbCommand, "@Month11", DbType.Decimal, Month11);
//            this.DbAccess.AddInParameter(dbCommand, "@Month12", DbType.Decimal, Month12);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_ExpenseUSEReturnOutAsync(int ApprovalItemIdx, int ApprovalIdx, string INTERFACE_FLAG, string INTERFACE_DATE, string INTERFACE_TIME)
//        {
//            string strSql = @"
// UPDATE WebDev.dbo.TB_Cost_WebToSAP SET INTERFACE_FLAG = @INTERFACE_FLAG, INTERFACE_DATE = @INTERFACE_DATE, INTERFACE_TIME = @INTERFACE_TIME WHERE ApprovalItemIdx = @ApprovalItemIdx AND ApprovalIdx = @ApprovalIdx
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalItemIdx", DbType.Int32, ApprovalItemIdx);
//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalIdx", DbType.Int32, ApprovalIdx);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_FLAG", DbType.AnsiString, INTERFACE_FLAG);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_TIME", DbType.AnsiString, INTERFACE_TIME);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_CurrencyRateSIOutAsync(string ExchangeRateType, string FromCurrency, string ToCurrency, string ValidDate, double ExchangeRate, double IndirectRate, double FromCurrencyUnit
//            , double ToCurrencyUnit)
//        {
//            string spName = "WMDM.dbo.up_SapCurrencyRate_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeRateType", DbType.AnsiString, ExchangeRateType);
//            this.DbAccess.AddInParameter(dbCommand, "@FromCurrency", DbType.AnsiString, FromCurrency);
//            this.DbAccess.AddInParameter(dbCommand, "@ToCurrency", DbType.AnsiString, ToCurrency);
//            this.DbAccess.AddInParameter(dbCommand, "@ValidDate", DbType.AnsiString, ValidDate);
//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeRate", DbType.Decimal, ExchangeRate);
//            this.DbAccess.AddInParameter(dbCommand, "@IndirectRate", DbType.Decimal, IndirectRate);
//            this.DbAccess.AddInParameter(dbCommand, "@FromCurrencyUnit", DbType.Decimal, FromCurrencyUnit);
//            this.DbAccess.AddInParameter(dbCommand, "@ToCurrencyUnit", DbType.Decimal, ToCurrencyUnit);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_T_Collaboration_Delivery_Item(string DeleveryNo, string DeliveryItemNo, string PurchaseOrderNo, string PurchaseOrderItemNo, string DeliveryComplete, string Plant, string MaterialCode,
//    double Quantity, string UpdatedDate)
//        {

//            string strSql = @"
// Update BD.dbo.T_Collaboration_Delivery_Item
//Set DeletionFlag = NULLIF(@DeletionFlag, ''), Plant = @Plant, MaterialCode = @MaterialCode, Quantity = @Quantity, UpdatedDate = @UpdatedDate
//Where DeliveryNo = @DeliveryNo and DeliveryItemNo = @DeliveryItemNo and PurchaseOrderNo = @PurchaseOrderNo and PurchaseOrderItemNo = @PurchaseOrderItemNo
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@DeliveryNo", DbType.AnsiString, DeleveryNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeliveryItemNo", DbType.AnsiString, DeliveryItemNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderNo", DbType.AnsiString, PurchaseOrderNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderItemNo", DbType.AnsiString, PurchaseOrderItemNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeletionFlag", DbType.AnsiString, DeliveryComplete);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Quantity", DbType.Decimal, Quantity);
//            this.DbAccess.AddInParameter(dbCommand, "@UpdatedDate", DbType.AnsiString, UpdatedDate);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_T_Collaboration_PurchaseOrder_Header(string PurchaseOrderNo, string CompanyCode, string Acceptance, string Status, string VendorCode, string OrderDate, string Currency,
//    string PurchasingOrg, string PurchasingGroup, string Incoterms, string PaymentTerms, string ShipTo)
//        {

//            string spName = "BD.dbo.up_Collaboration_PurchaseOrderHeader_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderNo", DbType.AnsiString, PurchaseOrderNo);
//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Acceptance", DbType.AnsiString, Acceptance);
//            this.DbAccess.AddInParameter(dbCommand, "@Status", DbType.AnsiString, Status);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorCode", DbType.AnsiString, VendorCode);
//            this.DbAccess.AddInParameter(dbCommand, "@OrderDate", DbType.AnsiString, OrderDate);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingOrg", DbType.AnsiString, PurchasingOrg);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchasingGroup", DbType.AnsiString, PurchasingGroup);
//            this.DbAccess.AddInParameter(dbCommand, "@Incoterms", DbType.AnsiString, Incoterms);
//            this.DbAccess.AddInParameter(dbCommand, "@PaymentTerms", DbType.AnsiString, PaymentTerms);
//            this.DbAccess.AddInParameter(dbCommand, "@ShipTo", DbType.AnsiString, ShipTo);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_CurrRateSIOutAsync(string ExchangeRateType, string ValidDate, string Currency, double ToCurrencyKRW, double ToCurrencyUSD)
//        {
//            string spName = "WMDM.dbo.up_ExchangeRate_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeRateType", DbType.AnsiString, ExchangeRateType);
//            this.DbAccess.AddInParameter(dbCommand, "@ValidDate", DbType.AnsiString, ValidDate);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@ToCurrencyKRW", DbType.Decimal, ToCurrencyKRW);
//            this.DbAccess.AddInParameter(dbCommand, "@ToCurrencyUSD", DbType.Decimal, ToCurrencyUSD);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_T_Collaboration_PurchaseOrder_Item(string PurchaseOrderNo, string PurchaseOrderItemNo, string DeletionFlag, string Plant, string StorageLocation, string MaterialCode, string MaterialDescription, double Quantity,
//      string UoM, double NetPrice, double Per, string Currency, double TaxRate, string DeliveryDate, string ManufacturerPN, string LCNo, double GRQty, double BalanceQty)
//        {

//            string spName = "BD.dbo.up_Collaboration_PurchaseOrderItem_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderNo", DbType.AnsiString, PurchaseOrderNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PurchaseOrderItemNo", DbType.AnsiString, PurchaseOrderItemNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DeletionFlag", DbType.AnsiString, DeletionFlag);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@StorageLocation", DbType.AnsiString, StorageLocation);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialDescription", DbType.AnsiString, MaterialDescription);
//            this.DbAccess.AddInParameter(dbCommand, "@Quantity", DbType.Decimal, Quantity);
//            this.DbAccess.AddInParameter(dbCommand, "@UoM", DbType.AnsiString, UoM);
//            this.DbAccess.AddInParameter(dbCommand, "@NetPrice", DbType.Decimal, NetPrice);
//            this.DbAccess.AddInParameter(dbCommand, "@Per", DbType.Decimal, Per);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxRate", DbType.Decimal, TaxRate);
//            this.DbAccess.AddInParameter(dbCommand, "@DeliveryDate", DbType.AnsiString, DeliveryDate);
//            this.DbAccess.AddInParameter(dbCommand, "@ManufacturerPN", DbType.AnsiString, ManufacturerPN);
//            this.DbAccess.AddInParameter(dbCommand, "@LCNo", DbType.AnsiString, LCNo);
//            this.DbAccess.AddInParameter(dbCommand, "@GRQty", DbType.Decimal, GRQty);
//            this.DbAccess.AddInParameter(dbCommand, "@BalanceQty", DbType.Decimal, BalanceQty);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_CCAReport(string CompanyCode, string CostCenter, string FiscalYear, string CostElement, string ValueType, string Currency, double JAN, double FEB, double MAR
//           , double APR, double MAY, double JUN, double JUL, double AUG, double SEP, double OCT, double NOV, double DEC, string INTERFACE_DATE)
//        {

//            string strSql = @"
// IF (EXISTS(Select * From WebDev.dbo.IB_Cost_CCA Where CompanyCode = @CompanyCode and CostCenter = @CostCenter and FiscalYear = @FiscalYear and CostElement = @CostElement and ValueType = @ValueType and Currency = @Currency))
//	BEGIN
//		Update WebDev.dbo.IB_Cost_CCA
//		Set JAN = @JAN, FEB = @FEB, MAR = @MAR, APR = @APR, MAY = @MAY, JUN = @JUN, JUL = @JUL, AUG = @AUG, SEP = @SEP, OCT = @OCT, NOV = @NOV, DEC = @DEC, INTERFACE_DATE = CONVERT(VARCHAR(8), GETDATE(), 112), INTERFACE_TIME = replace(Convert (varchar(8),GetDate(), 108),':',''), INTERFACE_FLAG = 'N'
//		Where CompanyCode = @CompanyCode and CostCenter = @CostCenter and FiscalYear = @FiscalYear and CostElement = @CostElement and ValueType = @ValueType and Currency = @Currency
//	END
//ELSE
//	BEGIN
//		Insert Into WebDev.dbo.IB_Cost_CCA
//		Values (@CompanyCode, @CostCenter, @FiscalYear, @CostElement, @ValueType, @Currency, @JAN, @FEB, @MAR, @APR, @MAY, @JUN, @JUL, @AUG, @SEP, @OCT, @NOV, @DEC, 'N', CONVERT(VARCHAR(8), GETDATE(), 112),  replace(Convert (varchar(8),GetDate(), 108),':',''))
//	END
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@CostCenter", DbType.AnsiString, CostCenter);
//            this.DbAccess.AddInParameter(dbCommand, "@FiscalYear", DbType.AnsiString, FiscalYear);
//            this.DbAccess.AddInParameter(dbCommand, "@CostElement", DbType.AnsiString, CostElement);
//            this.DbAccess.AddInParameter(dbCommand, "@ValueType", DbType.AnsiString, ValueType);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@JAN", DbType.Decimal, JAN);
//            this.DbAccess.AddInParameter(dbCommand, "@FEB", DbType.Decimal, FEB);
//            this.DbAccess.AddInParameter(dbCommand, "@MAR", DbType.Decimal, MAR);
//            this.DbAccess.AddInParameter(dbCommand, "@APR", DbType.Decimal, APR);
//            this.DbAccess.AddInParameter(dbCommand, "@MAY", DbType.Decimal, MAY);
//            this.DbAccess.AddInParameter(dbCommand, "@JUN", DbType.Decimal, JUN);
//            this.DbAccess.AddInParameter(dbCommand, "@JUL", DbType.Decimal, JUL);
//            this.DbAccess.AddInParameter(dbCommand, "@AUG", DbType.Decimal, AUG);
//            this.DbAccess.AddInParameter(dbCommand, "@SEP", DbType.Decimal, SEP);
//            this.DbAccess.AddInParameter(dbCommand, "@OCT", DbType.Decimal, OCT);
//            this.DbAccess.AddInParameter(dbCommand, "@NOV", DbType.Decimal, NOV);
//            this.DbAccess.AddInParameter(dbCommand, "@DEC", DbType.Decimal, DEC);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapMaterialPrice(string Period, string PriceType, string MaterialCode, string Plant, double Price, string Currency, int PriceUnit, string INTERFACE_DATE)
//        {

//            string strSql = @"
//Insert Into WMDM.dbo.SapMaterialPrice
//Values (@Period, @PriceType, @MaterialCode, @Plant, @Price, @Currency, @PriceUnit, @INTERFACE_DATE, replace(Convert (varchar(8),GetDate(), 108),':',''), 'N')
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@Period", DbType.AnsiString, Period);
//            this.DbAccess.AddInParameter(dbCommand, "@PriceType", DbType.AnsiString, PriceType);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Plant", DbType.AnsiString, Plant);
//            this.DbAccess.AddInParameter(dbCommand, "@Price", DbType.Decimal, Price);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@PriceUnit", DbType.Int32, PriceUnit);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_IB_ROI_Depreciation(string YearMonth, string CostType, string Companycode, string ProjectCode, string DocumentNo, string DocLineItem, string Postingdate
//            , string GLAccount, double Amount, string CostCenter, string AssetNo, string MaterialCode, string Remark, string INTERFACE_DATE)
//        {

//            string strSql = @"
//Insert Into BD.dbo.IB_ROI_Depreciation
//Values (@Companycode, @YearMonth, @CostType, @ProjectCode, @DocumentNo, @DocLineItem, @Postingdate, @GLAccount, @Amount, @CostCenter, @AssetNo, @MaterialCode, @Remark, @INTERFACE_DATE, replace(Convert (varchar(8),GetDate(), 108),':',''))
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@YearMonth", DbType.AnsiString, YearMonth);
//            this.DbAccess.AddInParameter(dbCommand, "@CostType", DbType.AnsiString, CostType);
//            this.DbAccess.AddInParameter(dbCommand, "@Companycode", DbType.AnsiString, Companycode);
//            this.DbAccess.AddInParameter(dbCommand, "@ProjectCode", DbType.AnsiString, ProjectCode);
//            this.DbAccess.AddInParameter(dbCommand, "@DocumentNo", DbType.AnsiString, DocumentNo);
//            this.DbAccess.AddInParameter(dbCommand, "@DocLineItem", DbType.AnsiString, DocLineItem);
//            this.DbAccess.AddInParameter(dbCommand, "@Postingdate", DbType.AnsiString, Postingdate);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@Amount", DbType.Decimal, Amount);
//            this.DbAccess.AddInParameter(dbCommand, "@CostCenter", DbType.AnsiString, CostCenter);
//            this.DbAccess.AddInParameter(dbCommand, "@AssetNo", DbType.AnsiString, AssetNo);
//            this.DbAccess.AddInParameter(dbCommand, "@MaterialCode", DbType.AnsiString, MaterialCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Remark", DbType.AnsiString, Remark);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.Decimal, INTERFACE_DATE);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_IB_Cost_InternalOrder(string CompanyCode, string InternalOrder, string Text, string OrderType, string INTERFACE_DATE)
//        {

//            string strSql = @"
//Insert Into WebDev.dbo.IB_Cost_InternalOrder
//Values (@CompanyCode, @InternalOrder, @Text, @OrderType, 'N', @INTERFACE_DATE, replace(Convert (varchar(8),GetDate(), 108),':',''))
//";
//            DbCommand dbCommand = this.DbAccess.GetSqlStringCommand(strSql);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@InternalOrder", DbType.AnsiString, InternalOrder);
//            this.DbAccess.AddInParameter(dbCommand, "@Text", DbType.AnsiString, Text);
//            this.DbAccess.AddInParameter(dbCommand, "@OrderType", DbType.AnsiString, OrderType);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_HR_ExchangeRate(string ExchangeDate, string CurrencyCode, string BasicRate, string BasicRate2, double DivideRate)
//        {

//            string spName = "WebDev.dbo.up_HR_ExchangeRate_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeDate", DbType.AnsiString, ExchangeDate);
//            this.DbAccess.AddInParameter(dbCommand, "@CurrencyCode", DbType.AnsiString, CurrencyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@BasicRate", DbType.AnsiString, BasicRate);
//            this.DbAccess.AddInParameter(dbCommand, "@BasicRate2", DbType.AnsiString, BasicRate2);
//            this.DbAccess.AddInParameter(dbCommand, "@DivideRate", DbType.Double, DivideRate);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_TaxInvoiceSIOutAsync_Header(string CompanyCode, string InvoiceKey, string ReferenceType, string InvoiceType, string TaxInvoiceKind, string ApprovalNumber, string Note, string ApprovalItemCode
//            , string ApprovalDate, string PostingDate, string SupplierID, string SupplierName, string SupplierDepName, string SupplierPersName, string SupplierEmail, string ReceiverID, string ReceiverName
//            , string ReceiverDepName1, string ReceiverPersName1, string ReceiverEmail1, string ReceiverEmail2, string TaxInvoiceType, string AccountType, string TaxOffice, string TaxCode
//            , string VAT, string VendorCode, string CustomerCode, double ChargeAmountHeader, double SupplyAmountHeader, double VATAmoutHeader, string BusinessPlace, string IssueDateTime, string AmendCode
//            , string INV_SEQ_OLD, string INTERFACE_DATE)
//        {
//            string spName = "WebDev.dbo.up_IB_Cost_TaxInvoice_Header_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@InvoiceKey", DbType.AnsiString, InvoiceKey);
//            this.DbAccess.AddInParameter(dbCommand, "@ReferenceType", DbType.AnsiString, ReferenceType);
//            this.DbAccess.AddInParameter(dbCommand, "@InvoiceType", DbType.AnsiString, InvoiceType);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxInvoiceKind", DbType.AnsiString, TaxInvoiceKind);
//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalNumber", DbType.AnsiString, ApprovalNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@Note", DbType.AnsiString, Note);
//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalItemCode", DbType.AnsiString, ApprovalItemCode);
//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalDate", DbType.AnsiString, ApprovalDate);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingDate", DbType.AnsiString, PostingDate);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplierID", DbType.AnsiString, SupplierID);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplierName", DbType.AnsiString, SupplierName);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplierDepName", DbType.AnsiString, SupplierDepName);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplierPersName", DbType.AnsiString, SupplierPersName);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplierEmail", DbType.AnsiString, SupplierEmail);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiverID", DbType.AnsiString, ReceiverID);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiverName", DbType.AnsiString, ReceiverName);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiverDepName1", DbType.AnsiString, ReceiverDepName1);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiverPersName1", DbType.AnsiString, ReceiverPersName1);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiverEmail1", DbType.AnsiString, ReceiverEmail1);
//            this.DbAccess.AddInParameter(dbCommand, "@ReceiverEmail2", DbType.AnsiString, ReceiverEmail2);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxInvoiceType", DbType.AnsiString, TaxInvoiceType);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountType", DbType.AnsiString, AccountType);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxOffice", DbType.AnsiString, TaxOffice);
//            this.DbAccess.AddInParameter(dbCommand, "@TaxCode", DbType.AnsiString, TaxCode);
//            this.DbAccess.AddInParameter(dbCommand, "@VAT", DbType.AnsiString, VAT);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorCode", DbType.AnsiString, VendorCode);
//            this.DbAccess.AddInParameter(dbCommand, "@CustomerCode", DbType.AnsiString, CustomerCode);
//            this.DbAccess.AddInParameter(dbCommand, "@ChargeAmountHeader", DbType.Decimal, ChargeAmountHeader);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplyAmountHeader", DbType.Decimal, SupplyAmountHeader);
//            this.DbAccess.AddInParameter(dbCommand, "@VATAmoutHeader", DbType.Decimal, VATAmoutHeader);
//            this.DbAccess.AddInParameter(dbCommand, "@BusinessPlace", DbType.AnsiString, BusinessPlace);
//            this.DbAccess.AddInParameter(dbCommand, "@IssueDateTime", DbType.AnsiString, IssueDateTime);
//            this.DbAccess.AddInParameter(dbCommand, "@AmendCode", DbType.AnsiString, AmendCode);
//            this.DbAccess.AddInParameter(dbCommand, "@INV_SEQ_OLD", DbType.AnsiString, INV_SEQ_OLD);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_TaxInvoiceSIOutAsync_Item(string CompanyCode, string InvoiceKey, int ItemKey, string Date, string Note, int Quantity, double UnitAmount, double SupplyAmount, double VATAmount
//            , string Description, string INTERFACE_DATE)
//        {
//            string spName = "WebDev.dbo.up_IB_Cost_TaxInvoice_Item_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@InvoiceKey", DbType.AnsiString, InvoiceKey);
//            this.DbAccess.AddInParameter(dbCommand, "@ItemKey", DbType.Int32, ItemKey);
//            this.DbAccess.AddInParameter(dbCommand, "@Date", DbType.AnsiString, Date);
//            this.DbAccess.AddInParameter(dbCommand, "@Note", DbType.AnsiString, Note);
//            this.DbAccess.AddInParameter(dbCommand, "@Quantity", DbType.Int32, Quantity);
//            this.DbAccess.AddInParameter(dbCommand, "@UnitAmount", DbType.Decimal, UnitAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@SupplyAmount", DbType.Decimal, SupplyAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@VATAmount", DbType.Decimal, VATAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@Description", DbType.AnsiString, Description);
//            this.DbAccess.AddInParameter(dbCommand, "@INTERFACE_DATE", DbType.AnsiString, INTERFACE_DATE);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        #endregion

//        #region ================= Royalty =================
//        public DataTable Insert_Royalty_Forecast_Return_async(string CompanyCode, string SalesType, string DocumentNo, string AccountDocNo, string Year, string ExchangeRate
//            , string ExchangeDate, string KRWAmount, string Message)
//        {
//            string spName = "BD.Royalty_AL.up_RoyaltyDocumentContract_Posting_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@Company", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@SalesType", DbType.AnsiString, SalesType);
//            this.DbAccess.AddInParameter(dbCommand, "@DocumentNo", DbType.AnsiString, DocumentNo);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountDocNo", DbType.AnsiString, AccountDocNo);
//            this.DbAccess.AddInParameter(dbCommand, "@Year", DbType.AnsiString, Year);
//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeRate", DbType.AnsiString, ExchangeRate);
//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeDate", DbType.AnsiString, ExchangeDate);
//            this.DbAccess.AddInParameter(dbCommand, "@KRWAmount", DbType.AnsiString, KRWAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@Message", DbType.AnsiString, Message);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_Sales_Reverse_Return_SI_async(string CompanyCode, string SalesType, string ConfirmID, string ReverseReason, string DocumentNo, string AccountDocNo
//            , string Year, string ReverseDate, string ReverseAccountDocNo, string ReverseYear, string Message)
//        {
//            string spName = "BD.Royalty_AL.up_RoyaltyDocumentContract_Reverse_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@Company", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@SalesType", DbType.AnsiString, SalesType);
//            this.DbAccess.AddInParameter(dbCommand, "@ConfirmID", DbType.AnsiString, ConfirmID);
//            this.DbAccess.AddInParameter(dbCommand, "@ReverseReason", DbType.AnsiString, ReverseReason);
//            this.DbAccess.AddInParameter(dbCommand, "@DocumentNo", DbType.AnsiString, DocumentNo);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountDocNo", DbType.AnsiString, AccountDocNo);
//            this.DbAccess.AddInParameter(dbCommand, "@Year", DbType.AnsiString, Year);
//            this.DbAccess.AddInParameter(dbCommand, "@ReverseDate", DbType.AnsiString, ReverseDate);
//            this.DbAccess.AddInParameter(dbCommand, "@ReverseAccountDocNo", DbType.AnsiString, ReverseAccountDocNo);
//            this.DbAccess.AddInParameter(dbCommand, "@ReverseYear", DbType.AnsiString, ReverseYear);
//            this.DbAccess.AddInParameter(dbCommand, "@Message", DbType.AnsiString, Message);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Delete_SapBSPLActual(string CompanyCode, string Year, string Month, string Type)
//        {
//            string spName = "WMDM.dbo.up_SapBSPLActual_Delete";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Year", DbType.AnsiString, Year);
//            this.DbAccess.AddInParameter(dbCommand, "@Month", DbType.AnsiString, Month);
//            this.DbAccess.AddInParameter(dbCommand, "@Type", DbType.AnsiString, Type);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        public DataTable Insert_SapBSPLActual(string CompanyCode, string Year, string Month, string Type, string GLAccount, string CostCenter, string ProjectCode, string DocAmount
//            , string Currency, string LOCAmount, string KRWAmount, string ExchangeRate, string TranslationDate, string InterfaceDate, string InterfaceTime)
//        {
//            string spName = "WMDM.dbo.up_SapBSPLActual_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Year", DbType.AnsiString, Year);
//            this.DbAccess.AddInParameter(dbCommand, "@Month", DbType.AnsiString, Month);
//            this.DbAccess.AddInParameter(dbCommand, "@Type", DbType.AnsiString, Type);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@CostCenter", DbType.AnsiString, CostCenter);
//            this.DbAccess.AddInParameter(dbCommand, "@ProjectCode", DbType.AnsiString, ProjectCode);
//            this.DbAccess.AddInParameter(dbCommand, "@DocAmount", DbType.AnsiString, DocAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@LOCAmount", DbType.AnsiString, LOCAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@KRWAmount", DbType.AnsiString, KRWAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@ExchangeRate", DbType.AnsiString, ExchangeRate);
//            this.DbAccess.AddInParameter(dbCommand, "@TranslationDate", DbType.AnsiString, TranslationDate);
//            this.DbAccess.AddInParameter(dbCommand, "@InterfaceDate", DbType.AnsiString, InterfaceDate);
//            this.DbAccess.AddInParameter(dbCommand, "@InterfaceTime", DbType.AnsiString, InterfaceTime);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }

//        #endregion

//        #region ================= Altimedia CMS =================
//        public DataTable Insert_CMS_Transfer(string CompanyCode, string RunDate, string Identification, string SeqNo, string PostingDate, string PaymentNo, string VendorCode, string Currency,
//            string BusienssPartnerName, string CountryCode, string BankKey, string NameOfBank, string BankAccountNumber, string SWIFT, string GLAccount, string GLAccountName,
//            string IndicatorType, string DocAmount, string LocalAmount)
//        {
//            string spName = "WebDev.dbo.up_CMSAutoTransfer_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@RunDate", DbType.AnsiString, RunDate);
//            this.DbAccess.AddInParameter(dbCommand, "@Identification", DbType.AnsiString, Identification);
//            this.DbAccess.AddInParameter(dbCommand, "@SeqNo", DbType.AnsiString, SeqNo);
//            this.DbAccess.AddInParameter(dbCommand, "@PostingDate", DbType.AnsiString, PostingDate);
//            this.DbAccess.AddInParameter(dbCommand, "@PaymentNo", DbType.AnsiString, PaymentNo);
//            this.DbAccess.AddInParameter(dbCommand, "@VendorCode", DbType.AnsiString, VendorCode);
//            this.DbAccess.AddInParameter(dbCommand, "@Currency", DbType.AnsiString, Currency);
//            this.DbAccess.AddInParameter(dbCommand, "@BusienssPartnerName", DbType.AnsiString, BusienssPartnerName);
//            this.DbAccess.AddInParameter(dbCommand, "@CountryCode", DbType.AnsiString, CountryCode);
//            this.DbAccess.AddInParameter(dbCommand, "@BankKey", DbType.AnsiString, BankKey);
//            this.DbAccess.AddInParameter(dbCommand, "@NameOfBank", DbType.AnsiString, NameOfBank);
//            this.DbAccess.AddInParameter(dbCommand, "@BankAccountNumber", DbType.AnsiString, BankAccountNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@SWIFT", DbType.AnsiString, SWIFT);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccount", DbType.AnsiString, GLAccount);
//            this.DbAccess.AddInParameter(dbCommand, "@GLAccountName", DbType.AnsiString, GLAccountName);
//            this.DbAccess.AddInParameter(dbCommand, "@IndicatorType", DbType.AnsiString, IndicatorType);
//            this.DbAccess.AddInParameter(dbCommand, "@DocAmount", DbType.AnsiString, DocAmount);
//            this.DbAccess.AddInParameter(dbCommand, "@LocalAmount", DbType.AnsiString, LocalAmount);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }
//        #endregion

//        #region ================= Altimedia Tax Invoice Reqest Status =================
//        public DataTable Insert_TaxInvoiceStatus_Transfer(string ApprovalIdx, string CompanyCode, string FiscalYear, string AccountDocNumber, string Status, string ReferenceDocNumber, string InvoiceType)
//        {
//            string spName = "WebDev.dbo.up_TaxInvoiceStatusTransfer_Insert";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            this.DbAccess.AddInParameter(dbCommand, "@ApprovalIdx", DbType.AnsiString, ApprovalIdx);
//            this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            this.DbAccess.AddInParameter(dbCommand, "@FiscalYear", DbType.AnsiString, FiscalYear);
//            this.DbAccess.AddInParameter(dbCommand, "@AccountDocNumber", DbType.AnsiString, AccountDocNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@Status", DbType.AnsiString, Status);
//            this.DbAccess.AddInParameter(dbCommand, "@ReferenceDocNumber", DbType.AnsiString, ReferenceDocNumber);
//            this.DbAccess.AddInParameter(dbCommand, "@InvoiceType", DbType.AnsiString, InvoiceType);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }
//        #endregion

//        #region ================= Get Server Info =================
//        public DataTable Select_MonitorServer_Info()
//        {
//            string spName = "miner_db.dbo.GetServerInfo";

//            DbCommand dbCommand = this.DbAccess.GetStoredProcCommand(spName);

//            //this.DbAccess.AddInParameter(dbCommand, "@ApprovalIdx", DbType.AnsiString, ApprovalIdx);
//            //this.DbAccess.AddInParameter(dbCommand, "@CompanyCode", DbType.AnsiString, CompanyCode);
//            //this.DbAccess.AddInParameter(dbCommand, "@FiscalYear", DbType.AnsiString, FiscalYear);
//            //this.DbAccess.AddInParameter(dbCommand, "@AccountDocNumber", DbType.AnsiString, AccountDocNumber);
//            //this.DbAccess.AddInParameter(dbCommand, "@Status", DbType.AnsiString, Status);
//            //this.DbAccess.AddInParameter(dbCommand, "@ReferenceDocNumber", DbType.AnsiString, ReferenceDocNumber);
//            //this.DbAccess.AddInParameter(dbCommand, "@InvoiceType", DbType.AnsiString, InvoiceType);

//            return GetDataTable(DbAccess.ExecuteDataSet(dbCommand));
//        }
//        #endregion
//    }
}