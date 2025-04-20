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
            var project = _mapper.Map<ProjectEntity>(dto);
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> UpdateAsync(string id, UpdateProjectDto dto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return null!;

            _mapper.Map(dto, project);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(dto);
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
