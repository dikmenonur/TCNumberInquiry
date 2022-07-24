using System;
using System.Threading.Tasks;
using TCNumberInquiry.Core.Entities;

namespace TCNumberInquiry.Core.DataSources
{
    public interface IKpsRepository
    {
        Task<IdentificationNumberResponse> GetIdentificationNumberCheckAsync(IdentificationNumberRequest numberRequest);
    }
}
