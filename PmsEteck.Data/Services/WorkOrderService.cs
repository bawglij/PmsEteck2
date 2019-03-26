using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Ketenstandaard.Onderhoudsopdracht;
using PmsEteck.Data.Models.Results;
using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PmsEteck.Data.Services
{
    public class WorkOrderService : BaseService<WorkOrder>
    {
        public EndpointAddress basicendpoint = new EndpointAddress(new Uri("https://esight.esightmonitoring.nl/eteck/ws/Company.asmx"));
        public BasicHttpBinding basicHttpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

        private new readonly PmsEteckContext context;

        public WorkOrderService(PmsEteckContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public WorkOrderService()
        {
            context = new PmsEteckContext();
        }

        public async Task<Result> ShareWithMaintenanceContact(Guid workOrderId)
        {
            WorkOrder workOrder = FindById(workOrderId);
            // Check if sending email or by ketenstandaard
            Result result = Result.Success;
            switch (workOrder.ServiceTicket.MaintenanceContact.MaintenanceContactCommunicationType)
            {
                case MaintenanceContactCommunicationType.Ketenstandaard:
                    MaintenanceInstructionType message = MapMaintenanceInstructionType(workOrder);
                    WebserviceConnectionService webserviceConnectionService = new WebserviceConnectionService();
                    WebserviceConnection webserviceConnection = webserviceConnectionService.FindByMaintenanceContactId(workOrder.ServiceTicket.MaintenanceContactID.Value);
                    //result = SendKetenMessage(message, webserviceConnection, workOrder.WorkOrderID);
                    break;
                case MaintenanceContactCommunicationType.Email:
                default:
                    result = await workOrder.SendMailAsync(context);
                    break;
            }

            // Set status for send message if workorderstatus = nieuw
            if (result.Succeeded && workOrder.Status.StatusCode == 100)
                workOrder.Status = await context.WorkOrderStatuses.FirstOrDefaultAsync(f => f.StatusCode == 201);
            return await Task.FromResult(result);
        }
        /*
        private Result SendKetenMessage(MaintenanceInstructionType maintenanceInstructionType, WebserviceConnection webserviceConnection, Guid messageId)
        {
            //WSHttpBinding binding = new WSHttpBinding();
            //binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
            //binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.InheritedFromHost;
            //ServiceHost serviceHost = new ServiceHost()
            MessageServiceSoapClient messageService = new MessageServiceSoapClient(basicHttpBinding, basicendpoint);
            messageService.Credentials = !string.IsNullOrWhiteSpace(webserviceConnection.Domain) ? new NetworkCredential(webserviceConnection.UserName, webserviceConnection.PasswordHash, webserviceConnection.Domain) : new NetworkCredential(webserviceConnection.UserName, webserviceConnection.PasswordHash);
            //messageService.ClientCertificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate("", "", System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.));
            messageService.Url = webserviceConnection.BaseUrl;
            messageService.CustomInfo = new VolkerWesselsServices.CustomInfoType() {
                ApplicationId = "ApplicationId",
                RelationId = "RelationId"
            };
            
            string message = Serialize(maintenanceInstructionType);
            message = message.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n", "");
            VolkerWesselsServices.MessageType messageType = new VolkerWesselsServices.MessageType() {
                MsgContent = message,
                MsgProperties = new VolkerWesselsServices.MessagePropertiesType()
                {
                    MsgDateTime = maintenanceInstructionType.MessageDate,
                    MsgFormat = "SALES",
                    MsgId = messageId.ToString(),
                    MsgType = "MTNINS",
                    MsgVersion = "1"

                }
            };
            VolkerWesselsServices.MessageResponseType result = messageService.PostMessage(messageType);

            return Result.Success;
        }
        */
        private static string Serialize<T>(T maintenanceInstructionType)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, maintenanceInstructionType);
            return stringwriter.ToString();
        }

        public static MaintenanceInstructionType MapMaintenanceInstructionType(WorkOrder workOrder)
        {
            MaintenanceInstructionType message = new MaintenanceInstructionType()
            {
                MessageNumber = workOrder.WorkOrderCodeString,
                MessageDate = workOrder.CreatedDateTime,
                MessageTime = workOrder.CreatedDateTime,
                MessageTimeSpecified = true,
                //Gegevens van Eteck
                Buyer = new PartyWithContactType()
                {
                    //ContactInformation = new ContactInformationType[] {
                    //            new ContactInformationType(){},
                    //        },
                    //Country = "NL",
                    //City = "Waddinxveen",
                    //Street = "Coenecoop",
                    GLN = "GLNNumberEteck",
                    //HouseNumber = "12",
                    Name = "Eteck Energie Techniek B.V.",
                    //PostalCode = "2741PG"
                },
                //Affiliate = new PartyWithContactType() { },
                Contractor = new PartyWithContactType() {
                    GLN = workOrder.ServiceTicket.MaintenanceContact.GlobalLocationNumber,
                    Name = workOrder.ServiceTicket.MaintenanceContact.sOrganisation
                },
                InstructionData = new InstructionDataType()
                {
                    InstructionNumber = workOrder.WorkOrderCodeString,
                    InstructionSubNumber = "1",
                    InstructionDate = workOrder.CreatedDateTime,
                    InstructionDateSpecified = true,
                    InstructionTime = workOrder.CreatedDateTime,
                    InstructionTimeSpecified = true,
                    FreeText = workOrder.Instruction,
                    //ContractReference = new ContractReferenceType() { },
                    MaintenanceLocation = new MaintenanceLocationType()
                    {
                        //ComplexNumber = ""
                        //RealEstateUnitNumber = "",
                        Street = workOrder.ServiceTicket.Address.sStreetName,
                        HouseNumber = workOrder.ServiceTicket.Address.iNumber.ToString(),
                        HouseNumberSuffix = workOrder.ServiceTicket.Address.sNumberAddition,
                        PostalCode = workOrder.ServiceTicket.Address.sPostalCode,
                        City = workOrder.ServiceTicket.Address.sCity,
                        //ContactPersonName = "",
                        //PhoneNumber1 = "",
                        //PhoneNumber2 = "",
                        //EmailAddress1 = "",
                        //GeographicalCoordinates = workOrder.ServiceTicket.Address.dLatitude.HasValue && workOrder.ServiceTicket.Address.dLongitude.HasValue ? new GeographicalCoordinatesType()
                        //{
                        //    Longitude = workOrder.ServiceTicket.Address.dLongitude.ToString(),
                        //    Latitude = workOrder.ServiceTicket.Address.dLatitude.ToString()
                        //} : null
                    },
                    //AppointmentDateTimeInformation = new DeliveryDateTimeInformationType()
                    //{
                    //    DeliveryTimeFrame = new DeliveryTimeFrameType()
                    //    {
                    //        DeliveryDateEarliest = DateTime.UtcNow,
                    //        DeliveryDateEarliestSpecified = true,
                    //        DeliveryDateLatest = DateTime.UtcNow,
                    //        DeliveryDateLatestSpecified = true,
                    //        DeliveryTimeEarliest = DateTime.UtcNow,
                    //        DeliveryTimeEarliestSpecified = true,
                    //        DeliveryTimeLatest = DateTime.UtcNow,
                    //        DeliveryTimeLatestSpecified = true
                    //    },
                    //    RequiredDeliveryDate = DateTime.UtcNow,
                    //    RequiredDeliveryDateSpecified = true,
                    //    RequiredDeliveryTime = DateTime.UtcNow,
                    //    RequiredDeliveryTimeSpecified = true
                    //},
                    InstructionLine = new InstructionLineType[] {
                            new InstructionLineType(){
                                LineNumber = "1",
                                Quantity = 1,
                                MeasurementUnitQuantity = OrderUnitCodeType.HUR,
                                //ShortDescription = string.Empty,
                                LongDescription = workOrder.ServiceTicket.Description,
                                //LEDOInformation = new LEDOInformationType(){
                                //    Cause = string.Empty,
                                //    Defect = string.Empty,
                                //    Element = string.Empty,
                                //    Location = string.Empty
                                //},
                                FreeText = workOrder.Instruction,
                                PriceInformation = new PriceInformationType(){
                                    Price = workOrder.ServiceTicket.MaintenanceContact.UnitPriceHourlyRate.GetValueOrDefault(),
                                    PriceBase = new PriceBaseType(){
                                        MeasureUnitPriceBasis = OrderUnitCodeType.HUR,
                                        NumberOfUnitsInPriceBasis = 0,
                                        //PriceBaseDescription = string.Empty
                                    }
                                },
                                VATInformation = new VATInformationAndBaseAmountType(){
                                    //VATBaseAmount = 0,
                                    //VATBaseAmountSpecified = true,
                                    VATPercentage = new decimal(0.21),
                                    VATPercentageSpecified = true,
                                    VATRate = VATInformationAndBaseAmountTypeVATRate.E
                                }
                                //Attachment = new AttachmentType[]{
                                //    new AttachmentType(){
                                //        AttachedData = new byte[]{ },
                                //        AttachmentCharacteristics = new AttachmentCharacteristicsType[]{
                                //            new AttachmentCharacteristicsType()
                                //            {
                                //                AttributeName = string.Empty,
                                //                AttributeValue = string.Empty
                                //            }
                                //        },
                                //        DocumentType = AttachmentTypeDocumentType.CAD,
                                //        FileName = string.Empty,
                                //        FileType = string.Empty,
                                //        URL = new Uri("").ToString()
                                //    }
                                //},
                                //NormPriceCode = string.Empty,

                            }
                        },
                    InstructionInformation = new InstructionInformationType()
                    {
                        InstructionType = InstructionTypeCodeType.INS,
                        InstructionTypeSpecified = true,
                        InstructionAgreementMethod = InstructionAgreementMethodCodeType.AFK,
                        InstructionAgreementMethodSpecified = true,
                        InstructionTypeSpecification = InstructionTypeSpecificationCodeType.GAR,
                        InstructionTypeSpecificationSpecified = true,
                    },

                },

            };
            return message;
        }
    }
}
