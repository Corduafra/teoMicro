using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Abstraction
{
    public interface IGuidiciObservable
    {
        IObservable<int> VotazioneObservable { get; }
    }
}
