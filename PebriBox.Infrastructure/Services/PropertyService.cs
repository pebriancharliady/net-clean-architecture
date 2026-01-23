using Microsoft.EntityFrameworkCore;
using PebriBox.Application.Features.Properties;
using PebriBox.Domain.Entities;
using PebriBox.Infrastructure.Contexts;

namespace PebriBox.Infrastructure.Services;

public class PropertyService(ApplicationDbContext context) : IPropertyService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> CreateAsync(Property newProperty)
    {
        newProperty.ListingDate = DateTime.UtcNow;
        await _context.Properties.AddAsync(newProperty);
        await _context.SaveChangesAsync();
        return newProperty.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return 0;
        }

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
        return property.Id;

    }

    public async Task<bool> DoesExistAsync(int id)
    {
        return await _context.Properties.AnyAsync(a => a.Id == id);
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<Property> GetByIdAsync(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
        {
            return null;
        }
        return property;
    }

    public async Task<Property> UpdateAsync(Property property)
    {
        if (await DoesExistAsync(property.Id) == false)
        {
            return null;
        }
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
        return property;
    }

    public async Task<List<Property>> GetByAgentIdAsync(int agentId)
    {
        return await _context.Properties.Where(property => property.AgentId == agentId).ToListAsync();
    }
}
