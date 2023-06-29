﻿using AutoMapper;
using LearningManagementSystemAPI.Context;
using LearningManagementSystemAPI.DTOs;
using LearningManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystemAPI.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly LmsContext _context;
        private readonly IMapper _mapper;
        public ResourcesService(LmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Resources> CreateResourcesAsync(CreateResourcesDTO resourcesDTO, IFormFile file)
        {
            var resources = _mapper.Map<Resources>(resourcesDTO);
            if (file != null && file.Length > 0)
            {
                var fileName = file.FileName;
                var filePath = Path.Combine("Uploads\\Resources", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                resources.SentDate = DateTime.Now;
                resources.FileName = fileName;
                resources.Status = 0;
            }
            await _context.Resources.AddAsync(resources);
            await _context.SaveChangesAsync();

            return resources;
        }

        public async Task<bool> DeleteResourcesAsync(int id)
        {
            var resources = await _context.Resources.FirstOrDefaultAsync(p => p.ResourcesId == id);

            if (resources != null)
            {
                var filePath = Path.Combine("Uploads\\Resources", resources.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _context.Resources.Remove(resources);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Resources> UpdateResourcesAsync(CreateResourcesDTO resourcesDTO, int id, IFormFile file)
        {
            var resources = await _context.Resources.FindAsync(id);
            if (resources == null)
            {
                return null;
            }
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("Uploads\\Resources", resources.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                var fileName = file.FileName;
                filePath = Path.Combine("Uploads\\Resources", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                resources.SentDate = DateTime.Now;
                resources.FileName = fileName;
                resources.Status = 0;
            }
            _mapper.Map(resourcesDTO, resources);
            _context.Resources.Update(resources);
            await _context.SaveChangesAsync();
            return resources;
        }
    }
}