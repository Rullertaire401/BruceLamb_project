using BruceLamb_project.Data;
using BruceLamb_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace BruceLamb_project.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(_context.Members);
        }
        [HttpGet("{bookingid}")]
        public IActionResult GetByBookingId(int? bookingid)
        {
            var members = _context.Members.FirstOrDefault(e => e.BookingId == bookingid);
            if (members == null)
                return Problem(detail: "Member with bookingid " + bookingid + " is not found.", statusCode: 404);

            return Ok(_context.Members);
        }
        [HttpGet("{facilitydescription}")]
        public IActionResult GetByFacilityDescription(string facilitydescription = "All")
        {
            switch (facilitydescription.ToLower())
            {
                case "Freedo":
                    return Ok(_context.Members);
                case "Airblock":
                    return Ok(_context.Members.Where(e => e.FacilityDescription.ToLower() == "Airblock"));
                case "rtt":
                    return Ok(_context.Members.Where(e => e.FacilityDescription.ToLower() == "rtt"));
                case "errr":
                    return Ok(_context.Members.Where(e => e.FacilityDescription.ToLower() == "errr"));
                default:
                    return Problem(detail: "Member with facility description  " + facilitydescription + " is not found.", statusCode: 404);
            }
        }
        [HttpPost]
        public IActionResult Post(Member  member) 
        {
            _context.Members.Add(member);
            _context.SaveChanges();

            return CreatedAtAction("GetAll", new { bookingid = member.BookingId }, member);
        }
        [HttpPut]
        public IActionResult Put(int? bookingid, Member member)
        {
            var entity = _context.Members.FirstOrDefault(e => e.BookingId == bookingid);
            if (entity == null)
                return Problem(detail: "Employee with id" + bookingid + " is not found.", statusCode: 404);

            entity.FacilityDescription = member.FacilityDescription;
            entity.BookingStatus = member.BookingStatus;
            entity.BookingId = member.BookingId;

            _context.SaveChanges();

            return Ok(entity);
        }
        [HttpDelete]
        public IActionResult Delete(int? bookingid, Member member)
        {
            var entity = _context.Members.FirstOrDefault(e => e.BookingId == bookingid);
            if (entity == null)
                return Problem(detail: "Employee with bookingid" + bookingid + " is not found.", statusCode: 404);

            _context.Members.Remove(entity);
            _context.SaveChanges();

            return Ok(entity);
        }
    }
}
