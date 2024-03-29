﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketLeagueStatsAPI.Models;

namespace RocketLeagueStatsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class teamsController : ControllerBase
    {
        // db
        private RocketLeagueStatsModel db;

        public teamsController(RocketLeagueStatsModel db)
        {
            this.db = db;
        }

        // GET: api/teams
        [HttpGet]
        public IEnumerable<team> Get()
        {
            return db.teams;
        }

        // GET: api/teams/4
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            team team = db.teams.SingleOrDefault(a => a.teamid == id);

            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        // POST: api/teams
        [HttpPost]
        public ActionResult Post([FromBody] team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.teams.Add(team);
            db.SaveChanges();
            return CreatedAtAction("Post", new { id = team.teamid });
        }

        // PUT: api/teams/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] team team)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(team).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return AcceptedAtAction("Put", team);
        }

        // DELETE: api/teams/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            team team = db.teams.SingleOrDefault(a => a.teamid == id);

            if (team == null)
            {
                return NotFound();
            }

            db.teams.Remove(team);
            db.SaveChanges();
            return Ok();
        }
    }
}