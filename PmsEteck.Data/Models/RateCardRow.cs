using PmsEteck.Data.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table(name: "RateCardRows", Schema = "meter")]
    public class RateCardRow
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        private MaximumPower _maximumPower = new MaximumPower();
        private BlindConsumption _blindConsumption = new BlindConsumption();
        private Consumption _consumption = new Consumption();
        #endregion

        #region Properties

        [Key]
        public int iRateCardRowKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sDescription { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Tariefkaartjaar")]
        public int iRateCardYearKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Rubriek")]
        public int iRubricKey { get; set; }

        [Display(Name = "Telwerktype")]
        public int? iCounterTypeKey { get; set; }

        [Display(Name = "Staffel")]
        public int? iRateCardScaleRowKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eenheid")]
        public int iUnitKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Bedrag")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal dAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "BTW")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal dVAT { get; set; }

        [Display(Name = "Geïndexeerd?")]
        public bool bIndexed { get; set; }

        [Display(Name = "Korting")]
        public bool Discount { get; set; }

        public int? VatConditionID { get; set; }

        public virtual CounterType CounterType { get; set; }

        public virtual RateCardScaleRow RateCardScaleRow { get; set; }

        public virtual RateCardYear RateCardYear { get; set; }

        public virtual Rubric Rubric { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual VatCondition VatCondition { get; set; }

        [Display(Name = "Betaalstaffel")]
        public ICollection<RateCardScaleHistory> RateCardScaleHistories { get; set; }

        #endregion

        #region Methods
        public decimal GetAddressAmount(RateCardRow rateCardRow, Address address, DateTime periodStartDate, DateTime periodEndDate)
        {
            //periodStartDate as 1-1-2016 and periodEndDate as first day of next month 1-2-2016

            decimal amount = 0;
            // Count number of days between periodStartDate and periodEndDate
            int daysInPeriod = (int)(periodEndDate - periodStartDate).Days;

            // Count number of days in the year of periodStartDate
            DateTime firstDayOfYear = new DateTime(periodStartDate.Year, 1, 1);
            DateTime nextYear = firstDayOfYear.AddYears(1);
            int daysInYear = new DateTime(periodStartDate.Year, 12, 31).DayOfYear;

            // Count number of Months between periodStartDate and periodEndDate
            int monthsInPeriod = ((periodEndDate.Year - periodStartDate.Year) * 12) + periodEndDate.Month - periodStartDate.Month;

            //int daysInMonth = DateTime.DaysInMonth(periodStartDate.Year, periodStartDate.Month);
            switch (rateCardRow.iUnitKey)
            {
                case 5:
                    // measurement in Jaar
                    //amount = rateCardRow.dAmount / daysInYear * daysInPeriod;
                    amount = rateCardRow.dAmount / 12;
                    break;
                case 6:
                    // measurement in Maand
                    //amount = rateCardRow.dAmount * monthsInPeriod;
                    amount = rateCardRow.dAmount;
                    break;
                case 7:
                    // measurement in Dag
                    amount = rateCardRow.dAmount * daysInPeriod;
                    break;
                case 8:
                    // measurement in kW/jaar
                    decimal aantalKW = 0;
                    switch (rateCardRow.iCounterTypeKey)
                    {
                        case 5:
                            // counterType is Warmte
                            aantalKW = address.dHeatElectricPower.GetValueOrDefault();
                            break;
                        case 6:
                            // counterType is Koude
                            aantalKW = address.dColdElectricPower.GetValueOrDefault();
                            break;
                        case 8:
                            // counterType is Tapwater
                            aantalKW = address.dTapWaterPower.GetValueOrDefault();
                            break;
                    }

                    // Check if rateCardRow has Scale
                    if (rateCardRow.iRateCardScaleRowKey.HasValue)
                        // Has ScaleRow
                        aantalKW = aantalKW > rateCardRow.RateCardScaleRow.iRowStart ? Math.Min(rateCardRow.RateCardScaleRow.iRowEnd.HasValue ? rateCardRow.RateCardScaleRow.iRowEnd.Value : int.MaxValue, aantalKW) - rateCardRow.RateCardScaleRow.iRowStart : 0;
                    amount = rateCardRow.dAmount / 12 * aantalKW;
                    break;
                case 9:
                    // measurement in m²/jaar
                    if (rateCardRow.iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (address.dBVO.GetValueOrDefault() > rateCardRow.RateCardScaleRow.iRowStart && address.dBVO.GetValueOrDefault() <= (rateCardRow.RateCardScaleRow.iRowEnd.HasValue ? rateCardRow.RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            amount = rateCardRow.dAmount / 12;
                    }
                    else
                    {
                        // No ScaleRow
                        // Amount divided to 12 multiplication by addressBVO
                        amount = rateCardRow.dAmount / 12 * address.dBVO.GetValueOrDefault();
                    }
                    break;
                case 10:
                    // measurement for Categorie/jaar
                    if (rateCardRow.iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (address.iCategory.GetValueOrDefault() > rateCardRow.RateCardScaleRow.iRowStart && address.iCategory.GetValueOrDefault() <= (rateCardRow.RateCardScaleRow.iRowEnd.HasValue ? rateCardRow.RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            amount = rateCardRow.dAmount / 12;
                    }
                    else
                    {
                        amount = rateCardRow.dAmount / 12 * address.iCategory.GetValueOrDefault();
                    }
                    break;
                case 12:
                    // measurement for kW/maand
                    // extra control for countertype Elektra - Gecontracteerd Vermogen
                    if (rateCardRow.iCounterTypeKey == 12)
                        amount = rateCardRow.dAmount * address.dContractedCapacityEnergy.GetValueOrDefault();
                    break;
                case 13:
                    // measurement for m³/h/maand
                    // extra control for countertype Gas - Gecontracteerd Vermogen
                    if (rateCardRow.iCounterTypeKey == 13)
                        amount = rateCardRow.dAmount * address.dContractedCapacityGas.GetValueOrDefault();
                    break;
                case 11:
                case 14:
                    // measurement for kW or m³/h
                    // Extra control if unit is kW and counterType is Elektra - Max Vermogen or unit is m³/h and counterType is Gas - Max Vermogen
                    if ((rateCardRow.iUnitKey == 11 && rateCardRow.iCounterTypeKey == 11) || (rateCardRow.iUnitKey == 14 && rateCardRow.iCounterTypeKey == 14))
                        amount = _maximumPower.GetMaxPowerByAddressInPeriod(address.iAddressKey, rateCardRow.iCounterTypeKey.Value, periodStartDate, periodEndDate);
                    break;
                case 15:
                    // measurement for kVArh
                    // Extra control if counterType is Elektra - Blindvermogen
                    if (rateCardRow.iCounterTypeKey == 15)
                    {
                        decimal elektraConsumption = 0;
                        elektraConsumption += _consumption.GetAddressConsumption(address.iAddressKey, rateCardRow.RateCardYear.iRateCardKey, periodStartDate, periodEndDate, 3, 2);
                        elektraConsumption += _consumption.GetAddressConsumption(address.iAddressKey, rateCardRow.RateCardYear.iRateCardKey, periodStartDate, periodEndDate, 4, 2);
                        decimal blindConsumption = _blindConsumption.GetBlindConsumptionByAddress(address.iAddressKey, rateCardRow.iCounterTypeKey.Value, periodStartDate, periodEndDate);
                        amount = (blindConsumption - (elektraConsumption * (decimal)0.62)) * rateCardRow.dAmount;
                    }
                    break;
                default:
                    // what to do with al units with consumption
                    decimal consumption = 0;
                    // Control if counterType is Elektra - Som then is consumption sum of Elektra - Piek + Elektra - Dal + Elektra - Enkel
                    if (rateCardRow.iCounterTypeKey == 10)
                    {
                        consumption = _consumption.GetAddressConsumption(address.iAddressKey, rateCardRow.RateCardYear.iRateCardKey, periodStartDate, periodEndDate, 1, rateCardRow.iUnitKey);
                        consumption += _consumption.GetAddressConsumption(address.iAddressKey, rateCardRow.RateCardYear.iRateCardKey, periodStartDate, periodEndDate, 3, rateCardRow.iUnitKey);
                        consumption += _consumption.GetAddressConsumption(address.iAddressKey, rateCardRow.RateCardYear.iRateCardKey, periodStartDate, periodEndDate, 4, rateCardRow.iUnitKey);

                    }
                    else
                    {
                        consumption = _consumption.GetAddressConsumption(address.iAddressKey, rateCardRow.RateCardYear.iRateCardKey, periodStartDate, periodEndDate, rateCardRow.iCounterTypeKey, rateCardRow.iUnitKey);
                    }

                    // Check if rateCardRow has ScaleRow
                    if (rateCardRow.iRateCardScaleRowKey.HasValue)
                    {
                        // changing rowStart and rowEnd from year values to month values
                        decimal rowStart = rateCardRow.RateCardScaleRow.iRowStart / 12;
                        decimal rowEnd = rateCardRow.RateCardScaleRow.iRowEnd.HasValue ? rateCardRow.RateCardScaleRow.iRowEnd.Value / 12 : decimal.MaxValue;
                        consumption = consumption > rowStart ? Math.Min(rowEnd, consumption) - rowStart : 0;
                    }
                    amount = consumption * rateCardRow.dAmount;
                    break;
            }

            return amount;
        }

        public async Task<InvoiceLine> CreateInvoiceLineAsync(int addressKey, Invoice invoice, DateTime periodStartDate, DateTime periodEndDate)
        {
            //periodStartDate as 1-1-2016 and periodEndDate as first day of next month 1-2-2016
            Address address = await db.Addresses.FindAsync(addressKey);

            InvoiceLine invoiceLine = new InvoiceLine
            {
                bIsEndCalculation = invoice.InvoiceBatch.iInvoiceTypeID == 3,
                dAmount = 0,
                dQuantity = 1,
                dUnitPrice = dAmount,
                iLedgerNumber = Rubric.iAccountNumber.Value,
                Invoice = invoice,
                //iRateCardRowID = iRateCardRowKey,
                RateCardRow = this,
                VatConditionCode = VatCondition?.Code,
                iRubricTypeID = Rubric.iRubricTypeKey ?? 2,
                iUnitID = invoice.InvoiceBatch.iInvoiceTypeID == 1 ? 6 : iUnitKey,
                sDescription = sDescription.Length <= 50 ? sDescription : sDescription.Substring(0, 47) + "...",
                sSettlementCode = string.Join("-", invoice.iYear, invoice.iDebtorID, address.iAddressKey),
                sSettlementText = periodStartDate.ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture)
            };
            // Count number of days between periodStartDate and periodEndDate
            int periodDays = (int)(periodEndDate - periodStartDate).Days;

            int monthsInPeriod = periodStartDate.NumberOfMonthsBetweenDates(periodEndDate);

            switch (iUnitKey)
            {
                case 5:
                    // measurement in Jaar
                    int daysInYear = new DateTime(periodStartDate.Year, 12, 31).DayOfYear;
                    invoiceLine.dAmount = dAmount * decimal.Divide(monthsInPeriod, 12);
                    break;
                case 6:
                    // measurement in Maand
                    invoiceLine.dAmount = dAmount * monthsInPeriod;
                    break;
                case 7:
                    // measurement in Dag
                    invoiceLine.dAmount = dAmount * periodDays;
                    break;
                case 8:
                    // measurement in kW/jaar
                    decimal aantalKW = 0;
                    switch (iCounterTypeKey)
                    {
                        case 5:
                            // counterType is Warmte
                            aantalKW = address.dHeatElectricPower.GetValueOrDefault();
                            break;
                        case 6:
                            // counterType is Koude
                            aantalKW = address.dColdElectricPower.GetValueOrDefault();
                            break;
                        case 8:
                            // counterType is Tapwater
                            aantalKW = address.dTapWaterPower.GetValueOrDefault();
                            break;
                    }

                    // Check if rateCardRow has Scale
                    if (iRateCardScaleRowKey.HasValue)
                        // Has ScaleRow
                        aantalKW = aantalKW > RateCardScaleRow.iRowStart ? Math.Min(RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : int.MaxValue, aantalKW) - RateCardScaleRow.iRowStart : 0;
                    invoiceLine.dAmount = dAmount * decimal.Divide(monthsInPeriod, 12) * aantalKW;
                    break;
                case 9:
                    // measurement in m²/jaar
                    if (iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (address.dBVO.GetValueOrDefault() > RateCardScaleRow.iRowStart && address.dBVO.GetValueOrDefault() <= (RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            invoiceLine.dAmount = dAmount * decimal.Divide(monthsInPeriod, 12);
                    }
                    else
                    {
                        // No ScaleRow
                        // Amount divided to 12 multiplication by addressBVO
                        invoiceLine.dAmount = dAmount * decimal.Divide(monthsInPeriod, 12) * address.dBVO.GetValueOrDefault();
                    }
                    break;
                case 10:
                    // measurement for Categorie/jaar
                    if (iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (address.iCategory.GetValueOrDefault() > RateCardScaleRow.iRowStart && address.iCategory.GetValueOrDefault() <= (RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            invoiceLine.dAmount = dAmount * decimal.Divide(monthsInPeriod, 12);
                    }
                    else
                    {
                        invoiceLine.dAmount = dAmount * decimal.Divide(monthsInPeriod, 12) * address.iCategory.GetValueOrDefault();
                    }
                    break;
                case 12:
                    // measurement for kW/maand
                    // extra control for countertype Elektra - Gecontracteerd Vermogen
                    if (iCounterTypeKey == 12)
                        invoiceLine.dAmount = dAmount * address.dContractedCapacityEnergy.GetValueOrDefault();
                    break;
                case 13:
                    // measurement for m³/h/maand
                    // extra control for countertype Gas - Gecontracteerd Vermogen
                    if (iCounterTypeKey == 13)
                        invoiceLine.dAmount = dAmount * address.dContractedCapacityGas.GetValueOrDefault();
                    break;
                case 11:
                case 14:
                    // measurement for kW or m³/h
                    // Extra control if unit is kW and counterType is Elektra - Max Vermogen or unit is m³/h and counterType is Gas - Max Vermogen
                    if ((iUnitKey == 11 && iCounterTypeKey == 11) || (iUnitKey == 14 && iCounterTypeKey == 14))
                        invoiceLine.dAmount = _maximumPower.GetMaxPowerByAddressInPeriod(address.iAddressKey, iCounterTypeKey.Value, periodStartDate, periodEndDate);
                    break;
                case 15:
                    // measurement for kVArh
                    // Extra control if counterType is Elektra - Blindvermogen
                    if (iCounterTypeKey == 15)
                    {
                        decimal elektraConsumption = 0;
                        // Get Elektra - Dal consumption
                        Consumption consumption2 = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 3, 2, periodStartDate, periodEndDate);
                        elektraConsumption += consumption2.dConsumption;
                        consumption2 = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 4, 2, periodStartDate, periodEndDate);
                        elektraConsumption += consumption2.dConsumption;
                        decimal blindConsumption = _blindConsumption.GetBlindConsumptionByAddress(address.iAddressKey, iCounterTypeKey.Value, periodStartDate, periodEndDate);
                        invoiceLine.dQuantity = (blindConsumption - (elektraConsumption * (decimal)0.62));
                        invoiceLine.dAmount = dAmount;
                    }
                    break;
                default:
                    invoiceLine.dAmount = dAmount;
                    // what to do with al units with consumption
                    decimal consumption = 0;
                    // Control if counterType is Elektra - Som then is consumption sum of Elektra - Piek + Elektra - Dal + Elektra - Enkel
                    if (iCounterTypeKey == 10)
                    {
                        Consumption consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 1, iUnitKey, periodStartDate, periodEndDate);
                        consumption += consumptionRow.dConsumption;
                        consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 3, iUnitKey, periodStartDate, periodEndDate);
                        consumption += consumptionRow.dConsumption;
                        consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 4, iUnitKey, periodStartDate, periodEndDate);
                        invoiceLine.dtStartDate = periodStartDate;
                        invoiceLine.dtEndDate = periodEndDate;
                        consumption += consumptionRow.dConsumption;
                    }
                    else
                    {
                        Consumption consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, iCounterTypeKey.Value, iUnitKey, periodStartDate, periodEndDate);
                        consumption = consumptionRow.dConsumption;
                        invoiceLine.dStartPosition = consumptionRow.dEndPosition - consumptionRow.dConsumption;
                        invoiceLine.dtStartDate = consumptionRow.dtStartDateTime;
                        invoiceLine.dEndPosition = consumptionRow.dEndPosition;
                        invoiceLine.dtEndDate = consumptionRow.dtEndDateTime;
                    }

                    // Check if rateCardRow has ScaleRow
                    if (iRateCardScaleRowKey.HasValue)
                    {
                        // changing rowStart and rowEnd from year values to month values
                        decimal rowStart = RateCardScaleRow.iRowStart;
                        decimal rowEnd = RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : decimal.MaxValue;
                        consumption = consumption > rowStart ? Math.Min(rowEnd, consumption) - rowStart : 0;
                        if(consumption != 0)
                        { 
                            List<RateCardScaleHistory> rateCardScaleHistories = RateCardScaleHistories.Where(w => w.AddressID == addressKey && w.DebtorID == invoice.iDebtorID && w.RateCardRowID == iRateCardRowKey && w.Period.Year == periodStartDate.Year).ToList();
                            if (rateCardScaleHistories.Count != 0)
                            {
                                if (rateCardScaleHistories.Sum(sm => sm.Consumed) < rowEnd)
                                {
                                    consumption -= rateCardScaleHistories.Sum(sm => sm.Consumed);
                                    RateCardScaleHistories.Add(new RateCardScaleHistory
                                    {
                                        AddressID = addressKey,
                                        Consumed = consumption,
                                        DebtorID = invoice.iDebtorID,
                                        Period = periodStartDate.FirstDayOfMonth(),
                                        RateCardRowID = iRateCardRowKey
                                    });
                                }
                            }
                            else
                            {
                                RateCardScaleHistories.Add(new RateCardScaleHistory {
                                    AddressID = addressKey,
                                    Consumed = consumption,
                                    DebtorID = invoice.iDebtorID,
                                    Period = periodStartDate.FirstDayOfMonth(),
                                    RateCardRowID = iRateCardRowKey
                                });
                            }
                        }
                    }
                    invoiceLine.dQuantity = consumption;
                    break;
            }
            if (Rubric.iRubricGroupKey == 1)
                invoiceLine.dQuantity *= -1;
            
            invoiceLine.dTotalAmount = invoiceLine.dQuantity * invoiceLine.dAmount;
            return invoiceLine;
        }

        public async Task<decimal> GetConsumptionAsync(int addressID, DateTime periodStart, DateTime periodEnd)
        {
            Address address = await db.Addresses.FindAsync(addressID);
            int monthsInPeriod = periodStart.NumberOfMonthsBetweenDates(periodEnd);
            int daysInPeriod = (periodEnd - periodStart).Days;
            switch (iUnitKey)
            {
                case 5:
                    // Eenheid per jaar
                    return decimal.Divide(monthsInPeriod, 12);
                case 6:
                    return monthsInPeriod;
                case 7:
                    return daysInPeriod;
                case 8:
                    decimal aantalKW = 0;
                    switch (iCounterTypeKey)
                    {
                        case 5:
                            // counterType is Warmte
                            aantalKW = address.dHeatElectricPower.GetValueOrDefault();
                            break;
                        case 6:
                            // counterType is Koude
                            aantalKW = address.dColdElectricPower.GetValueOrDefault();
                            break;
                        case 8:
                            // counterType is Tapwater
                            aantalKW = address.dTapWaterPower.GetValueOrDefault();
                            break;
                    }

                    // Check if rateCardRow has Scale
                    if (iRateCardScaleRowKey.HasValue)
                        // Has ScaleRow
                        aantalKW = aantalKW > RateCardScaleRow.iRowStart ? Math.Min(RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : int.MaxValue, aantalKW) - RateCardScaleRow.iRowStart : 0;
                    return decimal.Divide(monthsInPeriod, 12) * aantalKW;
                case 9:
                    if (iRateCardScaleRowKey.HasValue)
                    {
                        if (address.dBVO.GetValueOrDefault() > RateCardScaleRow.iRowStart && address.dBVO.GetValueOrDefault() <= (RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                        {
                            return decimal.Divide(monthsInPeriod, 12);
                        }
                        return 0;
                    }
                    return decimal.Divide(monthsInPeriod, 12) * address.dBVO.GetValueOrDefault();
                case 10:
                    if (iRateCardScaleRowKey.HasValue)
                    {
                        // Has ScaleRow
                        if (address.iCategory.GetValueOrDefault() > RateCardScaleRow.iRowStart && address.iCategory.GetValueOrDefault() <= (RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value : int.MaxValue))
                            return decimal.Divide(monthsInPeriod, 12);
                        return 0;
                    }
                    return decimal.Divide(monthsInPeriod, 12) * address.iCategory.GetValueOrDefault();
                case 12:
                    if (iCounterTypeKey == 12)
                        return address.dContractedCapacityEnergy.GetValueOrDefault();
                    return 0;
                case 13:
                    if (iCounterTypeKey == 13)
                        address.dContractedCapacityGas.GetValueOrDefault();
                    return 0;
                case 11:
                case 14:
                    if (iUnitKey == iCounterTypeKey)
                        return _maximumPower.GetMaxPowerByAddressInPeriod(addressID, iCounterTypeKey.Value, periodStart, periodEnd);
                    return 0;
                case 15:
                    if (iCounterTypeKey == 15)
                    {
                        decimal elektraConsumption = 0;
                        // Elektra - Dal Consumption
                        var elektraDal = await _consumption.GetAddressConsumptionAsync(addressID, 3, 2, periodStart, periodEnd);
                        elektraConsumption += elektraDal.dConsumption;
                        // Elektra - Piek Consumption
                        var elektraPiek = await _consumption.GetAddressConsumptionAsync(addressID, 3, 2, periodStart, periodEnd);
                        elektraConsumption += elektraPiek.dConsumption;
                        // Elektra - Blindconsumption
                        decimal blindConsumption = _blindConsumption.GetBlindConsumptionByAddress(addressID, iCounterTypeKey.Value, periodStart, periodEnd);
                        return (blindConsumption - (elektraConsumption * (decimal)0.62));
                    }
                    return 0;
                default:
                    decimal consumption = 0;
                    if (iCounterTypeKey == 10)
                    {
                        Consumption consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 1, iUnitKey, periodStart, periodEnd);
                        consumption += consumptionRow.dConsumption;
                        consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 3, iUnitKey, periodStart, periodEnd);
                        consumption += consumptionRow.dConsumption;
                        consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, 4, iUnitKey, periodStart, periodEnd);
                        consumption += consumptionRow.dConsumption;
                    }
                    else
                    {
                        Consumption consumptionRow = await _consumption.GetAddressConsumptionAsync(address.iAddressKey, iCounterTypeKey.Value, iUnitKey, periodStart, periodEnd);
                        consumption = consumptionRow.dConsumption;
                    }

                    if (iRateCardScaleRowKey.HasValue)
                    {
                        // changing rowStart and rowEnd from year values to month values
                        decimal rowStart = RateCardScaleRow.iRowStart * decimal.Divide(monthsInPeriod, 12);
                        decimal rowEnd = RateCardScaleRow.iRowEnd.HasValue ? RateCardScaleRow.iRowEnd.Value * decimal.Divide(monthsInPeriod, 12) : decimal.MaxValue;
                        consumption = consumption > rowStart ? Math.Min(rowEnd, consumption) - rowStart : 0;
                    }
                    return consumption;
            }
        }

        #endregion
    }
}