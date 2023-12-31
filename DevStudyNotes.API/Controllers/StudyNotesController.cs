﻿using DevStudyNotes.API.Entities;
using DevStudyNotes.API.Models;
using DevStudyNotes.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevStudyNotes.API.Controllers
{
    [ApiController]
    [Route("api/study-notes")]

    public class StudyNotesController: ControllerBase
    {
        private readonly StudyNoteDbContext _context;
        public StudyNotesController(StudyNoteDbContext context)
        {
            _context = context;
        }
        // api/study-notes HTTP GET
        [HttpGet]
        public IActionResult GetAll()
        {
            var studyNotes = _context.StudyNotes.ToList();

            return Ok(studyNotes);
        }

        // api/study-notes/1 HTTP GET
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var studyNote = _context.StudyNotes
                .Include(s=>s.Reactions)
                .SingleOrDefault(s => s.Id == id);

            if(studyNote == null)
            {
                return NotFound();
            }

            return Ok(studyNote);
        }

        // api/study-notes HTTP POST
        /// <summary>
        /// Cadastrar uma nota de Estudo
        /// </summary>
        /// <param name="model">Dados de uma nota de estudo</param>
        /// <returns>recém-criado</returns>
        /// <response code = "201">Sucesso</response>
        [HttpPost]
        public IActionResult Post(AddStudyNoteInputModel model)
        {
            var studyNote = new StudyNote(model.Title, model.Description, model.IsPublic);

            _context.StudyNotes.Add(studyNote);
            _context.SaveChanges();

            return CreatedAtAction("GetById", new { id = 1 }, model);
        }

        // api/study-notes/1/reactions HTTP POST
        [HttpPost("{id}/reactions")]
        public IActionResult PostReaction(int id, AddReactionsStudyNoteInputModel model)
        {
            var studyNote = _context.StudyNotes.SingleOrDefault(s => s.Id == id);

            if(studyNote == null)
            {
                return BadRequest();
            }

            studyNote.AddReaction(model.IsPositive);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
