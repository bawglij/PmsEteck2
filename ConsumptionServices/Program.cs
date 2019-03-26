using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumptionServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Service is gestart.");

            PmsEteckContext context = new PmsEteckContext();

            Console.WriteLine("Verbinding met de database is tot stand gebracht.");
            Console.WriteLine(string.Format("De volgende argumenten hebben we binnen gekregen: {0}.", string.Join(", ", args)));

            List<string> serviceNames = args.ToList();
            Console.WriteLine("De omzetting van de argumenten is gelukt.");

            Console.WriteLine("Alle services worden opgehaald vanuit de databases.");
            List<Service> services = context.Services.Where(w => serviceNames.Contains(w.sServiceName)).ToList();

            Console.WriteLine("Voor de volgende services ({0}) wordt er een nieuwe servicerun gestart", string.Join(", ", services.Select(s => s.sServiceName)));
            foreach (Service service in services)
            {
                Console.WriteLine("We zijn gestart met de servicerun voor service " + service.sServiceName);
                ServiceRun serviceRun = new ServiceRun();

                switch (service.iServiceKey)
                {
                    case 16:
                    case 19:
                        List<Counter> countersForUpdate = context.Counters
                        .Where(w => w.iConsumptionMeterKey.HasValue && w.ConsumptionMeter.iConsumptionMeterSupplierKey == 6 && w.ConsumptionMeter.iAddressKey.HasValue && w.CounterType.bCanExchange).ToList();
                        Console.WriteLine("In totaal zullen er " + countersForUpdate.Count + " telwerken bijgewerkt moeten worden.");
                        service.NewServiceRunAsync(countersForUpdate, context).Wait();
                        serviceRun = service.NewServiceRunAsync(countersForUpdate, context).Result;
                        break;
                    case 20:
                        countersForUpdate = context.Counters
                            .Where(w => w.iConsumptionMeterKey.HasValue && w.ConsumptionMeter.iConsumptionMeterSupplierKey == 4 && w.ConsumptionMeter.iAddressKey.HasValue && w.ConsumptionMeter.Address.ObjectID.HasValue)
                            .ToList();
                        Console.WriteLine("In totaal zullen er " + countersForUpdate.Count + " bijgewerkt moeten worden.");
                        service.NewServiceRunAsync(countersForUpdate, context).Wait();
                        serviceRun = service.NewServiceRunAsync(countersForUpdate, context).Result;
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Moment voor de volgende servicerun wordt ingesteld.");
                service.dtNextServiceRun = service.iServiceKey == 16 ? DateTime.Today.AddHours(38).ToUniversalTime() : DateTime.Today.AddHours(44).ToUniversalTime();
                context.Entry(service).State = EntityState.Modified;

                Console.WriteLine("De servicerun voor " + service.sServiceName + " is afgerond.");
                Console.WriteLine("De volgende run voor " + service.sServiceName + " is" + service.dtNextServiceRun + " .");
            }
            Console.WriteLine("Alle serviceruns zijn aangemaakt en verwerkt. De gegevens worden opgeslagen in de database.");

            try
            {
                context.SaveChanges();
                Console.WriteLine("Gegevens zijn goed opgeslagen");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Het ging niet goed met het opslaan van de gegevens. De volgende melding hebben we ontvangen: " + ex.Message);
            }
        }
    }
}
