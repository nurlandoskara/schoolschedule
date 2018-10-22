using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolSchedule.Models;
using SchoolSchedule.Models.DTO;

namespace SchoolSchedule.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelContainer db = new ModelContainer();

        [HttpGet]
        [Route("api/Groups")]
        public IQueryable<Group> GetGroups()
        {
            return db.Groups.Where(x => !x.IsDeleted);
        }

        [HttpGet]
        [Route("api/Schedule")]
        public List<DayDto> GetSchedule(int groupId)
        {
            var schedule = GetLessons(groupId);
            return schedule;
        }

        private List<DayDto> GetLessons(int groupId)
        {
            return db.Lessons.Where(x => !x.IsDeleted && x.SubjectGroup.GroupId == groupId)
                .Include(x => x.SubjectGroup)
                .Include(x => x.SubjectGroup.Subject)
                .Include(x => x.SubjectTeacher)
                .Include(x => x.SubjectTeacher.Teacher)
                .OrderBy(x => x.Order).ToList().GroupBy(x => x.DayOfWeek).Select(day => new DayDto
                {
                    DayOfWeek = day.Key,
                    Lessons = day.Select(lesson => new LessonDto
                    {
                        Order = lesson.Order,
                        SubjectName = lesson.SubjectGroup.Subject.NameKz,
                        TeacherName = lesson.SubjectTeacher.Teacher.DisplayName
                    }).ToList()
                }).ToList();
        }
    }
}
