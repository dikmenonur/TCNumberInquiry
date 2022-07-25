using System;
using System.Threading.Tasks;
using KPSPublicServices;
using TCNumberInquiry.Core.Entities;
using KpsRepository = KPSPublicServices.KPSPublicSoap;

namespace TCNumberInquiry.Core.DataSources
{
    public class KpsDataSource : IKpsDataSource
    {
        private readonly KPSPublicSoap _kpsPublicSoap;
        public KpsDataSource()
        {
            this._kpsPublicSoap = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
        }

        public async Task<IdentificationNumberResponse> GetIdentificationNumberCheckAsync(IdentificationNumberRequest numberRequest)
        {
            try
            {
                TCKimlikNoDogrulaRequest inValue = new TCKimlikNoDogrulaRequest();
                inValue.Body = new TCKimlikNoDogrulaRequestBody();
                inValue.Body.TCKimlikNo = numberRequest.IdentificationNumber;
                inValue.Body.Ad = numberRequest.FirstName;
                inValue.Body.Soyad = numberRequest.LastName;
                inValue.Body.DogumYili = numberRequest.BirthDate;

                TCKimlikNoDogrulaResponse numberResponse = await this._kpsPublicSoap.TCKimlikNoDogrulaAsync(inValue);
                IdentificationNumberResponse identificationNumberResponse = new IdentificationNumberResponse();
                identificationNumberResponse.Body = numberResponse.Body;
                return identificationNumberResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
