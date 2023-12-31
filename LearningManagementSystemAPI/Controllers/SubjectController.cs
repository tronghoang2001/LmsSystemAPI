﻿using LearningManagementSystemAPI.DTOs;
using LearningManagementSystemAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize]
        [HttpGet("list-subject")]
        public async Task<IActionResult> GetAllSubject()
        {
            try
            {
                return Ok(await _subjectService.GetAllSubjectAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("subject-by-id/{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            return subject == null ? NotFound() : Ok(subject);
        }

        [Authorize]
        [HttpGet("subject-by-accountId/{id}")]
        public async Task<IActionResult> GetSubjectByAccountId(int id)
        {
            var subject = await _subjectService.GetSubjectByAccountIdAsync(id);
            return subject == null ? NotFound() : Ok(subject);
        }

        [Authorize]
        [HttpGet("{subjectId}/get-documents-by-id")]
        public async Task<IActionResult> GetDocumentsBySubjectId(int subjectId)
        {
            var documents = await _subjectService.GetDocumentsBySubjectIdAsync(subjectId);

            if (documents == null)
            {
                // Xử lý khi không tìm thấy môn học
                return NotFound();
            }

            return Ok(documents);
        }

        [Authorize]
        [HttpGet("get-all-documents")]
        public async Task<ActionResult<List<DocumentDTO>>> GetAllDocuments(int? subjectId, int? lecturers, int? status)
        {
            var documents = await _subjectService.GetAllDocumentsAsync(subjectId, lecturers, status);
            return Ok(documents);
        }

        [Authorize(Roles = "Admin,Lecturers")]
        [HttpPost("create-subject")]
        public async Task<IActionResult> CreateSubject([FromForm] CreateSubjectDTO subjectDTO, IFormFile file)
        {
            try
            {
                var subject = await _subjectService.CreateSubjectAsync(subjectDTO, file);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Lecturers")]
        [HttpPut("update-subject/{id}")]
        public async Task<IActionResult> UpdateSubject([FromForm] CreateSubjectDTO subjectDTO, int id, IFormFile file)
        {
            try
            {
                var subject = await _subjectService.UpdateSubjectAsync(subjectDTO, id, file);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Lecturers")]
        [HttpDelete("delete-subject/{id}")]
        public async Task<ActionResult> DeleteSubject(int id)
        {
            var result = await _subjectService.DeleteSubjectAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok("Delete Success!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("approve-subject/{id}")]
        public async Task<IActionResult> ApproveSubject(ApproveDTO subjectDTO, int id)
        {
            try
            {
                var subject = await _subjectService.ApproveSubjectAsync(subjectDTO, id);
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Lecturers")]
        [HttpPost("create-subjectAssignment")]
        public async Task<IActionResult> CreateSubjectAssignment(CreateSubjectAssignmentDTO subjectAssignmentDTO)
        {
            var assignment = await _subjectService.CreateSubjectAssignmentAsync(subjectAssignmentDTO);

            return Ok(assignment);
        }

        [Authorize]
        [HttpGet("download-subjectFile/{subjectId}")]
        public async Task<IActionResult> DownloadSubjectFile(int subjectId)
        {
            try
            {
                var fileStreamResult = await _subjectService.DownloadSubjectFileAsync(subjectId);
                return fileStreamResult;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
