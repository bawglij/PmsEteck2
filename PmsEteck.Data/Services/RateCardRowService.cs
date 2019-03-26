using PmsEteck.Data.Extensions;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PmsEteck.Data.Services
{
    public class RateCardRowService
    {
        private PmsEteckContext context;
        private Address Address;
        private RateCardRow RateCardRow;
        private Invoice Invoice;
        private int NumberOfDaysInPeriod = 0;
        private int NumberOfMonths = 1;
        private InvoiceLine InvoiceLine;
        private DateTime FirstPeriodDate;
        private DateTime LastPeriodDate;
        public CultureInfo CultureInfo { get; private set; }

        public RateCardRowService()
        {
            context = new PmsEteckContext() ?? throw new ArgumentNullException(nameof(context));
            CultureInfo = new CultureInfo("nl-NL");
        }

        public RateCardRowService(PmsEteckContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            CultureInfo = new CultureInfo("nl-NL");
        }

        public InvoiceLine CreateInvoiceLine(RateCardRow rateCardRow, Address address, Invoice invoice, DateTime firstPeriodDate, DateTime lastPeriodDate, int invoicePeriodID)
        {
            RateCardRow = rateCardRow;
            Address = address;
            Invoice = invoice;
            FirstPeriodDate = firstPeriodDate;
            LastPeriodDate = lastPeriodDate;
            NumberOfDaysInPeriod = (lastPeriodDate - firstPeriodDate).Days;
            NumberOfMonths = firstPeriodDate.NumberOfMonthsBetweenDates(lastPeriodDate.AddDays(-1));
            InvoiceBatch invoiceBatch = invoice.InvoiceBatch;
            InvoiceLine = new InvoiceLine
            {
                bIsEndCalculation = invoiceBatch.iInvoiceTypeID == 3,
                dAmount = 0,
                dConsumption = 0,
                dEndPosition = 0,
                dQuantity = 1,
                dStartPosition = 0,
                dtStartDate = FirstPeriodDate,
                dtEndDate = LastPeriodDate,
                dTotalAmount = 0,
                dUnitPrice = rateCardRow.dAmount,
                iLedgerNumber = rateCardRow.Rubric.iAccountNumber.Value,
                Invoice = Invoice,
                iRateCardRowID = rateCardRow.iRateCardRowKey,
                iRubricTypeID = rateCardRow.Rubric.iRubricTypeKey.Value,
                iUnitID = invoiceBatch.iInvoiceTypeID == 1 ? 6 : rateCardRow.iUnitKey,
                sDescription = rateCardRow.sDescription.Length <= 50 ? rateCardRow.sDescription : rateCardRow.sDescription.Substring(0, 47) + "...",
                sSettlementCode = string.Join("-", invoice.iYear, invoice.iDebtorID, address.iAddressKey),
                sSettlementText = (invoicePeriodID == 1 ? FirstPeriodDate.ToString("MMMM yyyy", CultureInfo) : invoicePeriodID == 2 ? string.Format("Q{0}-{1}", Math.Ceiling(LastPeriodDate.Month / (decimal)3), FirstPeriodDate.Year) : string.Empty)
            };
            Calculate();
            return InvoiceLine;
        }

        void Calculate()
        {
            ConsumptionService consumptionService = new ConsumptionService(context, Address);
            switch (RateCardRow.iUnitKey)
            {
                case 5:
                    // measurement in Jaar
                    int daysInYear = new DateTime(FirstPeriodDate.Year, 12, 31).DayOfYear;
                    InvoiceLine.dAmount = RateCardRow.dAmount * decimal.Divide(NumberOfMonths, 12);
                    break;
                case 6:
                    // measurement in Maand
                    InvoiceLine.dAmount = RateCardRow.dAmount * NumberOfMonths;
                    break;
                case 7:
                    // measurement in Dag
                    InvoiceLine.dAmount = RateCardRow.dAmount * NumberOfDaysInPeriod;
                    break;
                case 8:
                    // measurement in kW/jaar
                    decimal aantalKW = 0;
                    switch (RateCardRow.iCounterTypeKey)
                    {
                        case 5:
                            // counterType is Warmte
                            aantalKW = Address.dHeatElectricPower.GetValueOrDefault();
                            break;
                        case 6:
                            // counterType is Koude
                            aantalKW = Address.dColdElectricPower.GetValueOrDefault();
                            break;
                        case 8:
                            // counterType is Tapwater
                            aantalKW = Address.dTapWaterPower.GetValueOrDefault();
                            break;
                    }

                    // Check if rateCardRow has Scale
                    if (RateCardRow.iRateCardScaleRowKey.HasValue)
                        // Has ScaleRow
                        aantalKW = aantalKW > RateCardRow.RateCardScaleRow.iRowStart ? Math.Min(RateCardRow.RateCardScaleRow.iRowEnd.HasValue ? RateCardRow.RateCardScaleRow.iRowEnd.Value : int.MaxValue, aantalKW) - RateCardRow.RateCardScaleRow.iRowStart : 0;
                    InvoiceLine.dAmount = RateCardRow.dAmount * decimal.Divide(NumberOfMonths, 12) * aantalKW;
                    break;
                case 9:
                    // measurement in m²/jaar
                    if (RateCardRow.iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (Address.dBVO.GetValueOrDefault() > RateCardRow.RateCardScaleRow.iRowStart && Address.dBVO.GetValueOrDefault() <= (RateCardRow.RateCardScaleRow.iRowEnd.HasValue ? RateCardRow.RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            InvoiceLine.dAmount = RateCardRow.dAmount * decimal.Divide(NumberOfMonths, 12);
                    }
                    else
                    {
                        // No ScaleRow
                        // Amount divided to 12 multiplication by addressBVO
                        InvoiceLine.dAmount = RateCardRow.dAmount * decimal.Divide(NumberOfMonths, 12) * Address.dBVO.GetValueOrDefault();
                    }
                    break;
                case 10:
                    // measurement for Categorie/jaar
                    if (RateCardRow.iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (Address.iCategory.GetValueOrDefault() > RateCardRow.RateCardScaleRow.iRowStart && Address.iCategory.GetValueOrDefault() <= (RateCardRow.RateCardScaleRow.iRowEnd.HasValue ? RateCardRow.RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            InvoiceLine.dAmount = RateCardRow.dAmount * decimal.Divide(NumberOfMonths, 12);
                    }
                    else
                    {
                        InvoiceLine.dAmount = RateCardRow.dAmount * decimal.Divide(NumberOfMonths, 12) * Address.iCategory.GetValueOrDefault();
                    }
                    break;
                case 12:
                    // measurement for kW/maand
                    // extra control for countertype Elektra - Gecontracteerd Vermogen
                    if (RateCardRow.iCounterTypeKey == 12)
                        InvoiceLine.dAmount = RateCardRow.dAmount * Address.dContractedCapacityEnergy.GetValueOrDefault();
                    break;
                case 13:
                    // measurement for m³/h/maand
                    // extra control for countertype Gas - Gecontracteerd Vermogen
                    if (RateCardRow.iCounterTypeKey == 13)
                        InvoiceLine.dAmount = RateCardRow.dAmount * Address.dContractedCapacityGas.GetValueOrDefault();
                    break;
                case 11:
                case 14:
                    // measurement for kW or m³/h
                    // Extra control if unit is kW and counterType is Elektra - Max Vermogen or unit is m³/h and counterType is Gas - Max Vermogen
                    InvoiceLine.dAmount = RateCardRow.dAmount;
                    if ((RateCardRow.iUnitKey == 11 && RateCardRow.iCounterTypeKey == 11) || (RateCardRow.iUnitKey == 14 && RateCardRow.iCounterTypeKey == 14))
                        InvoiceLine.dQuantity = consumptionService.GetConsumption(RateCardRow, FirstPeriodDate, LastPeriodDate);
                    break;
                case 15:
                    // measurement for kVArh
                    // Extra control if counterType is Elektra - Blindvermogen
                    if (RateCardRow.iCounterTypeKey == 15)
                    {
                        InvoiceLine.dQuantity = consumptionService.GetConsumption(RateCardRow, FirstPeriodDate, LastPeriodDate);
                        InvoiceLine.dAmount = RateCardRow.dAmount;
                        // Get Elektra - Dal consumption
                        //Consumption consumption2 = await _consumption.GetAddressConsumptionAsync(Address.iAddressKey, 3, 2, FirstPeriodDate, LastPeriodDate);
                        //elektraConsumption += consumption2.dConsumption;
                        //consumption2 = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 4, 2, FirstPeriodDate, LastPeriodDate);
                        //elektraConsumption += consumption2.dConsumption;
                        //decimal blindConsumption = _blindConsumption.GetBlindConsumptionByAddress(Address.iAddressKey, RateCardRow.iCounterTypeKey.Value, FirstPeriodDate, LastPeriodDate);
                        //InvoiceLine.dQuantity = (blindConsumption - (elektraConsumption * (decimal)0.62));
                    }
                    break;
                default:
                    // what to do with al units with consumption
                    InvoiceLine.dAmount = RateCardRow.dAmount;
                    decimal consumption = 0;
                    // Control if counterType is Elektra - Som then is consumption sum of Elektra - Piek + Elektra - Dal + Elektra - Enkel
                    if (RateCardRow.iCounterTypeKey == 10)
                    {
                        consumption = consumptionService.GetConsumption(RateCardRow, FirstPeriodDate, LastPeriodDate);
                        //Consumption consumptionRow = await _consumption.GetAddressConsumptionAsync(Address.iAddressKey, 1, RateCardRow.iUnitKey, FirstPeriodDate, LastPeriodDate);
                        //consumption += consumptionRow.dConsumption;
                        //consumptionRow = await _consumption.GetAddressConsumptionAsync(Address.iAddressKey, 3, RateCardRow.iUnitKey, FirstPeriodDate, LastPeriodDate);
                        //consumption += consumptionRow.dConsumption;
                        //consumptionRow = await _consumption.GetAddressConsumptionAsync(Address.iAddressKey, 4, RateCardRow.iUnitKey, FirstPeriodDate, LastPeriodDate);
                        //InvoiceLine.dtStartDate = FirstPeriodDate;
                        //InvoiceLine.dtEndDate = LastPeriodDate;
                        //consumption += consumptionRow.dConsumption;
                    }
                    else
                    {
                        consumption = consumptionService.GetConsumption(RateCardRow, FirstPeriodDate, LastPeriodDate);
                        InvoiceLine.dConsumption = consumption;
                        InvoiceLine.dStartPosition = consumptionService.Consumptions.Count > 0 ? consumptionService.Consumptions.LastOrDefault().dEndPosition - consumption : 0;
                        InvoiceLine.dtStartDate = FirstPeriodDate;
                        InvoiceLine.dEndPosition = consumptionService.Consumptions.Count > 0 ? consumptionService.Consumptions.LastOrDefault().dEndPosition : 0;
                        InvoiceLine.dtEndDate = LastPeriodDate;
                        //Consumption consumptionRow = await _consumption.GetAddressConsumptionAsync(Address.iAddressKey, RateCardRow.iCounterTypeKey.Value, RateCardRow.iUnitKey, FirstPeriodDate, LastPeriodDate);
                        //consumption = consumptionRow.dConsumption;
                        //InvoiceLine.dStartPosition = consumptionRow.dEndPosition - consumptionRow.dConsumption;
                        //InvoiceLine.dtStartDate = consumptionRow.dtStartDateTime;
                        //InvoiceLine.dEndPosition = consumptionRow.dEndPosition;
                        //InvoiceLine.dtEndDate = consumptionRow.dtEndDateTime;
                    }

                    // Check if rateCardRow has ScaleRow
                    if (RateCardRow.iRateCardScaleRowKey.HasValue)
                    {
                        // changing rowStart and rowEnd from year values to month values
                        decimal rowStart = RateCardRow.RateCardScaleRow.iRowStart;
                        decimal rowEnd = RateCardRow.RateCardScaleRow.iRowEnd.HasValue ? RateCardRow.RateCardScaleRow.iRowEnd.Value : decimal.MaxValue;
                        if (consumption != 0)
                        {
                            context.Entry(RateCardRow).Collection(c => c.RateCardScaleHistories).Load();
                            List<RateCardScaleHistory> rateCardScaleHistories = RateCardRow.RateCardScaleHistories.Where(w => w.AddressID == Address.iAddressKey && w.DebtorID == Invoice.iDebtorID && w.RateCardRowID == RateCardRow.iRateCardRowKey && w.Period.Year == FirstPeriodDate.Year).ToList();
                            if (rateCardScaleHistories.Count != 0)
                            {
                                if (rateCardScaleHistories.Sum(sm => sm.Consumed) < rowEnd)
                                {
                                    decimal staffelRestant = rowEnd - rateCardScaleHistories.Sum(sm => sm.Consumed);
                                    consumption = Math.Min(staffelRestant, consumption);
                                    RateCardRow.RateCardScaleHistories.Add(new RateCardScaleHistory
                                    {
                                        AddressID = Address.iAddressKey,
                                        Consumed = consumption,
                                        DebtorID = Invoice.iDebtorID,
                                        Period = FirstPeriodDate.FirstDayOfMonth(),
                                        RateCardRowID = RateCardRow.iRateCardRowKey
                                    });
                                }
                                else
                                {
                                    consumption = 0;
                                }
                            }
                            else if(consumption > rowStart)
                            {
                                consumption = consumption > rowStart ? Math.Min(rowEnd, consumption) - rowStart : 0;
                                RateCardRow.RateCardScaleHistories.Add(new RateCardScaleHistory
                                {
                                    AddressID = Address.iAddressKey,
                                    Consumed = consumption,
                                    DebtorID = Invoice.iDebtorID,
                                    Period = FirstPeriodDate.FirstDayOfMonth(),
                                    RateCardRowID = RateCardRow.iRateCardRowKey
                                });
                            }
                            else
                            {
                                consumption = 0;
                            }
                        }
                    }
                    InvoiceLine.dQuantity = consumption;
                    break;
            }

            if (RateCardRow.Rubric.iRubricGroupKey == 1)
                InvoiceLine.dQuantity *= -1;
            InvoiceLine.dTotalAmount = InvoiceLine.dQuantity * InvoiceLine.dAmount;
        }
    }
}
