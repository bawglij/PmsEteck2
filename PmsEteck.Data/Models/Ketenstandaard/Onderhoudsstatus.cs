﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.7.3081.0.
// 
namespace PmsEteck.Data.Models.Ketenstandaard.Onderhoudsstatus
{


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    [System.Xml.Serialization.XmlRootAttribute("MaintenanceStatus", Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005", IsNullable=false)]
    public partial class MaintenanceStatusType {
        
        private string messageNumberField;
        
        private System.DateTime messageDateField;
        
        private System.DateTime messageTimeField;
        
        private bool messageTimeFieldSpecified;
        
        private string reverseChargeIndicatorField;
        
        private PartyGLNOnlyContactType buyerField;
        
        private PartyGLNOnlyContactType contractorField;
        
        private InstructionDataType instructionDataField;
        
        /// <remarks/>
        public string MessageNumber {
            get {
                return this.messageNumberField;
            }
            set {
                this.messageNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime MessageDate {
            get {
                return this.messageDateField;
            }
            set {
                this.messageDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="time")]
        public System.DateTime MessageTime {
            get {
                return this.messageTimeField;
            }
            set {
                this.messageTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageTimeSpecified {
            get {
                return this.messageTimeFieldSpecified;
            }
            set {
                this.messageTimeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string ReverseChargeIndicator {
            get {
                return this.reverseChargeIndicatorField;
            }
            set {
                this.reverseChargeIndicatorField = value;
            }
        }
        
        /// <remarks/>
        public PartyGLNOnlyContactType Buyer {
            get {
                return this.buyerField;
            }
            set {
                this.buyerField = value;
            }
        }
        
        /// <remarks/>
        public PartyGLNOnlyContactType Contractor {
            get {
                return this.contractorField;
            }
            set {
                this.contractorField = value;
            }
        }
        
        /// <remarks/>
        public InstructionDataType InstructionData {
            get {
                return this.instructionDataField;
            }
            set {
                this.instructionDataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class PartyGLNOnlyContactType {
        
        private string gLNField;
        
        private ContactInformationType[] contactInformationField;
        
        /// <remarks/>
        public string GLN {
            get {
                return this.gLNField;
            }
            set {
                this.gLNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ContactInformation")]
        public ContactInformationType[] ContactInformation {
            get {
                return this.contactInformationField;
            }
            set {
                this.contactInformationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class ContactInformationType {
        
        private string contactPersonNameField;
        
        private string phoneNumberField;
        
        private string emailAddressField;
        
        /// <remarks/>
        public string ContactPersonName {
            get {
                return this.contactPersonNameField;
            }
            set {
                this.contactPersonNameField = value;
            }
        }
        
        /// <remarks/>
        public string PhoneNumber {
            get {
                return this.phoneNumberField;
            }
            set {
                this.phoneNumberField = value;
            }
        }
        
        /// <remarks/>
        public string EmailAddress {
            get {
                return this.emailAddressField;
            }
            set {
                this.emailAddressField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class VATInformationAndBaseAmountType {
        
        private VATInformationAndBaseAmountTypeVATRate vATRateField;
        
        private decimal vATPercentageField;
        
        private bool vATPercentageFieldSpecified;
        
        private decimal vATBaseAmountField;
        
        private bool vATBaseAmountFieldSpecified;
        
        /// <remarks/>
        public VATInformationAndBaseAmountTypeVATRate VATRate {
            get {
                return this.vATRateField;
            }
            set {
                this.vATRateField = value;
            }
        }
        
        /// <remarks/>
        public decimal VATPercentage {
            get {
                return this.vATPercentageField;
            }
            set {
                this.vATPercentageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VATPercentageSpecified {
            get {
                return this.vATPercentageFieldSpecified;
            }
            set {
                this.vATPercentageFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal VATBaseAmount {
            get {
                return this.vATBaseAmountField;
            }
            set {
                this.vATBaseAmountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VATBaseAmountSpecified {
            get {
                return this.vATBaseAmountFieldSpecified;
            }
            set {
                this.vATBaseAmountFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public enum VATInformationAndBaseAmountTypeVATRate {
        
        /// <remarks/>
        E,
        
        /// <remarks/>
        S,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class PriceBaseType {
        
        private decimal numberOfUnitsInPriceBasisField;
        
        private OrderUnitCodeType measureUnitPriceBasisField;
        
        private string priceBaseDescriptionField;
        
        /// <remarks/>
        public decimal NumberOfUnitsInPriceBasis {
            get {
                return this.numberOfUnitsInPriceBasisField;
            }
            set {
                this.numberOfUnitsInPriceBasisField = value;
            }
        }
        
        /// <remarks/>
        public OrderUnitCodeType MeasureUnitPriceBasis {
            get {
                return this.measureUnitPriceBasisField;
            }
            set {
                this.measureUnitPriceBasisField = value;
            }
        }
        
        /// <remarks/>
        public string PriceBaseDescription {
            get {
                return this.priceBaseDescriptionField;
            }
            set {
                this.priceBaseDescriptionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public enum OrderUnitCodeType {
        
        /// <remarks/>
        CMT,
        
        /// <remarks/>
        DAY,
        
        /// <remarks/>
        GRM,
        
        /// <remarks/>
        HUR,
        
        /// <remarks/>
        KGM,
        
        /// <remarks/>
        LTR,
        
        /// <remarks/>
        MIN,
        
        /// <remarks/>
        MLT,
        
        /// <remarks/>
        MMT,
        
        /// <remarks/>
        MTK,
        
        /// <remarks/>
        MTQ,
        
        /// <remarks/>
        MTR,
        
        /// <remarks/>
        PCE,
        
        /// <remarks/>
        TNE,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class PriceInformationType {
        
        private decimal priceField;
        
        private PriceBaseType priceBaseField;
        
        /// <remarks/>
        public decimal Price {
            get {
                return this.priceField;
            }
            set {
                this.priceField = value;
            }
        }
        
        /// <remarks/>
        public PriceBaseType PriceBase {
            get {
                return this.priceBaseField;
            }
            set {
                this.priceBaseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class AttachmentCharacteristicsType {
        
        private string attributeNameField;
        
        private string attributeValueField;
        
        /// <remarks/>
        public string AttributeName {
            get {
                return this.attributeNameField;
            }
            set {
                this.attributeNameField = value;
            }
        }
        
        /// <remarks/>
        public string AttributeValue {
            get {
                return this.attributeValueField;
            }
            set {
                this.attributeValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class AttachmentType {
        
        private string uRLField;
        
        private byte[] attachedDataField;
        
        private AttachmentTypeDocumentType documentTypeField;
        
        private string fileTypeField;
        
        private string fileNameField;
        
        private AttachmentCharacteristicsType[] attachmentCharacteristicsField;
        
        /// <remarks/>
        public string URL {
            get {
                return this.uRLField;
            }
            set {
                this.uRLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] AttachedData {
            get {
                return this.attachedDataField;
            }
            set {
                this.attachedDataField = value;
            }
        }
        
        /// <remarks/>
        public AttachmentTypeDocumentType DocumentType {
            get {
                return this.documentTypeField;
            }
            set {
                this.documentTypeField = value;
            }
        }
        
        /// <remarks/>
        public string FileType {
            get {
                return this.fileTypeField;
            }
            set {
                this.fileTypeField = value;
            }
        }
        
        /// <remarks/>
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AttachmentCharacteristics")]
        public AttachmentCharacteristicsType[] AttachmentCharacteristics {
            get {
                return this.attachmentCharacteristicsField;
            }
            set {
                this.attachmentCharacteristicsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public enum AttachmentTypeDocumentType {
        
        /// <remarks/>
        CAD,
        
        /// <remarks/>
        CHR,
        
        /// <remarks/>
        FAC,
        
        /// <remarks/>
        LOG,
        
        /// <remarks/>
        MAN,
        
        /// <remarks/>
        MTE,
        
        /// <remarks/>
        OTA,
        
        /// <remarks/>
        PAR,
        
        /// <remarks/>
        PHI,
        
        /// <remarks/>
        PPI,
        
        /// <remarks/>
        PRT,
        
        /// <remarks/>
        PVI,
        
        /// <remarks/>
        SCH,
        
        /// <remarks/>
        SOF,
        
        /// <remarks/>
        STR,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class LEDOInformationType {
        
        private string locationField;
        
        private string elementField;
        
        private string defectField;
        
        private string causeField;
        
        /// <remarks/>
        public string Location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        public string Element {
            get {
                return this.elementField;
            }
            set {
                this.elementField = value;
            }
        }
        
        /// <remarks/>
        public string Defect {
            get {
                return this.defectField;
            }
            set {
                this.defectField = value;
            }
        }
        
        /// <remarks/>
        public string Cause {
            get {
                return this.causeField;
            }
            set {
                this.causeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class InstructionLineType {
        
        private string lineNumberField;
        
        private decimal quantityField;
        
        private OrderUnitCodeType measurementUnitQuantityField;
        
        private StatusCodeType statusField;
        
        private bool statusFieldSpecified;
        
        private string normPriceCodeField;
        
        private string shortDescriptionField;
        
        private string longDescriptionField;
        
        private ReasonRepairCodeType reasonRepairCodeField;
        
        private bool reasonRepairCodeFieldSpecified;
        
        private LEDOInformationType lEDOInformationField;
        
        private AttachmentType[] attachmentField;
        
        private string freeTextField;
        
        private PriceInformationType priceInformationField;
        
        private VATInformationAndBaseAmountType vATInformationField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
        public string LineNumber {
            get {
                return this.lineNumberField;
            }
            set {
                this.lineNumberField = value;
            }
        }
        
        /// <remarks/>
        public decimal Quantity {
            get {
                return this.quantityField;
            }
            set {
                this.quantityField = value;
            }
        }
        
        /// <remarks/>
        public OrderUnitCodeType MeasurementUnitQuantity {
            get {
                return this.measurementUnitQuantityField;
            }
            set {
                this.measurementUnitQuantityField = value;
            }
        }
        
        /// <remarks/>
        public StatusCodeType Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatusSpecified {
            get {
                return this.statusFieldSpecified;
            }
            set {
                this.statusFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string NormPriceCode {
            get {
                return this.normPriceCodeField;
            }
            set {
                this.normPriceCodeField = value;
            }
        }
        
        /// <remarks/>
        public string ShortDescription {
            get {
                return this.shortDescriptionField;
            }
            set {
                this.shortDescriptionField = value;
            }
        }
        
        /// <remarks/>
        public string LongDescription {
            get {
                return this.longDescriptionField;
            }
            set {
                this.longDescriptionField = value;
            }
        }
        
        /// <remarks/>
        public ReasonRepairCodeType ReasonRepairCode {
            get {
                return this.reasonRepairCodeField;
            }
            set {
                this.reasonRepairCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReasonRepairCodeSpecified {
            get {
                return this.reasonRepairCodeFieldSpecified;
            }
            set {
                this.reasonRepairCodeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public LEDOInformationType LEDOInformation {
            get {
                return this.lEDOInformationField;
            }
            set {
                this.lEDOInformationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Attachment")]
        public AttachmentType[] Attachment {
            get {
                return this.attachmentField;
            }
            set {
                this.attachmentField = value;
            }
        }
        
        /// <remarks/>
        public string FreeText {
            get {
                return this.freeTextField;
            }
            set {
                this.freeTextField = value;
            }
        }
        
        /// <remarks/>
        public PriceInformationType PriceInformation {
            get {
                return this.priceInformationField;
            }
            set {
                this.priceInformationField = value;
            }
        }
        
        /// <remarks/>
        public VATInformationAndBaseAmountType VATInformation {
            get {
                return this.vATInformationField;
            }
            set {
                this.vATInformationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public enum StatusCodeType {
        
        /// <remarks/>
        ACC,
        
        /// <remarks/>
        AFH,
        
        /// <remarks/>
        AFR,
        
        /// <remarks/>
        AFW,
        
        /// <remarks/>
        ANN,
        
        /// <remarks/>
        BBT,
        
        /// <remarks/>
        BNT,
        
        /// <remarks/>
        GER,
        
        /// <remarks/>
        MIB,
        
        /// <remarks/>
        TFC,
        
        /// <remarks/>
        UIT,
        
        /// <remarks/>
        VBW,
        
        /// <remarks/>
        VER,
        
        /// <remarks/>
        WEI,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public enum ReasonRepairCodeType {
        
        /// <remarks/>
        DEF,
        
        /// <remarks/>
        HUU,
        
        /// <remarks/>
        OUD,
        
        /// <remarks/>
        SLT,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class DeliveryTimeFrameType {
        
        private System.DateTime deliveryDateEarliestField;
        
        private bool deliveryDateEarliestFieldSpecified;
        
        private System.DateTime deliveryTimeEarliestField;
        
        private bool deliveryTimeEarliestFieldSpecified;
        
        private System.DateTime deliveryDateLatestField;
        
        private bool deliveryDateLatestFieldSpecified;
        
        private System.DateTime deliveryTimeLatestField;
        
        private bool deliveryTimeLatestFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime DeliveryDateEarliest {
            get {
                return this.deliveryDateEarliestField;
            }
            set {
                this.deliveryDateEarliestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryDateEarliestSpecified {
            get {
                return this.deliveryDateEarliestFieldSpecified;
            }
            set {
                this.deliveryDateEarliestFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="time")]
        public System.DateTime DeliveryTimeEarliest {
            get {
                return this.deliveryTimeEarliestField;
            }
            set {
                this.deliveryTimeEarliestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryTimeEarliestSpecified {
            get {
                return this.deliveryTimeEarliestFieldSpecified;
            }
            set {
                this.deliveryTimeEarliestFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime DeliveryDateLatest {
            get {
                return this.deliveryDateLatestField;
            }
            set {
                this.deliveryDateLatestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryDateLatestSpecified {
            get {
                return this.deliveryDateLatestFieldSpecified;
            }
            set {
                this.deliveryDateLatestFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="time")]
        public System.DateTime DeliveryTimeLatest {
            get {
                return this.deliveryTimeLatestField;
            }
            set {
                this.deliveryTimeLatestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryTimeLatestSpecified {
            get {
                return this.deliveryTimeLatestFieldSpecified;
            }
            set {
                this.deliveryTimeLatestFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class DeliveryDateTimeInformationType {
        
        private System.DateTime requiredDeliveryDateField;
        
        private bool requiredDeliveryDateFieldSpecified;
        
        private System.DateTime requiredDeliveryTimeField;
        
        private bool requiredDeliveryTimeFieldSpecified;
        
        private DeliveryTimeFrameType deliveryTimeFrameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime RequiredDeliveryDate {
            get {
                return this.requiredDeliveryDateField;
            }
            set {
                this.requiredDeliveryDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequiredDeliveryDateSpecified {
            get {
                return this.requiredDeliveryDateFieldSpecified;
            }
            set {
                this.requiredDeliveryDateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="time")]
        public System.DateTime RequiredDeliveryTime {
            get {
                return this.requiredDeliveryTimeField;
            }
            set {
                this.requiredDeliveryTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequiredDeliveryTimeSpecified {
            get {
                return this.requiredDeliveryTimeFieldSpecified;
            }
            set {
                this.requiredDeliveryTimeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public DeliveryTimeFrameType DeliveryTimeFrame {
            get {
                return this.deliveryTimeFrameField;
            }
            set {
                this.deliveryTimeFrameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.ketenstandaard.nl/onderhoudsstatus/SALES/005")]
    public partial class InstructionDataType {
        
        private string instructionNumberField;
        
        private string instructionSubNumberField;
        
        private StatusCodeType statusField;
        
        private bool statusFieldSpecified;
        
        private string statusDescriptionField;
        
        private System.DateTime dateReadyField;
        
        private bool dateReadyFieldSpecified;
        
        private decimal totalAmountField;
        
        private bool totalAmountFieldSpecified;
        
        private string freeTextField;
        
        private DeliveryDateTimeInformationType appointmentDateTimeInformationField;
        
        private InstructionLineType[] instructionLineField;
        
        /// <remarks/>
        public string InstructionNumber {
            get {
                return this.instructionNumberField;
            }
            set {
                this.instructionNumberField = value;
            }
        }
        
        /// <remarks/>
        public string InstructionSubNumber {
            get {
                return this.instructionSubNumberField;
            }
            set {
                this.instructionSubNumberField = value;
            }
        }
        
        /// <remarks/>
        public StatusCodeType Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatusSpecified {
            get {
                return this.statusFieldSpecified;
            }
            set {
                this.statusFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string StatusDescription {
            get {
                return this.statusDescriptionField;
            }
            set {
                this.statusDescriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime DateReady {
            get {
                return this.dateReadyField;
            }
            set {
                this.dateReadyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateReadySpecified {
            get {
                return this.dateReadyFieldSpecified;
            }
            set {
                this.dateReadyFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public decimal TotalAmount {
            get {
                return this.totalAmountField;
            }
            set {
                this.totalAmountField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TotalAmountSpecified {
            get {
                return this.totalAmountFieldSpecified;
            }
            set {
                this.totalAmountFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string FreeText {
            get {
                return this.freeTextField;
            }
            set {
                this.freeTextField = value;
            }
        }
        
        /// <remarks/>
        public DeliveryDateTimeInformationType AppointmentDateTimeInformation {
            get {
                return this.appointmentDateTimeInformationField;
            }
            set {
                this.appointmentDateTimeInformationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InstructionLine")]
        public InstructionLineType[] InstructionLine {
            get {
                return this.instructionLineField;
            }
            set {
                this.instructionLineField = value;
            }
        }
    }
}