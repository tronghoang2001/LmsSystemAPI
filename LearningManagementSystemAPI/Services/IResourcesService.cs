﻿using LearningManagementSystemAPI.DTOs;
using LearningManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemAPI.Services
{
    public interface IResourcesService
    {
        Task<List<ResourcesDTO>> GetAllResourcesAsync();
        public Task<Resources> CreateResourcesAsync(CreateResourcesDTO resourcesDTO, IFormFile file);
        public Task<Resources> UpdateResourcesAsync(CreateResourcesDTO resourcesDTO, int id, IFormFile file);
        public Task<bool> DeleteResourcesAsync(int id);
        public Task<Resources> ApproveResourcesAsync(ApproveDTO approveDTO, int id);
        public Task<FileStreamResult> DownloadResourcesFileAsync(int id);
    }
}
