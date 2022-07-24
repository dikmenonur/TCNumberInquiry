using System;
using System.Threading.Tasks;
using TCNumberInquiry.Core.Entitiy;

namespace TCNumberInquiry.Core.DataSources
{
    public interface IKpsRepository
    {
        Task<IdentificationNumberResponse> GetIdentificationNumberCheckAsync(IdentificationNumberRequest numberRequest);
    }
}
