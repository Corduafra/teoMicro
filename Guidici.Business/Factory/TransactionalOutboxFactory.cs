using Guidici.Repository.Model;
using Guidici.Shared;


namespace Guidici.Business.Factory
{
    public class TransactionalOutboxFactory
    {
        #region Votazioni
        /* public static TransactionalOutbox CreateVotazione(VotazioneDto votazioneDto)
         {
             return new TransactionalOutbox
             {
                 Id = Guid.NewGuid().ToString(),
                 Type = "Votazione",
                 Payload = JsonSerializer.Serialize(votazioneDto),
                 CreatedAt = DateTime.UtcNow
             };
         }
        */
        private static TransactionalOutbox Create(VotazioneDto dto, string operation) => Create(nameof(VotazioneKafka), dto, operation);

        private static TransactionalOutbox Create<TODO>(string table, TDTO dto,string operation) where TDTO: class, new()
        {
            OperationMessage<TDTO> opMsg = new OperationMessage<TDTO>()
            {
                Operation = operation,
               Dto = dto
            };

            return new TransactionalOutbox()
            {
                
                Tabella = table,
                Messaggio = JsonSerializer.Serialize(opMsg),
              
            };
        }

        public static TransactionalOutbox CreateInsert(VotazioneDto dto) => Create(dto, Operations.Insert);
        #endregion
    }
}
