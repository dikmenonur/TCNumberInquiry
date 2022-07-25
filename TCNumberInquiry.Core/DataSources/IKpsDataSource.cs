using System;
using System.Threading.Tasks;
using TCNumberInquiry.Core.Entities;

namespace TCNumberInquiry.Core.DataSources
{
    public interface IKpsDataSource
    {
        Task<IdentificationNumberResponse> GetIdentificationNumberCheckAsync(IdentificationNumberRequest numberRequest);
    }
}
