
using Guidici.Business.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Guidici.Business.Kafka
{
    public class ProducerServices (IGuidiciObservable observable,
        IOptions<KafkaTopics> optionsTopic

        )
    {

        protected override IDisposable Subscribe(TaskCompletionSource tcs)
        {
            return observable.VotazioneObservable.Subscribe((change) => tcs.TrySetResult());
        }

        protected override IEnumerable<string> GetTopics()
        {
            return optionsTopic.Value.GetTopics();
        }


    }
}
