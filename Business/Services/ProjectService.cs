using AutoMapper;
using Business.Dtos;
using Business.Services.Interfaces;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    //Got some help from ChatGPT4o whit the mapper parts here. to simplify mapping between the DTOs and Entities.
    //Automapper is simply used to reduce the manual mapping code. Thiis makes it alot cleaner. 
    //I did not use this in the database course project, and now i see the big differences on using or not using mapper.
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.Projects.Include(p => p.Client).ToListAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }


        public async Task<ProjectDto> GetProjectByIdAsync(string id)
        {
            var project = await _context.Projects.Include(p => p.Client).FirstOrDefaultAsync(p => p.Id == id);
            return project != null ? _mapper.Map<ProjectDto>(project) : null!;
        }

        
        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientName.ToLower().Trim() == dto.ClientName.ToLower().Trim());

            if(existingClient == null)
            {
                existingClient = new ClientEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientName = dto.ClientName
                };
                _context.Clients.Add(existingClient);
                await _context.SaveChangesAsync();
            }

            var project = _mapper.Map<ProjectEntity>(dto);
            project.ClientId = existingClient.Id;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> UpdateAsync(string id, UpdateProjectDto dto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return null!;
            }
            _mapper.Map(dto, project);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }


        public async Task<bool> DeleteAsync(string id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return false;
            
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
