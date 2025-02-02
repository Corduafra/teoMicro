using Guidici.Business.Abstraction;
using Guidici.Repository.Abstraction;
using Guidici.Business.Abstraction;
using Guidici.Shared;
using Microsoft.Extensions.Logging;
using Automapper;

namespace Guidici.Business;

    public class Business(IRepository repository, ILogger<Business> logger) : IBusiness

    {
        public async Task CreateGuidiceAsync(PersonaDto personaDto, CancellationToken c = default)
        {
            await repository.CreateGuidiceAsync(personaDto.Nome, personaDto.Cognome, personaDto.CodiceFiscale, c);

            await repository.SaveChangesAsync(c);
        }

        public async Task CreateVotazione(VotazioneDto votazioneDto, CancellationToken c = default)
        {
        await repository.BeginTransactionAsync(async (CancellationToken c) =>
        {
            var votazione = await repository.InsertVotazioneKafka(votazioneDto, c);
            await repository.SaveChangesAsync(c);
            var newVotazione = map.Map<VotazioneDto>(votazione);
        }, c);

            

            await repository.SaveChangesAsync(c);
        }

        public async Task<PersonaDto?> GetGuidice(int id, CancellationToken c = default)
        {
            var persona = await repository.GetGuidice(id, c);

            if (persona is null)
                return null;

            return new PersonaDto
            {
               
                Nome = persona.Nome,
                Cognome = persona.Cognome,
                CodiceFiscale = persona.CodiceFiscale
            };
        }

       
    }
