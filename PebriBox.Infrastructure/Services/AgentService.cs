using Microsoft.EntityFrameworkCore;
using PebriBox.Application.Features.Agents;
using PebriBox.Domain.Entities;
using PebriBox.Infrastructure.Contexts;

namespace PebriBox.Infrastructure.Services;

public class AgentService(ApplicationDbContext context) : IAgentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> CreateAsync(Agent newAgent)
    {
        await _context.Agents.AddAsync(newAgent);
        await _context.SaveChangesAsync();
        return newAgent.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var agent = await _context.Agents.FindAsync(id);
        if (agent == null)
        {
            return 0;
        }

        _context.Agents.Remove(agent);
        await _context.SaveChangesAsync();
        return agent.Id;

    }

    public async Task<bool> DoesExistAsync(int id)
    {
        return await _context.Agents.AnyAsync(a => a.Id == id);
    }

    public async Task<List<Agent>> GetAllAsync()
    {
        return await _context.Agents.ToListAsync();
    }

    public async Task<Agent> GetByIdAsync(int id)
    {
        var agent = await _context.Agents.FindAsync(id);
        if (agent == null)
        {
            return null;
        }
        return agent;
    }

    public async Task<Agent> UpdateAsync(Agent agent)
    {
        if (await DoesExistAsync(agent.Id) == false)
        {
            return null;
        }
        _context.Agents.Update(agent);
        await _context.SaveChangesAsync();
        return agent;
    }
}
