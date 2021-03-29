using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursusAdministratie2021.Shared.DTO;
using CursusAdministratie2021.Shared.Interfaces;
using CursusAdministratie2021.Shared.Services;
using CursusAdministratie2021.UnitTests.Builders.DTO;
using Moq;
using Xunit;

namespace CursusAdministratie2021.Shared.UnitTests.Services {
    public class CoursesOverviewServiceTests {
        private Mock<ICoursesOverviewRepository> coursesOverviewRepositoryMock;
        private CoursesOverviewService sut;

        public CoursesOverviewServiceTests() {
            coursesOverviewRepositoryMock = new();
            sut = new (coursesOverviewRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCoursesOverview_ShouldReturn_ListOfCourses() {
            List<CourseOverview> expectedCourseOverviews = typicalCourseOverviews();
            coursesOverviewRepositoryMock.Setup(cor => cor.GetCoursesOverview()).ReturnsAsync(expectedCourseOverviews);
            
            IEnumerable<CourseOverview> actualCourseOverviews = await sut.GetCoursesOverview();

            Assert.Equal(expectedCourseOverviews, actualCourseOverviews);
        }
        
        [Fact]
        public async Task GetCoursesPerWeek_ShouldReturn_ListOfCoursesReturnedByRepository() {
            int yearNumber = 2021;
            int weekNumber = 23;

            List<CourseOverview> expectedCourseOverviews = typicalCourseOverviews();
            coursesOverviewRepositoryMock.Setup(cor => cor.GetCoursesPerWeek(yearNumber,weekNumber)).ReturnsAsync(expectedCourseOverviews);
            
            IEnumerable<CourseOverview> actualCourseOverviews = await sut.GetCoursesPerWeek(yearNumber,weekNumber);

            Assert.Equal(expectedCourseOverviews, actualCourseOverviews);
        }

        [Fact]
        public async Task GetCourseOverviewByEditionId_ShouldReturn_Course() {
            int editionId = 2;
            CourseOverview expectedCourseOverview = CourseOverviewBuilder.Default().WithEditionId(editionId).Build();

            coursesOverviewRepositoryMock.Setup(cor => cor.GetCourseOverviewByEditionId(editionId)).ReturnsAsync(expectedCourseOverview);

            CourseOverview actualCourseOverview = await sut.GetCourseOverviewByEditionId(editionId);

            Assert.Equal(expectedCourseOverview, actualCourseOverview);
        }

        private static List<CourseOverview> typicalCourseOverviews() {
            return new List<CourseOverview>() {
                CourseOverviewBuilder.Default().Typical().Build(),
                CourseOverviewBuilder.Default().Typical().Build(),
                CourseOverviewBuilder.Default().Typical().Build()
            };
        }

    }
}
