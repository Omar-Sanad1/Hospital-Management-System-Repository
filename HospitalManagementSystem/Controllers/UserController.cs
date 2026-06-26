using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Filtering;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _repo;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<User> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPagedFiltered")]
        public IActionResult GetAllPagedFiltered([FromQuery] UserFiltering userFiltering, [FromQuery] PaginationParameters paginationParameters)
        {
            var users = _repo.GetAllFiltered(u =>
            // فلترة ب UserName
            (string.IsNullOrEmpty(userFiltering.UserName) || u.UserName == userFiltering.UserName) &&
            // فلترة ب EmailAddress
            (string.IsNullOrEmpty(userFiltering.EmailAddress) || u.EmailAddress == userFiltering.EmailAddress)
            );

            // Sorting
             users = userFiltering.SortBy?.ToLower() switch
            {
                "username" => userFiltering.isDescending
                ? users.OrderByDescending(u => u.UserName)
                : users.OrderBy(u => u.UserName),

                "createdat" => userFiltering.isDescending
                ? users.OrderByDescending(u => u.CreatedAt)
                : users.OrderBy(u => u.CreatedAt),

                _ => users.OrderBy(u => u.ID)
            };

            // Pagination
            var totalUsers = users.Count();
            var usersPaged = _repo.GetAllPaged(paginationParameters.PageNumber, paginationParameters.PageSize);
            var result = new PaginationResponse<UserToReturnDTO>
                (
                    data: _mapper.Map<IEnumerable<User>, IEnumerable<UserToReturnDTO>>(users),
                    totalitems: totalUsers,
                    pageNumber: paginationParameters.PageNumber,
                    pageSize: paginationParameters.PageSize
                );
            return Ok(result);
        }

        [HttpGet("GetByID{id}")]
        public IActionResult GetByID(int id)
        {
            var user = _repo.GetByID(id);
            var userMapping = _mapper.Map<User, UserToReturnDTO>(user);
            return Ok(userMapping);
        }

        [HttpPost("add")]
        public void Add(User user)
        {
            _repo.Add(user);
        }

        [HttpPut("update")]
        public void Update(User user)
        {
            _repo.Update(user);
        }

        [HttpDelete("delete")]
        public void Delete(User user)
        {
            _repo.Delete(user);
        }

    }
}
