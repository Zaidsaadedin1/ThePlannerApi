using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.Task;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Model;
using ThePlannerAPI.Services;
using ThePlannerAPI.Shared;
using ThePlannerAPI.Validators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ThePlannerAPI.Controllers
{
    public class AssignmentsController : ControllerBase
    {
        private readonly IValidator<AddAssignmentDTO> _addAssignmentValidator;
        private readonly IValidator<UpdateAssignmentDTO> _updateAssignmentValidator;
        private readonly IValidator<AssignmentFilterDTO> _fillterAssignmentsValidator;
        private readonly IAssignment _assignmentService;
        public AssignmentValidationQuery _assignmentValidationQuery;
        public CategoryValidationQuery _categoryValidationQuery;

        public AssignmentsController(
            IAssignment assignmentService,
            IValidator<AddAssignmentDTO> validator,
            IValidator<UpdateAssignmentDTO> updateAssignmentValidator,
            IValidator<AssignmentFilterDTO> fillterAssignmentsValidator,
            AssignmentValidationQuery assignmentValidationQuery,
            CategoryValidationQuery categoryValidationQuery
            )
        {
            _addAssignmentValidator = validator;
            _assignmentService = assignmentService;
            _assignmentValidationQuery = assignmentValidationQuery;
            _categoryValidationQuery = categoryValidationQuery;
            _fillterAssignmentsValidator = fillterAssignmentsValidator;
            _updateAssignmentValidator = updateAssignmentValidator;
        }

        [HttpPost("/AddAssignment")]
        public async Task<ActionResult<int>> AddAssignment([FromBody] AddAssignmentDTO assignment)
        {
            var validationResult = await _addAssignmentValidator.ValidateAsync(assignment);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult);
            }

            var assignmentExist = await _assignmentValidationQuery.IsAssignmentExist(assignment.Name);
            if (assignmentExist)
            {
                return BadRequest(new
                {
                    Message = $"Assignment With This Name: ({assignment.Name}),Dose Already Exist"
                });
            }
            var categoryExist = await _categoryValidationQuery.IsCategoryExist(assignment.CategoryId);
            if (!categoryExist)
            {
                return BadRequest(new
                {
                    Message = $"Category With id: ({assignment.CategoryId}),Dose Not Exist"
                });
            }

            var newAssignment = await _assignmentService.AddAssignment(assignment);

            return Ok(new { Message = $"Assignment With Name: ({assignment.Name}), Created Successfully" });
        }

        [HttpDelete("/Assignment")]
        public async Task<ActionResult> DeleteAssignment([FromQuery] int id)
        {

            var isAssignmentExist = await _assignmentValidationQuery.IsAssignmentExist(id);
            if (!isAssignmentExist)
            {
                return BadRequest(new
                {
                    Message = $"There Is No Assignment With This: {id} "
                });
            }

            var deleteAssignment = await _assignmentService.DeleteAssignment(id);
            return Ok(new { Message = $"Assignment With This ID  {id} Deleted Successfully" });
        }

        [HttpGet("/Assignments")]
        public async Task<ActionResult> GetAllAssignments()
        {
            var assignments = await _assignmentService.GetAllAssignments();
            return Ok(new { Assignments = assignments });
        }

        [HttpGet("/Assignment")]
        public async Task<ActionResult> GetAssignmentById([FromQuery] int id)
        {
            var assignmentExist = await _assignmentValidationQuery.IsAssignmentExist(id);
            if (!assignmentExist)
            {
                return BadRequest(new
                {
                    Message = $"Assignment With This ID: ({id}),Dose Not Exist"
                });
            }

            var assignment = await _assignmentService.GetAssignmentById(id);
            return Ok(new { Assignment = assignment });
        }



        [HttpPut("/Assignment")]
        public async Task<ActionResult> UpdateAssignment([FromQuery] int categoryId, [FromBody] UpdateAssignmentDTO updateAssignment)
        {
            var validationResult = await _updateAssignmentValidator.ValidateAsync(updateAssignment);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult);
            }

            var assignmentExist = await _assignmentValidationQuery.IsAssignmentExist(categoryId);
            if (!assignmentExist)
            {
                return BadRequest(new
                {
                    Message = $"Assignment With This ID: ({categoryId}),Dose Not  Exist"
                });
            }

            var result = await _assignmentService.UpdateAssignment(updateAssignment, categoryId);
            return Ok(new
            {
                Message = $"Assignment With This ID: ({categoryId}),Updated Successfully"
            });
            }


        [HttpPost("/Assignment/filter")]
        public async Task<ActionResult> GetAssignmentsByFilter([FromBody] AssignmentFilterDTO assignmentFilterDTO)
        {
            var validationResult = await _fillterAssignmentsValidator.ValidateAsync(assignmentFilterDTO);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult);
            }

            var filteredAssignments = await _assignmentService.GetAssignmentsByFilter(assignmentFilterDTO);

            return Ok(new { Message = "Filtered Assignments Retrieved Successfully", Assignments = filteredAssignments });
        }
        [HttpPost("/Assignments/Search")]
        public async Task<IActionResult> Search([FromBody] string searchValue)
        {
            var result = await _assignmentService.GetAssignmentsBySearch(searchValue);

            return Ok(result);
        }
    }
}