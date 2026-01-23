using PebriBox.Domain.Entities;

namespace PebriBox.Application.Features.Properties;

public interface IPropertyService
{
    Task<int> CreateAsync(Property newAgent);
    Task<Property> UpdateAsync(Property agent);
    Task<int> DeleteAsync(int id);
    Task<Property> GetByIdAsync(int id);
    Task<List<Property>> GetAllAsync();
    Task<bool> DoesExistAsync(int id);
}
