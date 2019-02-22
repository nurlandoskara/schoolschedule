using SchoolSchedule.Models;
using SchoolSchedule.Models.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace SchoolSchedule.Controllers
{
    public class BaseApiController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/News")]
        public IQueryable<News> GetNews()
        {
            return _db.Newses.Where(x => !x.IsDeleted).OrderByDescending(x => x.Id);
        }

        [HttpGet]
        [Route("api/Groups")]
        public IQueryable<Group> GetGroups()
        {
            return _db.Groups.Where(x => !x.IsDeleted);
        }

        [HttpGet]
        [Route("api/Teachers")]
        public IQueryable<Teacher> GetTeachers()
        {
            return _db.Teachers.Where(x => !x.IsDeleted);
        }

        [HttpGet]
        [Route("api/Schedule")]
        public List<LessonDto> GetSchedule(int groupId)
        {
            var schedule = GetLessonsOfGroup(groupId);
            return schedule;
        }

        [HttpGet]
        [Route("api/TSchedule")]
        public List<LessonDto> GetTSchedule(int teacherId)
        {
            var schedule = GetTLessonsOfTeacher(teacherId);
            return schedule;
        }

        private List<LessonDto> GetLessonsOfGroup(int groupId)
        {
            return _db.Lessons.Where(x => !x.IsDeleted && x.SubjectGroup.GroupId == groupId)
                .Include(x => x.SubjectGroup)
                .Include(x => x.SubjectGroup.Subject)
                .Include(x => x.SubjectTeacher)
                .Include(x => x.SubjectTeacher.Teacher)
                .Include(x => x.Auditory)
                .OrderBy(x => x.Order).ToList().Select(x => new LessonDto
                {
                    Order = x.Order,
                    SubjectName = x.SubjectGroup.Subject.NameKz,
                    GroupOrTeacherName = x.SubjectTeacher.Teacher.DisplayName,
                    WeekDay = x.WeekDay,
                    AuditoryName = x.Auditory.DisplayName
                }).ToList();
        }

        private List<LessonDto> GetTLessonsOfTeacher(int teacherId)
        {
            return _db.Lessons.Where(x => !x.IsDeleted && x.SubjectTeacher.TeacherId == teacherId)
                .Include(x => x.SubjectGroup)
                .Include(x => x.SubjectGroup.Subject)
                .Include(x => x.SubjectGroup.Group)
                .Include(x => x.Auditory)
                .OrderBy(x => x.Order).ToList().Select(x => new LessonDto
                {
                    Order = x.Order,
                    SubjectName = x.SubjectGroup.Subject.NameKz,
                    GroupOrTeacherName = x.SubjectGroup.Group.DisplayName,
                    WeekDay = x.WeekDay,
                    AuditoryName = x.Auditory.DisplayName
                }).ToList();
        }
    }
}
