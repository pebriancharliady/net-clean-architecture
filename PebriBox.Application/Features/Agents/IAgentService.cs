using PebriBox.Domain.Entities;

namespace PebriBox.Application.Features.Agents;

public interface IAgentService
{
    Task<int> CreateAsync(Agent newAgent);
    Task<Agent> UpdateAsync(Agent agent);
    Task<int> DeleteAsync(int id);
    Task<Agent> GetByIdAsync(int id);
    Task<List<Agent>> GetAllAsync();
    Task<bool> DoesExistAsync(int id);
}
