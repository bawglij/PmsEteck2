using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PmsEteck.Data.Services.IBAN
{
    public class IBANValidator
    {
        readonly string apiKey = "130bf691910062607ecea769de0791ff";
        IBAN IBAN;
        public IBANValidator()
        {
            IBAN = new IBAN();
        }

        public IBAN Validate(string iBAN)
        {
            if (string.IsNullOrWhiteSpace(iBAN))
                throw new ArgumentNullException(nameof(iBAN));
            IBAN.IbanNumber = iBAN;
            Validate();

            return IBAN;
        }

        void Validate()
        {
            RestClient client = new RestClient("https://api.iban.com");
            RestRequest request = new RestRequest("clients/api/v4/iban/", Method.GET);
            request.AddQueryParameter("api_key", apiKey);
            request.AddQueryParameter("format", "json");
            request.AddQueryParameter("iban", IBAN.IbanNumber);
            IRestResponse response = client.Execute(request);
            IBAN = JsonConvert.DeserializeObject<IBAN>(response.Content);
        }
    }

    public class IBAN
    {
        public string IbanNumber { get; set; }
        public BankData bank_data { get; set; }
        public List<object> errors { get; set; }
        public Validations validations { get; set; }
        public SepaData sepa_data { get; set; }
        public bool Valid
        {
            get
            {
                return (
                    validations.iban.code.StartsWith("0") &&
                    validations.account.code.StartsWith("0") &&
                    validations.length.code.StartsWith("0") &&
                    validations.structure.code.StartsWith("0") &&
                    validations.chars.code.StartsWith("0") &&
                    validations.country_support.code.StartsWith("0")
                    );
            }
        }
    }

    public class BankData
    {
        public string bic { get; set; }
        public string branch { get; set; }
        public string bank { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string www { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string country_iso { get; set; }
        public string account { get; set; }
        public string bank_code { get; set; }
        public string branch_code { get; set; }
    }
    public class Length
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Structure
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Iban
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Account
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Chars
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class CountrySupport
    {
        public string code { get; set; }
        public string message { get; set; }
    }
    public class Validations
    {
        public Chars chars { get; set; }
        public Account account { get; set; }
        public Iban iban { get; set; }
        public Structure structure { get; set; }
        public Length length { get; set; }
        public CountrySupport country_support { get; set; }
    }

    public class SepaData
    {
        public string SCT { get; set; }
        public string SDD { get; set; }
        public string COR1 { get; set; }
        public string B2B { get; set; }
        public string SCC { get; set; }
    }
}