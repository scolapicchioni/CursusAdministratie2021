using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using CursusAdministratie2021.Shared.Services;
using Moq;
using Xunit;

namespace CursusAdministratie2021.Shared.UnitTests.Services {
    public class CoursesOverviewServiceTests {
        [Fact]
        public async Task GetCoursesOverview_ShouldReturn_ListOfCourses() {
            Mock<ICoursesOverviewRepository> coursesOverviewRepositoryMock = new ();
            List<CourseOverview> expectedCourses = new List<CourseOverview>() {
                new CourseOverview(){StartDate = new DateTime(2021,3,15), Duration = 3, Title = "C#"},
                new CourseOverview(){StartDate = new DateTime(2021,3,10), Duration = 5, Title = "JPA"},
                new CourseOverview(){StartDate = new DateTime(2021,3,8), Duration = 3, Title = "Azure"}
            };
            coursesOverviewRepositoryMock.Setup(cor => cor.GetCoursesOverview()).ReturnsAsync(expectedCourses);
            CoursesOverviewService sut = new CoursesOverviewService(coursesOverviewRepositoryMock.Object);

            IEnumerable<CourseOverview> actualCourses = await sut.GetCoursesOverview();

            Assert.Equal(expectedCourses, actualCourses);
        }
        [Fact]
        public async Task GetCoursesPerWeek_ShouldReturn_ListOfCoursesReturnedByRepository() {
            int yearNumber = 2021;
            int weekNumber = 23;
            Mock<ICoursesOverviewRepository> coursesOverviewRepositoryMock = new();
            List<CourseOverview> expectedCourses = new List<CourseOverview>() {
                new CourseOverview(){StartDate = new DateTime(2021,3,15), Duration = 3, Title = "C#"},
                new CourseOverview(){StartDate = new DateTime(2021,3,10), Duration = 5, Title = "JPA"},
                new CourseOverview(){StartDate = new DateTime(2021,3,8), Duration = 3, Title = "Azure"}
            };
            coursesOverviewRepositoryMock.Setup(cor => cor.GetCoursesPerWeek(yearNumber,weekNumber)).ReturnsAsync(expectedCourses);
            CoursesOverviewService sut = new CoursesOverviewService(coursesOverviewRepositoryMock.Object);

            IEnumerable<CourseOverview> actualCourses = await sut.GetCoursesPerWeek(yearNumber,weekNumber);

            Assert.Equal(expectedCourses, actualCourses);
        }
    }
}
