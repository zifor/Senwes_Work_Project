using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SenwesAssignment_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly LoadData _loadData;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _loadData = new LoadData();
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var employeeData = _loadData.LoadEmployeeData();
                return Ok(employeeData);
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("/Get/{empId}")]
        [HttpGet]
        public IActionResult GetByEmployeeId(int empId)
        {
            var employee = _loadData.LoadEmployeeData().Where(x => x.EmpID == empId).FirstOrDefault();
            return Ok(employee);
        }

        /// <summary>
        /// Get all employees over the age of 30
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("Get/{empAge}")]
        [HttpGet]
        public IActionResult GetEmployeeOverThirty()
        {
            try
            {
                var employee = _loadData.LoadEmployeeData().Where(x => x.Age >= 30);
                return Ok(employee);
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all employees that joined in the last Five years
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("Get/EmployeJoined")]
        [HttpGet]
        public IActionResult GetEmployeeJoined()
        {
            DateTime now = DateTime.Now;
            try
            {
                var employee = _loadData.LoadEmployeeData().Where(x => Convert.ToDateTime(x.DateOfJoining) >= now.AddYears(-5));
                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all employees that is the top 10 paid
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("Get/TopPaid")]
        [HttpGet]
        public IActionResult GetEmployeeTopPaid()
        {
            try
            {
                var employee = _loadData.LoadEmployeeData().OrderByDescending(x => x.Salary).Take(10);
                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all employees with specific name or surname and city
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("Get/Search/{empCity}")]
        [HttpGet]
        public IActionResult SearchEmplyee(string empName, string empSurname, string empCity)
        {
            try
            {
                var employee = _loadData.LoadEmployeeData().Where(x => x.FirstName == empName || x.LastName == empSurname && x.City == empCity);
                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all employees who's name is Treasure and display their salary
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("Get/TreasureSalary")]
        [HttpGet]
        public IActionResult GetTreasureSalary()
        {
            try
            {
                var employee = _loadData.LoadEmployeeData().Where(x => x.FirstName == "Treasure").Select(x => x.Salary);
                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all Cities
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [Route("Get/Cities")]
        [HttpGet]
        public IActionResult GetCities()
        {
            try
            {
                var employee = _loadData.LoadEmployeeData().Select(x => x.City);
                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
