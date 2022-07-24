using System.Threading.Tasks;
using KPSPublicServices;
using TCNumberInquiry.Core.Entities;
using KpsRepository = KPSPublicServices.KPSPublicSoap;

namespace TCNumberInquiry.Core.DataSources
{
    public class KpsRepository : IKpsRepository
    {
        private readonly KPSPublicSoap _kpsPublicSoap;
        public KpsRepository()
        {
            this._kpsPublicSoap = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
        }

        public async Task<IdentificationNumberResponse> GetIdentificationNumberCheckAsync(IdentificationNumberRequest numberRequest)
        {
            TCKimlikNoDogrulaRequest inValue = new TCKimlikNoDogrulaRequest();
            inValue.Body = new TCKimlikNoDogrulaRequestBody();
            inValue.Body.TCKimlikNo = numberRequest.IdentificationNumber;
            inValue.Body.Ad = numberRequest.FistName;
            inValue.Body.Soyad = numberRequest.LastNane;
            inValue.Body.DogumYili = numberRequest.BirthDate;

            IdentificationNumberResponse identificationNumberResponse = (IdentificationNumberResponse)await this._kpsPublicSoap.TCKimlikNoDogrulaAsync(inValue);
            return identificationNumberResponse;
        }
    }
}
