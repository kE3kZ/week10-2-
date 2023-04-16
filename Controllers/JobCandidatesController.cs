using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aaronKuechle_Week10_2nd_attempt_.Data;
using aaronKuechle_Week10_2nd_attempt_.Models;

namespace aaronKuechle_Week10_2nd_attempt_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidatesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public JobCandidatesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/JobCandidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCandidate>>> GetJobCandidate()
        {
          if (_context.JobCandidate == null)
          {
              return NotFound();
          }
            return await _context.JobCandidate.ToListAsync();
        }

        // GET: api/JobCandidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobCandidate>> GetJobCandidate(int id)
        {
          if (_context.JobCandidate == null)
          {
              return NotFound();
          }
            var jobCandidate = await _context.JobCandidate.FindAsync(id);

            if (jobCandidate == null)
            {
                return NotFound();
            }

            return jobCandidate;
        }

        // PUT: api/JobCandidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCandidate(int id, JobCandidate jobCandidate)
        {
            if (id != jobCandidate.JobCandidateId)
            {
                return BadRequest();
            }

            _context.Entry(jobCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JobCandidates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobCandidate>> PostJobCandidate(JobCandidate jobCandidate)
        {
          if (_context.JobCandidate == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.JobCandidate'  is null.");
          }
            _context.JobCandidate.Add(jobCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobCandidate", new { id = jobCandidate.JobCandidateId }, jobCandidate);
        }

        // DELETE: api/JobCandidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCandidate(int id)
        {
            if (_context.JobCandidate == null)
            {
                return NotFound();
            }
            var jobCandidate = await _context.JobCandidate.FindAsync(id);
            if (jobCandidate == null)
            {
                return NotFound();
            }

            _context.JobCandidate.Remove(jobCandidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobCandidateExists(int id)
        {
            return (_context.JobCandidate?.Any(e => e.JobCandidateId == id)).GetValueOrDefault();
        }
    }
}
